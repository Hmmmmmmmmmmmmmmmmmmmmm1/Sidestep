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
    public GameObject atkUp;
    public PlayerStatusManager playerStatusManager;
    public Text HpNum;

    // Start is called before the first frame update
    void Start()
    {
        hpBar = GameObject.Find("PlayerHPBar");
        playerStatusManager = GameObject.Find("Player").GetComponent<PlayerStatusManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHPChange();
        hp = Mathf.Min(hp,100);
        HpNum.text = hp.ToString();
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
            if(hp >= oldHp)
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
            hpChangeBar.transform.localScale = new Vector3(0.8f,0.8f,0.8f);
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
            hpChangeBar.transform.localScale = new Vector3(0.8f,0.8f,0.8f);
        }
        hp += hpChangeAmount;
        //hpBar.GetComponent<RectTransform>().sizeDelta = new Vector2(hp * 4, 15);
        hpChangeBar.GetComponent<RectTransform>().sizeDelta = new Vector2(hp * 4, 15);
        hpChangeBar.GetComponent<Image>().color = Color.white;
        hpChangeTick = 2f;
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "damage")
        {
            DecreaseHP(10);
        }
        else if(other.gameObject.tag == "heal")
        {
            IncreaseHP(20);
            //playerStatusManager.AddEffect(atkUp);
        }
    }
}
