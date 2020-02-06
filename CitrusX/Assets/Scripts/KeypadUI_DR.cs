using UnityEngine;

public class KeypadUI_DR : MonoBehaviour
{
    private string input;

    public void SetInput(string to) { input = to; }

    private void OnTriggerEnter(Collider collider)
    {
        if (!doorUnlocked)
        {
            notificationText.text = "Press E to use the keypad";

            if (Input.GetKeyDown(KeyCode.E))
            {
                OpenKeypad();
            }
        }
    }
}
