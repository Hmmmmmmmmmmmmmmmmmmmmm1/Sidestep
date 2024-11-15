using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    private float timer;
    private bool startTime = false;

    public Color black;
    public Color yellow;
    // Start is called before the first frame update
    void Awake()
    {
        startTime = true;
        //Material black = gameObject.GetComponent<MeshRenderer>().material.size

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(gameObject.GetComponent<MeshRenderer>().material.color);
        if (startTime){
            timer += Time.deltaTime;
        }
        if (timer >= 10){
            Destroy(gameObject);
        }
        if (Mathf.Round(timer) % 2 == 0){
            gameObject.GetComponent<MeshRenderer>().material.color = new Color(gameObject.GetComponent<Renderer>().material.color.r,gameObject.GetComponent<Renderer>().material.color.g,gameObject.GetComponent<Renderer>().material.color.b,0f);
        }
        else{
            //gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
