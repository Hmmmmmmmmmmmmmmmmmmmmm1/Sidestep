using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    //Movement

    //direction change
    //blink
    //launch                :)
    //temp sturctures       :)
    //healing

    //Battle

    //lifesteal
    //immobiliazaiton
    //fire aspect II
    //knockback

    public enum MovementAbility {DirectionChange, Blink, Launch, LegoBuildMode, Heal}
    public String Skill1;
    public KeyCode Skill1Trigger;

    public enum BattleAbility {Lifesteal, Immobilize, FireAspectII, Knockback, doxx}
    public String Skill2;
    public KeyCode Skill2Trigger;

    private bool activated;

    public Text Skill1Text;
    public Text Skill2Text;
    private float Skill1Cooldown = 0;
    private float Skill2Cooldown = 0;

    //Lunch

    //Lego
    private int legoesPerAbility = 0;
    private GameObject legos;

    // Start is called before the first frame update
    void Start()
    {
        legos = Resources.Load<GameObject>("Wall");
    }

    // Update is called once per frame
    void Update()
    {
        Cooldown();

        //Lunch Mode
        if (Skill1 == "Lynch"){
            if (Input.GetKeyDown(Skill1Trigger)){

            int lookingDown;
            if (Skill1Cooldown < 0){
                if ((Vector3.Dot(GameObject.Find("Main Camera").transform.forward,Vector3.up) + 1) * 9 > 1 - Vector3.Dot(GameObject.Find("Main Camera").transform.forward,Vector3.up)){
                    lookingDown = 1;
                }
                else{
                    lookingDown = -1;
                }
                gameObject.GetComponent<Rigidbody>().velocity += Vector3.up * lookingDown * 1000 + transform.forward * 50;

                gameObject.GetComponent<Rigidbody>().drag = 5;
                activated = true;
                Skill1Cooldown = 5;
            }

            }
            if(gameObject.GetComponent<Rigidbody>().velocity.y >= 20){
                gameObject.GetComponent<Rigidbody>().velocity += new Vector3(0,-3,0);
            }

            if (gameObject.GetComponent<Rigidbody>().velocity.y <= 10 && activated){
                //gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,gameObject.GetComponent<Rigidbody>().velocity.y,0);
                gameObject.GetComponent<Rigidbody>().drag = 0;
                activated = false;
            }
        }
        if (Skill1 == "Duplo mode"){
            if (Input.GetKeyDown(Skill1Trigger) && Skill1Cooldown < 0){
                Instantiate(legos, transform.position + transform.forward * 10 + Vector3.up * 2, legos.transform.rotation);
                legoesPerAbility++;
                if(legoesPerAbility == 5){
                    legoesPerAbility = 0;
                    Skill1Cooldown = 10;
                }
            }
        }

    }

    public void Cooldown(){
        Skill1Cooldown -= Time.deltaTime;
        Skill2Cooldown -= Time.deltaTime;

        Skill1Text.text = Skill1Cooldown.ToString().Substring(0,1);
        Skill2Text.text = Skill2Cooldown.ToString().Substring(0,1);
    }

    public void getAbilities(String Skill1, String Skill2){
        this.Skill1 = Skill1;
        this.Skill2 = Skill2;
    }
}