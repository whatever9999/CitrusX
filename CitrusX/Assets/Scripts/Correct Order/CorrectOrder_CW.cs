/*Chase Wilding 17/2/2020
 * This holds the base for the correct order puzzle
 */

using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;
using UnityEngine.UI;

public class CorrectOrder_CW : MonoBehaviour
{
    #region VARIABLES
    private Journal_DR journal;
    private bool isActive = false;
    private Image[] sequenceImages;
    private Image[] passwordImages;
    private Color[] passwordColours;
    internal Color[] inputColours;
    private FirstPersonController fpsController;
    private KeyCode closePCKey;
    public static CorrectOrder_CW instance;
    #endregion

    public void SetActive(bool value) { isActive = value; }
    // Start is called before the first frame update
    private void Awake()
    {
        journal = Journal_DR.instance;
        instance = this;
        fpsController = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
        closePCKey = KeyCode.Escape;

        //sequenceImages[0] = GameObject.Find("Correct Order Image 1").GetComponent<Image>();
        //sequenceImages[1] = GameObject.Find("Correct Order Image 2").GetComponent<Image>();
        //sequenceImages[2] = GameObject.Find("Correct Order Image 3").GetComponent<Image>();
        //sequenceImages[3] = GameObject.Find("Correct Order Image 4").GetComponent<Image>();

        //passwordImages[0] = GameObject.Find("Password Image 1").GetComponent<Image>();
        //passwordImages[1] = GameObject.Find("Password Image 2").GetComponent<Image>();
        //passwordImages[2] = GameObject.Find("Password Image 3").GetComponent<Image>();
        //passwordImages[3] = GameObject.Find("Password Image 4").GetComponent<Image>();

        //passwordColours[0] = Color.green;
        //passwordColours[1] = Color.red;
        //passwordColours[2] = Color.yellow;
        //passwordColours[3] = Color.cyan;
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameObject.SetActive(false);
    }
    public void OpenPC()
    {
        //Make the cursor useable for solving the puzzle
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        gameObject.SetActive(true);

     
        fpsController.enabled = false;
    }
    private void CheckForClose()
    {
        if (Input.GetKeyDown(closePCKey))
        {
            ClosePC();
        }
    }
    public void ClosePC()
    {
        
        gameObject.tag = "PC";

        //Make the cursor invisible again
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        gameObject.SetActive(false);

        //Let the player move again
        fpsController.enabled = true;
    }
}
