using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class roomlist : MonoBehaviourPunCallbacks
{
    public GameObject RoomPrefab;
    public override void OnRoomListUpdate(List<RoomInfo> roomlist)
    {
        for (int i = 0; i < roomlist.Count; i++)
        {
            print(roomlist[i].Name);
            GameObject Room = Instantiate(RoomPrefab, Vector3.zero, Quaternion.identity, GameObject.Find("Content").transform);
            Room.GetComponent<Room>().Name.text = roomlist[i].Name;
        }
    }
}
