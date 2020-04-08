/*Chase Wilding 11/2/2020
 * This puzzle calls the scripts and starts the next puzzle when one is completed
 * Chase (Changes) 16/2/2020
 * Set up the bool system for all puzzles so the game can be played in order
 * Chase (Changes) 26/2/2020
 * Set up trigger references, as these can be activated from here to activate triggers for events such as subtitles
 * 
 * Chase (Changes) 2/3/2020
 * Added disturbances, changed initiate to a reference instead of calling the instance every time
 * Chase (Changes) 4/3/2020
 * Added GameObjects for puzzles, tidied script up aswell
 * Chase (Changes) 16/3/2020
 * Tidied up the script by moving all none game state related variables and features to EventManager_CW
 */

/**
* \class GameTesting_CW
* 
* \brief Starts each puzzle once the last one is completed, triggering disturbances, using triggers in game that react to the player and changes GOs
* 
* Works by checking the status of puzzles in Update using booleans.
* 
* \author Chase
* 
* \date Last Modified: 04/03/2020
*/

using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GameTesting_CW : MonoBehaviour
{
    #region BOOLS
    internal bool[] setUpPuzzle = { false, false, false, false, false, false, false, false, false, false };
    internal bool[] arePuzzlesDone = { false, false, false, false, false, false, false, false, false, false, false };
    internal bool[] cutscenes = { false, false, false };
    internal bool[] cutscenesDone = { false, false, false };
    internal bool controlsSeen = false;
    #endregion
    #region OTHER_VARIABLES
    private InitiatePuzzles_CW initiate;
    private Cinematics_DR cinematics;
    private Interact_HR interact;
    private GameObject controls;
    internal FirstPersonController fpsController;
    #endregion

    public static GameTesting_CW instance;

    /// <summary>
    /// Initialise variables
    /// </summary>
    private void Awake()
    {
        instance = this;
        initiate = GameObject.Find("FirstPersonCharacter").GetComponent<InitiatePuzzles_CW>();
        interact = GameObject.Find("FirstPersonCharacter").GetComponent<Interact_HR>();
        fpsController = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
        cinematics = GameObject.Find("Cinematics").GetComponent<Cinematics_DR>();
        controls = GameObject.Find("Controls");
    }
    private void Start()
    {
        controls.SetActive(false);
    }
    /// <summary>
    /// Check the status of booleans in  cutscenes and arePuzzlesDone to start the next puzzle
    /// </summary>
    
    internal void OpenControls()
    {
        controls.SetActive(true);
    
    }
    public void ControlsScreen()
    {
        controlsSeen = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        fpsController.enabled = true;
        controls.SetActive(false);
    }
    private void Update()
    {
        if (!controlsSeen && cutscenes[0])
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            fpsController.enabled = false;
            OpenControls();
        }
        else if (controlsSeen && !setUpPuzzle[0])
        {
            setUpPuzzle[0] = true;
            initiate.InitiateSetUpRitualPuzzle();
        }
        else if (arePuzzlesDone[0] && !setUpPuzzle[1])
        {
            setUpPuzzle[1] = true;
            initiate.InitiateFuseboxPuzzle();
        }
        else if (arePuzzlesDone[1] && !setUpPuzzle[2])
        {
            setUpPuzzle[2] = true;
            initiate.InitiateColourMatchingPuzzle();
        }
  
        else if (arePuzzlesDone[2] && !setUpPuzzle[3])
        {
            setUpPuzzle[3] = true;
            initiate.InitiateKeycodePuzzle();
        }
        else if (arePuzzlesDone[3] && !setUpPuzzle[4])
        {
            setUpPuzzle[4] = true;
            initiate.InitiateBalancePuzzle();
        }
        else if (arePuzzlesDone[4] && !setUpPuzzle[5])
        {
            setUpPuzzle[5] = true;
  
            initiate.InitiateChessBoardPuzzle();
        }
        else if (arePuzzlesDone[5] && !setUpPuzzle[6])
        {
            setUpPuzzle[6] = true;
            initiate.InitiateThrowingPuzzle();
        }
        else if (arePuzzlesDone[6] && !setUpPuzzle[7])
        {
            setUpPuzzle[7] = true;
            initiate.InitiateHiddenMechanismPuzzle();
        }
        else if (arePuzzlesDone[7] && !setUpPuzzle[8])
        {
            setUpPuzzle[8] = true; 
            initiate.InitiateCorrectOrderPuzzle();
        }
        else if (arePuzzlesDone[8] && !setUpPuzzle[9])
        {
            setUpPuzzle[9] = true;
            initiate.InitiateCoinCountPuzzle();
        }
    }
}
