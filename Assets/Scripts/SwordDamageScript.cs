using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.CharacterControl

{
    public class SwordDamageScript : MonoBehaviour
    {
        public float damage;
        public GameObject ClassObject;
        private Classism classism;
        public Vector3 velocity;
        public void Start()
        {
            damage = .2f;

        }
        public void CheckClass()
        {
            classism = ClassObject.GetComponent<Classism>();
            if (classism.tank == true || classism.wizard == true)
            {
                Class = 1.5f;
                Debug.Log("tank or wizard");
            }
            if (classism.assassin == true)
            {
                Class = 0.75f;
                Debug.Log("Assassin");
            }
            else
            {
                Class = 1f;
                Debug.Log("else hopefully fighter");
            }

        }


        private void OnTriggerEnter(Collider other)
        {
            Vector3 velocity = gameObject.transform.parent.parent.gameObject.GetComponent<PlayerInputManager>().rb.velocity;

            if (other.gameObject.GetComponent<PlayerHP2>() != null)
            {
                Debug.Log(velocity * damage);
                other.gameObject.GetComponent<PlayerHP2>().changeHealth(-(int)((velocity.magnitude * damage)));

            }

        }
    }

}
