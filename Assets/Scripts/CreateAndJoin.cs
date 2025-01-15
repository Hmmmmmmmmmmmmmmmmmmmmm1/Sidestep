using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class CreateAndJoin : MonoBehaviourPunCallbacks
{
    public TMP_InputField input_Create;
    public TMP_InputField input_Join;
    public bool Lucian = true;
    public bool Casey = false;
    public bool Ryan = false;

    //not sure which we want
    /*
    public void CreateOrJoinRoom()
    {
        PhotonNetwork.JoinOrCreateRoom(input_Create.text, new RoomOptions() {MaxPlayers = 2}, TypedLobby.Default, null);

    */

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(input_Create.text, new RoomOptions() { MaxPlayers = 4, IsVisible = true }, TypedLobby.Default, null);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(input_Join.text);
    }
    public void JoinRoomInList(string RoomName)
    {
        PhotonNetwork.JoinRoom(RoomName);
    }

    public override void OnJoinedRoom()
    {
        if (Casey == true)
        {
            PhotonNetwork.LoadLevel("Casey's Scene");
        }
        if (Ryan == true)
        {
            PhotonNetwork.LoadLevel("Ryan's Scene");
        }
        if (Lucian == true)
        {
            PhotonNetwork.LoadLevel("Rough Draft");
        }
    }
    public void setLucian()
    {
        Lucian = true;
        Ryan = false;
        Casey = false;
    }
    public void setCasey()
    {
        Lucian = false;
        Ryan = false;
        Casey = true;
    }
    public void setRyan()
    {
        Lucian = false;
        Ryan = true;
        Casey = false;
    }


}
