using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grapplinghook2 : MonoBehaviour
{
    [Header("References")]
    public LineRenderer lr;
    private Vector3 grapplePoint;
    public Transform gunTip;
    public Camera cam;
    private SpringJoint joint;

    [Header("Grappling")]
    private float maxDistance = 300f;

    [Header("Input")]
    public KeyCode grappleKey = KeyCode.Mouse1;

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
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            joint.spring = 7.5f;
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
            joint.spring = 0.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

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
