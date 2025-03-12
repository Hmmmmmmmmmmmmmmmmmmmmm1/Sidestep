using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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
        
        PhotonView PV;

        public void Start()
        {
            damage = .2f;
            damageMultiplier = 1f;

                        PV = gameObject.transform.parent.parent.gameObject.GetComponent<PhotonView>();
        }
        public void Update()
        {
            CheckClass();
            burnDamage();
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


        private void OnTriggerEnter(Collider other)
        {
            Vector3 velocity = gameObject.transform.parent.parent.gameObject.GetComponent<PlayerInputManager>().rb.velocity;

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
                    gameObject.GetComponent<PlayerHP2>().EnemyDamage((int)((velocity.magnitude * damage * damageMultiplier / 2) + 1));
                }

                //PV.RPC("EnemyDamage",RpcTarget.All,30);
                other.GetComponent<PlayerHP2>().EnemyDamage(-(int)((velocity.magnitude * damage * damageMultiplier) + 1));
                //other.gameObject.GetComponent<PlayerHP2>().changeHealth(-(int)((velocity.magnitude * damage * damageMultiplier)));
            }
        }
    }
}
