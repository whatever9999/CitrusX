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
 * 
 * Dominique 02/04/2020
 * Added loading text animation (only shows in builds), start game animation, lightning effect and added settings to same scene as main
 * 
 * Hugo 11/04/2020
 * Adding controller navigation
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
* \date Last Modified: 02/04/2020
*/

using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager_AR : MonoBehaviour
{
    internal enum Scenes
    {
        START,
        MENU,
        GAME
    }

    public Vector2 lightningTimerRange = new Vector2(5, 10);
    public Vector3 lightningLightIntensity = new Vector3(0.5f, 5, 10);
    private float currentLightningTimer = 0;
    private float lightningTimer = 0;
    private AudioSource lightningCrack;
    private bool lightningHappening;

    private GameObject loadGameGO;
    private Text notificationText;
    private const float secondPerLoadingTextChange = 0.2f;
    private Animator cameraAnimator;
    private Light sceneLight;

    private GameObject menuUI;
    private GameObject optionsUI;

    public GameObject optionsFirstSelected;
    public GameObject mainFirstSelected;

    private void Awake()
    {
        cameraAnimator = Camera.main.GetComponent<Animator>();
        loadGameGO = GameObject.Find("LoadGameButton");
        notificationText = GameObject.Find("NotificationText").GetComponent<Text>();
        sceneLight = GameObject.Find("Directional Light").GetComponent<Light>();
        lightningCrack = sceneLight.GetComponent<AudioSource>();
        menuUI = GameObject.Find("MainMenu");
        optionsUI = GameObject.Find("OptionsMenu");
        optionsUI.SetActive(false);
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

        StartCoroutine(BeginningLightning());

        NewLightningTimer();
    }

    private IEnumerator BeginningLightning()
    {
        yield return new WaitForSeconds(2);
        StartCoroutine(LightningFlash());
    }

    private void Update()
    {
        lightningTimer += Time.deltaTime;
        if(lightningTimer >= currentLightningTimer)
        {
            StartCoroutine(LightningFlash());
            lightningTimer = 0;
            NewLightningTimer();
        }
    }

    public void StartNewGameButton()
    {
        DeleteFile();
        LoadGameButton();
    }

    public void LoadGameButton()
    {
        StartCoroutine(LoadGame((int)Scenes.GAME));
    }

    public void OptionsButton()
    {
        menuUI.SetActive(false);
        //Clear current selection
        EventSystem.current.SetSelectedGameObject(null);
        optionsUI.SetActive(true);
        //Set new selection
        EventSystem.current.SetSelectedGameObject(optionsFirstSelected);
    }

    public void BackButton()
    {
        optionsUI.SetActive(false);
        //Clear current selection
        EventSystem.current.SetSelectedGameObject(null);
        menuUI.SetActive(true);
        //Set new selection
        EventSystem.current.SetSelectedGameObject(mainFirstSelected);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    private void NewLightningTimer()
    {
        currentLightningTimer = Random.Range(lightningTimerRange[0], lightningTimerRange[1]);
    }

    private IEnumerator LightningFlash()
    {
        if(!lightningHappening)
        {
            lightningHappening = true;
            float timeBetweenLightChange = lightningCrack.time / 3;
            lightningCrack.Play();
            sceneLight.intensity = lightningLightIntensity[1];
            yield return new WaitForSeconds(timeBetweenLightChange);
            sceneLight.intensity = lightningLightIntensity[2];
            yield return new WaitForSeconds(timeBetweenLightChange);
            sceneLight.intensity = lightningLightIntensity[1];
            yield return new WaitForSeconds(timeBetweenLightChange);
            sceneLight.intensity = lightningLightIntensity[0];
            lightningHappening = false;
        }
    }

    private IEnumerator LoadGame(int sceneIndex)
    {
        cameraAnimator.SetTrigger("Play");
        yield return new WaitForSeconds(cameraAnimator.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(cameraAnimator.GetCurrentAnimatorStateInfo(0).length);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
        while (!asyncLoad.isDone)
        {
            notificationText.text = "Loading";
            yield return new WaitForSeconds(secondPerLoadingTextChange);
            notificationText.text = "Loading.";
            yield return new WaitForSeconds(secondPerLoadingTextChange);
            notificationText.text = "Loading..";
            yield return new WaitForSeconds(secondPerLoadingTextChange);
            notificationText.text = "Loading...";
            yield return new WaitForSeconds(secondPerLoadingTextChange);
        }
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
            //Starting a game for the first time
        }
    }
}
