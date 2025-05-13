using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerHP2 : MonoBehaviour
{
    public Slider slider;
    public Text hpNum;
    public int hp = 100;

    private int oldHp = 0;
    public float hpChangeTick = 0;

    private float dmgInterval = 0;
    private GameObject touchingObject;

    PhotonView PV;

    void Start(){
        changeHealth(0);
        hpNum.text = hp + "";
        PV = gameObject.GetComponent<PhotonView>();
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

    public void EnemyDamage(int dmg){
        PV.RPC("EnemyDamageRPC",RpcTarget.All,dmg);
    }

    [PunRPC]
    void EnemyDamageRPC(int dmg)
    {
        if (!PV.IsMine){
        //gameObject.GetComponent<PlayerHP2>().changeHealth(-7);
        }
        else{
            gameObject.GetComponent<PlayerHP2>().changeHealth(dmg);
        }
        //gameObject.GetComponent<PlayerHP2>().changeHealth(-7);
    }
}
