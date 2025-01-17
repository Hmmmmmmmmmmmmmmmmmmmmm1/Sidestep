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
        
        Debug.Log("hit");
        if (other.gameObject.GetComponent<PlayerHP2>() != null)
        {
            other.gameObject.GetComponent<PlayerHP2>().changeHealth(-(int)(velocity.magnitude *damage));
            
        }

    }
}

}
