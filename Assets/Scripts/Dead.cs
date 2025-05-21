using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;

public class Dead : MonoBehaviour
{
    public Text text;
    public float timer = 5;
    public GameObject counter;
    public GameObject classism;
    void Update()
    {
        //find gameobjects that need to be destroyed on death
        counter = GameObject.Find("PlayerCounter");
        classism = GameObject.Find("Classes");
       //find current scene 
        Scene scene  = SceneManager.GetActiveScene();
        if (!scene.name.Equals("Casey's Scene")){
            if (gameObject.GetComponent<PlayerHP2>()){
                if (gameObject.GetComponent<PlayerHP2>().hp == 0 && gameObject.name.Equals("Player 1")){
                    //destroys game objects
                    Destroy(gameObject);
                    Destroy(counter);
                    Destroy(classism);
                    //disconnects then brings you to respawn screen
                    PhotonNetwork.Disconnect();
                    SceneManager.LoadScene(4);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    Debug.Log("you died" + gameObject.name);
                }
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
