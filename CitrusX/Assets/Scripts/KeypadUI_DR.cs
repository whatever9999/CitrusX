using UnityEngine;
using UnityEngine.UI;

public class KeypadUI_DR : MonoBehaviour
{
    private string input;

    private Text notificationText;
    private KeypadItem_DR keypadItem;

    public void SetKeypadItem(KeypadItem_DR newKeypadItem) { keypadItem = newKeypadItem; }

    private void Awake()
    {
        notificationText = GameObject.Find("NotificationText").GetComponent<Text>();
        gameObject.SetActive(false);
    }

    public void NumberButton(int number)
    {
        //The player can't enter more digits than the length of the password
        if(input.Length != keypadItem.password.Length)
        {
            input += number;
            notificationText.text = input;
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
        notificationText.text = input;
    }

    private void OnDisable()
    {
        input = "";
    }

    public void OpenKeypad(KeypadItem_DR newKeypadItem)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        keypadItem = newKeypadItem;
        gameObject.SetActive(true);
    }

    public void CloseKeypad()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        gameObject.SetActive(false);
    }
}
