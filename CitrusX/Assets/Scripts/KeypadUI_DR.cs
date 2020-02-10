/*
 * Dominique
 * 
 * The keypad UI allows the player to enter numbers up to the length of the passcode for the door, clear the passcode and close the UI
 * 
 * Dominique (Changes) 10/02/2020
 * Added SFX and negative/positive feedback according to passcode being incorrect/correct
 */
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class KeypadUI_DR : MonoBehaviour
{
    private string input = "";

    private Text inputText;
    private KeypadItem_DR keypadItem;
    private FirstPersonController firstPersonController;

    public void SetKeypadItem(KeypadItem_DR newKeypadItem) { keypadItem = newKeypadItem; }

    private void Awake()
    {
        firstPersonController = GameObject.FindObjectOfType<FirstPersonController>().GetComponent<FirstPersonController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        inputText = GameObject.Find("InputText").GetComponent<Text>();
        gameObject.SetActive(false);
    }

    public void NumberButton(int number)
    {
        SFXManager_DR.instance.PlayEffect(SoundEffectNames.BUTTON);
        //The player can't enter more digits than the length of the password
        if (input.Length != keypadItem.password.Length)
        {
            input += number;
            inputText.text = input;
        }
    }

    public void EnterButton()
    {
        if (input == keypadItem.password)
        {
            keypadItem.door.unlocked = true;
            SFXManager_DR.instance.PlayEffect(SoundEffectNames.CORRECT);
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
                    SFXManager_DR.instance.PlayEffect(SoundEffectNames.INCORRECT);
                }
            }
        }
    }

    //Set the input to a number of Xs (e.g. "XXXX") if the passcode the player enters is incorrect
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

    public void ClearButton()
    {
        SFXManager_DR.instance.PlayEffect(SoundEffectNames.BUTTON);
        input = "";
        inputText.text = input;
    }

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
    }

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
}
