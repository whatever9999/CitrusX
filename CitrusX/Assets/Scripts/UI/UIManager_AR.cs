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

/**
* \class UIManager_AR
* 
* \brief Consists of functions that are added to buttons on UI to carry out actions such as loading scenes, toggling sound effects, quitting and playing animations
* 
* LoadByIndex(sceneIndex) loads a scene according to its index using the SceneManager
* ToggleSFX() turns the SFX from the SFXManager on/off
* QuitButton() quits the application
* PlayAnimation() is to be used on the start button - it plays an animation moving into the house in the menu scene then starts the game
* 
* \author Alex
* 
* \date Last Modified: 04/03/2020
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

    public void QuitButton()
    {
        Application.Quit();
    }

    public void PLayAnimation()
    {
        Camera.main.GetComponent<Animator>().SetBool("Play", true);
        TimerStart = true;
    }
}
