using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightGroundCheckerScript : RightGroundCheckerScript
{
    public void OnTriggerEnter(Collider collider)
    {
        rGrounded = true;
    }
}
