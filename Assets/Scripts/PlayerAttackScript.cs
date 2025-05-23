using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Photon.Realtime;
using Photon.Pun;
namespace Assets.Scripts.CharacterControl
{
    public class PlayerAttackScript
    {
        public float side = 1f;
        //mouse input
        public KeysPressed Keys;
        //player movement
        public PlayerMoveScript move;
        //sword holder transform
        public Transform SwordHolder;

        //For whatever shitty things Ryan has cooking up in his head
        public Transform Sword;
        //bool for trail on/off
        public GameObject Trail;
        //if swung recently
        public bool swung;
        //waiter for swung
        public bool waiter;
        private Vector3 pos;
        private bool t;
        PlayerInputManager input;
        private static Vector3 orientation;
        private static float time;

        private PhotonView PV;
        private int guy = 120;
        private int guyNegative = 60;
        private int guyTemp;

        private static Vector2 rotations = new Vector2(60,120);

        public PlayerAttackScript(KeysPressed Keys, /*PlayerMoveScript move, */Transform SwordHolder, Transform Sword, GameObject Trail, Vector2 rotations, bool swung, PlayerInputManager input, PhotonView PV)
        {
            this.Keys = Keys;
//            this.move = move;
            this.SwordHolder = SwordHolder;
            this.Sword = Sword;
            this.Trail = Trail;
            this.pos = orientation * -1;
            this.swung = swung;
            this.waiter = waiter;
            this.input = input;
            this.PV = PV;
        }

        public bool Begin()
        {
            if ((Keys.ML) && (!swung))
            {

                swung = true;
                time = 0f;
                input.Invoke("SetFalse", .45f);
                orientation = SwordHolder.localPosition;
                rotations = new Vector2(rotations.y,rotations.x);
                
            } else if (swung)
            {
                Swing();
                SwingVisibility();
            }   
            else{
                Trail.SetActive(false);
            }
            return swung;
        }

        

        public void Swing()
        {   
            time += Time.deltaTime;
            SwordHolder.localPosition = Vector3.Lerp (orientation, pos, time/0.4f);
            Sword.localEulerAngles = Vector3.Lerp (new Vector3(0,rotations.x,0), new Vector3(0, rotations.y, 0), time / 0.4f);
            Trail.SetActive(true);
            //Debug.Log(pos);
        }


        public void SwingVisibility()
        {   
            //PV.RPC("SwingVisibilityRPC",RpcTarget.All);
        }

        [PunRPC]
        public void SwingVisibilityRPC()
        {   
            time += Time.deltaTime;
            SwordHolder.localPosition = Vector3.Lerp (orientation, pos, time/0.4f);
            //Debug.Log(pos);
        }
//        public float side = 1f;
//        //mouse input
//        public KeysPressed Keys;
//        //player movement
//        public PlayerMoveScript move;
//        //sword holder transform
//        public Transform SwordHolder;
//        //if swung recently
//        public bool swung;
//        //waiter for swung
//        public bool waiter;
//        public PlayerAttackScript(KeysPressed Keys, PlayerMoveScript move, Transform SwordHolder, ref bool swung, ref bool waiter)
//        {
//            this.Keys = Keys;
//            this.move = move;
//            this.SwordHolder = SwordHolder;
//            this.swung = swung;
//            this.waiter = waiter;
//        }
//
//        //if swung then swing, else move to resting position
//        public void Begin()
//        {
//            Debug.Log("pluh");
//            if ((Keys.ML) && (!waiter))
//            {
//                swung = true;
//                waiter = true;
//                Swing();
//                Task.Delay(400).ContinueWith(t=> SetFalse());
//            } else if (swung)
//            {
//                Swing();
//            } else 
//            {
//                Debug.Log(MoveToRestingPosition());
//                SwordHolder.transform.localPosition = MoveToRestingPosition();// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
//            }
//            
//        }
//
//        public void SetFalse()
//        {
//            waiter = false;
//            swung = false;
//            side *= -1;
//        }
//
//        public Vector3 CheckCurrentMovement()
//        {
//            Vector3 vec = move.GetVelocity().normalized;
//            Vector3 sim = Vector3.zero;//dot product of normalized forward and normalized vector returns similarity
//            sim.x = Vector3.Dot(vec, move.tra.right);
//            sim.z = Vector3.Dot(vec, move.tra.forward);
//            sim.y = Vector3.Dot(vec, move.tra.up);
//            return sim;
//        }
//
//        private Vector3 MoveToRestingPosition()
//        {
//            //multiply sim values by a constant to get values
//            Vector3 goal = CheckCurrentMovement();
//            goal.x *= 0.5f;
//            goal.x += 0.5f;
//            goal.x *= side;
//            goal.z += -0.5f;
//            goal.y += -0.5f;
//            
//            //use class variable side to determine side
//            //y value is sword hold position y
//            //x value is sword hold position y
//            //z value is sword hold position z
//
//
//            //ADD ROTATION LATER
//
//
//            //find difference between current position and goal position
//            Vector3 MoveAmmount = goal - new Vector3(SwordHolder.position.x, SwordHolder.position.y, SwordHolder.position.z);
//            //move a percentage of the way to goal position
//            return SwordHolder.transform.localPosition + (MoveAmmount*0.40f);// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -- 
//        }
//
//        private void Swing()
//        {
//            //multiply resting position by -1, then make sword path there
//            SwordHolder.transform.localPosition = MoveToRestingPosition() * -1f;// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
//            //resting position includes a 1 or -1 for which side its on
//            //make move to resting position not be called for a bit
//            //that variable in class
//
//            //lots of this happens in awake
//        }
    }
}