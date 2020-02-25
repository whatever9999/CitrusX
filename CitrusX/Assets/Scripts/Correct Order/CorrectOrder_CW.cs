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
    public Color[] boxes;
    private FirstPersonController fpsController;
    public KeyCode closePCKey = KeyCode.Escape;
    private Text correctOrderText;
    private Text completionText;
    #endregion

    public void SetActive(bool value) { isActive = value; }
    // Start is called before the first frame update
    private void Awake()
    {
        journal = Journal_DR.instance;
        fpsController = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
        correctOrderText = GameObject.Find("Correct Order Message Text").GetComponent<Text>();
        completionText = GameObject.Find("Completion Text").GetComponent<Text>();
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameObject.SetActive(false);
    }
    private void Update()
    {
        CheckForClose();
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
    internal void AssignBoxColour(int box, Color colour)
    {
        boxes[box] = colour;
    }
    public void CheckForCompletion()
    {
        if (boxes[0] == boxes[4])
        {
            if (boxes[1] == boxes[5])
            {
                if (boxes[2] == boxes[6])
                {
                    if (boxes[3] == boxes[7])
                    {
                        completionText.text = "PASSWORD CORRECT";
                        //VOICEOVER 9-5
                        //close article
                        //VOICEOVER 9-6
                    }
                    else
                    {
                        //VOICEOVER 9-4
                    }
                }
                else
                {
                    //VOICEOVER 9-4
                }
            }
            else
            {
                //VOICEOVER 9-4
            }
        }
        else
        {
            //VOICEOVER 9-4
        }
    }
}

