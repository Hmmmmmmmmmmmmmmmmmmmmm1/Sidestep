using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerSpeed : MonoBehaviour
{
    public GameObject Speed;
    // Update is called once per frame
    void Update()
    {
        Speed = GameObject.Find("PlayerSpeed");
        Speed.GetComponent<TextMeshProUGUI>().text = (Mathf.Round(GetComponent<Rigidbody>().velocity.magnitude * 100)/100).ToString();
    }
}
