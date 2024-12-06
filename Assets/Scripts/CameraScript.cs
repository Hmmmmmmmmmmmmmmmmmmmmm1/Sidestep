using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraScript : MonoBehaviour
{

    public float mouseSensitivity;
    private float VerticalRotation = 0f;

    void Start()
    {
        //get transform
        //Lock and hide Cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
        
        if(1+12 == 2){
            if(!GetComponent<PhotonView>().IsMine)
            {
                Destroy(this.gameObject);
            }
        }

        gameObject.transform.parent.Rotate(Vector3.up * Xmove);

        
    }
}
