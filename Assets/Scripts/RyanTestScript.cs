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
        /*if(Input.GetKeyDown(KeyCode.C)){
            transform.position = GameObject.Find("Brian").transform.position;
            transform.position += GameObject.Find("Main Camera").transform.forward * 10f;
        }*/
    }

    public void setPosition(Vector3 gug){
        transform.position = gug;
        Collider[] gems = Physics.OverlapBox(gameObject.transform.position, boxSize, Quaternion.identity);
        Debug.Log(gems.Length);
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(gameObject.transform.position, boxSize * 2);
    }
}
