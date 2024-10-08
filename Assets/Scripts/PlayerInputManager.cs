using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public Rigidbody rb;
    public Transform tra;
    //Active Effects

    void Update()
    {
        KeysPressed keys = 
                new KeysPressed(
                    Input.GetKey(KeyCode.W), 
                    Input.GetKey(KeyCode.S), 
                    Input.GetKey(KeyCode.A), 
                    Input.GetKey(KeyCode.D), 
                    Input.GetKey(KeyCode.Q), 
                    Input.GetKey(KeyCode.E), 
                    Input.GetMouseButtonDown(0), 
                    Input.GetMouseButtonDown(1));
        
        //AddForce(Movement()*Time.Delta)
    }


    private KeysPressed KeysPressedfromBoolArray(bool[] bools)
        {
            return new KeysPressed
            (
                bools[0],
                bools[1],
                bools[2],
                bools[3],
                bools[4],
                bools[5],
                bools[6],
                bools[7]
            );
        }

}


record KeysPressed
{
    public bool W;
    public bool S;
    public bool A;
    public bool D;
    public bool Q;//Movement Ability
    public bool E;//Attack Ability
    public bool ML;//Mouse Down (Left)
    public bool MR;//Mouse Down (Right)
    public KeysPressed(bool W, bool S, bool A, bool D, bool Q, bool E, bool ML, bool MR)
    {
        this.W = W;
        this.S = S;
        this.A = A;
        this.D = D;
        this.Q = Q;
        this.E = E;
        this.ML = ML;
        this.MR = MR;
    }
}
    /*

    MAPING OUT THE INPUT MANAGEMENT
    Variables
        RigidBody
        Transform
        Keep Track of Active Affects



    Update
        Take Inputs
            Keys
                Via Keys Class
                we can copy from crimeline
        Call MovementScript
            Just input the things in the constructor
            Run get Update Velocity
            mybe we apply it here maybe we apply it 
        Call ActionScript (I think ActionScript will handle attacks and Abilities)
            Run the inputs
            //going to need to update this after I map out Action Script
        Update Active effects

    
    */

