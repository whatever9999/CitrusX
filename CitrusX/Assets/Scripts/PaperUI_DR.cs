/*
 * Dominique
 * 
 * While the UI is open it checks to see if the player presses escape to close it
 */
using UnityEngine;

public class PaperUI_DR : MonoBehaviour
{
    public KeyCode keyToClose = KeyCode.Z;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyToClose))
        {
            gameObject.SetActive(false);
        }
    }
}
