using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon.StructWrapping;
using Unity.VisualScripting;
using ExitGames.Client.Photon;

public class Spawner : MonoBehaviour
{
    GameObject Player;
    GameObject PlayerManager;
    public Material[] glows;
    private List<GameObject> players = new List<GameObject>();
    public GameObject Camera;
    public static int playerCount = 1;

    PhotonView PV;

    void Start()
    {
        Player = PhotonNetwork.Instantiate("Ian 1", new Vector3(Random.Range(-6, 0), 15, 2), Quaternion.identity);
        GameObject Camera = PhotonNetwork.Instantiate("Camera", new Vector3(Player.transform.position.x,Player.transform.position.y + 0.5f,Player.transform.position.z ), Quaternion.identity);
        Camera.transform.parent = Player.transform;
        //Camera.SetActive(true);
        //Camera.GetComponent<CameraScript>().player = Player;
    }
}
