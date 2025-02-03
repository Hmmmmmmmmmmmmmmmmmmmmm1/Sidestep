using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WallCheckerScript : MonoBehaviour
{
    public bool lHit;
    public bool rHit;
    
    void Update()
    {
        if(!transform.parent.gameObject.GetComponent<PhotonView>().IsMine)
        {
            Destroy(this.gameObject);
        }
    }
}
