using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class KeypadUI_DR : MonoBehaviour
{
    private string input;

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
        //The player can't enter more digits than the length of the password
        if(input.Length != keypadItem.password.Length)
        {
            input += number;
            inputText.text = input;
        }
    }

    public void EnterButton()
    {
        if (input == keypadItem.password)
        {
            keypadItem.door.SetUnlocked(true);
            CloseKeypad();
        }
    }

    public void ClearButton()
    {
        input = "";
        inputText.text = input;
    }

    private void OnDisable()
    {
        input = "";
    }

    public void OpenKeypad(KeypadItem_DR newKeypadItem)
    {
        keypadItem = newKeypadItem;

        //Makes sure the notification text doesn't say to press E to use the keypad when the UI is open
        keypadItem.tag = "Untagged";

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        gameObject.SetActive(true);

        firstPersonController.enabled = false;
    }

    public void CloseKeypad()
    {
        //Make sure raycasts know the keypad item is a keypad again
        keypadItem.tag = "Keypad";
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        input = "";
        inputText.text = input;

        gameObject.SetActive(false);

        firstPersonController.enabled = true;
    }
}
