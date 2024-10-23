using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.CharacterControl;
using ExitGames.Client.Photon.StructWrapping;
using Unity.VisualScripting;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public Material blue;
    public Material red;

   [Header("References")]
    public Transform cam;
    public Transform gunTip;
    public LayerMask whatIsGrappleable;
    public LineRenderer lr;

    [Header("Grappling")]
    public float maxGrappleDistance;
    public float grappleDelayTime;
    public float overshootYAxis;

    private Vector3 grapplePoint;

     private SpringJoint joint;

    [Header("Cooldown")]
    public float grapplingCd;
    private float grapplingCdTimer;

    [Header("Input")]
    public KeyCode grappleKey = KeyCode.Mouse1;

    private bool grappling;
    float distance = 0;

    private void Start()
    {
        lr.enabled = false;
    }

    private void Update()
    {
        lr.SetPosition(0,gunTip.position);
        if (Input.GetKeyDown(grappleKey)){
            StartGrapple();
            lr.enabled = true;
            lr.SetPosition(1, grapplePoint);
            lr.GetComponent<LineRenderer>().material = blue;
            distance = (float)Math.Sqrt(Math.Pow((grapplePoint.x - gunTip.position.x),2) + Math.Pow((grapplePoint.y - gunTip.position.y),2) + Math.Pow((grapplePoint.z - gunTip.position.z),2));
        } 
        if (Input.GetKeyUp(grappleKey)){
            lr.enabled = false;
        }

        if (grapplingCdTimer > 0){
            grapplingCdTimer -= Time.deltaTime;
        }
        
        if ((float)Math.Sqrt(Math.Pow((grapplePoint.x - gunTip.position.x),2) + Math.Pow((grapplePoint.y - gunTip.position.y),2) + Math.Pow((grapplePoint.z - gunTip.position.z),2)) - 2 > distance && grappling && distance != 0){
            lr.GetComponent<LineRenderer>().material = red;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        Debug.Log((float)Math.Sqrt(Math.Pow((grapplePoint.x - gunTip.position.x),2) + Math.Pow((grapplePoint.y - gunTip.position.y),2) + Math.Pow((grapplePoint.z - gunTip.position.z),2)) + "   " + distance);
    }

    private void StartGrapple()
    {
        if (grapplingCdTimer > 0) return;

        grappling = true;

        //pm.freeze = true;

        RaycastHit hit;
        if(Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance, whatIsGrappleable))
        {
            Debug.Log("cow" + hit.transform.gameObject.name);
            grapplePoint = hit.point;
            joint = gameObject.AddComponent<SpringJoint>();

            joint.autoConfigureConnectedAnchor = false;

            joint.connectedAnchor = grapplePoint;

            Invoke(nameof(ExecuteGrapple), grappleDelayTime);
        }
        else
        {
            grapplePoint = cam.position + cam.forward * maxGrappleDistance;

            Invoke(nameof(StopGrapple), grappleDelayTime);
        }
    }

    private void ExecuteGrapple()
    {
        //pm.freeze = false;

        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        float grapplePointRelativeYPos = grapplePoint.y - lowestPoint.y;
        float highestPointOnArc = grapplePointRelativeYPos + overshootYAxis;

        if (grapplePointRelativeYPos < 0) highestPointOnArc = overshootYAxis;

        Vector3 spot = new Vector3(grapplePoint.x,grapplePoint.y + 5,grapplePoint.z);
        //transform.position = Vector3.MoveTowards(transform.position,spot, 100);
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //pm.JumpToPosition(grapplePoint, highestPointOnArc);

        Invoke(nameof(StopGrapple), 1f);
    }

    public void StopGrapple()
    {
        //pm.freeze = false;

        grappling = false;

        grapplingCdTimer = grapplingCd;

        //lr.enabled = false;
    }

    public bool IsGrappling()
    {
        return grappling;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }
}
