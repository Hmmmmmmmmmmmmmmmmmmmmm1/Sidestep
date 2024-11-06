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
    public KeyCode Skill1Trigger;

    public enum BattleAbility {Lifesteal, Immobilize, FireAspectII, Knockback, doxx}
    public BattleAbility Skill2;
    public KeyCode Skill2Trigger;

    private bool activated;

    private float Skill1Timer = 1000;
    private float Skill2Timer = 1000;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        Skill1Timer += Time.deltaTime;
        Skill2Timer += Time.deltaTime;
        //Lunch Mode
        if (Skill1 == MovementAbility.Launch){
            if (Input.GetKeyDown(Skill1Trigger)){

            int lookingDown;
            if (Skill1Timer >= 5){
                if ((Vector3.Dot(GameObject.Find("Main Camera").transform.forward,Vector3.up) + 1) * 9 > 1 - Vector3.Dot(GameObject.Find("Main Camera").transform.forward,Vector3.up)){
                    lookingDown = 1;
                }
                else{
                    lookingDown = -1;
                }
                gameObject.GetComponent<Rigidbody>().velocity += Vector3.up * lookingDown * 1000 + transform.forward * 50;

                gameObject.GetComponent<Rigidbody>().drag = 5;
                activated = true;
                Skill1Timer = 0;
            }

            }
            if(gameObject.GetComponent<Rigidbody>().velocity.y >= 20 && Skill1 == MovementAbility.Launch){
                gameObject.GetComponent<Rigidbody>().velocity += new Vector3(0,-3,0);
            }

            if (gameObject.GetComponent<Rigidbody>().velocity.y <= 10 && activated){
                gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,gameObject.GetComponent<Rigidbody>().velocity.y,0);
                gameObject.GetComponent<Rigidbody>().drag = 0;
                activated = false;
            }
        }
    }
}
