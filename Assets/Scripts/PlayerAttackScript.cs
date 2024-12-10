using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
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
        //if swung recently
        public bool swung;
        //waiter for swung
        public bool waiter;
        private Vector3 pos;

        public PlayerAttackScript(KeysPressed Keys, /*PlayerMoveScript move, */Transform SwordHolder/*, ref bool swung, ref bool waiter*/)
        {
            this.Keys = Keys;
//            this.move = move;
            this.SwordHolder = SwordHolder;
            this.pos = SwordHolder.localPosition;
//            this.swung = swung;
//            this.waiter = waiter;
        }

        public void Begin()
        {
            
            if ((Keys.ML) && (!waiter))
            {
                Debug.Log("pluh");
                swung = true;
                waiter = true;
                Debug.Log("Pos before multiplication: " + pos);
                pos = pos * -1;
                Debug.Log("Pos after multiplication: " + pos);
                Swing();
                Task.Delay(400).ContinueWith(t=> SetFalse());
            } else if (swung)
            {
               Swing();
            } 
        }

        public void SetFalse()
        {
            waiter = false;
            swung = false;
            side *= -1;
        }

        public void Swing()
        {   
            SwordHolder.localPosition = Vector3.Lerp (SwordHolder.localPosition, pos, 0.2f);
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