using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    //Movement

    //direction change
    //blink                 :)
    //launch                :)
    //temp sturctures       :)
    //healing

    //Battle

    //lifesteal
    //immobiliazaiton
    //fire aspect II
    //knockback

    public enum MovementAbility {DirectionChange, Blink, Launch, LegoBuildMode, Heal}
    public static int Skill1;
    public KeyCode Skill1Trigger;

    public enum BattleAbility {Lifesteal, Immobilize, FireAspectII, Knockback, doxx}
    public static int Skill2;
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

    //lightspeed
    private Vector3 boxSize;
    private Ray ray;
    private float grub = 20;

    // Start is called before the first frame update
    void Start()
    {
        legos = Resources.Load<GameObject>("Wall");
        boxSize = gameObject.GetComponent<Collider>().bounds.size / 1.75f;
    }

    // Update is called once per frame
    void Update()
    {
        Cooldown();

        //Lunch Mode
        if (Skill1 == 1){
            if (Input.GetKeyDown(Skill1Trigger)){

            int lookingDown;
            if (Skill1Cooldown < 1){
                if ((Vector3.Dot(GameObject.Find("Main Camera").transform.forward,Vector3.up) + 1) * 9 > 1 - Vector3.Dot(GameObject.Find("Main Camera").transform.forward,Vector3.up)){
                    lookingDown = 1;
                }
                else{
                    lookingDown = -1;
                }
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.up * lookingDown * 100 + transform.forward * 20;

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
        //Legos Mode
        if (Skill1 == 2){
            if (Input.GetKeyDown(Skill1Trigger) && Skill1Cooldown < 1){
                Instantiate(legos, transform.position + transform.forward * 10 + Vector3.up * 2, legos.transform.rotation);
                legoesPerAbility++;
                if(legoesPerAbility == 5){
                    legoesPerAbility = 0;
                    Skill1Cooldown = 10;
                }
            }
        }
        //Lightspeed Mode
        if (Skill1 == 3){
            if (Input.GetKeyDown(Skill1Trigger) && Skill1Cooldown < 1){
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                //Debug.DrawLine(ray.origin,ray.GetPoint(grub),Color.cyan,100f);
                Collider[] gems = Physics.OverlapBox(ray.GetPoint(grub), boxSize, Quaternion.identity);
                //Debug.Log(gems.Length);
                while(gems.Length > 0){
                    grub -= 1;
                    gems = Physics.OverlapBox(ray.GetPoint(grub), boxSize, Quaternion.identity);
                    //Debug.Log(gems.Length);
                }
                gameObject.transform.position = ray.GetPoint(grub);
                grub = 20;
                Skill1Cooldown = 3;
            }
        }
        //Ligma Mode
        if (Skill1 == 5){
            if (Input.GetKeyDown(Skill1Trigger) && Skill1Cooldown < 1){
                gameObject.GetComponent<PlayerHP>().IncreaseHP(20);
                Skill1Cooldown = 0;
            }
        }


        if (Input.GetKeyDown(KeyCode.P)){
            Debug.Log(Skill1);
        }

    }

    public void Cooldown(){
        Skill1Cooldown -= Time.deltaTime;
        Skill2Cooldown -= Time.deltaTime;

        Skill1Text.text = Skill1Cooldown.ToString().Substring(0,1);
        Skill2Text.text = Skill2Cooldown.ToString().Substring(0,1);

        if(Skill1Text.text.Equals("0")){
            Skill1Text.text = "-";
        }
        if(Skill2Text.text.Equals("0")){
            Skill2Text.text = "-";
        }
    }
}