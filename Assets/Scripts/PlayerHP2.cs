using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP2 : MonoBehaviour
{
    public Slider slider;
    public Text hpNum;
    private int progress = 100;

    void Start(){
        UpdateSlider(progress);
        hpNum.text = progress + "";
    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.Q)){
            UpdateSlider(-20);
        }
    }

    public void UpdateSlider(int chage){
        progress += chage;
        slider.value = progress;
        hpNum.text = progress + "";
    }
}
