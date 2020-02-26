/*Chase Wilding Fuse script 10/02/2020
 * This script opens/closes the fusebox and also registers whether they have been completed
 * It also keeps track of which colour is set for 'drawing' the wires
 * I have used some of Dominique's code from the keypad and manipulated it to open/close my fusebox for consistency
 * 
 * Dominique (Changes) 11/02/2020
 * Removed unused imported packages
 * Moved variable initialisations to Awake instead of Start
 * 
 * Chase (Changes) 22/2/2020
 * Added voiceover comments and bools
*/
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class Fusebox_CW : MonoBehaviour
{
    #region VARIABLES
    private FirstPersonController fpsController;
    private bool isFuseboxSolved = false;
    private bool isActive = false;
    private KeyCode closeFuseboxKey = KeyCode.Z;
    internal KeyCode resetPipesKey = KeyCode.X;
    internal int pipeCompletedCount;
    //internal int wireCompletedCount;
    private Text fuseboxText;
    public Pipes_CW[] pipes;
    public Pipes_CW[] wires;
    internal Color drawColour;
    private Journal_DR journal;
    private bool[] voiceovers = { false, false };
    private Subtiles_HR subtitles;
    #endregion
    internal bool GetState() { return isFuseboxSolved; }

    internal void SetGameActive(bool value) { isActive = value; }

    void Awake()
    {
        fpsController = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
        journal = Journal_DR.instance;
        fuseboxText = GameObject.Find("Fusebox Message Text").GetComponent<Text>();
        subtitles = GameObject.Find("FirstPersonCharacter").GetComponent<Subtiles_HR>();
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameObject.SetActive(false);
    }
    private void Update()
    {

        

        if (!voiceovers[0])
        {
                subtitles.PlayAudio(Subtiles_HR.ID.P2_LINE2);
                voiceovers[0] = true;
                journal.AddJournalLog("The cameras have gone out, I should check that fusebox.");
                journal.ChangeTasks(new string[] { "Fix fusebox" });
        }

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
            fuseboxText.text = "COMPLETED";
            journal.TickOffTask("Fix fusebox");
            if(!voiceovers[1])
            {
                subtitles.PlayAudio(Subtiles_HR.ID.P2_LINE3);
                voiceovers[1] = true;
            }
            GameTesting_CW.instance.arePuzzlesDone[1] = true;
            //if (wireCompletedCount >= wires.Length)
            //{
            //    fuseboxText.text = "COMPLETE";
              
                
            //}
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
