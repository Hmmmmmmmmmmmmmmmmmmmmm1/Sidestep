using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    //Movement

    //direction change
    //blink
    //launch
    //temp sturctures
    //healing

    //Battle

    //lifesteal
    //immobiliazaiton
    //fire aspect II
    //knockback

    public String ability;
    public enum MovementAbility {DirectionChange, Blink, Launch, LegoBuildMode, Heal}
    public MovementAbility Skill1;

    public enum BattleAbility {Lifesteal, Immobilize, FireAspectII, Knockback, doxx}
    public BattleAbility Skill2;

    private bool activated;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Vector3.Dot(GameObject.Find("Main Camera").transform.forward,Vector3.up));
        if (Input.GetKeyDown(KeyCode.Alpha7)){

            int lookingDown;
            bool grounded = Physics.Raycast(transform.position, Vector3.down, 2);
            grounded = true;
            if (Skill1 == MovementAbility.Launch && grounded){
                if ((Vector3.Dot(GameObject.Find("Main Camera").transform.forward,Vector3.up) + 1) * 9 > 1 - Vector3.Dot(GameObject.Find("Main Camera").transform.forward,Vector3.up)){
                    Debug.Log("up");
                    lookingDown = 1;
                }
                else{
                    Debug.Log("dn");
                    lookingDown = -1;
                }
                //gameObject.GetComponent<Rigidbody>().velocity += new Vector3(0,600,0) + transform.forward * 400;
                gameObject.GetComponent<Rigidbody>().velocity += Vector3.up * lookingDown * 600 + transform.forward * 200;

                gameObject.GetComponent<Rigidbody>().drag = 9;
                activated = true;
            }

        }
        if(activated && gameObject.GetComponent<Rigidbody>().velocity.magnitude <= 20 && Skill1 == MovementAbility.Launch){
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,-30,0);
            
            gameObject.GetComponent<Rigidbody>().drag = 0;
            activated = false;
        }
    }
}
