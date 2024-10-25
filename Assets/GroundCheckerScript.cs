using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckerScript : MonoBehaviour
{
    public bool lGrounded;
    public bool rGrounded;
    void Update()
    {
        lGrounded = false;
        rGrounded = false;
    }
}
