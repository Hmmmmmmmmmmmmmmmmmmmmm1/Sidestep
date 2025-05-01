using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftWallCheckerScript : WallCheckerScript
{
    public void OnTriggerEnter(Collider collider)
    {
        lHit = true;
        //Debug.Log("ENTERING");
    }
    public void OnTriggerExit(Collider collider)
    {
        lHit = false;
        Debug.Log(collider);
    }
}
