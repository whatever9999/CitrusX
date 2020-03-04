/*
 * Dominique
 * 
 * While the UI is open it checks to see if the player presses escape to close it
 * 
 * Chase (Changes)
 * Added a reference to interaction to let it know when paper is closed, this then triggers a subtitle (under testing rn)
 */
using UnityEngine;

public class PaperUI_DR : MonoBehaviour
{
    public KeyCode keyToClose = KeyCode.Z;
    private Interact_HR interaction;

    private void Awake()
    {
        interaction = GameObject.Find("FirstPersonCharacter").GetComponent<Interact_HR>();
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyToClose))
        {
            interaction.paperIsClosed = true;
            gameObject.SetActive(false);
        }
    }
}
