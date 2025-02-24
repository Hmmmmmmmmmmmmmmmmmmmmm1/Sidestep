using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RyanTestScript : MonoBehaviour
{
    private float gobo = 0.025f;
    private Vector3 goob;
    // Start is called before the first frame update
    void Start()
    {
        goob = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Math.Abs(goob.x-transform.position.x) >= 12){
            gobo*=-1;
        }
        transform.position += new Vector3(gobo,0,0);
    }
}
