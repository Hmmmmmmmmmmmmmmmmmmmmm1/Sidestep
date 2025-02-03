using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FireBeam : MonoBehaviour
{
    LineRenderer lr;
    PhotonView PV;

        // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();

        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && PV.IsMine)
        {
            PV.RPC("RPC_Beam", RpcTarget.All, this.transform.position, new Vector3(this.transform.position.x, this.transform.position.y + 5));
        }
        else
        {
            lr.enabled = false;
        }
    }

    [PunRPC]
    public void RPC_Beam(Vector3 start, Vector3 end)
    {
        lr.enabled = true;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        Debug.Log("Beam!! " + start + " | " + end);
    }
}
