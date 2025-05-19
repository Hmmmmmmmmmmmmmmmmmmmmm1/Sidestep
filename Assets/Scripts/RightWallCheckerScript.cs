using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightWallCheckerScript : WallCheckerScript
{
    public void OnTriggerEnter(Collider collider)
    {
        if (!collider.CompareTag("NotWall"))
        {
            rHit = true;
        }
    }
    public void OnTriggerExit(Collider collider)
    {
        rHit = false;
    }
}
