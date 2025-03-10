using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GroundCheckerScript : MonoBehaviour
{
    public bool Grounded;
    
    
    void Update()
    {
        Ray groundcheck = new Ray(transform.position, transform.down, 1f, DefaultRaycastLayers, QueryTriggerInteraction.UseGlobal);
        RaycastHit hitData;

        if(!transform.parent.gameObject.GetComponent<PhotonView>().IsMine)
        {
            Destroy(this.gameObject);
        }
    }
}
