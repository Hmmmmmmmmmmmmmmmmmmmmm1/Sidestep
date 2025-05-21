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

    void Update()
    {
        child.transform.localRotation = Quaternion.Euler (0,child.transform.localEulerAngles.y,0);
    }
}
