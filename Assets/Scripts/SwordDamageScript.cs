using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.CharacterControl

{
public class SwordDamageScript : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter(Collider other)
    {
        Vector3 velocity = gameObject.transform.parent.transform.parent.GetComponent<PlayerInputManager>().move.GetVelocity();
        Debug.Log("hit");
        if (other.gameObject.GetComponent<PlayerHP2>() != null)
        {
            other.gameObject.GetComponent<PlayerHP2>().changeHealth(-(int)(velocity.magnitude *damage));
            
        }

    }
}

}
