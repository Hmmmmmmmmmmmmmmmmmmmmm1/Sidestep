using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour
{
    void Start()
    {
        transform.localPosition = new Vector3(0, 0, 0);
    }
}
