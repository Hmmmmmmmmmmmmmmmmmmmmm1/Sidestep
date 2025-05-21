using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
namespace Assets.Scripts.CharacterControl

{
public class GroundCheckerScript : MonoBehaviour
{
    public bool Grounded;
    public RaycastHit HitData;
    
    
    
    void Update()
    {
        Ray groundcheck = new Ray(transform.position, (-1*transform.up));
        Grounded = Physics.Raycast(groundcheck, out HitData, 1f);
  
        if(!transform.parent.gameObject.GetComponent<PhotonView>().IsMine)
        {
            Destroy(this.gameObject);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, HitData.point);
    }
}
}
