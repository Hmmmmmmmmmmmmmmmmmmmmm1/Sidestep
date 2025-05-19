using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Respawn : MonoBehaviour
{
    public void respawn()
    {
        SceneManager.LoadScene(9);
        PhotonNetwork.ConnectUsingSettings();
        seeingPeopleOnlyBasedOnTheirColor.playerCount = 1; 
    }
}
