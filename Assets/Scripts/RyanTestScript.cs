using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RyanTestScript : MonoBehaviour
{
    private Collider player;
    private Vector3 boxSize;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Brian").GetComponent<Collider>();
        boxSize = player.bounds.size / 1.75f;
    }

    // Update is called once per frame
    void Update()
    {
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
                        Input.GetMouseButtonDown(0), 
                        Input.GetMouseButtonDown(1));
        Debug.Log(keys.ML);
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

    void OnDrawGizmos(){
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(gameObject.transform.position, boxSize * 2);
    }
}
