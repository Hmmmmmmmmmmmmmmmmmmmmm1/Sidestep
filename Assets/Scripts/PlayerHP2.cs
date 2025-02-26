using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP2 : MonoBehaviour
{
    public Slider slider;
    public Text hpNum;
    public static int hp = 100;

    private int oldHp = 0;
    public float hpChangeTick = 0;

    private float dmgInterval = 0;
    private GameObject touchingObject;

    void Start(){
        changeHealth(0);
        hpNum.text = hp + "";
    }

    void Update(){
        dmgCheck();
    }

    public void changeHealth(int change){
        hp += change;
        hp = Mathf.Min(hp,100);
        hp = Math.Max(hp,0);
        slider.value = hp;
        hpNum.text = hp + "";
    }
    
    public void dmgCheck()
    {
        if (dmgInterval > 0){
            dmgInterval -= Time.deltaTime;
        }
        if (dmgInterval <= 0){
            if(touchingObject && touchingObject.tag == "damage"){
                changeHealth(-3);
            }
            else if(touchingObject && touchingObject.tag == "heal"){
                changeHealth(6);
                //playerStatusManager.AddEffect(atkUp);
            }
            dmgInterval = 0.25f;
        }
    }
    
    void OnCollisionEnter(Collision other){
        touchingObject = other.gameObject;
    }

        void OnCollisionExit(Collision other){
        touchingObject = null;
    }
}
