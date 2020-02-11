/*Chase Wilding Fuse script 10/02/2020
 * This script opens/closes the fusebox and also registers whether they have been completed
 * It also keeps track of which colour is set for 'drawing' the wires
 * I have used some of Dominique's code from the keypad and manipulated it to open/close my fusebox for consistency
 * 
 * Dominique (Changes) 11/02/2020
 * Removed unused imported packages
 * Moved variable initialisations to Awake instead of Start
*/
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class Fusebox_CW : MonoBehaviour
{
    #region VARIABLES
    private FirstPersonController fpsController;
    private bool isFuseboxSolved;
    private KeyCode closeFuseboxKey = KeyCode.Escape;
    internal KeyCode resetPipesKey = KeyCode.X;
    internal int pipeCompletedCount;
    internal int wireCompletedCount;
    public Pipes_CW[] pipes;
    public Pipes_CW[] wires;
    internal Color drawColour;
    private Journal_DR journal;
    #endregion
    internal bool GetState() { return isFuseboxSolved; }

    void Awake()
    {
        fpsController = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
        journal = GameObject.Find("FPSController").GetComponent<Journal_DR>();
        //journal.ChangeTasks(new string[] { "fix fusebox" });
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
        //set the pipes in the inspector
        //set the wire ends into the wires array in the inspector also
        if(pipeCompletedCount == pipes.Length)
        {
            GameObject.Find("Fusebox Message Text").GetComponent<Text>().text = "pipes COMPLETE";
            if (wireCompletedCount == wires.Length)
            {
                GameObject.Find("Fusebox Message Text").GetComponent<Text>().text = "COMPLETE";
                journal.TickOffTask("fix fusebox");
                
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
