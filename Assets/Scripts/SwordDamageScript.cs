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
            CheckClass();
        }
        public void CheckClass()
        {
            ClassObject = GameObject.Find("Classes");
            classism = ClassObject.GetComponent<Classism>();
            if (classism.tank == true || classism.fighter == true)
            {
                damage = .25f;
                Debug.Log("tank or wizard");
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
