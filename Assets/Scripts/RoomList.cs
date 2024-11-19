using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class roomlist : MonoBehaviourPunCallbacks
{
    public override void OnRoomListUpdate(List<RoomInfo> roomlist)
    {
        for (int i = 0; i < roomlist.Count; i++)
        {
            print(roomlist[i].Name);
        }
    }
}
