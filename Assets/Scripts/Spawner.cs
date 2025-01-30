using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon.StructWrapping;
using Unity.VisualScripting;

public class Spawner : MonoBehaviour
{
    public GameObject Camera;

    void Start()
    {
        GameObject Player = PhotonNetwork.Instantiate("Ian 1", new Vector3(Random.Range(-6, 0), 10, 2), Quaternion.identity);
        GameObject Camera = PhotonNetwork.Instantiate("Camera", new Vector3(Player.transform.position.x,Player.transform.position.y + 0.5f,Player.transform.position.z ), Quaternion.identity);
        Camera.transform.parent = Player.transform;
        //Camera.SetActive(true);
        //Camera.GetComponent<CameraScript>().player = Player;
        
    }
}
