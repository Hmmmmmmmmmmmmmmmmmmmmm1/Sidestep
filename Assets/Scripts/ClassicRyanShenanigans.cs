using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.CharacterControl;
using UnityEngine;
using UnityEngine.UI;

public class ClassicRyanShenanigans : MonoBehaviour
{
    private float timer;
    public GameObject tell;

    private bool secret = false;

    // Update is called once per frame
    void Update()
    {
        secretActivate();
        timer += Time.deltaTime;

        if (secret)
        {
            timer = 10;
        }
        if (timer > 1)
        {
            tell.GetComponent<Outline>().enabled = true;
        }
        else
        {
            tell.GetComponent<Outline>().enabled = false;
        }

        if (Input.GetKey(KeyCode.W) && Input.GetMouseButtonDown(0) && timer > 1)
        {
            Debug.Log("devil may cry type gameplay");
            gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 1000, ForceMode.Force);
            timer = 0;
        }

        if (Input.GetKey(KeyCode.A) && Input.GetMouseButtonDown(0) && timer > 1)
        {
            Debug.Log("devil may cry type gameplay");
            //transform.position += new Vector3(-10,0,0);
            transform.position += transform.right * -5;
            timer = 0;
        }

        if (Input.GetKey(KeyCode.D) && Input.GetMouseButtonDown(0) && timer > 1)
        {
            Debug.Log("devil may cry type gameplay");
            transform.position += transform.right * 5;
            timer = 0;
        }

        if (Input.GetKey(KeyCode.S) && Input.GetMouseButtonDown(0) && timer > 1)
        {
            Debug.Log("devil may cry type gameplay");
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 500, ForceMode.Force);
            timer = 0;
        }
    }

    public void secretActivate()
    {
        if (Input.GetKeyDown(KeyCode.Backspace) && Input.GetKey(KeyCode.P))
        {
            secret = true;
        }
    }
}
