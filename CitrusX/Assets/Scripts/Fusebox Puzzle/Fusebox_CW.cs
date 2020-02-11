﻿/*Chase Wilding Fuse script 10/02/2020
 * This script opens/closes the fusebox and also registers whether they have been completed
 * It also keeps track of which colour is set for 'drawing' the wires
 * I have used some of Dominique's code from the keypad and manipulated it to open/close my fusebox for consistency
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class Fusebox_CW : MonoBehaviour
{
    #region VARIABLES
    private FirstPersonController fpsController;
    private bool isFuseboxSolved;
    private bool isActive = false;
    private KeyCode closeFuseboxKey = KeyCode.Escape;
    internal KeyCode resetPipesKey = KeyCode.X;
    internal int pipeCompletedCount;
    internal int wireCompletedCount;
    private Text fuseboxText;
    public Pipes_CW[] pipes;
    public Pipes_CW[] wires;
    internal Color drawColour;
    private Journal_DR journal;
    #endregion
    internal bool GetState() { return isFuseboxSolved; }
    internal void SetActive(bool value) { isActive = value; }
    private void Awake()
    {
        fpsController = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
        journal = Journal_DR.instance;
        fuseboxText = GameObject.Find("Fusebox Message Text").GetComponent<Text>();
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameObject.SetActive(false);
    }
    private void Update()
    {
        GetAllPipesInScene();
        CheckForClose();
    }


    //reused and tweaked some of Dominique's code to open/close the fusebox to lock the cursor etc as will only have one fusebox in game
    public void OpenFusebox()
    {
        //Make the cursor useable for solving the puzzle
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        gameObject.SetActive(true);

        //Stop the player from moving while using the fusebox
        fpsController.enabled = false;
    }
    public void GetAllPipesInScene()
    {
        //set the pipes in the inspector
        //set the wire ends into the wires array in the inspector also
        if(pipeCompletedCount == pipes.Length)
        {
            if (wireCompletedCount == wires.Length)
            {
                fuseboxText.text = "COMPLETE";
                journal.TickOffTask("Fix fusebox");
                GameTesting_CW.instance.arePuzzlesDone[2] = true;
                
            }
        }
    }
    private void CheckForClose()
    {
        if (Input.GetKeyDown(closeFuseboxKey))
        {
            CloseFusebox();
        }
    }
    public void CloseFusebox()
    {
        //Make sure raycasts know the fusebox is a fusebox
        gameObject.tag = "Fusebox";

        //Make the cursor invisible again
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        gameObject.SetActive(false);

        //Let the player move again
        fpsController.enabled = true;
    }
}
