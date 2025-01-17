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
    //healing               :)

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
    private float blinkDistance = 20;

    //Lymphoma
    private float timer;
    private float healInterval = 0f;

    private float evilTimer;
    private float dmgInterval = 0f;

    private float speedMultiplier;

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
        SpeedCheck();

        //Lunch Mode
        if (Skill1 == 1){
            if (Input.GetKeyDown(Skill1Trigger)){

            int lookingDown;
            if (Skill1Cooldown < 1){
                if ((Vector3.Dot(transform.Find("Camera").forward,Vector3.up) + 1) * 9 > 1 - Vector3.Dot(transform.Find("Camera").forward,Vector3.up)){
                    lookingDown = 1;
                }
                else{
                    lookingDown = -1;
                }
                gameObject.GetComponent<Rigidbody>().velocity = (Vector3.up * lookingDown * 100 + transform.forward * 20) * speedMultiplier;

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
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                GameObject lego = Instantiate(legos, ray.GetPoint(20 * speedMultiplier), legos.transform.rotation);
                lego.GetComponent<SelfDestruct>().expire = (int)(lego.GetComponent<SelfDestruct>().expire * speedMultiplier);
                lego.transform.localScale *= (int)Math.Pow(speedMultiplier,2);
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
                Collider[] gems = Physics.OverlapBox(ray.GetPoint(blinkDistance), boxSize, Quaternion.identity);
                //Debug.Log(gems.Length);
                while(gems.Length > 0){
                    blinkDistance -= 1;
                    gems = Physics.OverlapBox(ray.GetPoint(blinkDistance), boxSize, Quaternion.identity);
                    //Debug.Log(gems.Length);
                }
                gameObject.transform.position = ray.GetPoint(blinkDistance);
                blinkDistance = 20 * speedMultiplier;
                Skill1Cooldown = 3;
            }
        }
        //Lapse in Judgement Mode
        if (Skill1 == 4){
            if (Input.GetKeyDown(Skill1Trigger) && Skill1Cooldown < 1){
                //gameObject.GetComponent<Rigidbody>().velocity *= -1;
                gameObject.GetComponent<Rigidbody>().velocity = new Vector3(gameObject.GetComponent<Rigidbody>().velocity.x * -1, gameObject.GetComponent<Rigidbody>().velocity.y, gameObject.GetComponent<Rigidbody>().velocity.z * -1) * speedMultiplier/2;
                Skill1Cooldown = 3;
            }
        }
        //Lymphoma Mode
        if (Skill1 == 5){
            if (timer > 0){
                if (healInterval > 0){
                    healInterval -= Time.deltaTime;
                }
                if (healInterval <= 0){
                    gameObject.GetComponent<PlayerHP2>().changeHealth(1);
                    healInterval = 0.65f;
                }
            }
            timer -= Time.deltaTime;
            if (Input.GetKeyDown(Skill1Trigger) && Skill1Cooldown < 1){
                timer = 7.5f * speedMultiplier;
                if (PlayerHP2.hp < 100){
                    Skill1Cooldown = 9;
                }
            }
        }


        //Lymphoma Mode
        if (Skill2 == 1){
            if (timer > 0){
                if (dmgInterval > 0){
                    dmgInterval -= Time.deltaTime;
                }
                if (dmgInterval <= 0){
                    gameObject.GetComponent<PlayerHP2>().changeHealth(-1);
                    dmgInterval = 0.35f;
                }
            }
            timer -= Time.deltaTime;
            if (Input.GetKeyDown(Skill2Trigger) && Skill2Cooldown < 1){
                timer = 4.5f * speedMultiplier;
                    Skill1Cooldown = 9;
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
    public void SpeedCheck(){
        speedMultiplier = (float)(Math.Pow(Speedometer.currentSpeed + 1,1/7f));
    }
}