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
        Vector3 velocity = gameObject.transform.parent.GetComponent<PlayerInputManager>().move.GetVelocity();
        if (GetComponent<Collider>().GetComponent<PlayerHP>() != null)
        {
            GetComponent<Collider>().GetComponent<PlayerHP>().DecreaseHP((int)(velocity.magnitude *damage));
        }

    }
}

}
