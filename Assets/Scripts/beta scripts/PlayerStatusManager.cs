using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStatusManager : MonoBehaviour
{

    public ArrayList effects = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddEffect(GameObject newEffect)
    {
        GameObject effect = Instantiate(newEffect, GameObject.Find("Canvas").transform);
        effects.Add(effect);
        effect.GetComponent<RectTransform>().position = new Vector3(1316 - effects.Count * 108, 720 -36 * (effects.Count % 2) - 72, 0);
    }

    public void RemoveEffect(GameObject effectToRemove)
    {
        effects.Remove(effectToRemove);
        effects.TrimToSize();

        int i = 0;
        foreach(GameObject effect in effects)
        {
            effect.GetComponent<RectTransform>().position = new Vector3(1316 - (i + 1) * 108, 720 -36 * ((i + 1) % 2) - 72, 0);
            i++;
        }
    }
}
