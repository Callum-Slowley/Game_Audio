using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsMenu;

    public void playButton(){
        SceneManager.LoadScene("GameScene");
    }
    public void optionsButton(){
        optionsMenu.SetActive(true);
    }
    public void exitButton(){
        Application.Quit();
    }
    public void highLightSound(){
        Debug.Log("highlight");
    }
}
