using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Dead : MonoBehaviour
{
    public Text text;
    public float timer = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Scene scene  = SceneManager.GetActiveScene();
        if (!scene.name.Equals("Loser's Scene")){
            if (PlayerHP2.hp == 0){
                SceneManager.LoadScene(4);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
        timer -= Time.deltaTime;
        if (timer >= 0){
            if (text){
            text.text = timer.ToString().Substring(0,1);
            }
        }
        else{
            if (text){
                text.text = "try again";
            }
        }
    }

    public void shut(){
        if (timer <= 0){
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }
    }
}