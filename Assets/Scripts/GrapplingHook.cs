using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System;
using System.Data;
namespace Assets.Scripts.CharacterControl
{

    public class GrapplingHook : MonoBehaviour
    {
        [Header("References")]
        public LineRenderer lr;
        private GameObject grapplePoint;
        public Transform gunTip;
        private Camera cam;
        private SpringJoint joint;

        [Header("Grappling")]
        public float maxDistance = 300f;

        [Header("Input")]
        public KeyCode grappleKey = KeyCode.Mouse1;

        [Header("Grapple Physics")]
        public float spring = 1.25f;
        public float damper = 5.5f;
        public float massScale = 4.5f;

        [Header("Grapple Check")]
        public Text crosshair;
        private Ray ray;

        public bool grappling = false;
        PhotonView PV;
        public GameObject playerObject;
        public PlayerInputManager playerInputManager;

        [Header("OdmGear")]
        public Transform orientation;
        public Rigidbody rb;
        public float horizontalThrustForce = 2000;

        void Awake()
        {
            PV = gameObject.GetComponent<PhotonView>();
        }

        void Start()
        {
            if (transform.Find("Camera(Clone)"))
            {
                cam = transform.Find("Camera(Clone)").GetComponent<Camera>();
            }
        }

        void Update()
        {
            playerObject = GameObject.Find("Player 1");
            if (playerObject){
                playerInputManager = playerObject.GetComponent<PlayerInputManager>();
                if(playerInputManager.swung)
                {
                    grappling = false;
                    StopGrapple();
                }
            }
            //canGrapple();
            if (Input.GetKeyDown(grappleKey))
            {
                //gameObject.GetComponent<Rigidbody>().velocity *= 0.75f;
                StartGrapple();

                grappling = true;
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
            }

            else if (Input.GetKeyUp(grappleKey))
            {
                grappling = false;
                StopGrapple();
                PV.RPC("RPC_BeamOff", RpcTarget.All);
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                rb.angularVelocity = Vector3.zero;
            }

            if (grappling)
            {
                if (gunTip && grapplePoint){
                    PV.RPC("RPC_BeamOn", RpcTarget.All, gunTip.position, grapplePoint.transform.position);
                    float distanceFromPoint = Vector3.Distance(gameObject.transform.position, grapplePoint.transform.position);
                }
                if (joint)
                {
                    joint.connectedAnchor = grapplePoint.transform.position;
                }
            }

            if (!transform.Find("GroundChecker").GetComponent<GroundCheckerScript>().Grounded && gameObject.name.Equals("Player 1")){
                gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            }

            if (joint != null){
                OdmGearMovement();
            }
        }


        //Called after Update

        void LateUpdate()
        {
            DrawRope();
        }

        void StartGrapple()
        {
            RaycastHit hit;
            //maybe do spherecast
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, maxDistance))
            {
                Transform parentCheck = hit.transform;
                bool touchingYourself = true;
                if (parentCheck)
                {
                    while (parentCheck.parent)
                    {
                        parentCheck = parentCheck.parent;
                    }
                    if (parentCheck == transform)
                    {
                        touchingYourself = false;
                    }
                }
                if (touchingYourself)
                {
                    gameObject.GetComponent<Rigidbody>().velocity *= 0.75f;
                    grapplePoint = new GameObject("hit");
                    grapplePoint.transform.parent = hit.transform;
                    grapplePoint.transform.position = hit.point;

                    joint = gameObject.AddComponent<SpringJoint>();
                    joint.autoConfigureConnectedAnchor = false;
                    joint.connectedAnchor = grapplePoint.transform.position;

                    float distanceFromPoint = Vector3.Distance(gameObject.transform.position, grapplePoint.transform.position);

                    //The distance grapple will try to keep from grapple point.
                    joint.maxDistance = distanceFromPoint * 0.08f;
                    //joint.minDistance = distanceFromPoint * 0.25f;

                    //Adjust these values to fit your game.
                    joint.spring = spring;
                    joint.damper = damper;
                    joint.massScale = massScale;

                    lr.positionCount = 2;
                }
            }
        }

        void DrawRope()
        {
            if (!joint) return;

            //lr.SetPosition(0, gunTip.position);
            //lr.SetPosition(1, grapplePoint.transform.position);
        }

        void StopGrapple()
        {
            if (!grappling)
            {
                //lr.positionCount = 0;
                lr.enabled = false;
                Destroy(joint);
                //grapplePoint = null;
                Destroy(grapplePoint);
            }

            //Destroy(grapplePoint);
        }

        public void canGrapple()
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.name);
                if (hit.distance >= maxDistance)
                {
                    crosshair.color = Color.red;
                }
                else
                {
                    crosshair.color = Color.black;
                }
            }
        }

        private void OdmGearMovement()
        {
            // right
            if (Input.GetKey(KeyCode.D)){
                rb.AddForce(orientation.right * horizontalThrustForce * Time.deltaTime);
                transform.RotateAround(transform.position,transform.forward,0.03f);
            }
            // left
            if (Input.GetKey(KeyCode.A)){
                rb.AddForce(-orientation.right * horizontalThrustForce * Time.deltaTime);
                transform.RotateAround(transform.position,transform.forward,-0.03f);
            }

            //forwards
            if (Input.GetKey(KeyCode.W)){ 
                rb.AddForce(-orientation.forward * horizontalThrustForce * Time.deltaTime);
                transform.RotateAround(transform.position,transform.right,0.03f);
            }

            //backwards
            if (Input.GetKey(KeyCode.S)){ 
                transform.RotateAround(transform.position,transform.right,-0.03f);
            }
        }

        [PunRPC]
        void RPC_BeamOn(Vector3 start, Vector3 end)
        {
            lr.enabled = true;
            lr.SetPosition(0, start);
            lr.SetPosition(1, end);
            //Debug.Log("Beam!! " + start + " | " + end);
        }

        [PunRPC]
        void RPC_BeamOff()
        {
            lr.enabled = false;
        }
    }
}