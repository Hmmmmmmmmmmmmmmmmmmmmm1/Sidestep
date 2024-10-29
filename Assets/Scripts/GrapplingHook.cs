using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [Header("References")]
    public LineRenderer lr;
    private Vector3 grapplePoint;
    public Transform gunTip;
    public Camera cam;
    private SpringJoint joint;

    [Header("Grappling")]
    public float maxDistance = 1000f;

    [Header("Input")]
    public KeyCode grappleKey = KeyCode.Mouse1;

    [Header("Grapple Physics")]
    public float spring = 2.5f;
    public float damper = 5.5f;
    public float massScale = 4.5f;

    private bool grappling = false;

    void Awake()
    {

    }


    void Update()
    {
        if (Input.GetKeyDown(grappleKey))
        {
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
}
