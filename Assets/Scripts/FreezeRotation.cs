using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeRotation : MonoBehaviour
{
    [Header("Freeze Rotation")]
    public Transform child;
    private float temp;
    public bool x;
    public bool y;
    public bool z;
    public bool w;

    private Quaternion rotation;
    void Start()
    {
        //rotation = transform.rotation;
        //transform.localPosition = new Vector3(0, 0, 0);
    }

    void Update()
    {

        child.transform.localRotation = Quaternion.Euler (0,90,0);




        /*Quaternion temp = transform.rotation;
        if (x){
            temp = new Quaternion (temp.x,rotation.y,rotation.z,rotation.w);
        }
        if (y){
            temp = new Quaternion (rotation.x,temp.y,rotation.z,rotation.w);
        }
        if (z){
            temp = new Quaternion (rotation.x,rotation.y,temp.z,rotation.w);
        }
        if (w){
            temp = new Quaternion (rotation.x,rotation.y,rotation.z,temp.w);
        }
        transform.rotation = temp;
        */
    }
}
