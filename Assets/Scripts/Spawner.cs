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

        //PV = gameObject.GetComponent<PhotonView>();
        Player = PhotonNetwork.Instantiate("Ian 1", new Vector3(Random.Range(-6, 0), 15, 2), Quaternion.identity);
        //bigSad();
        PV = Player.GetComponent<PhotonView>();
        //players.Add(Player);
        //PV.RPC("PlayerSpawn", RpcTarget.All);
        //Debug.Log(playerCount + " " + players.Count);
        GameObject Camera = PhotonNetwork.Instantiate("Camera", new Vector3(Player.transform.position.x,Player.transform.position.y + 0.5f,Player.transform.position.z ), Quaternion.identity);
        Camera.transform.parent = Player.transform;
        //Camera.SetActive(true);
        //Camera.GetComponent<CameraScript>().player = Player;
    }

    [PunRPC]
    void PlayerSpawn()
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
        /*
        gameObject.name = "Player " + num;
        Player.transform.Find("Marker").GetComponent<MeshRenderer>().material = glows[num - 1];
        Debug.Log("Player #" + num + " has joined");
        */

        Player.name = "Player " + seeingPeopleOnlyBasedOnTheirColor.playerCount;
        Player.transform.Find("Marker").GetComponent<MeshRenderer>().material = PlayerManager.GetComponent<seeingPeopleOnlyBasedOnTheirColor>().glows[seeingPeopleOnlyBasedOnTheirColor.playerCount - 1];
        PlayerManager.GetComponent<seeingPeopleOnlyBasedOnTheirColor>().newPlayerJoin();
    }

    public void bigSad(){
        Player.name = "Player " + playerCount;
        Player.transform.Find("Marker").GetComponent<MeshRenderer>().material = glows[playerCount - 1];
        playerCount++;
    }

    private void SendMoveUnitsToTargetPositionEvent()
    {
        const byte dodohed = 1;
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        PhotonNetwork.RaiseEvent(dodohed, Player, raiseEventOptions, SendOptions.SendReliable);
    }
}
