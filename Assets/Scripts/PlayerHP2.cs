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
    public float speedMultiplier;

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
        if (dmgInterval > 0)
        {
            dmgInterval -= Time.deltaTime;
        }
        if (dmgInterval <= 0)
        {
            if (touchingObject && touchingObject.tag == "damage")
            {
                changeHealth(-3);
                this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(this.gameObject.GetComponent<Rigidbody>().velocity.x * -1, this.gameObject.GetComponent<Rigidbody>().velocity.y * -11/10, this.gameObject.GetComponent<Rigidbody>().velocity.z * -1);
            }
            else if (touchingObject && touchingObject.tag == "heal")
            {
                changeHealth(6);
                //playerStatusManager.AddEffect(atkUp);
            }
            dmgInterval = 0.25f;
        }
    }
    
    void OnTriggerEnter(Collider other){
        touchingObject = other.gameObject;
    }

        void OnTriggerExit(Collider other){
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
