/*
 * Alex
 * This script loads a scene by it's index number in the build settings
 * 
 * Dominique
 * Added a quit function and changed the script to a UIManager
 * 
 * Alex 
 * Added the SFX manager toggle that Dominique told me to add
 * 
 * Alex Added PlayAnimation funtion to enable the animation 
 * for the camera also added a timer to the load scene function so that
 * the aniamtion has time to play before the scene is loaded
 */


using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager_AR : MonoBehaviour
{
    public float Timer = 5;
    public bool TimerStart = false;

    private void Update()
    {
        if (TimerStart == true)
        {
            Timer -= Time.deltaTime;
        }


    }

    public void LoadByIndex(int sceneIndex)
    {
        
        
        if (Timer <= 0)
        {
           SceneManager.LoadScene(sceneIndex);
        }
        
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void SFXToggle()
    {
        SFXManager_DR.instance.ToggleSFX();
    }

    public void PLayAnimation()
    {
        GetComponent<Animator>().SetBool("Play", true);
        TimerStart = true;

    }
}
