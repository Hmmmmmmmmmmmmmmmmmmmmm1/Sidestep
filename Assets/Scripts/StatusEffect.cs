using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using Microsoft.Unity.VisualStudio.Editor;

public class StatusEffect : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string effect;
    public float duration;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateDuration();
    }

    void UpdateDuration()
    {
        duration -= Time.deltaTime;
        text.text = Mathf.Round(duration).ToString();
        if(duration <= 0)
        {
            Destroy(gameObject);
            PlayerStatusManager.RemoveEffect(gameObject);
        }
    }
}
