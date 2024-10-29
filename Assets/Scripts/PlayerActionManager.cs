/*

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts.CharacterControl
{
public class PlayerActionManager : MonoBehaviour
{
    public PlayerInputManager input;

    float moveAbilityCd = 0;
    float attackAbilityCd = 0;
    float basicAttackCd = 0;
    void Start()
    {
        input = GameObject.Find("Player").GetComponent<PlayerInputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCooldowns();
        UpdateAbilityStart();
    }

    void UpdateCooldowns()
    {
        moveAbilityCd = Mathf.Max(0, moveAbilityCd - Time.deltaTime);
        attackAbilityCd = Mathf.Max(0, attackAbilityCd - Time.deltaTime);
        basicAttackCd = Mathf.Max(0, basicAttackCd - Time.deltaTime);
    }
    
    void UpdateAbilityStart()
    {
        if(moveAbilityCd == 0 && input.keys.Q)
        {

        }
        if(attackAbilityCd == 0 && input.keys.Q)
        {

        }
        if(basicAttackCd == 0 && input.keys.ML)
        {

        }
    }

    /*
    MAPPING OUT THE ACTION MANAGER

    Variables
        Sword GameObject

    Constuctor
        Inputs
            Keys
            Current Ability Cooldown Status
            What Abilities the Player Chose
            Current Portion of Attack//could maybe be gotten by this script

    Run Attacks
        Uses
            Keys
            Current Velocity
            Currnet Portion of attack
        
        Check if in attack
            Simply check based on input
        Find Direction and Magnitude of movement
            Construct the Movement Class and call get velocity
        Determine Direction of stab/swing from that
            Not Entirely sure how we do this, prob is going to unfrtunately use trig
        Apply Swing //probably will always have asword that the character is holding, and applying the swing would just have the player move the sword, not making anything else
            Just Tell something to the sword //Should the sword have it's own script for movement? Or should we just physically move it from here? either way it needs one for dealing damage

        Returns
            Null? nothing to return


    Run Abilities
        Uses 
            Keys
            Current Ability Cooldown Status
            What Abilties the Player Chose

        Determine which abilities are being run 
        Via Keys and Chosen Abilities
        Apply their effect
            If Movement Ability
                Either Instantly Apply the Effect???
                Or we could try and put it in the MoveScript
            If Attack Ability
                Just make a bool true in the sword's scripts
        


        Returns
            Null Again? maybe what abilities were used so that Input can keep track of cooldown



    */
//}
//}