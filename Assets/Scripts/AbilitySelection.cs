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
    private int Select1;
    private int Select2;
    public Text Skill1Text;
    public Text Skill2Text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void geagling(int skill){
        if (skill ==0){
            Skill1 = "Undecdided";
            Select1 = 0;
        }
        if (skill ==1){
            Skill1 = "Lynch";
            Select1 = 1;
        }
        if (skill ==2){
            Skill1 = "Duplo mode";
            Select1 = 2;
        }
        if (skill ==3){
            Skill1 = "blunk";
            Select1 = 3;
        }
        if (skill ==4){
            Skill1 = "turn right";
            Select1 = 4;
        }
        if (skill ==5){
            Skill1 = "used-bandaid";
            Select1 = 5;
        }
        Skill1Text.text = Skill1;
        Abilities.Skill1 = Select1;
    }

    public void geagled(int fortnite){
        if (fortnite ==0){
            Skill2 = "Independent";
            Select2 = 0;
        }
        if (fortnite ==1){
            Skill2 = "republiCANT";
            Select2 = 1;
        }
        if (fortnite ==2){
            Skill2 = "DUMBocrat";
            Select2 = 2;
        }
        Skill2Text.text = Skill2;
        Abilities.Skill2 = Select2;
    }

    public void sendScore(){
        SceneManager.LoadScene(3);
    }
}
