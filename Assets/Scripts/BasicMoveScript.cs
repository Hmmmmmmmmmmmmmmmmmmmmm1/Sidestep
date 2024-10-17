using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMoveScript : MonoBehaviour
{
    public Rigidbody rb;
    public Transform trans;

    // Update is called once per frame
    void Update()
    {
        Movement();
    }



    public void Movement()
    {
        if (Input.GetKey("w"))
        {
            rb.AddForce(Vector3.Min(trans.forward * 2, trans.forward * 20 - rb.velocity));
        } 
        if (Input.GetKey("s"))
        {
            rb.AddForce(Vector3.Max(trans.forward * -2, trans.forward * -20 - rb.velocity));
        } 
        if (Input.GetKey("a"))
        {
            rb.AddForce(Vector3.Max(trans.right * -2, trans.right * -20 - rb.velocity));
        } 
        if (Input.GetKey("d"))
        {
            rb.AddForce(Vector3.Min(trans.right * 2, trans.right * 20 - rb.velocity));
        }
    }

}