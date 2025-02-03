using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GrapplingHook : MonoBehaviour
{
    [Header("References")]
    public LineRenderer lr;
    private GameObject grapplePoint;
    public Transform gunTip;
    public Camera cam;
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

    private bool grappling = false;

    PhotonView PV;

    void Awake()
    {
        
    }


    void Update()
    {
        if (transform.Find("Camera(Clone)")){
            cam = transform.Find("Camera(Clone)").GetComponent<Camera>();
        }
        if (transform.Find("Camera(Clone)/Grapple Gun")){
        lr = transform.Find("Camera(Clone)/Grapple Gun").GetComponent<LineRenderer>();
        }
        if (transform.Find("Camera(Clone)/Grapple Gun/Grapple Tip")){
        gunTip = transform.Find("Camera(Clone)/Grapple Gun/Grapple Tip");
        }
        //canGrapple();
        if (Input.GetKeyDown(grappleKey))
        {
            //gameObject.GetComponent<Rigidbody>().velocity *= 0.75f;
            StartGrapple();

            grappling = true;
        }

        else if (Input.GetKeyUp(grappleKey))
        {
            StopGrapple();
            grappling = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse2) && grappling){
            //gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,gameObject.GetComponent<Rigidbody>().velocity.y,0);
            /*
            joint.spring = 4.5f;
            joint.damper = 10f;
            */
        }
        if (grappling){
            joint.connectedAnchor = grapplePoint.transform.position;
            PV.RPC("RPC_Beam", RpcTarget.All, this.transform.position, new Vector3(this.transform.position.x, this.transform.position.y + 5));
            float distanceFromPoint = Vector3.Distance(gameObject.transform.position, grapplePoint.transform.position);

            //joint.maxDistance = distanceFromPoint * 0.08f;
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
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, maxDistance))
        {
            Transform parentCheck = hit.transform;
            bool touchingYourself = true;
            if (parentCheck){
                //Debug.Log(doug.name + "juah" + doug.parent);
                while(parentCheck.parent){
                    parentCheck = parentCheck.parent;
                    Debug.Log(parentCheck.name + "juah" + parentCheck.parent);
                }
                if (parentCheck == transform){
                    Debug.Log("augh>?");
                    touchingYourself = false;
                }
            }
            if (touchingYourself){
                gameObject.GetComponent<Rigidbody>().velocity *= 0.75f;
                //GameObject ooog = Instantiate(Resources.Load("crud").GetComponent<Transform>().gameObject,hit.point,hit.transform.rotation,hit.transform);
                grapplePoint = new GameObject("hit");
                grapplePoint.transform.parent = hit.transform;
                //ooog.transform.localPosition = Vector3.zero;
                grapplePoint.transform.position = hit.point;

                //ooog.transform.position = hit.point;
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

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, grapplePoint.transform.position);
    }

    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
        Destroy(grapplePoint);
    }

    public void canGrapple(){
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)){
            Debug.Log(hit.collider.name);
            if (hit.distance >= maxDistance){
                crosshair.color = Color.red;
            }
            else{
                crosshair.color = Color.black;
            }
        }
    }

    [PunRPC]
    public void RPC_Beam(Vector3 start, Vector3 end)
    {
        lr.enabled = true;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        Debug.Log("Beam!! " + start + " | " + end);
    }
}