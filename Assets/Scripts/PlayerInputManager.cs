using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
namespace Assets.Scripts.CharacterControl

{
    public class PlayerInputManager : MonoBehaviour
    {
        public Rigidbody rb;
        public Transform tra;
        public ArrayList Effects = new ArrayList();
        public ArrayList Dash = new ArrayList();
        public ArrayList KeyUp = new ArrayList();
        (bool, RaycastHit) GroundChecker;
        public bool Grounded;
        public bool lHit;
        public bool rHit;
        public PlayerMoveScript move;
        public PlayerAttackScript attack;
        public GameObject ClassObject;
        public bool pluh;
        public GameObject SwordHolder;
        public bool swung;
        public bool waiter;

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.W))
            {
                Dash.Add(DashKey.UpPush);
                Debug.Log("forward");
            }
            if(Input.GetKeyDown(KeyCode.A))
            {
                Dash.Add(DashKey.LeftPush);
                Debug.Log("left");
            }
            if(Input.GetKeyDown(KeyCode.S))
            {
                Dash.Add(DashKey.DownPush);
                Debug.Log("down");
            }
            if(Input.GetKeyDown(KeyCode.D))
            {
                Dash.Add(DashKey.RightPush);
                Debug.Log("right");
            }
            if(Input.GetKeyUp(KeyCode.W))
            {
                KeyUp.Add(KeyRelease.UpUp);
                Debug.Log("up release");
            }
            if(Input.GetKeyUp(KeyCode.A))
            {
                KeyUp.Add(KeyRelease.LeftUp);
                Debug.Log("left release");
            }
            if(Input.GetKeyUp(KeyCode.S))
            {
                KeyUp.Add(KeyRelease.DownUp);
                Debug.Log("down release");
            }
            if(Input.GetKeyUp(KeyCode.D))
            {
                KeyUp.Add(KeyRelease.RightUp);
                Debug.Log("right release");
            }
        }

        void FixedUpdate()
        {
            ClassObject = GameObject.Find("Classes");
            pluh = Effects.Contains(ActiveEffects.ForwardHeld);
            if (GetComponent<PhotonView>().IsMine == true)
            {
                GroundCheckerScript GroundCheckerOb = tra.Find("GroundChecker").gameObject.GetComponent<GroundCheckerScript>();
                GroundChecker.Item1 = GroundCheckerOb.Grounded;
                Grounded =GroundChecker.Item1;
                GroundChecker.Item2 = GroundCheckerOb.HitData;
                lHit = tra.Find("WallCheckers/LeftWallChecker").gameObject.GetComponent<LeftWallCheckerScript>().lHit;
                rHit = tra.Find("WallCheckers/RightWallChecker").gameObject.GetComponent<RightWallCheckerScript>().rHit;
                KeysPressed keys =
                    new KeysPressed(
                        Input.GetKey(KeyCode.W),
                        Input.GetKey(KeyCode.S),
                        Input.GetKey(KeyCode.A),
                        Input.GetKey(KeyCode.D),
                        Input.GetKey(KeyCode.Q),
                        Input.GetKey(KeyCode.E),
                        Input.GetKey(KeyCode.LeftShift),
                        Input.GetKey(KeyCode.Space),
                        Input.GetMouseButton(0),
                        Input.GetMouseButton(1));
                PlayerMoveScript move = new PlayerMoveScript(keys, ref Effects, rb, tra, GroundChecker, ClassObject, lHit, rHit, Dash, KeyUp);

                PlayerAttackScript attack = new PlayerAttackScript(keys, SwordHolder.transform/*, move, SwordHolder.transform*/, swung, this);
                swung = attack.Begin();
                move.CheckClass();
                rb.AddForce(move.UpdateVelocity() * Time.deltaTime);
                for (int i = 0; i < Dash.Count; i++)
                {
                    //Debug.Log(Dash[i]);
                    Dash[i] = null;
                }
                for (int i = 0; i < KeyUp.Count; i++)
                {
                    KeyUp[i] = null;
                }
            }
        }

        public void AnActualWaitClass(string method, float time)
        {
            Invoke(method, time);
        }

        public void SetFalse()
        {
            swung = false;
            Debug.Log("off");
            attack.Swing();
        }
    }



    public record KeysPressed
    {
        public bool W;
        public bool S;
        public bool A;
        public bool D;
        public bool Q;//Movement Ability
        public bool E;//Attack Ability
        public bool SH;//shift
        public bool SP;//space
        public bool ML;//Mouse Down (Left)
        public bool MR;//Mouse Down (Right)
        public KeysPressed(bool W, bool S, bool A, bool D, bool Q, bool E, bool SH, bool SP, bool ML, bool MR)
        {
            this.W = W;
            this.S = S;
            this.A = A;
            this.D = D;
            this.Q = Q;
            this.E = E;
            this.SH = SH;
            this.SP = SP;
            this.ML = ML;
            this.MR = MR;
        }
    }

    public enum ActiveEffects
    {
        Lifesteal,
        SpeedBoost,
        ForwardHeld,
        BackHeld,
        LeftHeld,
        RightHeld,
    }
    public enum DashKey
    {
        DownPush,
        LeftPush,
        RightPush,
        UpPush,

    }
    public enum KeyRelease
    {
        UpUp,
        DownUp,
        RightUp,
        LeftUp,
    }
    /*

    MAPING OUT THE INPUT MANAGEMENT
    Variables
        RigidBody
        Transform
        Keep Track of Active Affects



    Update
        Take Inputs
            Keys
                Via Keys Class
                we can copy from crimeline
        Call MovementScript
            Just input the things in the constructor
            Run get Update Velocity
            mybe we apply it here maybe we apply it 
        Call ActionScript (I think ActionScript will handle attacks and Abilities)
            Run the inputs
            //going to need to update this after I map out Action Script
        Update Active effects


    */

}