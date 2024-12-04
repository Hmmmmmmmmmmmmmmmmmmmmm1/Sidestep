using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AbilitySelection : MonoBehaviour
{
    private String Skill1;
    private String Skill2;
    public Text greg;
    public Text rodrick;
    public String frank;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void geagling(int fortnite){
        if (fortnite ==0){
            Skill1 = "Undecdided";
        }
        if (fortnite ==1){
            Skill1 = "Lynch";
        }
        if (fortnite ==2){
            Skill1 = "Duplo mode";
        }
        if (fortnite ==3){
            Skill1 = "stop";
        }
        if (fortnite ==4){
            Skill1 = "turn right";
        }
        if (fortnite ==5){
            Skill1 = "used-bandaid";
        }
        greg.text = Skill1;
        Abilities.Skill1 = Skill1;
    }

    public void geagled(int fortnite){
        if (fortnite ==0){
            Skill2 = "Independent";
        }
        if (fortnite ==1){
            Skill2 = "republiCANT";
        }
        if (fortnite ==2){
            Skill2 = "DUMBocrat";
        }
        rodrick.text = Skill2;
        Abilities.Skill2 = Skill2;
    }

    public void sendScore(){
        SceneManager.LoadScene(3);
    }
}
