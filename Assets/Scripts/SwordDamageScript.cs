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


        private void OnTriggerEnter(Collider other)
        {
            Vector3 velocity = gameObject.transform.parent.parent.gameObject.GetComponent<PlayerInputManager>().rb.velocity;

            PV = gameObject.transform.parent.parent.gameObject.GetComponent<PhotonView>();

            if (other.gameObject.GetComponent<PlayerHP2>() != null)
            {
                if (gameObject.transform.parent.parent.gameObject.GetComponent<Abilities>().fireActive)
                {
                    other.GetComponent<PlayerHP2>().EnemyDamage(1);
                }

                //PV.RPC("EnemyDamage",RpcTarget.All,30);
                Debug.Log(velocity * damage);
                other.GetComponent<PlayerHP2>().EnemyDamage(-(int)((velocity.magnitude * damage * damageMultiplier)));
                //other.gameObject.GetComponent<PlayerHP2>().changeHealth(-(int)((velocity.magnitude * damage * damageMultiplier)));
            }
        }
    }
}
