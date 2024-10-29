using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boostPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Rigidbody>().velocity += other.gameObject.transform.forward * 50;
        }
        Debug.Log(other.gameObject.tag);
    }
}
