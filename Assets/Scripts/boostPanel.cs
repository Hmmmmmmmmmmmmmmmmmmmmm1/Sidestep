using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boostPanel : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Rigidbody>().velocity += other.gameObject.transform.forward * 50;
        }
        Debug.Log(other.gameObject.tag);
    }
}
