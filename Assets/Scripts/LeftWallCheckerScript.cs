using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftWallCheckerScript : WallCheckerScript
{
    public void OnTriggerEnter(Collider collider)
    {
        if (!collider.CompareTag("NotWall") && !collider.CompareTag("damage"))
        {
            lHit = true;
        }
        

        //Debug.Log("ENTERING");
    }
    public void OnTriggerExit(Collider collider)
    {
        lHit = false;
        //Debug.Log(collider);
    }
}
