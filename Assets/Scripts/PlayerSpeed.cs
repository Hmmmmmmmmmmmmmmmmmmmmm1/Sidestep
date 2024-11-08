using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerSpeed : MonoBehaviour
{
    GameObject speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = GameObject.Find("PlayerSpeed");
    }

    // Update is called once per frame
    void Update()
    {
        speed.GetComponent<TextMeshProUGUI>().text = (Mathf.Round(GetComponent<Rigidbody>().velocity.magnitude * 100)/100).ToString();
    }
}
