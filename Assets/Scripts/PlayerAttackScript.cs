using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts.CharacterControl
{
    public class PlayerAttackScript : MonoBehaviour
    {
        public float side = 1f;
        //anglular velocity
        public Vector3 Avec;
        //mouse input
        public KeysPressed Keys;
        //player movement
        public PlayerMoveScript move;
        public PlayerAttackScript(Vector3 Avec, KeysPressed Keys, PlayerMoveScript move)
        {
            this.Avec = Avec;
            this.Keys = Keys;
            this.move = move;
        }

        public Vector3 CheckCurrentMovement()
        {
            Vector3 vec = move.GetVelocity().normalized;
            Vector3 sim = Vector3.zero;//dot product of normalized forward and normalized vector returns similarity
            sim.x = Vector3.Dot(vec, move.tra.right);
            sim.z = Vector3.Dot(vec, move.tra.forward);
            sim.y = Vector3.Dot(vec, move.tra.up);
            return sim;
        }

        private void SetToRestingPosition()
        {
            //multiply sim values by a constant to get values and set them
            //use class variable side to determine side
            //y value is sword hold position y
            //x value is sword hold rotation y
            //z value is sword hold position z
        }

        private void Swing()
        {
            //multiply resting position by -1, then make sword path there
            //resting position includes a 1 or -1 for which side its on
            //that variable in class
        }
    }
}