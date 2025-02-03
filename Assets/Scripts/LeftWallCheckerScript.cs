using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftWallCheckerScript : WallCheckerScript
{
    public void OnTriggerEnter(Collider collider)
    {
        lHit = true;
    }
    public void OnTriggerExit(Collider collider)
    {
        lHit = false;
    }
}
