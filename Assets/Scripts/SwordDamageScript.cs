using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.CharacterControl

{
    public class SwordDamageScript : MonoBehaviour
    {
        public float damage;
        public float damageMultiplier;
        public GameObject ClassObject;
        private Classism classism;
        public Vector3 velocity;
        public void Start()
        {
            damage = .2f;
            damageMultiplier = 1f;
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
            if (other.gameObject.GetComponent<PlayerHP2>() != null)
            {
                if (gameObject.transform.parent.parent.gameObject.GetComponent<Abilities>().fireActive){
                    Debug.Log(other);
                    //other.gameObject.GetComponent<PlayerHP2>().changeHealth(-100);
                    other.gameObject.GetComponent<Abilities>().burnDmgActivate();
                }

                Debug.Log(velocity * damage);
                other.gameObject.GetComponent<PlayerHP2>().changeHealth(-(int)((velocity.magnitude * damage * damageMultiplier)));
            }

        }
    }

}
