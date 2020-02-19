/*
 *Alex 
 * opens and closes the pause menu
 * 
 * Dominique
 * Made open and close menu key the same
 * Made script work on FPSController instead of itself as then it won't run update
 * When the player presses the pause button the baron timer is disabled so nothing happens while they're paused and they lose the ability to move and their mouse appears
 */
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PauseMenu_AR : MonoBehaviour
{
    public KeyCode openAndCloseMenu = KeyCode.Escape;

    private FirstPersonController firstPersonController;
    private GameObject pauseMenu;
    //The water bowl handles the baron's appearances on a timer
    private WaterBowl_DR waterBowl;

    void Start()
    {
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);

        firstPersonController = gameObject.GetComponent<FirstPersonController>();
        waterBowl = GameObject.Find("Water Bowl").GetComponent<WaterBowl_DR>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(openAndCloseMenu))
        {
            if (pauseMenu.activeInHierarchy)
            {
                pauseMenu.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                firstPersonController.enabled = true;
                waterBowl.enabled = true;
            } else
            {
                pauseMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                firstPersonController.enabled = false;
                waterBowl.enabled = false;
            }
        }
    }
}
