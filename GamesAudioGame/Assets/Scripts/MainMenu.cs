using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public FMOD.Studio.EventInstance MenuSounds;
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
        MenuSounds = FMODUnity.RuntimeManager.CreateInstance("event:/Menu/Hover");
        MenuSounds.start();
        MenuSounds.release();
    }
    public void clickNoise(){
        MenuSounds = FMODUnity.RuntimeManager.CreateInstance("event:/Menu/ClickButon");
        MenuSounds.start();
        MenuSounds.release();
    }
        public void optionsNo(){
        MenuSounds = FMODUnity.RuntimeManager.CreateInstance("event:/Menu/No");
        MenuSounds.start();
        MenuSounds.release();
    }
}
