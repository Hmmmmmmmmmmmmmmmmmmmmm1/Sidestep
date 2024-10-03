using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    GameObject hpBar;
    GameObject hpChangeBar = null;
    public int hp = 100;
    private int oldHp = 0;
    public float hpChangeTick = 0;

    // Start is called before the first frame update
    void Start()
    {
        hpBar = GameObject.Find("PlayerHPBar");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHPChange();
    }

    void UpdateHPChange()
    {
        if(hpChangeTick <= 0)
        {
            hpChangeTick = 0;
            Destroy(hpChangeBar);
            hpChangeBar = null;
            oldHp = 0;
            
            return;
        }
        else if(hpChangeTick > 0 && hpChangeTick <= 1)
        {
            hpChangeTick -= Time.deltaTime;
            if(hp > oldHp)
            {
                hpBar.GetComponent<RectTransform>().sizeDelta = new Vector2((hp * 4) - ((hp * 4) - (oldHp * 4)) * hpChangeTick, 15);
            }
            else if(hp < oldHp)
            {
                hpChangeBar.GetComponent<RectTransform>().sizeDelta = new Vector2((oldHp * 4) - ((oldHp * 4) - (hp * 4)) * (1 - hpChangeTick), 15);
            }
        }
        else if(hpChangeTick > 1)
        {
            hpChangeTick -= Time.deltaTime;
        }
    }

    public void DecreaseHP(int hpChangeAmount)
    {
        if(hpChangeTick == 0)
        {
            oldHp = hp;
            hpChangeBar = Instantiate(hpBar, hpBar.transform.position, hpBar.transform.rotation, GameObject.Find("UI Frame").transform);
        }
        hp -= hpChangeAmount;
        hpBar.GetComponent<RectTransform>().sizeDelta = new Vector2(hp * 4, 15);
        hpChangeBar.GetComponent<Image>().color = Color.red;
        hpChangeTick = 2f;
    }

    public void IncreaseHP(int hpChangeAmount)
    {
        if(hpChangeTick == 0)
        {
            oldHp = hp;
            hpChangeBar = Instantiate(hpBar, hpBar.transform.position, hpBar.transform.rotation, GameObject.Find("UI Frame").transform);
        }
        hp += hpChangeAmount;
        hpChangeBar.GetComponent<RectTransform>().sizeDelta = new Vector2(hp * 4, 15);
        hpChangeBar.GetComponent<Image>().color = Color.white;
        hpChangeTick = 2f;
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("col");
        if(other.gameObject.tag == "damage")
        {
            DecreaseHP(20);
        }
        else if(other.gameObject.tag == "heal")
        {
            IncreaseHP(20);
        }
    }
}
