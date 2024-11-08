using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerInputManagerBeta : MonoBehaviour
{
    public KeysPressed keys = new KeysPressed();

    void Update()
    {
        UpdateKeys();
    }

    void UpdateKeys()
    {
        keys.W = Input.GetKey(KeyCode.W);
        keys.S = Input.GetKey(KeyCode.S);
        keys.A = Input.GetKey(KeyCode.A); 
        keys.D = Input.GetKey(KeyCode.D); 
        keys.Q = Input.GetKey(KeyCode.Q); 
        keys.E = Input.GetKey(KeyCode.E); 
        keys.SH = Input.GetKey(KeyCode.LeftShift);
        keys.SP = Input.GetKey(KeyCode.Space);
        keys.ML = Input.GetMouseButtonDown(0); 
        keys.MR = Input.GetMouseButtonDown(1);
    }

}


public record KeysPressed
{
    public bool W;
    public bool S;
    public bool A;
    public bool D;
    public bool Q;//Movement Ability
    public bool E;//Attack Ability
    public bool SH;//shift
    public bool SP;//space
    public bool ML;//Mouse Down (Left)
    public bool MR;//Mouse Down (Right)
    public KeysPressed()
    {
        this.W = false;
        this.S = false;
        this.A = false;
        this.D = false;
        this.Q = false;
        this.E = false;
        this.SH = false;
        this.SP = false;
        this.ML = false;
        this.MR = false;
    }
}