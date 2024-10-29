using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightGroundCheckerScript : GroundCheckerScript
{
    public void OnTriggerEnter(Collider collider)
    {
        rGrounded = true;
    }
    public void OnTriggerExit(Collider collider)
    {
        rGrounded = false;
    }
}
