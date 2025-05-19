using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class theresClassAndThenTheresRace : MonoBehaviour
{
    public GameObject PlayerManager;
    PhotonView PV;
    void Awake()
    {
        PlayerManager = GameObject.Find("PlayerCounter");
    }

    void Start()
    {
        PV = gameObject.GetComponent<PhotonView>();
        PV.RPC("PlayerSpawn", RpcTarget.All);
    }

    [PunRPC]
    void PlayerSpawn()
    {
        GameObject target = GameObject.Find("Ian 1(Clone)");
        if (PlayerManager && target){
            target.name = "Player " + seeingPeopleOnlyBasedOnTheirColor.playerCount;
            target.GetComponent<LineRenderer>().material = PlayerManager.GetComponent<seeingPeopleOnlyBasedOnTheirColor>().glows[seeingPeopleOnlyBasedOnTheirColor.playerCount - 1];
            target.transform.Find("Marker").GetComponent<MeshRenderer>().material = PlayerManager.GetComponent<seeingPeopleOnlyBasedOnTheirColor>().glows[seeingPeopleOnlyBasedOnTheirColor.playerCount - 1];
            target.transform.Find("Sword Holder/Sword/Trail").GetComponent<ParticleSystemRenderer>().trailMaterial = PlayerManager.GetComponent<seeingPeopleOnlyBasedOnTheirColor>().glows[seeingPeopleOnlyBasedOnTheirColor.playerCount - 1];
            PlayerManager.GetComponent<seeingPeopleOnlyBasedOnTheirColor>().newPlayerJoin();
        //Debug.Log(gameObject.transform.Find("Marker").GetComponent<MeshRenderer>().material.name);
        }
    }
}
