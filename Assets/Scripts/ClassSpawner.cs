using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ClassSpawner : MonoBehaviour
{
    void Start()
    {
        GameObject Classes = PhotonNetwork.Instantiate("Classes", new Vector3(Random.Range(-6, 0), 1, 2), Quaternion.identity);
    }
}
