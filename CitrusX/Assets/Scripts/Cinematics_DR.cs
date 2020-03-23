/*
 * Dominique
 * 
 * This script handles any changes in the scene needed for cinematics and can be used to make cinematics start
 * 
 * Chase (Changes) 19/3/2020
 * Linked to game state to start first puzzle when start cinematic is done
 */

/**
* \class Cinematics_DR
* 
* \brief Contains functions for cinematic events and has functions to make cinematics start
* 
* If playStartCinematic is active upon starting the game then the start cinematic will play
* Play the good or bad end cinematic using PlayEndCinematic(), passing in the enum GOOD or BAD
* 
* \author Dominique
* 
* \date Last Modified: 17/03/2020
*/

using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityStandardAssets.Characters.FirstPerson;

public class Cinematics_DR : MonoBehaviour
{
    public bool playStartCinematic = true;

    #region CinematicProgress
    private VideoPlayer videoPlayer;
    private Animator endCameraAnimator;
    private Animator startCameraAnimator;
    private GameObject monitor;
    private bool monitorOn = false;
    private GameObject cutsceneCoins;
    private Image blackScreen;
    private GameObject creditScreen;
    #endregion
    #region Cinematics
    private PlayableDirector startCinematic;
    private PlayableDirector goodEndCinematic;
    private PlayableDirector badEndCinematic;
    #endregion
    #region PlayerControl
    private Interact_HR playerInteraction;
    private FirstPersonController playerController;
    #endregion
    

    public enum END_CINEMATICS
    {
        GOOD,
        BAD
    }

    /// <summary>
    /// Initialisations
    /// </summary>
    private void Awake()
    {
        videoPlayer = GameObject.Find("LaptopScreen").GetComponent<VideoPlayer>();
        startCinematic = GameObject.Find("StartCinematic").GetComponent<PlayableDirector>();
        goodEndCinematic = GameObject.Find("GoodEndCinematic").GetComponent<PlayableDirector>();
        badEndCinematic = GameObject.Find("BadEndCinematic").GetComponent<PlayableDirector>();

        monitor = GameObject.Find("Monitor");
        for(int i = 0; i < monitor.transform.childCount; i++)
        {
            monitor.transform.GetChild(i).gameObject.SetActive(false);
        }

        playerController = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
        playerInteraction = GameObject.Find("FirstPersonCharacter").GetComponent<Interact_HR>();

        startCameraAnimator = GameObject.Find("StartCinematicCamera").GetComponent<Animator>();
        endCameraAnimator = GameObject.Find("EndCinematicCamera").GetComponent<Animator>();

        cutsceneCoins = GameObject.Find("CoinsForCutscene");

        blackScreen = GameObject.Find("BlackScreen").GetComponent<Image>();

        creditScreen = GameObject.Find("CreditScreen");
        creditScreen.SetActive(false);
    }

    /// <summary>
    /// If the start cinematic needs to be played upon start then it will be (this also stops the player from being able to interact with anything)
    /// </summary>
    private void Start()
    {
        if(playStartCinematic)
        {
            playerInteraction.enabled = false;
            playerController.enabled = false;
            startCameraAnimator.SetTrigger("StartCinematic");
            startCinematic.Play();
        }
    }

    /// <summary>
    /// Play the end cinematic and then roll the credits
    /// </summary>
    /// <param name="type - GOOD or BAD"></param>
    public void PlayEndCinematic(END_CINEMATICS type)
    {
        switch (type)
        {
            case END_CINEMATICS.GOOD:
                playerInteraction.enabled = false;
                playerController.enabled = false;
                endCameraAnimator.SetBool("GoodEnding", true);
                goodEndCinematic.Play();
                break;
            case END_CINEMATICS.BAD:
                playerInteraction.enabled = false;
                playerController.enabled = false;
                endCameraAnimator.SetBool("GoodEnding", false);
                badEndCinematic.Play();
                break;
        }
        endCameraAnimator.SetTrigger("PlayEnd");
    }

    /// <summary>
    /// The video of the player researching the baron's story is shown on the laptop screen
    /// </summary>
    public void PlayVideo()
    {
        videoPlayer.Play();
    }

    /// <summary>
    /// The camera displays on the monitor are activated/deactivated
    /// </summary>
    public void ToggleMonitor()
    {
        monitorOn = !monitorOn;
        for (int i = 0; i < monitor.transform.childCount; i++)
        {
            monitor.transform.GetChild(i).gameObject.SetActive(monitorOn);
        }
    }

    /// <summary>
    /// However many coins the player picked up are laid on the table
    /// </summary>
    public void CoinsAppear()
    {
        for(int i = 0; i < playerInteraction.numberCoinsCollected; i++)
        {
            cutsceneCoins.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// A black screen is toggled on or off
    /// </summary>
    public void ToggleBlackScreen()
    {
        if (blackScreen.color.a == 1)
        {
            Color newColor = blackScreen.color;
            newColor.a = 0;
            blackScreen.color = newColor;
        } else
        {
            Color newColor = blackScreen.color;
            newColor.a = 1;
            blackScreen.color = newColor;
        }
    }

    /// <summary>
    /// A coroutine for the credits is started
    /// </summary>
    public void StartCredits()
    {
        StartCoroutine(Credits());
    }

    /// <summary>
    /// The credit screen is shown for 4 seconds and then we go to the menu
    /// </summary>
    /// <returns></returns>
    private IEnumerator Credits()
    {
        creditScreen.SetActive(true);
        yield return new WaitForSeconds(4);
        //Go back to menu
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Event occurs at the end of the start cinematic so the script is updated and the player can move again - this is saved so the cinematic does not play each time the player starts the game
    /// </summary>
    public void StartCinematicDone()
    {
        playStartCinematic = false;
        playerController.enabled = true;
        playerInteraction.enabled = true;
        GameTesting_CW.instance.cutscenes[0] = true;
    }
}
