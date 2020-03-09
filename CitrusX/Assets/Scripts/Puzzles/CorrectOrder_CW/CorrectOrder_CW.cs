/*Chase Wilding 17/2/2020
 * This holds the base for the correct order puzzle
 */

/**
* \class CorrectOrder_CW
* 
* \brief Opens and closes the PC and checks if the buttons are the right colour
* 
* \author Chase
* 
* \date Last Modified: 17/02/2020
*/

using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;
using UnityEngine.UI;

public class CorrectOrder_CW : MonoBehaviour
{
    #region VARIABLES
    private Journal_DR journal;
    private bool isActive = false;
    public Color[] boxes;
    private FirstPersonController fpsController;
    public KeyCode closePCKey = KeyCode.Escape;
    private Text correctOrderText;
    private Text completionText;
    private Subtitles_HR subtitles;
    
    #endregion

    public void SetActive(bool value) { isActive = value; }
    /// <summary>
    /// Inititalise variables
    /// </summary>
    private void Awake()
    {
        journal = Journal_DR.instance;
        fpsController = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
        correctOrderText = GameObject.Find("Correct Order Message Text").GetComponent<Text>();
        completionText = GameObject.Find("Completion Text").GetComponent<Text>();
        subtitles = GameObject.Find("FirstPersonCharacter").GetComponent<Subtitles_HR>();
    }
    /// <summary>
    /// Ensure that the mouse cursor is invisible and locked and the correct order UI is deactivated
    /// </summary>
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameObject.SetActive(false);
    }
    /// <summary>
    /// Update checks to see player is trying to close the box
    /// </summary>
    private void Update()
    {
        CheckForClose();
    }
    /// <summary>
    /// Let the player use the cursor but don't let them move and activate the UI
    /// </summary>
    public void OpenPC()
    {
        //Make the cursor useable for solving the puzzle
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        gameObject.SetActive(true);



        fpsController.enabled = false;
    }
    /// <summary>
    /// If the player presses the close key then call ClosePC()
    /// </summary>
    private void CheckForClose()
    {
        if (Input.GetKeyDown(closePCKey))
        {
            ClosePC();
        }
    }
    /// <summary>
    /// If the puzzle is done play an audio clip
    /// Let the player move again, disabling their mouse and the UI
    /// </summary>
    public void ClosePC()
    {
        if(GameTesting_CW.instance.arePuzzlesDone[8])
        {
            subtitles.PlayAudio(Subtitles_HR.ID.P9_LINE6);
        }
       
        gameObject.tag = "PC";

        //Make the cursor invisible again
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        gameObject.SetActive(false);

        //Let the player move again
        fpsController.enabled = true;
    }
    /// <summary>
    /// Give each box its colour
    /// </summary>
    /// <param name="box - the box in the UI that is used for the password"></param>
    /// <param name="colour - the colour the box is set to"></param>
    internal void AssignBoxColour(int box, Color colour)
    {
        boxes[box] = colour;
    }
    /// <summary>
    /// Check if each box matches the flashing box it corresponds to. If so update the game state.
    /// </summary>
    public void CheckForCompletion()
    {
        if (boxes[0] == boxes[4])
        {
            if (boxes[1] == boxes[5])
            {
                if (boxes[2] == boxes[6])
                {
                    if (boxes[3] == boxes[7])
                    {
                        subtitles.PlayAudio(Subtitles_HR.ID.P9_LINE5);
                        completionText.text = "PASSWORD CORRECT";
                        GameTesting_CW.instance.arePuzzlesDone[8] = true;
                        journal.AddJournalLog("This is too much, I need to finish this now.");
                        journal.TickOffTask("Solve puzzle");
                        journal.ChangeTasks(new string[] { "Return to ritual" });
                    }
                    else
                    {
                        subtitles.PlayAudio(Subtitles_HR.ID.P9_LINE4);
                    }
                }
                else
                {
                    subtitles.PlayAudio(Subtitles_HR.ID.P9_LINE4);
                }
            }
            else
            {
                subtitles.PlayAudio(Subtitles_HR.ID.P9_LINE4);
            }
        }
        else
        {
            subtitles.PlayAudio(Subtitles_HR.ID.P9_LINE4);
        }
    }
}

