/*
 * Dominique
 * 
 * The keypad UI allows the player to enter numbers up to the length of the passcode for the door, clear the passcode and close the UI
 * 
 * Dominique (Changes) 10/02/2020
 * Added SFX and negative/positive feedback according to passcode being incorrect/correct
 * 
 * Chase (Changes) 17/2/2020
 * Added a SetActive Function and linked it to the GameTesting script, also added the journal for the tasks
 * 
 * Chase (Changes) 22/2/2020
 * Added voiceover comments and bools for them, added an update to use voiceovers
 * and for interaction with the note for voiceovers
 * 
 * Chase(Changes) 26/2/2020
 * Added voiceover functionality
 */

/**
* \class KeypadUI_DR
* 
* \brief Consists of functions that are added to buttons on a keypad UI that let the player put in numbers, clear input and enter input.
* 
* NumberButton(number) is placed on a button that is linked to a certain number and the number is an int that pertains to that number.
* EnterButton() is placed on an enter button and will check if the current input aligns with the keyPadItem's passcode.
* ClearButton() ensures that the input shown in the input text is blank, along with the string that stores the current input.
* ClearInput() is a coroutine that makes the input flash as a number of Xs to show that the user input the wrong passcode
* OpenKeypad() and CloseKeypad() change the lockMode and visibility of the mouse and enable/disable playerMovement accordingly.
* 
* \author Dominique
* 
* \date Last Modified: 26/02/2020
*/

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class KeypadUI_DR : MonoBehaviour
{
    private string input = "";
    private Text inputText;
    private Baron_DR baron;
    private bool firstTime = true;
    #region REFERENCES
    private KeypadItem_DR keypadItem;
    private FirstPersonController firstPersonController;
    internal bool isActive = false;
    private Journal_DR journal;
    private Subtitles_HR subtitles;
    #endregion
    #region BOOLS
    internal bool interactedWithSafe = false; //needs to be set in interact
    internal bool hasAlreadyInteractedWithSafe = false;
    internal bool playerInteractsWithDoc = false;
    internal bool[] voiceovers = { false, false, false, false, false, false, false };
    #endregion
    public void SetKeypadItem(KeypadItem_DR newKeypadItem) { keypadItem = newKeypadItem; }
    public void SetActive(bool value) { isActive = value; }

    /// <summary>
    /// Initialise variables
    /// </summary>
    private void Awake()
    {
        firstPersonController = GameObject.FindObjectOfType<FirstPersonController>().GetComponent<FirstPersonController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        inputText = GameObject.Find("InputText").GetComponent<Text>();
        journal = Journal_DR.instance;
        subtitles = GameObject.Find("FirstPersonCharacter").GetComponent<Subtitles_HR>();
        baron = GameObject.Find("Baron").GetComponent<Baron_DR>();
    }

    /// <summary>
    /// Ensure the keypadUI GO is disabled
    /// </summary>
    private void Start()
    {
        //Do this here so the UI can be gotten in other scripts
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Play appropriate voiceovers according to the state of the puzzle
    /// </summary>
    private void Update()
    {
        if(isActive)
        {
            if (!voiceovers[0])
            {
                subtitles.PlayAudio(Subtitles_HR.ID.P4_LINE4);
                journal.TickOffTask("Check the desk");
                journal.AddJournalLog("This safe needs a password, there’s got to be some clue somewhere…");
                journal.ChangeTasks(new string[] {"Find clue"});
                voiceovers[0] = true;
            }
            if(interactedWithSafe && !hasAlreadyInteractedWithSafe)
            {
                if(!voiceovers[1])
                {
                    subtitles.PlayAudio(Subtitles_HR.ID.P4_LINE4);
                    voiceovers[1] = true;
                    hasAlreadyInteractedWithSafe = true;
                }
            }
            if(playerInteractsWithDoc)
            {
                if(!voiceovers[2])
                {
                    subtitles.PlayAudio(Subtitles_HR.ID.P4_LINE7);
                    voiceovers[2] = true;
                    GameTesting_CW.instance.arePuzzlesDone[3] = true;
                }

            }
        }
    }

    /// <summary>
    /// Add the number to the input string
    /// </summary>
    /// <param name="number - a number to be added onto the input if the player pushes the button"></param>
    public void NumberButton(int number)
    {
        SFX_Manager_HR.instance.PlaySFX(SFX_Manager_HR.SoundEffectNames.KEYPAD_BUTTON, Vector3.zero);
        //The player can't enter more digits than the length of the password
        if (input.Length != keypadItem.password.Length)
        {
            input += number;
            inputText.text = input;
        }
    }

    /// <summary>
    /// Check if the input string matches the keypadItem's passcode. Give negative or positive feedback accordingly.
    /// </summary>
    public void EnterButton()
    {
        if (input == keypadItem.password)
        {
            keypadItem.door.ToggleOpen();
            SFX_Manager_HR.instance.PlaySFX(SFX_Manager_HR.SoundEffectNames.KEYPAD_CORRECT, Vector3.zero);
            //finish journal tasks and let game know the puzzle is complete
            journal.TickOffTask("Solve password");
            journal.AddJournalLog("Finally, what’s this note?");
            journal.ChangeTasks(new string[] { "Read note" });
            subtitles.PlayAudio(Subtitles_HR.ID.P4_LINE6);
            GameTesting_CW.instance.arePuzzlesDone[3] = true;
            CloseKeypad();
        } else
        {
            //Only play the buzzer sound if the player has entered something and the buzzer is currently not playing
            if(input.Length != 0)
            {
                //Not an && check because input[0] results in an exception if the length is 0
                if(input[0] != 'X')
                {
                    StartCoroutine(ClearInput());
                    subtitles.PlayAudio(Subtitles_HR.ID.P4_LINE5);
                    SFX_Manager_HR.instance.PlaySFX(SFX_Manager_HR.SoundEffectNames.KEYPAD_INCORRECT,Vector3.zero);
                }
            }
        }
    }

    /// <summary>
    /// Set the input to a number of Xs (e.g. "XXXX") if the passcode the player enters is incorrect
    /// </summary>
    private IEnumerator ClearInput()
    {
        string xString = "";
        for(int i = 0; i < keypadItem.password.Length; i++)
        {
            xString += "X";
        }
        input = xString;
        inputText.text = input;
        yield return new WaitForSeconds(0.7f);
        input = "";
        inputText.text = input;
    }


    /// <summary>
    /// Clear the input and show this on screen too
    /// </summary>
    public void ClearButton()
    {
        SFX_Manager_HR.instance.PlaySFX(SFX_Manager_HR.SoundEffectNames.KEYPAD_BUTTON, Vector3.zero);
        input = "";
        inputText.text = input;
    }

    /// <summary>
    /// Set the UI's keypadItem and ensure the player can use the mouse and can't move
    /// </summary>
    /// <param name="newKeypadItem - the keypadItem that the player clicked on (means that different keypads can have different passcodes)"></param>
    public void OpenKeypad(KeypadItem_DR newKeypadItem)
    {
        //Make sure the UI is for the keypad used (not another one in the scene)
        keypadItem = newKeypadItem;

        //Makes sure the notification text doesn't say to press E to use the keypad when the UI is open
        keypadItem.tag = "Untagged";

        //Make the cursor useable for entering the code
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        gameObject.SetActive(true);

        //Stop the player from moving while using the keypad
        firstPersonController.enabled = false;
        if(firstTime)
        {
            StartCoroutine(BaronTimer());
            firstTime = false;
        }
       
    }

    /// <summary>
    /// Makes the mouse invisible and lock it in the centre, reset the keypad input and let the player move again
    /// </summary>
    public void CloseKeypad()
    {
        //Make sure raycasts know the keypad item is a keypad again
        keypadItem.tag = "Keypad";
        
        //Make the cursor invisible again
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //Clear the UI input
        input = "";
        inputText.text = input;

        gameObject.SetActive(false);

        //Let the player move again
        firstPersonController.enabled = true;
    }
    private IEnumerator BaronTimer()
    {
        float i = 10.0f;
        while(i > 0.0f)
        {
            yield return new WaitForSeconds(1.0f);
            i--;
            if (i == 1.0f)
            {
                baron.GetCoin();
            }
        }
       
        
    }
   
}
