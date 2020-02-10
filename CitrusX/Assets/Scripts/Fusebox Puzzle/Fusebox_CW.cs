/*Chase Wilding Fuse script 10/02/2020
 * This script opens/closes the fusebox and also registers whether they have been completed
 * It also keeps track of which colour is set for 'drawing' the wires
 * I have used some of Dominique's code from the keypad and manipulated it to open/close my fusebox for consistency
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class Fusebox_CW : MonoBehaviour
{
    private FirstPersonController fpsController;
    private bool isFuseboxSolved;
    private KeyCode closeFuseboxKey = KeyCode.Escape;
    internal KeyCode resetPipesKey = KeyCode.X;
    internal int pipeCompletedCount;
    internal int wireCompletedCount;
    public Pipes_CW[] pipes;
    public Pipes_CW[] wires;
    internal Color drawColour;
    internal bool GetState() { return isFuseboxSolved; }

    void Start()
    {
        fpsController = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameObject.SetActive(false);
    }
    private void Update()
    {
        GetAllPipesInScene();
        CheckForClose();
    }


    //reused and tweaked some of Dominique's code to open/close the fusebox to lock the cursor etc as will only have one fusebox in game
    public void OpenFusebox()
    {
        //Make the cursor useable for solving the puzzle
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        gameObject.SetActive(true);

        //Stop the player from moving while using the fusebox
        fpsController.enabled = false;
    }
    public void GetAllPipesInScene()
    {
        if(pipeCompletedCount == pipes.Length)
        {
            GameObject.Find("Fusebox Message Text").GetComponent<Text>().text = "pipes COMPLETE";
            if (wireCompletedCount == wires.Length)
            {
                GameObject.Find("Fusebox Message Text").GetComponent<Text>().text = "COMPLETE";
            }
        }
    }
    private void CheckForClose()
    {
        if (Input.GetKeyDown(closeFuseboxKey))
        {
            CloseFusebox();
        }
    }
    public void CloseFusebox()
    {
        //Make sure raycasts know the fusebox is a fusebox
        gameObject.tag = "Fusebox";

        //Make the cursor invisible again
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        gameObject.SetActive(false);

        //Let the player move again
        fpsController.enabled = true;
    }
}
