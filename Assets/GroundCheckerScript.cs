using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GroundCheckerScript : MonoBehaviour
{
    public bool lGrounded;
    public bool rGrounded;
    
    void Update()
    {
        if(!transform.parent.gameObject.GetComponent<PhotonView>().IsMine)
        {
            Destroy(this.gameObject);
        }
    }
}
