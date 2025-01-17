using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.CharacterControl

{
public class SwordDamageScript : MonoBehaviour
{
    public float damage;

    public Vector3 velocity;
    public void Start()
    {
        Vector3 velocity = gameObject.transform.parent.parent.gameObject.GetComponent<PlayerInputManager>().rb.velocity;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        
        
        if (other.gameObject.GetComponent<PlayerHP2>() != null)
        {
            Debug.Log("hit");
            other.gameObject.GetComponent<PlayerHP2>().changeHealth(-(int)((velocity.magnitude *damage)+5));
            
        }

    }
}

}
