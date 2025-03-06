using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class City : MonoBehaviour
{
    public GameObject[] children;
    private GameObject [] subchildren;
    private GameObject [] subsubchildren;
     // Start is called before the first frame update
    void Start()
    {
        children = new GameObject[gameObject.transform.childCount];
        for (int i = 0; i < children.Length; i++)
        {
            children[i] = gameObject.transform.GetChild(i).gameObject;
            if (children[i].GetComponent<MeshCollider>() == null){
                children[i].AddComponent<MeshCollider>();
            }
            children[i].GetComponent<MeshCollider>().convex = true;
            //Debug.Log(children[i].name);
            subchildren = new GameObject[children[i].transform.childCount];
            for (int j = 0; j < subchildren.Length; j++)
            {
                subchildren[j] = children[i].transform.GetChild(j).gameObject;
                if (subchildren[j].GetComponent<MeshCollider>() == null){
                    subchildren[j].AddComponent<MeshCollider>();
                }
                subchildren[j].GetComponent<MeshCollider>().convex = true;
                //Debug.Log(subchildren[j].name);
                subsubchildren = new GameObject[subchildren[j].transform.childCount];
                for (int k = 0; k < subsubchildren.Length; k++)
                {
                    subsubchildren[k] = subchildren[j].transform.GetChild(k).gameObject;
                    if (subsubchildren[k].GetComponent<MeshCollider>() == null){
                        subsubchildren[k].AddComponent<MeshCollider>();
                    }
                    subsubchildren[k].GetComponent<MeshCollider>().convex = true;
                    //Debug.Log(subsubchildren[k].name);            
                }
            }
        }
        GameObject.Find("Ground").GetComponent<MeshCollider>().convex = false;
        GameObject.Find("Stairway").GetComponent<MeshCollider>().convex = false;
        GameObject.Find("Rail Lights").GetComponent<MeshCollider>().convex = false;
        GameObject Blacons = GameObject.Find("Blacons");
        GameObject[] blacons = new GameObject[Blacons.transform.childCount];
        for (int i = 0; i < blacons.Length; i++)
        {
            blacons[i] = gameObject.transform.GetChild(i).gameObject;
            blacons[i].GetComponent<MeshCollider>().convex = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
}