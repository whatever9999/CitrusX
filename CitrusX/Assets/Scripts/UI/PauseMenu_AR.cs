/*
 * Alex 
 * opens and closes the pause menu
 * 
 * Dominique
 * Made open and close menu key the same
 * Made script work on FPSController instead of itself as then it won't run update
 * When the player presses the pause button the baron timer is disabled so nothing happens while they're paused and they lose the ability to move and their mouse appears
 * 
 * Hugo
 * 
 *12/04 Corona virus is upon us but I made controller work for the pause menu.
 */

/**
* \class PauseMenu_AR
* 
* \brief Lets the player open and close the menu. When it is open they can use their mouse to click on buttons. It stops the game from running while paused.
* 
* \author Alex
* 
* \date Last Modified: 18/02/2020
*/
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.EventSystems;

public class PauseMenu_AR : MonoBehaviour
{
    public KeyCode openPause = KeyCode.Escape;
    public GameObject pauseFirstSelected;
    private FirstPersonController firstPersonController;
    private GameObject pauseMenu;

    /// <summary>
    /// Initialise variables
    /// </summary>
    void Awake()
    {
        firstPersonController = gameObject.GetComponent<FirstPersonController>();
        pauseMenu = GameObject.Find("PauseMenu");
    }
    
    /// <summary>
    /// When the player presses the open/close button the GO is disabled/enabled accordingly.
    /// The mouse's lock state and visibility is also changed along with the ability of the player to move and the appearances of the baron are paused
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(openPause))
        {
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            firstPersonController.enabled = false;
        }

        if (Input.GetButtonDown("Pause"))
        {
            //Clear current selection
            EventSystem.current.SetSelectedGameObject(null);
            //Set new selection
            EventSystem.current.SetSelectedGameObject(pauseFirstSelected);
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            firstPersonController.enabled = false;
        }
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(1);
    }

    public void ClosePauseButton()
    {
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        firstPersonController.enabled = true;
    }
}
