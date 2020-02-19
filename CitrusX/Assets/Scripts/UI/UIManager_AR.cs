/*
 * Alex
 * This script loads a scene by it's index number in the build settings
 * 
 * Dominique
 * Added a quit function and changed the script to a UIManager
 */


using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager_AR : MonoBehaviour
{
    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
