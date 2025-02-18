using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class theresClassAndThenTheresRace : MonoBehaviour
{
    GameObject PlayerManager;
    PhotonView PV;
    void Awake()
    {
        PV = gameObject.GetComponent<PhotonView>();
        PV.RPC("PlayerSpawn", RpcTarget.All);
    }

        [PunRPC]
    void PlayerSpawn()
    {
        gameObject.name = "Player " + seeingPeopleOnlyBasedOnTheirColor.playerCount;
        gameObject.transform.Find("Marker").GetComponent<MeshRenderer>().material = PlayerManager.GetComponent<seeingPeopleOnlyBasedOnTheirColor>().glows[seeingPeopleOnlyBasedOnTheirColor.playerCount - 1];
        PlayerManager.GetComponent<seeingPeopleOnlyBasedOnTheirColor>().newPlayerJoin();
    }
}
