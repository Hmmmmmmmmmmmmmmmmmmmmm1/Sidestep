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

    public Text voting;
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
            Skill2 = "no options available";
            Select2 = 0;
        }
        if (fortnite ==1){
            Skill2 = "just regular aids";
            Select2 = 1;
        }
        if (fortnite ==2){
            Skill2 = "Knockback II";
            Select2 = 2;
        }
        if (fortnite ==3){
            Skill2 = "Lifesteal";
            Select2 = 3;
        }
        if (fortnite ==4){
            Skill2 = "Immobilize";
            Select2 = 4;
        }
        if (fortnite ==5){
            Skill2 = "Like the weird flashing. Yeah you freak. Pick it. I dare you.";
            Select2 = 5;
        }
        Skill2Text.text = Skill2;
        Abilities.Skill2 = Select2;
    }

        public void geag(int fortnite){
        if (fortnite ==0){
            Skill2 = "Independent";
        }
        if (fortnite ==1){
            Skill2 = "republiCANT";
        }
        if (fortnite ==2){
            Skill2 = "DUMBocrat";
        }
        voting.text = Skill2;
    }

    public void sendScore(){
        SceneManager.LoadScene(6);
    }
}
