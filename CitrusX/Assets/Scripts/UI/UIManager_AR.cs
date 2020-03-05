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
 * 
 * Dominique 03/03/2020
 * Added a function that gets the SFXManager in the scene and turns the sound on/off
 * 
 * Dominique 04/03/2020
 * Changed the timer a bit so that once it reaches zero it uses LoadSceneByIndex
 */


using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager_AR : MonoBehaviour
{

    public float Timer = 3;
    public bool TimerStart = false;

    private void Update()
    {
        if (TimerStart == true)
        {
            Timer -= Time.deltaTime;

            if (Timer <= 0)
            {
                LoadByIndex(3);
            }
        }
    }

    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void ToggleSFX()
    {
        GameObject.Find("SFXManager").GetComponent<SFXManager_DR>().ToggleSFX();
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
        Camera.main.GetComponent<Animator>().SetBool("Play", true);
        TimerStart = true;
    }
}
