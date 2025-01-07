using System;
using System.Collections;
using System.Collections.Generic;
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

    void Start(){
        changeHealth(0);
        hpNum.text = hp + "";
    }
    public void changeHealth(int change){
        hp += change;
        hp = Mathf.Min(hp,100);
        hp = Math.Max(hp,0);
        slider.value = hp;
        hpNum.text = hp + "";
    }

    void OnCollisionStay(Collision other)
    {
        if (dmgInterval > 0){
            dmgInterval -= Time.deltaTime;
        }
        if (dmgInterval <= 0){
            if(other.gameObject.tag == "damage"){
                changeHealth(-10);
            }
            else if(other.gameObject.tag == "heal"){
                changeHealth(20);
                //playerStatusManager.AddEffect(atkUp);
            }
            dmgInterval = 0.5f;
        }
    }
}
