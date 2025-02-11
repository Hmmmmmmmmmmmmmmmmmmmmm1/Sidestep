using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftGroundCheckerScript : GroundCheckerScript
{
    public BoxCollider collider;
    public void OnTriggerEnter(Collider collider)
    {
        lGrounded = true;
        //this.collider = collider;
    }
    public void OnTriggerExit(Collider collider)
    {
        lGrounded = false;
    }

    public void Check()
    {
        //gameObject.GetComponent<BoxCollider>().bounds.Intersects(collider);
    }
}
