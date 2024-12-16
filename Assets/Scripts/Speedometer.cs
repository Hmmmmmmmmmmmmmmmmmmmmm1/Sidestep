using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    public GameObject player;
    public GameObject needle;
    public Text speed;

    private int currentSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float grum = player.GetComponent<Rigidbody>().velocity.magnitude / 100;
        //Debug.Log(grum + "  " + player.GetComponent<Rigidbody>().velocity.magnitude);
        //needle.transform.rotation = new Quaternion(0,0,90 - 0,0);
        if (grum <= 1){
            needle.transform.eulerAngles = new Vector3(0,0,90 - (180 *grum));
            currentSpeed = int.Parse(player.GetComponent<Rigidbody>().velocity.magnitude.ToString().Substring(0,player.GetComponent<Rigidbody>().velocity.magnitude.ToString().IndexOf(".")));
            while (currentSpeed % 10 != 0 && currentSpeed != 0){
                currentSpeed --;
            }
            speed.text = currentSpeed + " mph";
        }
        if (grum > 1){
            needle.transform.eulerAngles = new Vector3(0,0,-90);
            speed.text = "Too FAST!";
        }
    }
}
