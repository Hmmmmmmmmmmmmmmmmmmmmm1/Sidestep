using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    private float timer;
    private bool startTime = false;

    public Material GLOW;

    // Start is called before the first frame update
    void Awake()
    {
        startTime = true;

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(gameObject.GetComponent<MeshRenderer>().materials[0]);
        if (startTime){
            timer += Time.deltaTime;
        }
        if (timer >= 15){
            Destroy(gameObject);
        }
        if (Mathf.Round(timer) % 2 == 1 && timer >= 10){
            gameObject.GetComponent<MeshRenderer>().materials[1].color = Color.red;
        }
        else if (timer >= 10){
            gameObject.GetComponent<MeshRenderer>().materials[0].color = Color.black;
            gameObject.GetComponent<MeshRenderer>().materials[1].color = Color.yellow;
        }
    }
}
