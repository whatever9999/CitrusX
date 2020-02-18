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
    public Color[] boxes = {Color.white, Color.white, Color.white, Color.white, Color.white, Color.white, Color.white, Color.white};
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

