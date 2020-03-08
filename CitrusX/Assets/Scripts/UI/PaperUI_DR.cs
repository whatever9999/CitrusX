/*
 * Dominique
 * 
 * While the UI is open it checks to see if the player presses escape to close it
 * 
 * Chase (Changes)
 * Added a reference to interaction to let it know when paper is closed, this then triggers a subtitle (under testing rn)
 */

/**
* \class PaperUI_DR
* 
* \brief Allows the player to close the UI of an image with text they see on the screen.
* 
* \author Dominique
* 
* \date Last Modified: 18/02/2020
*/

using UnityEngine;

public class PaperUI_DR : MonoBehaviour
{
    public KeyCode keyToClose = KeyCode.Z;
    private Interact_HR interaction;

    /// <summary>
    /// Inititalise variables
    /// </summary>
    private void Awake()
    {
        interaction = GameObject.Find("FirstPersonCharacter").GetComponent<Interact_HR>();
    }

    /// <summary>
    /// Ensure the UI GO is deactivated
    /// </summary>
    private void Start()
    {
        gameObject.SetActive(false);
    }


    /// <summary>
    /// If the player closes the UI then the GO is deactivated and the game state is updated on this
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(keyToClose))
        {
            interaction.paperIsClosed = true;
            gameObject.SetActive(false);
        }
    }
}
