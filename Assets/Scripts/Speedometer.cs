using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    public GameObject player;
    public GameObject needle;
    public Text speed;

    public static int currentSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float faster = player.GetComponent<Rigidbody>().velocity.magnitude / 70;
        //Debug.Log(grum + "  " + player.GetComponent<Rigidbody>().velocity.magnitude);
        //needle.transform.rotation = new Quaternion(0,0,90 - 0,0);
        if (faster <= 1){
            needle.transform.eulerAngles = new Vector3(0,0,90 - (180 *faster));
            if(player.GetComponent<Rigidbody>().velocity.magnitude.ToString().Length > 1){
                currentSpeed = int.Parse(player.GetComponent<Rigidbody>().velocity.magnitude.ToString().Substring(0,player.GetComponent<Rigidbody>().velocity.magnitude.ToString().IndexOf(".")));
            }
            while (currentSpeed % 10 != 0 && currentSpeed != 0){
                currentSpeed --;
            }
            speed.color = Color.white;
            speed.text = currentSpeed +"" /*+ " mph"*/;
        }
        if (faster > 1){
            needle.transform.eulerAngles = new Vector3(0,0,-90);
            speed.color = Color.red;
            speed.text = "Too FAST!";
            currentSpeed = 70;
        }
    }
}
