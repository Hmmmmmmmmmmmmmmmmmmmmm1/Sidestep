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
        public Vector3 minVec = new Vector3(0.2f, 0.2f, 0.2f);
        public bool Grounded;
        (bool, RaycastHit) GroundChecker;
        public GameObject ClassObject;
        private Classism classism;
        public float Class;
        public bool lHit;
        public bool rHit;
        public ArrayList Dash;
        public ArrayList KeyUp;



        public void CheckClass()
        {
            classism = ClassObject.GetComponent<Classism>();
            if (classism.tank == true || classism.wizard == true)
            {
                Class = 1.5f;
                //Debug.Log("tank or wizard");
            }
            else if (classism.assassin == true)
            {
                Class = 0.75f;
                //Debug.Log("Assassin");
            }
            else if (classism.fighter == true)
            {
                Class = 1f;
                //Debug.Log("hopefully fighter");
            }


        }

        public PlayerMoveScript(KeysPressed Keys, ref ArrayList Effects, Rigidbody rb, Transform tra, (bool, RaycastHit) GroundChecker, GameObject ClassObject, bool lHit, bool rHit, ArrayList Dash, ArrayList KeyUp)
        {
            this.Keys = Keys;
            this.Effects = Effects;
            this.rb = rb;
            this.tra = tra;
            this.GroundChecker = GroundChecker;
            this.ClassObject = ClassObject;
            this.lHit = lHit;
            this.rHit = rHit;
            this.Dash = Dash;
            this.KeyUp = KeyUp;
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

        public void GroundedRotation()
        {
            if (Grounded)
            {
                //tra.localEulerAngles = new Vector3(GroundChecker.Item2.normal.x * 90,tra.localEulerAngles.y ,GroundChecker.Item2.normal.z * 90);//relative the y part
                /*
                ang = normal exept around axis of normal

                normal exept greatest in normal - x and z swapped
                if normal.x > all
                    x = 0
                if normal.y> all
                    y=0
                if normal.z>all
                    z=0



                

                //(abs(normal) - max(abs(normal)))  ------ nope
                
                X ======   normal z *90   *x    ||| Y========  normal y   *90   *y  ||||      Z=========== normal  *90   *z
                */
                tra.rotation = Quaternion.Euler(
                    /*
                (Mathf.Abs(GroundChecker.Item2.normal.z) > GroundChecker.Item2.normal.y && Mathf.Abs(GroundChecker.Item2.normal.z) > GroundChecker.Item2.normal.x) ? 
                            new Vector3(
                            tra.localEulerAngles.x,
                            (GroundChecker.Item2.normal.y)+90,
                            (GroundChecker.Item2.normal.z * -90)+90
                            )
                            :*/
                            new Vector3(
                            (Mathf.Abs(GroundChecker.Item2.normal.x) > GroundChecker.Item2.normal.z && Mathf.Abs(GroundChecker.Item2.normal.x) > GroundChecker.Item2.normal.y) ? tra.localEulerAngles.x : GroundChecker.Item2.normal.z * 90,
                            (Mathf.Abs(GroundChecker.Item2.normal.y) > GroundChecker.Item2.normal.z && Mathf.Abs(GroundChecker.Item2.normal.y) > GroundChecker.Item2.normal.x) ? tra.localEulerAngles.y : GroundChecker.Item2.normal.y,
                            (Mathf.Abs(GroundChecker.Item2.normal.z) > GroundChecker.Item2.normal.y && Mathf.Abs(GroundChecker.Item2.normal.z) > GroundChecker.Item2.normal.x) ? tra.localEulerAngles.z : GroundChecker.Item2.normal.x * -90
                            )
                            
                );
                Debug.Log("Normal - Y: "+GroundChecker.Item2.normal.y+", Abs of Normal - Y: "+Mathf.Abs(GroundChecker.Item2.normal.y)+", Normal - Z: "+GroundChecker.Item2.normal.z+", Abs of Normal - Z: "+Mathf.Abs(GroundChecker.Item2.normal.z)+", Normal - X: "+GroundChecker.Item2.normal.x+", Abs of Normal - X: "+Mathf.Abs(GroundChecker.Item2.normal.x));
                Debug.Log("Normal line is " + GroundChecker.Item2.normal); 
                rb.angularVelocity = Vector3.zero;
            }
            
        }

        //                if (vec.x <= minVec.x && vec.y <= minVec.y && vec.z <= minVec.z)
        //            vec = new Vector3(0,0,0);   for like min velocity

        public Vector3 UpdateVelocity()
        {
            Grounded = GroundChecker.Item1;

            Vector3 vec = Vector3.zero;
            if (Effects.Contains(ActiveEffects.ForwardHeld) && Dash.Contains(DashKey.UpPush))
            {
                rb.drag = 0.1f * Class;
                return (tra.forward) * ((-5 * Mathf.Log(rb.velocity.magnitude + 1) + 20) * 2000);
            }
            if (Effects.Contains(ActiveEffects.BackHeld) && Dash.Contains(DashKey.DownPush))
            {
                rb.drag = 0.1f * Class;
                return (tra.forward) * ((-5 * Mathf.Log(rb.velocity.magnitude + 1) + 20) * -2000);
            }
            if (Effects.Contains(ActiveEffects.LeftHeld) && Dash.Contains(DashKey.LeftPush))
            {
                rb.drag = 0.1f * Class;
                return (tra.right) * ((-5 * Mathf.Log(rb.velocity.magnitude + 1) + 20) * -2000);
            }
            if (Effects.Contains(ActiveEffects.RightHeld) && Dash.Contains(DashKey.RightPush))
            {
                rb.drag = 0.1f * Class;
                return (tra.right) * ((-5 * Mathf.Log(rb.velocity.magnitude + 1) + 20) * 2000);
            }
            if (Grounded && !Keys.SH)
            {
                rb.drag = 1f * Class;
                if (Keys.W)
                {
                    vec += (tra.forward * 900f);
                    if (!Effects.Contains(ActiveEffects.ForwardHeld))
                    {
                        Effects.Add(ActiveEffects.ForwardHeld);
                    }
                }
                else if (KeyUp.Contains(KeyRelease.UpUp))
                {
                    Task.Delay(100).ContinueWith(t => Effects.Remove(ActiveEffects.ForwardHeld));
                }
                //do that to all of them
                if (Keys.S)
                {
                    vec += (tra.forward * -900f);
                    if (!Effects.Contains(ActiveEffects.BackHeld))
                    {
                        Effects.Add(ActiveEffects.BackHeld);
                    }
                }
                else if (KeyUp.Contains(KeyRelease.DownUp))
                {
                    Task.Delay(100).ContinueWith(t => Effects.Remove(ActiveEffects.BackHeld));
                }
                if (Keys.A)
                {
                    vec += (tra.right * -900f);
                    if (!Effects.Contains(ActiveEffects.LeftHeld))
                    {
                        Effects.Add(ActiveEffects.LeftHeld);
                    }
                }
                else if (KeyUp.Contains(KeyRelease.LeftUp))
                {
                    Task.Delay(100).ContinueWith(t => Effects.Remove(ActiveEffects.LeftHeld));
                }
                if (Keys.D)
                {
                    vec += (tra.right * 900f);
                    if (!Effects.Contains(ActiveEffects.RightHeld))
                    {
                        Effects.Add(ActiveEffects.RightHeld);
                    }
                }
                else if (KeyUp.Contains(KeyRelease.RightUp))
                {
                    Task.Delay(200).ContinueWith(t => Effects.Remove(ActiveEffects.RightHeld));
                }
            }
            else if (!Keys.SH)
            {
                if (Keys.W)
                    vec += (tra.forward * 0.5f * 40);
                if (Keys.S)
                    vec += (tra.forward * -0.5f * 40);
                if (Keys.A)
                    vec += (tra.right * -0.5f * 40);
                if (Keys.D)
                    vec += (tra.right * 0.5f * 40);
            }

            if (Keys.SP && Grounded)
            {
                vec += new Vector3(0, Mathf.Max(GetVelocity().magnitude * 900f, 9000), 0);//or if get velocity is less than a certain ammount, just apply a set ammount
            }
            
            /*
            if ((lGrounded || rGrounded) && !Grounded)
            {
                if (!Keys.SH)
                {
                    rb.drag = 0.75f * Class;
                    if (Keys.W)
                    {
                        vec += tra.forward * 15f * 40;
                    }
                    if (Keys.S)
                    {
                        vec += tra.forward * -15f * 40;
                    }
                    if (Keys.A)
                    {
                        vec += tra.right * -15f * 40;
                    }
                    if (Keys.D)
                    {
                        vec += tra.right * 15f * 40;
                    }
                    if (Keys.SP)
                    {
                        vec += tra.up * 15f * 40;
                    }
                }


            }
            */
            if (Grounded)
            {
                GroundedRotation();
            } else 
            {
                if (lHit)
                {
                    tra.RotateAround(tra.GetComponent<Collider>().bounds.center, tra.forward, -90);
                    rb.velocity=Vector3.zero;
                    //tra.localEulerAngles = new Vector3(tra.localEulerAngles.y, GroundChecker.Item2.normal.y, tra.localEulerAngles.z);
                    //tra.position = tra.position + (tra.up*Time.deltaTime);
                    //Debug.Break(); 
                    //Time.timeScale = 0.2f;
                    //set y to normal and x to y

                } else if (rHit)
                {
                    tra.RotateAround(tra.GetComponent<Collider>().bounds.center, tra.forward, 90);
                    rb.velocity=Vector3.zero;
                    //Debug.Break();
                } 
            }
            //if grounded
            // reset
            //else
            // wall hit 
            //rotate

            //Vector3 friction;
            if (Keys.SH || (!Grounded))
            {
                rb.drag = 0f;
            }
            //    friction = Vector3.zero;
            //} else 
            //{
            //    friction = rb.velocity*-0.95f;
            //}
            //vec += friction;
            return vec;

        }
        void OnDrawGizmos()
    {
        Gizmos.DrawLine(GroundChecker.Item2.point, GroundChecker.Item2.normal);//gizmos normal line that could be the prob
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
                (if Classes)
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
