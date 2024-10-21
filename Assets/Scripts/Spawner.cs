using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawner : MonoBehaviour
{
    void Start()
    {
        PhotonNetwork.Instantiate("Player", new Vector3(Random.Range(-6, 0), 1, 2), Quaternion.identity);
    }

    void Update()
    {
        
    }
}
