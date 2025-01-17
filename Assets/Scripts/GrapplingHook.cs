using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrapplingHook : MonoBehaviour
{
    [Header("References")]
    public LineRenderer lr;
    public Vector3 grapplePoint;
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

    void Awake()
    {
        
    }


    void Update()
    {
        cam = transform.Find("Camera(Clone)").GetComponent<Camera>();
        lr = transform.Find("Camera(Clone)/Grapple Gun").GetComponent<LineRenderer>();
        gunTip = transform.Find("Camera(Clone)/Grapple Gun/Grapple Tip");
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
            gameObject.GetComponent<Rigidbody>().velocity *= 0.75f;
            grapplePoint = hit.point;
            joint = gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(gameObject.transform.position, grapplePoint);

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

    void DrawRope()
    {
        if (!joint) return;

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, grapplePoint);
    }

    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
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
}