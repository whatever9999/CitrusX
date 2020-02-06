using UnityEngine;
using UnityEngine.UI;

public class KeypadItem_DR : MonoBehaviour
{
    public string password = "1234";

    private bool doorUnlocked;

    private KeypadUI_DR keypadUIScript;
    private GameObject keypadUI;
    private Text notificationText;

    private void Awake()
    {
        keypadUI = GameObject.Find("Keypad");
        keypadUIScript = keypadUI.GetComponent<KeypadUI_DR>();
        notificationText = GameObject.Find("NotificationText").GetComponent<Text>();
    }

    private void OnTriggerExit(Collider other)
    {
        notificationText.text = "";
        keypadUI.SetActive(false);
        keypadUIScript.SetInput("");
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void CheckPassword(string input)
    {
        if (input == password)
        {
            doorUnlocked = true;
            Cursor.visible = false;
        }
    }

    private void OpenKeypad()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        keypadUI.SetActive(true);
    }
}
