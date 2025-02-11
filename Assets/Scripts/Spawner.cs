using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon.StructWrapping;
using Unity.VisualScripting;

public class Spawner : MonoBehaviour
{
    GameObject Player;

    public Material[] glows;
    private List<GameObject> players = new List<GameObject>();
    public GameObject Camera;
    public static int playerCount = 1;

    PhotonView PV;

    void Start()
    {
        PV = gameObject.GetComponent<PhotonView>();
        if(PV.IsMine){
        PV.RPC("PlayerSpawn", RpcTarget.All, playerCount);
        }

        Debug.Log(playerCount + " " + players.Count);
                playerCount++;

        //Camera.SetActive(true);
        //Camera.GetComponent<CameraScript>().player = Player;
        
    }

    [PunRPC]
    void PlayerSpawn(int num)
    {
        /*
        players[playerCount - 1].name = "Player " + playerCount;
        players[playerCount - 1].transform.Find("Marker").GetComponent<MeshRenderer>().material = glows[playerCount - 1];
        Debug.Log("Player #" + playerCount + " has joined");
        playerCount++;
        for(int x=0; x<players.Count;x++){
            Debug.Log(players[x].name);
        }
        */
        Player = PhotonNetwork.Instantiate("Ian 1", new Vector3(Random.Range(-6, 0), 15, 2), Quaternion.identity);
        players.Add(Player);
        GameObject Camera = PhotonNetwork.Instantiate("Camera", new Vector3(Player.transform.position.x,Player.transform.position.y + 0.5f,Player.transform.position.z ), Quaternion.identity);
        Camera.transform.parent = Player.transform;

        Player.name = "Player " + num;
        Player.transform.Find("Marker").GetComponent<MeshRenderer>().material = glows[num - 1];
        Debug.Log("Player #" + num + " has joined");
    }
}
