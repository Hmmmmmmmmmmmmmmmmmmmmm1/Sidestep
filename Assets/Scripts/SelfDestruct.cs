using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    private float timer;
    private bool startTime = false;

    public Material black;
    //public Color yellow;

    // Start is called before the first frame update
    void Awake()
    {
        startTime = true;
        black = gameObject.GetComponent<MeshRenderer>().materials[0];

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(gameObject.GetComponent<MeshRenderer>().materials[0]);
        if (startTime){
            timer += Time.deltaTime;
        }
        if (timer >= 10){
            Destroy(gameObject);
        }
        if (Mathf.Round(timer) % 2 == 0){
            gameObject.GetComponent<MeshRenderer>().material.color = new Color(black.color.r,black.color.g,black.color.b,0.5f);
        }
        else{
            //gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
