using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
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
        public PlayerMoveScript(KeysPressed Keys, ref ArrayList Effects, Rigidbody rb, Transform tra, bool lGrounded, bool rGrounded)
        {
            this.Keys = Keys;
            this.Effects = Effects;
            this.rb = rb;
            this.tra = tra;
            this.lGrounded = lGrounded;
            this.rGrounded = rGrounded;
        }

        public Vector3 GetVelocity()
        {
            return rb.velocity;
        }   

        public void RemoveEffect(ActiveEffects effect)
        {
            if (Effects.Contains(effect))
                Effects.Remove(effect);
        }

        //                if (vec.x <= minVec.x && vec.y <= minVec.y && vec.z <= minVec.z)
        //            vec = new Vector3(0,0,0);   for like min velocity

        public Vector3 UpdateVelocity()
        {
            float speed = rb.velocity.magnitude;
            Grounded = lGrounded & rGrounded;
            Vector3 vec = rb.velocity;//current speed
            if (Effects.Contains(ActiveEffects.ForwardHeld) && Input.GetKeyDown(KeyCode.W))
            {
                return (tra.forward)*(-5*Mathf.Log(rb.velocity.magnitude +1)+30)*8000;
            }
            if (Effects.Contains(ActiveEffects.BackHeld) && Input.GetKeyDown(KeyCode.S))//return dashes if case
            {
                return (tra.forward)*(-5*Mathf.Log(rb.velocity.magnitude +1)+30)*-8000;
            }
            if (Effects.Contains(ActiveEffects.LeftHeld) && Input.GetKeyDown(KeyCode.A))
            {
                return (tra.right)*(-5*Mathf.Log(rb.velocity.magnitude +1)+30)*-8000;
            }
            if (Effects.Contains(ActiveEffects.RightHeld) && Input.GetKeyDown(KeyCode.D))
            {
                return (tra.right)*(-5*Mathf.Log(rb.velocity.magnitude +1)+30)*8000;
            }
            if (Grounded && !Keys.SH)
            {
                //\frac{\left(\log\left(x\right)+10\right)}{-0.95x}

                if (Keys.W)
                {
                    vec = (tra.forward)*(Mathf.Log(speed)+10)/((-0.95f*speed)+0.001f);
                    if (!Effects.Contains(ActiveEffects.ForwardHeld))
                    {
                        Effects.Add(ActiveEffects.ForwardHeld);
                    }
                } else if (Input.GetKeyUp(KeyCode.W))
                {
                    Task.Delay(100).ContinueWith(t=> Effects.Remove(ActiveEffects.ForwardHeld));//set to walking
                }
                if (Keys.S)
                {
                    vec = -(tra.forward)*(Mathf.Log(speed)+10)/(-0.95f*speed);
                    if (!Effects.Contains(ActiveEffects.BackHeld))
                    {
                        Effects.Add(ActiveEffects.BackHeld);
                    }
                } else if (Input.GetKeyUp(KeyCode.S))
                {
                    Task.Delay(100).ContinueWith(t=> Effects.Remove(ActiveEffects.BackHeld));
                }
                if (Keys.A)
                {
                    vec += -(tra.right)*(Mathf.Log(speed)+10)/(-0.95f*speed);
                    if (!Effects.Contains(ActiveEffects.LeftHeld))
                    {
                        Effects.Add(ActiveEffects.LeftHeld);
                    }
                } else if (Input.GetKeyUp(KeyCode.A))
                {
                    Task.Delay(100).ContinueWith(t=> Effects.Remove(ActiveEffects.LeftHeld));
                }
                if (Keys.D)
                {
                    vec += (tra.right)*(Mathf.Log(speed)+10)/(-0.95f*speed);
                    if (!Effects.Contains(ActiveEffects.RightHeld))
                    {
                        Effects.Add(ActiveEffects.RightHeld);
                    }
                } else if (Input.GetKeyUp(KeyCode.D))
                {
                    Task.Delay(100).ContinueWith(t=> Effects.Remove(ActiveEffects.RightHeld));
                }
            } else if (!Keys.SH)
            {
                if (Keys.W)
                    vec += (tra.forward * 0.5f*40);//remove
                if (Keys.S)
                    vec +=(tra.forward * -0.5f*40);
                if (Keys.A)
                    vec +=(tra.right * -0.5f*40);
                if (Keys.D)
                    vec +=(tra.right * 0.5f*40);
            }
            
            if (Keys.SP && Grounded)
            {
                vec.y = Mathf.Max(8f*GetVelocity().magnitude*40);//or if get velocity is less than a certain ammount, just apply a set ammount //jump sets y
            }
            if ((lGrounded || rGrounded) && !Grounded)
            {
                if (!Keys.SH)
                {
                    if (Keys.W)
                    {
                        vec += tra.forward * 15f*40;
                    }
                    if (Keys.S)
                    {
                        vec += tra.forward * -15f*40;
                    }
                    if (Keys.A)
                    {
                        vec += tra.right * -15f*40;
                    }
                    if (Keys.D)
                    {
                        vec += tra.right * 15f*40;
                    }
                    if (Keys.SP)
                    {
                    vec += tra.up * 15f*40;
                    }
                }

                
            }
            float friction;
            if (Keys.SH || !Grounded)
            {
                friction = 1f;//friction will affect it all
            } else 
            {
                friction = -0.95f;
            }
            vec *= friction;
            return vec;//it gets set

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