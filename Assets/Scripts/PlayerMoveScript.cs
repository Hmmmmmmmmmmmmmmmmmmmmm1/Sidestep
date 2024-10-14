/*

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    public KeysPressed Keys;
    public Rigidbody rb;
    public Transform trans;

    public PlayerMoveScript(KeysPressed Keys )
    {
        this.Keys = Keys;
    }


    // Update is called once per frame
    void Update()
    {
        Movement();
    }



    public void Movement()
    {
        if (Input.GetKey("w"))
        {
            rb.AddForce(Vector3.Min(trans.forward * 2, trans.forward * 20 - rb.velocity));
        } 
        if (Input.GetKey("s"))
        {
            rb.AddForce(Vector3.Max(trans.forward * -2, trans.forward * -20 - rb.velocity));
        } 
        if (Input.GetKey("a"))
        {
            rb.AddForce(Vector3.Max(trans.right * -2, trans.right * -20 - rb.velocity));
        } 
        if (Input.GetKey("d"))
        {
            rb.AddForce(Vector3.Min(trans.right * 2, trans.right * 20 - rb.velocity));
        }
    }

    /*
    THE ACTUAL MOVE SCRIPT THAT WILL BE HERE IS NOT THAT
    I AM MAPPING IT OUT BELOW

    Variable to Get
    Current Movement //normal unity friction should be removed as we will determine it
    Touching Ground

    Constructor
        Input 
            Active Effects
            Keys
            Player Info(RigidBody, Transform, etc)
            (if classes)
                Class Movement Modifiers

    Get Velocity
        Checks Current Player's Velocity and returns it //Need this for Action Manager





    Update Velocity
        //Should be run every update
        Uses 
            Keys
            Inputs
        
        How
            Input Movement
                Check if on Ground
                    yes
                        Can run normally
                    no
                        Can adjust slightly
            Basic Movement Mechanics
                Grappleing Hook
                    Ammount of time Grappleing Hook has been out
                    Location of grappleing Hook
                    Current Velocity
                Jump
                    Basically is just normal jump if on ground
                    //we could make some stuff where going faster makes ou go higher
                Wall Running
                    Check Side that wall is on/ if there is a wall at all
                    Allows for limited movement based on speed
                    Would change left/right to up/down and forward/back to faster/slower
                Dashing
                    If input, add the extra force
                    //this one is very simple
                Sliding
                    Check if on ground
                    Reduce ammount removed by friction //also quite simple, but I feel like there could be a way to make this more complex and better but Idk
            Basic Friction
                Slow down based on speed and if air or on ground
            Abilities
                Check for any abilities that affect this players movement
                    Directional Change
                    Launch
                    //note that things like blink would not affect velocity, so would be run differently
                Aplly
                    This would be the final affects so that things like directional change can just effect the final vector




        Returns 
            //ammount to be added would be added and subtracted to throughout the method
            An ammount of force to add
            //I am pretty sure that using add force would be best here
    
    */
//}