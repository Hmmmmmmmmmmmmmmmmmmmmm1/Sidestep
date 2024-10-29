using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts.CharacterControl
{
    public class PlayerMoveScript
    {
        public KeysPressed Keys;
        public Rigidbody rb;
        public Transform tra;
        public ArrayList Effects;
        public Vector3 minVec = new Vector3(0.2f,0.2f,0.2f);
        public bool Grounded;
        public bool lGrounded;
        public bool rGrounded;
        public PlayerMoveScript(KeysPressed Keys, ArrayList Effects, Rigidbody rb, Transform tra, bool lGrounded, bool rGrounded)
        {
            this.Keys = Keys;
            this.Effects = Effects;
            this.rb = rb;
            this.tra = tra;
            this.lGrounded = lGrounded;
            this.rGrounded = rGrounded;
        }

        public void Awake()
        {
            Grounded = lGrounded & rGrounded;
        }

        public Vector3 GetVelocity()
        {
            return rb.velocity;
        }   

        //                if (vec.x <= minVec.x && vec.y <= minVec.y && vec.z <= minVec.z)
        //            vec = new Vector3(0,0,0);   for like min velocity

        public Vector3 UpdateVelocity()
        {
            Vector3 vec = Vector3.zero;
            Vector3 friction = GetVelocity()*-0.75f;
            if (Keys.SH || !Grounded)
            {
                friction *= 0.05f;
            }
            if (Grounded && !Keys.SH)
            {
                

                if (Keys.W)
                {
                    vec += (tra.forward * 3000f);
                    //Acces the ArrayList??
                }
                    //do that to all of them
                if (Keys.S)
                    vec += (tra.forward * -3000f);
                if (Keys.A)
                    vec += (tra.right * -3000f);
                if (Keys.D)
                    vec += (tra.right * 3000f);
            } else if (!Keys.SH)
            {
                if (Keys.W)
                    vec += (tra.forward * 0.5f);
                if (Keys.S)
                    vec +=(tra.forward * -0.5f);
                if (Keys.A)
                    vec +=(tra.right * -0.5f);
                if (Keys.D)
                    vec +=(tra.right * 0.5f);
            }
            if (Effects.Contains(ActiveEffects.ForwardHeld) && Input.GetKeyDown("W"))
            {
                vec.x += -5*Mathf.Log(GetVelocity().x +1)+30;
            }
            if (Effects.Contains(ActiveEffects.BackHeld) && Input.GetKeyDown("S"))
            {
                vec.x += -5*Mathf.Log(GetVelocity().x +1)+30;
            }
            if (Effects.Contains(ActiveEffects.LeftHeld) && Input.GetKeyDown("A"))
            {
                vec.x += -5*Mathf.Log(GetVelocity().x +1)+30;
            }
            if (Effects.Contains(ActiveEffects.RightHeld) && Input.GetKeyDown("D"))
            {
                vec.x += -5*Mathf.Log(GetVelocity().x +1)+30;
            }
            if (Keys.SP && Grounded)
            {
                vec += new Vector3(0,400f*GetVelocity().x,0);//or if get velcoity is less than a certain ammount, just apply a set ammount
            }
            if ((lGrounded || rGrounded) && !Grounded)
            {
                friction *= 0.75f;
                if (!Keys.SH)
                {
                    if (Keys.W)
                    {
                        vec += tra.forward * 1500f;
                    }
                    if (Keys.S)
                    {
                        vec += tra.forward * -1500f;
                    }
                    if (Keys.A)
                    {
                        vec += tra.right * -1500f;
                    }
                    if (Keys.D)
                    {
                        vec += tra.right * 1500f;
                    }
                    if (Keys.SP)
                    {
                    vec += tra.up * 1500f;
                    }
                }

                
            }
            vec += friction;
            return vec;

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
                            we would probably use a game object, and just pull closer to the origional point, so the game object would handle the swinging 
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
    }
}