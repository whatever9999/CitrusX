/*
 * Alex
 * This script loads a scene by it's index number in the build settings
 * 
 * Dominique
 * Added a quit function and changed the script to a UIManager
 * 
 * Dominique 03/03/2020
 * Added a function that gets the SFXManager in the scene and turns the sound on/off
 */


using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager_AR : MonoBehaviour
{

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
}
