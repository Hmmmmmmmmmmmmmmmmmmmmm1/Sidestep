using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MarkerScript : MonoBehaviour
{
    void Update()
    {
        if(!GetComponent<PhotonView>().IsMine)
        {
            Destroy(this.gameObject);
        }
    }
}
