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
    public PlayerStatusManager playerStatusManager;
    
    // Start is called before the first frame update
    void Start()
    {
        playerStatusManager = GameObject.Find("Player").GetComponent<PlayerStatusManager>();
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
            playerStatusManager.RemoveEffect(gameObject);
        }
    }
}
