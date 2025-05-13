using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEditor.PackageManager;

namespace Assets.Scripts.CharacterControl

{
    public class SwordDamageScript : MonoBehaviour
    {
        public float damage;
        public float damageMultiplier;
        public GameObject ClassObject;
        private Classism classism;
        public Vector3 velocity;

        private float burnTimer;
        private float burnInterval = 0f;
        private GameObject burnTarget;

        private float speed;
        public GameObject trail;
        
        PhotonView PV;

        public void Start()
        {
            damage = .2f;
            damageMultiplier = 1f;

            PV = gameObject.transform.parent.parent.gameObject.GetComponent<PhotonView>();

            //2x+1.35
        }
        public void Update()
        {
            CheckClass();
            burnDamage();
            speedCheck();
        }
        public void CheckClass()
        {
            ClassObject = GameObject.Find("Classes");
            classism = ClassObject.GetComponent<Classism>();
            if (classism.tank == true || classism.fighter == true)
            {
                damageMultiplier = 1.25f;
                //Debug.Log("tank or fighter");
            }
        }

        public void burnDamage(){
            if (burnTimer > 0)
                {
                    if (burnInterval > 0)
                    {
                        burnInterval -= Time.deltaTime;
                    }
                    if (burnInterval <= 0)
                    {
                        burnTarget.GetComponent<PlayerHP2>().EnemyDamage(-1);
                        burnInterval = 0.35f;
                    }
                }
            burnTimer -= Time.deltaTime;
        }

        public void speedCheck(){
            if (transform.parent.parent.GetComponent<PlayerInputManager>().swung){
                Debug.Log("this is the peak of my combat");
                speed = Speedometer.currentSpeed;
                gameObject.GetComponent<BoxCollider>().center = new Vector3(0,0,(float)(0.07 * speed - 0.1));
                gameObject.GetComponent<BoxCollider>().size = new Vector3(0.207f,(float)(0.0252 * speed + 0.207),(float)(0.14 * speed + 1.15));

                trail.transform.localScale = new Vector3((float)(0.14 * speed + 1.15), 1, 1);
                trail.transform.localPosition = new Vector3(0,0,(float)(0.07 * speed - 0.1));

                //gameObject.GetComponent<TrailRenderer>().enabled = true;
            }
            else{
                gameObject.GetComponent<BoxCollider>().center = new Vector3(0,0,(float)(0.07 * 0 - 0.1));
                gameObject.GetComponent<BoxCollider>().size = new Vector3(0.207f,(float)(0.0252 * 0 + 0.207),(float)(0.14 * 0 + 1.15));

                trail.transform.localScale = new Vector3((float)(0.14 * 0 + 1.15), 1, 1);
                trail.transform.localPosition = new Vector3(0,0,(float)(0.07 * 0 - 0.1));


                //gameObject.GetComponent<TrailRenderer>().enabled = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Vector3 velocity = gameObject.GetComponent<Rigidbody>().velocity;

            PV = gameObject.transform.parent.parent.gameObject.GetComponent<PhotonView>();

            if (other.gameObject.GetComponent<PlayerHP2>() != null)
            {
                if (gameObject.transform.parent.parent.gameObject.GetComponent<Abilities>().fireActive){
                    burnTarget = other.gameObject;
                    burnTimer = 6f;
                }

                if (gameObject.transform.parent.parent.gameObject.GetComponent<Abilities>().knockActive){
                    other.GetComponent<Abilities>().knockedBack(gameObject.transform.parent.parent.gameObject.GetComponent<Rigidbody>().velocity);
                    gameObject.transform.parent.parent.gameObject.GetComponent<Abilities>().knockActive = false;
                }

                if (gameObject.transform.parent.parent.gameObject.GetComponent<Abilities>().vampActive){
                    gameObject.transform.parent.parent.gameObject.GetComponent<PlayerHP2>().EnemyDamage((int)((velocity.magnitude * damage * damageMultiplier / 2) + 1));
                }

                if (transform.parent.parent.gameObject.GetComponent<PlayerInputManager>().swung){
                    damageMultiplier += 9;
                    Debug.Log(damageMultiplier);
                }

                //PV.RPC("EnemyDamage",RpcTarget.All,30);
                other.GetComponent<PlayerHP2>().EnemyDamage(-(int)((velocity.magnitude * damage * damageMultiplier) + 1));
                //other.gameObject.GetComponent<PlayerHP2>().changeHealth(-(int)((velocity.magnitude * damage * damageMultiplier)));
            }
        }
    }
}
