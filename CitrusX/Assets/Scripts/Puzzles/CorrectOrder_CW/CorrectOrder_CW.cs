/*Chase Wilding 17/2/2020
 * This holds the base for the correct order puzzle
 * 
 * Chase (Changes) 11/3/2020
 * Added a whichRound array of bools to progress through varying rounds
 * Chase (Changes) 2/4/2020
 * Commented in sounds for Dominique and changed where correct order stems from
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
    internal bool isActive = false;
    public Color[] boxes;
    private FirstPersonController fpsController;
    public KeyCode closePCKey = KeyCode.Z;
    private Text completionText;
    private Subtitles_HR subtitles;
    internal bool[] whichRound = { true, false, false };
    public Door_DR correctOrderDoor;
    private GameObject correctOrderScreen;
    private GameObject PC;
    #endregion

    public void SetActive(bool value) { isActive = value; }
    /// <summary>
    /// Inititalise variables
    /// </summary>
    private void Awake()
    {
        journal = GameObject.Find("FirstPersonCharacter").GetComponent<Journal_DR>();
        fpsController = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
        completionText = GameObject.Find("Completion Text").GetComponent<Text>();
        subtitles = GameObject.Find("FirstPersonCharacter").GetComponent<Subtitles_HR>();
        correctOrderScreen = GameObject.Find("CorrectOrderUI");
        PC = GameObject.Find("Correct Order PC");

    }
    /// <summary>
    /// Ensure that the mouse cursor is invisible and locked and the correct order UI is deactivated
    /// </summary>
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        correctOrderScreen.SetActive(false);
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
        SFX_Manager_HR.instance.PlaySFX(SFX_Manager_HR.SoundEffectNames.COMPUTER_ON_OFF, transform.position);
        //Make the cursor useable for solving the puzzle
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        correctOrderScreen.SetActive(true);
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
        PC.tag = "PC";

        //Make the cursor invisible again
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        correctOrderScreen.SetActive(false);
        SFX_Manager_HR.instance.PlaySFX(SFX_Manager_HR.SoundEffectNames.COMPUTER_ON_OFF, transform.position);

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
                        if(whichRound[0])
                        {
                            whichRound[1] = true;
                            whichRound[0] = false;
                            SFX_Manager_HR.instance.PlaySFX(SFX_Manager_HR.SoundEffectNames.PC_CORRECT, transform.position);
                            completionText.text = "ROUND 1 CORRECT";
                        }
                        else if(whichRound[1])
                        {
                            whichRound[2] = true;
                            whichRound[1] = false;
                            SFX_Manager_HR.instance.PlaySFX(SFX_Manager_HR.SoundEffectNames.PC_CORRECT, transform.position);
                            completionText.text = "ROUND 2 CORRECT";
                        }
                        else if(whichRound[2])
                        {
                            subtitles.PlayAudio(Subtitles_HR.ID.P9_LINE5);
                            completionText.text = "PUZZLE SOLVED";
                            correctOrderDoor.unlocked = true;
                            correctOrderDoor.ToggleOpen();
                            SFX_Manager_HR.instance.PlaySFX(SFX_Manager_HR.SoundEffectNames.PC_CORRECT, transform.position);
                            DisturbanceHandler_DR.instance.TriggerDisturbance(DisturbanceHandler_DR.DisturbanceName.BARONCLOSEUP);  
                            journal.AddJournalLog("This is too much, I need to finish this now. I need 10 coins but I can't check how many I have until it's over...how many more do I need to get to blow out the candles and end this?");
                            journal.TickOffTask("Solve puzzle");
                            journal.ChangeTasks(new string[] { "Blow out candles" });
                            ClosePC();
                            GameObject.Find("LaptopScreen").SetActive(false);
                            GameTesting_CW.instance.arePuzzlesDone[8] = true;
                        }
                    }
                    else
                    {
                        SFX_Manager_HR.instance.PlaySFX(SFX_Manager_HR.SoundEffectNames.PC_INCORRECT, transform.position);
                        subtitles.PlayAudio(Subtitles_HR.ID.P9_LINE4);
                    }
                }
                else
                {
                    SFX_Manager_HR.instance.PlaySFX(SFX_Manager_HR.SoundEffectNames.PC_INCORRECT, transform.position);
                    subtitles.PlayAudio(Subtitles_HR.ID.P9_LINE4);
                }
            }
            else
            {
                SFX_Manager_HR.instance.PlaySFX(SFX_Manager_HR.SoundEffectNames.PC_INCORRECT, transform.position);
                subtitles.PlayAudio(Subtitles_HR.ID.P9_LINE4);
            }
        }
        else
        {
            SFX_Manager_HR.instance.PlaySFX(SFX_Manager_HR.SoundEffectNames.PC_INCORRECT, transform.position);
            subtitles.PlayAudio(Subtitles_HR.ID.P9_LINE4);
        }
    }
}

