using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawner : MonoBehaviour
{
    public GameObject Camera;
    void Start()
    {
        GameObject Player = PhotonNetwork.Instantiate("Player", new Vector3(Random.Range(-6, 0), 1, 2), Quaternion.identity);
        Camera.GetComponent<CameraScript>().player = Player;

    }

    void Update()
    {
        
    }
}
