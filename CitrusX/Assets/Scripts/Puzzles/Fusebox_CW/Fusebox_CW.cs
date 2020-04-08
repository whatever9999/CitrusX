/*Chase Wilding Fuse script 10/02/2020
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
 * 
 * Dominique (Changes) 09/03/2020
 * CheckPipes() is a coroutine so the colour of the pipes changes one by one if they're complete
 * The fusebox now requires the pipes to be passed in through the inspector to ensure they're in order
 * 
 * Chase (Changes) 11/3/2020
 * Made it inherit from PuzzleBase Script and tidied the script up. Moved directions to pipes
 * 
 * Chase (Changes) 17/3/2020
 * Changed it from sprites to colours for completion
 * 
 * Chase (Changes) 7/4/2020
 * Added a bool to coroutine to stop spamming of power button and changed it so that it would not crash if close is triggered
 * whilst it is checking pipes
*/

/**
* \class Fusebox_CW
* 
* \brief Open and close the fusebox and check if the pipes are in the right position, updating the game state if they are
* 
* \author Chase
* 
* \date Last Modified: 09/03/2020
*/
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using System.Collections;

internal class Fusebox_CW : MonoBehaviour
{
    #region PUZZLE_VARS
    private const float timeForFlowInPipes = 0.5f;
    public KeyCode closeFuseboxKey = KeyCode.Z;
    public KeyCode resetPipesKey = KeyCode.X;
    public Pipes_CW[] pipesFromStartToEnd; //Pipes need to be passed in in order so that they will change colour in order
    private Text fuseboxText;
    private GameObject fusebox;
    internal bool isFuseboxSolved = false;
    internal Journal_DR journal;
    internal Subtitles_HR subtitles;
    internal FirstPersonController fpsController;
    internal GameTesting_CW game;
    internal bool[] voiceovers = { false, false, false };
    internal bool isActive = false;
    private bool coroutinePlaying = false;
    #endregion
    internal bool GetState() { return isFuseboxSolved; }
    internal void SetActive(bool value) { isActive = value; }
    /// <summary>
    /// Inititalise variables and ensure the UI GO is deactivated
    /// </summary>
    private void Awake()
    {
        fpsController = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
        subtitles = GameObject.Find("FirstPersonCharacter").GetComponent<Subtitles_HR>();
        fusebox = GameObject.Find("Fusebox");
        journal = GameObject.Find("FirstPersonCharacter").GetComponent<Journal_DR>();
        game = GameObject.Find("FirstPersonCharacter").GetComponent<GameTesting_CW>();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Check if the player is closing the UI
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(closeFuseboxKey) && !coroutinePlaying)
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
        journal.AddJournalLog("It’s disconnected? I’m sure I can sort this out.");
        journal.TickOffTask("Check fusebox");
        journal.ChangeTasks(new string[] { "Fix fusebox" });
        SFX_Manager_HR.instance.PlaySFX(SFX_Manager_HR.SoundEffectNames.FUSEBOX_DOOR, transform.position);
        //Stop the player from moving while using the fusebox
        fpsController.enabled = false;
    }
    /// <summary>
    /// Checks the position of each pipe. If they are all in their desired position then the puzzle is complete and the game state is updated with this. The player can no longer interact with the fusebox.
    /// The pipe changes to a different colour when complete (to show this there is a time between the checking of each pipe) and if the flow fails the pipes lose flow in reverse order
    /// </summary>
    private IEnumerator CheckPipes()
    {
        //set the pipes in the inspector
        //set the wire ends into the wires array in the inspector also
        bool complete = true;
        int numberComplete;
        for(numberComplete = 0; numberComplete < pipesFromStartToEnd.Length; numberComplete++)
        {
            coroutinePlaying = true;

            if(!pipesFromStartToEnd[numberComplete].GetIsInPosition())
            {
                SFX_Manager_HR.instance.PlaySFX(SFX_Manager_HR.SoundEffectNames.PIPE_INCORRECT, transform.position);
                complete = false;
                break;
            } else
            {
                SFX_Manager_HR.instance.PlaySFX(SFX_Manager_HR.SoundEffectNames.PIPE_CORRECT, transform.position);
                pipesFromStartToEnd[numberComplete].ChangeColour();
                yield return new WaitForSeconds(timeForFlowInPipes);
            }
        }

        if (complete)
        {
            isFuseboxSolved = true;
            coroutinePlaying = false;
            journal.TickOffTask("Fix fusebox");
            journal.AddJournalLog("Stupid old electrics, I’ll return to the ritual now.");
            journal.ChangeTasks(new string[] { "Return to ritual" });
            if (!voiceovers[0])
            {
                subtitles.PlayAudio(Subtitles_HR.ID.P2_LINE3);
                voiceovers[0] = true;
                GameObject.Find("GardenTrigger").GetComponent<TriggerScript_CW>().allowedToBeUsed = true;
                GameTesting_CW.instance.arePuzzlesDone[1] = true;
            }

            //Player can't use the fusebox anymore
            fusebox.tag = "Untagged";
        } else
        {
            for (; numberComplete >= 0; numberComplete--)
            {
                SFX_Manager_HR.instance.PlaySFX(SFX_Manager_HR.SoundEffectNames.PIPE_INCORRECT, transform.position);
                pipesFromStartToEnd[numberComplete].ResetColour();
                yield return new WaitForSeconds(timeForFlowInPipes);
            }
        }
        coroutinePlaying = false;
    }
    /// <summary>
    /// Start the coroutine CheckPipes() to go through each pipe one by one and check the flow in it, changing the image to show it's complete and chaning it back if the flow fails
    /// </summary>
    public void PowerButton()
    {
        if(!coroutinePlaying)
        {
            StartCoroutine(CheckPipes());
        }
        
    }
    /// <summary>
    /// reused and tweaked some of Dominique's code for Keypad_DR to open/close the fusebox to lock the cursor etc as will only have one fusebox in game
    /// </summary>
    public void CloseFusebox()
    {
       
            for (int i = 0; i < pipesFromStartToEnd.Length; i++)
            {
                pipesFromStartToEnd[i].ResetPipe();
            }
            //Make the cursor invisible again
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            SFX_Manager_HR.instance.PlaySFX(SFX_Manager_HR.SoundEffectNames.FUSEBOX_DOOR, transform.position);
            //Let the player move again
            fpsController.enabled = true;

            gameObject.SetActive(false);
       
       
    }
}

