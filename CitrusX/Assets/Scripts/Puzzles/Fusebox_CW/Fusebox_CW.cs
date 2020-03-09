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

public class Fusebox_CW : MonoBehaviour
{
    private const float timeForFlowInPipes = 0.5f;
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
    public Pipes_CW[] pipesFromStartToEnd; //Pipes need to be passed in in order so that they will change colour in order

    private FirstPersonController fpsController;
    private Text fuseboxText;
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
        journal.AddJournalLog("It’s disconnected? I’m sure I can sort this out.");
        journal.TickOffTask("Check fusebox");
        journal.ChangeTasks(new string[] { "Fix fusebox" });

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
            if(!pipesFromStartToEnd[numberComplete].GetIsInPosition())
            {
                complete = false;
                break;
            } else
            {
                pipesFromStartToEnd[numberComplete].ChangeColour();
                yield return new WaitForSeconds(timeForFlowInPipes);
            }
        }

        if (complete)
        {
            fuseboxText.text = "COMPLETED";
            isFuseboxSolved = true;
            journal.TickOffTask("Fix fusebox");
            journal.AddJournalLog("Stupid old electrics, I’ll return to the ritual now.");
            journal.ChangeTasks(new string[] { "Return to ritual" });
            if (!voiceovers[1])
            {
                subtitles.PlayAudio(Subtitles_HR.ID.P2_LINE3);
                voiceovers[1] = true;
                GameTesting_CW.instance.arePuzzlesDone[1] = true;
            }

            //Player can't use the fusebox anymore
            fusebox.tag = "Untagged";
        } else
        {
            for (; numberComplete >= 0; numberComplete--)
            {
                pipesFromStartToEnd[numberComplete].ResetColour();
                yield return new WaitForSeconds(timeForFlowInPipes);
            }
        }
    }
    /// <summary>
    /// Start the coroutine CheckPipes() to go through each pipe one by one and check the flow in it, changing the image to show it's complete and chaning it back if the flow fails
    /// </summary>
    public void PowerButton()
    {
        StartCoroutine(CheckPipes());
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

