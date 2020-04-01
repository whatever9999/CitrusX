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
 * 
 * Dominique 01/04/2020
 * Added start new and load options
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
* \date Last Modified: 01/04/2020
*/

using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager_AR : MonoBehaviour
{
    private GameObject loadGameGO;
    private Text notificationText;

    private void Awake()
    {
        loadGameGO = GameObject.Find("LoadGameButton");
        notificationText = GameObject.Find("NotificationText").GetComponent<Text>();
    }

    private void Start()
    {
        if(CheckIfSave())
        {
            loadGameGO.SetActive(true);
        } else
        {
            loadGameGO.SetActive(false);
        }
    }

    public void StartNewGameButton()
    {
        DeleteFile();
        LoadGameButton();
    }

    public void LoadGameButton()
    {
        notificationText.text = "Loading...";
        LoadByIndex(3);
    }

    public void OptionsButton()
    {
        LoadByIndex(1);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    private void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public bool CheckIfSave()
    {
        //Load data path
#if UNITY_EDITOR
        string path = Application.dataPath + "/save.dat";
#else
       string path = Application.persistentDataPath + "/save.dat";
#endif
        //If the file exists then return true
        if (File.Exists(path))
        {
            return true;
        }
        else
        {
            //No save file available
            return false;
        }
    }

    public void DeleteFile()
    {
        //Load data path
#if UNITY_EDITOR
        string path = Application.dataPath + "/save.dat";
#else
       string path = Application.persistentDataPath + "/save.dat";
#endif
        //If the file exists then return true
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        else
        {
            //No save file available
            Debug.LogError("Trying to delete a save file when there isn't one");
        }
    }
}
