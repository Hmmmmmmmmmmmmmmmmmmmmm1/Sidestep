using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStatusManager : MonoBehaviour
{

    public static List<GameObject> statusEffects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    static public void AddEffect(GameObject newEffect)
    {
        GameObject effect = Instantiate(newEffect, GameObject.Find("Canvas").transform);
        for(int i = 0; i <= statusEffects.Count; i++)
        {
            if(i == statusEffects.Count)
            {
                statusEffects.Add(effect);
                break;
            }
            if(statusEffects.ElementAtOrDefault(i) == null)
            {
                statusEffects.Insert(i, effect);
                break;
            }
            Debug.Log(statusEffects.ElementAtOrDefault(i) == null);
        }
        effect.GetComponent<RectTransform>().position = new Vector3(1316 - statusEffects.Count * 108, 720 -36 * (statusEffects.Count % 2) - 72, 0);
    }

    static public void RemoveEffect(GameObject effect)
    {
        Debug.Log(statusEffects.Remove(effect));
        statusEffects.TrimExcess();
        for(int i = 0; i < statusEffects.Count; i++)
        {
            statusEffects[i].GetComponent<RectTransform>().position = new Vector3(1316 - (i + 1) * 108, 720 -36 * ((i + 1) % 2) - 72, 0);
        }
    }
}
