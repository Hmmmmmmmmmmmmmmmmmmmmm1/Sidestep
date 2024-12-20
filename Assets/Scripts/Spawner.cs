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
        GameObject Camera = PhotonNetwork.Instantiate("Camera", Player.transform.position, Quaternion.identity);
        Camera.transform.parent = Player.transform;
        //Camera.SetActive(true);
        //Camera.GetComponent<CameraScript>().player = Player;
        
    }
}
