using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftGroundCheckerScript : GroundCheckerScript
{
    public void OnTriggerEnter(Collider collider)
    {
        lGrounded = true;
    }
    public void OnTriggerExit(Collider collider)
    {
        lGrounded = false;
    }
}
