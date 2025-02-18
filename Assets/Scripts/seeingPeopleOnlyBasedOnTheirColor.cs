using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seeingPeopleOnlyBasedOnTheirColor : MonoBehaviour
{
    public Material[] glows;
    public static int playerCount = 1;
    public void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void newPlayerJoin(){
        playerCount++;
    }
}
