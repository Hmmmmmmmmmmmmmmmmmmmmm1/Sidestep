using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Classism : MonoBehaviour
{
    public bool tank = false;
    public bool fighter = true;
    public bool assassin = false;
    public bool wizard = false;
    
    public void Awake()
    {
        DontDestroyOnLoad(this);
    }
    public void SetClassTank()
    {
        tank = true;
        fighter = false;
        assassin = false;
        wizard = false;
    }
    public void SetClassFighter()
    {
        tank = false;
        fighter = true;
        assassin = false;
        wizard = false;
    }
    public void SetClassAssassin()
    {
        tank = false;
        fighter = false;
        assassin = true;
        wizard = false;
    }
    public void SetClassWizard()
    {
        tank = false;
        fighter = false;
        assassin = false;
        wizard = true;
    }
}
