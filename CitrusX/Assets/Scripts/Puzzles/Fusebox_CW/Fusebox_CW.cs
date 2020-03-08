﻿/*Chase Wilding Fuse script 10/02/2020
 * This script opens/closes the fusebox and also registers whether they have been completed
 * It also keeps track of which colour is set for 'drawing' the wires
 * I have used some of Dominique's code from the keypad and manipulated it to open/close my fusebox for consistency
 *
 * Dominique(Changes) 11/02/2020
 * Removed unused imported packages
 * Moved variable initialisations to Awake instead of Start
 *
 * Chase(Changes) 22/2/2020
 * Added voiceover comments and bools
 * 
 * Dominique (Changes) 03/03/2020
 * Moved and changed directions, simplified script a bit, seperated the commented wires code a bit
*/

/**
* \class Fusebox_CW
* 
* \brief Open and close the fusebox and check if the pipes are in the right position, updating the game state if they are
* 
* \author Chase
* 
* \date Last Modified: 03/03/2020
*/
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class Fusebox_CW : MonoBehaviour
{
    public enum Directions
    {
        HORIZONTAL,
        VERTICAL,
        RIGHT_DOWN_BEND,
        LEFT_DOWN_BEND,
        RIGHT_UP_BEND,
        LEFT_UP_BEND
    }

    #region PuzzleVariables
    public KeyCode closeFuseboxKey = KeyCode.Z;
    public KeyCode resetPipesKey = KeyCode.X;

    private FirstPersonController fpsController;
    private Text fuseboxText;
    private Pipes_CW[] pipes;
    private GameObject fusebox;
    //public Pipes_CW[] wires;
    //private int wireCompletedCount;
    //private Color drawColour;
    #endregion

    #region GameStateVariables
    private Journal_DR journal;
    private bool[] voiceovers = { false, false };
    private Subtitles_HR subtitles;
    internal bool isFuseboxSolved = false;
    private bool isActive = false;
    #endregion
    internal bool GetState() { return isFuseboxSolved; }
    internal void SetGameActive(bool value) { isActive = value; }

    /// <summary>
    /// Inititalise variables and ensure the UI GO is deactivated
    /// </summary>
    void Awake()
    {
        fpsController = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
        journal = Journal_DR.instance;
        fuseboxText = GameObject.Find("FuseboxMessageText").GetComponent<Text>();
        subtitles = GameObject.Find("FirstPersonCharacter").GetComponent<Subtitles_HR>();
        pipes = GameObject.FindObjectsOfType<Pipes_CW>();
        fusebox = GameObject.Find("Fusebox");

        gameObject.SetActive(false);
    }
    /// <summary>
    /// Check if the player is closing the UI
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(closeFuseboxKey))
        {
            CloseFusebox();
        }
    }

    /// <summary>
    /// reused and tweaked some of Dominique's code for Keypad_DR to open/close the fusebox to lock the cursor etc as will only have one fusebox in game
    /// </summary>
    public void OpenFusebox()
    {
        //Make the cursor useable for solving the puzzle
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        gameObject.SetActive(true);

        //Stop the player from moving while using the fusebox
        fpsController.enabled = false;
    }
    /// <summary>
    /// Checks the position of each pipe. If they are all in their desired position then the puzzle is marked as complete and the game state is updated with this. The player can no longer interact with the fusebox
    /// </summary>
    public void CheckPipes()
    {
        //set the pipes in the inspector
        //set the wire ends into the wires array in the inspector also
        bool complete = true;
        for(int i = 0; i < pipes.Length; i++)
        {
            if(!pipes[i].GetIsInPosition())
            {
                complete = false;
            }
        }

        if (complete)
        {
            fuseboxText.text = "COMPLETED";
            isFuseboxSolved = true;
            journal.TickOffTask("Fix fusebox");
            if (!voiceovers[1])
            {
                subtitles.PlayAudio(Subtitles_HR.ID.P2_LINE3);
                voiceovers[1] = true;
                GameTesting_CW.instance.arePuzzlesDone[1] = true;
            }

            //Player can't use the fusebox anymore
            fusebox.tag = "Untagged";
        }
    }
    /// <summary>
    /// reused and tweaked some of Dominique's code for Keypad_DR to open/close the fusebox to lock the cursor etc as will only have one fusebox in game
    /// </summary>
    public void CloseFusebox()
    {
        //Make the cursor invisible again
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //Let the player move again
        fpsController.enabled = true;

        gameObject.SetActive(false);
    }
}

