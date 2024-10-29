using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovementManagerBeta : MonoBehaviour
{
    // Refrences
    Rigidbody rb;
    Transform tra;
    PlayerStatusManager StatusManager;
    PlayerInputManagerBeta InputManager;

    bool Grounded = true;
    bool lGrounded = true;
    bool rGrounded = true;

    void Start()
    {
        StatusManager = GetComponent<PlayerStatusManager>();
        InputManager = GetComponent<PlayerInputManagerBeta>();
        rb = GetComponent<Rigidbody>();
        tra = GetComponent<Transform>();
    }

    public void Awake()
    {
        Grounded = lGrounded & rGrounded;
    }

    public void Update()
    {
        UpdateVelocity();
    }

    public void UpdateVelocity()
    {
        Vector3 vec = Vector3.zero;
        Vector3 friction = GetVelocity()*-0.1f;
        KeysPressed Keys = InputManager.keys;
        ArrayList Effects = StatusManager.effects;

        if (Keys.SH || !Grounded)
        {
            friction *= 0.05f;
        }
        if (Grounded && !Keys.SH)
        {
            

            if (Keys.W)
            {
                vec += (tra.forward * 3000f);
                //Acces the ArrayList??
            }
                //do that to all of them
            if (Keys.S)
                vec += (tra.forward * -3000f);
            if (Keys.A)
                vec += (tra.right * -3000f);
            if (Keys.D)
                vec += (tra.right * 3000f);
        } else if (!Keys.SH)
        {
            if (Keys.W)
                vec += (tra.forward * 0.5f);
            if (Keys.S)
                vec +=(tra.forward * -0.5f);
            if (Keys.A)
                vec +=(tra.right * -0.5f);
            if (Keys.D)
                vec +=(tra.right * 0.5f);
        }
        if (Keys.SP && Grounded)
        {
            vec += new Vector3(0,0.4f*GetVelocity().x,0);//or if get velcoity is less than a certain ammount, just apply a set ammount
        }
        if ((lGrounded || rGrounded) && !Grounded)
        {
            friction *= 0.75f;
            if (!Keys.SH)
            {
                if (Keys.W)
                {
                    vec += tra.forward * 1500f;
                }
                if (Keys.S)
                {
                    vec += tra.forward * -1500f;
                }
                if (Keys.A)
                {
                    vec += tra.right * -1500f;
                }
                if (Keys.D)
                {
                    vec += tra.up * -1500f;
                }
                if (Keys.SP)
                {
                vec += tra.up * 1500f;
                }
            }

            
        }
        vec += friction;
        rb.AddForce(vec * Time.deltaTime);

    }

    public Vector3 GetVelocity()
    {
        return rb.velocity;
    }

    public float GetVelocityFoward()
    {
        Vector3 velFoward = Vector3.Scale(transform.forward, rb.velocity);
        return velFoward.x + velFoward.y + velFoward.z;
    }
}