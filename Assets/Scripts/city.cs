using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class city : MonoBehaviour
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
            //Debug.Log(children[i].name);

            subchildren = new GameObject[children[i].transform.childCount];

            for (int j = 0; j < subchildren.Length; j++)
            {
                subchildren[j] = children[i].transform.GetChild(j).gameObject;
                if (subchildren[j].GetComponent<MeshCollider>() == null){
                subchildren[j].AddComponent<MeshCollider>();
                }
                //Debug.Log(subchildren[j].name);

                subsubchildren = new GameObject[subchildren[j].transform.childCount];

                for (int k = 0; k < subsubchildren.Length; k++)
                {
                    subsubchildren[k] = subchildren[j].transform.GetChild(k).gameObject;
                    if (subsubchildren[k].GetComponent<MeshCollider>() == null){
                    subsubchildren[k].AddComponent<MeshCollider>();
                    }
                    //Debug.Log(subsubchildren[k].name);            
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
