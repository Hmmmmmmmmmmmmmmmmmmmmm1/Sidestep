using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Code.Fighting.CharacterControl
{
    public class PlayerMoveScript : MonoBehaviour
    {
        public KeysPressed Keys;
        public Rigidbody rb;
        public Transform tra;
        public ArrayList Effects;
        public Vector3 minVec = new Vector3(0.2,0.2,0.2);
        public PlayerMoveScript(KeysPressed Keys, ArrayList Effects, Rigidbody rb, Transform tra, bool Grounded)
        {
            this.Keys = Keys;
            this.Effects = Effects;
            this.rb = rb;
            this.tra = tra;
        }

        public Vector3 GetVelocity()
        {
            return rb.velocity;
        }

        //                if (vec.x <= minVec.x && vec.y <= minVec.y && vec.z <= minVec.z)
        //            vec = new Vector3(0,0,0);   for like min velocity

        public Vector3 UpdateVelocity()
        {
            Vector3 vec;
            Vector3 friction = GetVelocity()*-0.5;
            if (Keys.SH || !Grounded)
            {
                friction *= 0.05;
            }
            if (Grounded && !Keys.SH)
            {
                

                if (Keys.W)
                {
                    vec + (Vector3.forward * 3);
                    //Acces the ArrayList??
                }
                    //do that to all of them
                if (Keys.S)
                    vec += (Vector3.forward * -3);
                if (Keys.A)
                    vec += (Vector3.right * 3);
                if (Keys.D)
                    vec += (Vector3.right * -3);
            } else if (!Keys.SH)
            {
                if (Keys.W)
                    vec += (Vector3.forward * 0.5);
                if (Keys.S)
                    vec +=(Vector3.forward * -0.5);
                if (Keys.A)
                    vec +=(Vector3.right * 0.5);
                if (Keys.D)
                    vec +=(Vector3.right * -0.5);
            }
            if (ForwardHeld && GetKeyDown(W))
            {
                vec += -5*MathF.Log(GetVelocity().magnitude +1)+30;
            }
            if (BackHeld && GetKeyDown(S))
            {
                vec += -5*MathF.Log(GetVelocity().magnitude +1)+30;
            }
            if (LeftHeld && GetKeyDown(A))
            {
                vec += -5*MathF.Log(GetVelocity().magnitude +1)+30;
            }
            if (RightHeld && GetKeyDown(D))
            {
                vec += -5*MathF.Log(GetVelocity().magnitude +1)+30;
            }

        }


        // Update is called once per frame
        void Update()
        {

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