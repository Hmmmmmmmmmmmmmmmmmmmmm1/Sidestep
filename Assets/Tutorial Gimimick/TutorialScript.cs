using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour
{
    public GameObject circle;
    public RectTransform circler;
    public Text explainer;

    public Button back;

    private int stepNo = 1;
    // Start is called before the first frame update
    void Start()
    {
        circler = circle.GetComponent<RectTransform>();
        back.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        //health
        if (stepNo == 1){
            circler.anchoredPosition = new Vector2(600,-240);
            circler.localScale = new Vector3(2,2,2);
            explainer.text = "  This is your health. When your life points reach 0, you die. And when you die you aren't able to keep playing, and you feel really sad because now everyone is having fun without you.";
        }

        //speed
        if (stepNo == 2){
            circler.anchoredPosition = new Vector2(400,-270);
            circler.localScale = new Vector3(1.5f,1.5f,1.5f);
            explainer.text = "  This is how fast you're going. The faster you go, the more damage you deal, and how much stronger your abilities are.";
        }

        //Abilities
        if (stepNo == 3){
            circler.anchoredPosition = new Vector2(600,-360);
            circler.localScale = new Vector3(2.5f,1.5f,1.5f);
            explainer.text = "  These represent your abilities. You can choose one mobility and one combat ability in the main menu. (Do you get the joke? It's like mobile-ability but I combined it into one word and mobility is a word. I am very funny. Thank you.)";
        }

        //Cooldown
        if (stepNo == 4){
            circler.anchoredPosition = new Vector2(576,-366);
            circler.localScale = new Vector3(1f,1f,1f);
            explainer.text = "  This is your movement ability cooldown. Each ability has a different cooldown. You can press 'X' to use your movement ability.";
        }

        if (stepNo == 5){
            circler.anchoredPosition = new Vector2(648,-348);
            circler.localScale = new Vector3(1f,1f,1f);
            explainer.text = "  Unfortunately, we don't have the budget for more dialogue. Replace 'movement' with 'combat' and ''X'' with ''C''. Sorry for the inconvienence! \n   This is your movement ability cooldown. Each ability has a different cooldown. You can press 'X' to use your movement ability.";
        }

        //wasd
        if (stepNo == 6){
            circle.SetActive(true);
            circler.anchoredPosition = new Vector2(-310,80);
            circler.localScale = new Vector3(11.5f,7.5f,1f);
            explainer.text = "  You move around using WASD keys. Tap a key rapidly to dash in that direction. \n    The letters are a subtle reference to the fact that I can't read.";
        }

        //grapple 
        if (stepNo == 7){
            circle.SetActive(false);
            explainer.text = "  Grapple around using Right Click. When you are grappling and in the air, you can rotate your body using WASD keys.";
        }

        //sword
        if (stepNo == 8){
            circle.SetActive(false);
            explainer.text = "  Swing your sword using Left Click. When you swing it, you do more damage, and you get a cool light trail that I spent a lot of time on. Make sure to swing your sword so I get a lot of mileage out of that.";
        }

        if (stepNo == 9){
            circle.SetActive(true);
            circler.anchoredPosition = new Vector2(500,10);
            circler.localScale = new Vector3(1f,1f,1f);
            explainer.text = "  This is the 'next' button. If you press it, you'll realize that you've already been doing it the whole time. Really makes you think.";
            back.gameObject.SetActive(true);
        }
    }

    public void nextInstruction(){
        if (stepNo < 9){
        stepNo++;
        }
    }
    public void lastInstruction(){
        if (stepNo > 1){
            stepNo--;
        }
    }

    public void loadMenu(){
        SceneManager.LoadScene(8);
        //Get erroed idiot;
    }
}
