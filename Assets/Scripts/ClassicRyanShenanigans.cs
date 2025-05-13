using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.CharacterControl;
using UnityEngine;
using UnityEngine.UI;

public class ClassicRyanShenanigans : MonoBehaviour
{
    private float timer;
    public GameObject tell;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 1){
            tell.GetComponent<Outline>().enabled = true;
        }
        else{
            tell.GetComponent<Outline>().enabled = false;
        }

        if (Input.GetKey(KeyCode.W) && Input.GetMouseButtonDown(0) && timer > 1){
            Debug.Log("devil may cry type gameplay");
            gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 1000,ForceMode.Force);
            timer = 0;
        }

        if (Input.GetKey(KeyCode.A) && Input.GetMouseButtonDown(0) && timer > 1){
            Debug.Log("devil may cry type gameplay");
            //transform.position += new Vector3(-10,0,0);
            transform.position += transform.right * 5;
            timer = 0;
        }

        if (Input.GetKey(KeyCode.D) && Input.GetMouseButtonDown(0) && timer > 1){
            Debug.Log("devil may cry type gameplay");
            transform.position += transform.right * -5;
            timer = 0;
        }

        if (Input.GetKey(KeyCode.S) && Input.GetMouseButtonDown(0) && timer > 1){
            Debug.Log("devil may cry type gameplay");
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 500,ForceMode.Force);
            timer = 0;
        }
    }
}
