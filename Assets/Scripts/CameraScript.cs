using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public float mouseSensitivity = 2f;
    private float VerticalRotation = 0f;
    private Transform playertrans;

    void Start()
    {
        //get transform
        playertrans = player.GetComponent<Transform>();
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
        playertrans.Rotate(Vector3.up * Xmove);
    }
}
