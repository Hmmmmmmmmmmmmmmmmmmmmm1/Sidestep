using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Networking;

public class CameraScript : MonoBehaviourPunCallbacks
{

    public float mouseSensitivity;
    private float VerticalRotation = 0f;
    public GameObject Player;

    void Start()
    {
        //get transform
        //Lock and hide Cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public override void OnJoinedRoom()
    {
        gameObject.SetActive(false);
    }



    void Update()
    {
        //Collect mouse input
        float Xmove = Input.GetAxis("Mouse X")*mouseSensitivity;
        float Ymove = Input.GetAxis("Mouse Y")*mouseSensitivity;

        //rotate around x
        VerticalRotation -= Ymove;
        VerticalRotation = Mathf.Clamp(VerticalRotation, -90f, 90f);
        transform.localEulerAngles = Vector3.right * VerticalRotation;

        //rotate player around y
        
        if(GetComponent<PhotonView>().IsMine == true)
        {
            gameObject.transform.parent = Player.transform;
            //gameObject.SetActive(true);
        }

        gameObject.transform.parent.Rotate(Vector3.up * Xmove);

        
    }
}
