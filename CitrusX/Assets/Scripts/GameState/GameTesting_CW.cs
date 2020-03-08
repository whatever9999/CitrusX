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

public class GameTesting_CW : MonoBehaviour
{
    #region TRIGGERS
    private TriggerScript_CW ritualTrigger;
    private TriggerScript_CW chessTrigger;
    private TriggerScript_CW throwingTrigger;
    private TriggerScript_CW correctOrderTrigger;
    #endregion
    #region BOOLS
    private bool[] setUpPuzzle = { false, false, false, false, false, false, false, false, false, false };
    internal bool[] arePuzzlesDone = { false, false, false, false, false, false, false, false, false, false, false };
    private bool[] cutscenes = { false, false, false };
    private bool[] cutscenesDone = { false, false, false };
    #endregion
    #region OTHER_VARIABLES
    private DisturbanceHandler_DR disturbance;
    private InitiatePuzzles_CW initiate;
    GameObject throwingBox;
    GameObject hiddenMechDoc;
    #endregion

    public static GameTesting_CW instance;

    /// <summary>
    /// Initialise variables
    /// </summary>
    private void Awake()
    {
        instance = this;
        disturbance = DisturbanceHandler_DR.instance;
        initiate = InitiatePuzzles_CW.instance;
        throwingBox = GameObject.Find("ThrowingBox");
        hiddenMechDoc = GameObject.Find("HiddenMechNote");
        #region INTIALISE_TRIGGERS
        ritualTrigger = GameObject.Find("RitualTrigger").GetComponent<TriggerScript_CW>();
        chessTrigger = GameObject.Find("ChessboardTrigger").GetComponent<TriggerScript_CW>();
        throwingTrigger = GameObject.Find("ThrowingTrigger").GetComponent<TriggerScript_CW>();
        correctOrderTrigger = GameObject.Find("CorrectOrderTrigger").GetComponent<TriggerScript_CW>();
        #endregion
    }

    /// <summary>
    /// Ensure that GOs that are disabled are so
    /// </summary>
    private void Start()
    {
        throwingBox.SetActive(false);
        hiddenMechDoc.SetActive(false);
    }

    /// <summary>
    /// Check the status of booleans in  cutscenes and arePuzzlesDone to start the next puzzle
    /// </summary>
    void Update()
    {
        if (!cutscenes[0])
        {
            //play start cutscene
            cutscenes[0] = true;
        }
        else if (!arePuzzlesDone[0] && !setUpPuzzle[0])
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
            ritualTrigger.allowedToBeUsed = true;
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
            chessTrigger.allowedToBeUsed = true;
           // disturbance.TriggerDisturbance(DisturbanceHandler_DR.DisturbanceName.PAWNFALL); //might move to monitor interaction
            initiate.InitiateChessBoardPuzzle();
        }
        else if (arePuzzlesDone[5] && !setUpPuzzle[6])
        {
            setUpPuzzle[6] = true;
            chessTrigger.allowedToBeUsed = true;
          //  disturbance.TriggerDisturbance(DisturbanceHandler_DR.DisturbanceName.DOORCREAK);
            throwingTrigger.allowedToBeUsed = true;
            initiate.InitiateThrowingPuzzle();
        }
        else if (arePuzzlesDone[6] && !setUpPuzzle[7])
        {
            throwingBox.SetActive(true);
            setUpPuzzle[7] = true;
            //disturbance.TriggerDisturbance(DisturbanceHandler_DR.DisturbanceName.LAMPWOBBLE);
            initiate.InitiateHiddenMechanismPuzzle();
        }
        else if (arePuzzlesDone[7] && !setUpPuzzle[8])
        {
            hiddenMechDoc.SetActive(true);
            setUpPuzzle[8] = true;
            correctOrderTrigger.allowedToBeUsed = true;
         //   disturbance.TriggerDisturbance(DisturbanceHandler_DR.DisturbanceName.DOORCREAK);
            initiate.InitiateCorrectOrderPuzzle();
        }
        else if (arePuzzlesDone[8] && !setUpPuzzle[9])
        {
            ritualTrigger.allowedToBeUsed = true;
            setUpPuzzle[9] = true;
            initiate.InitiateCoinCountPuzzle();
        }
        else if (arePuzzlesDone[9])
        {
            //DISTURBANCES DONE
        }
    }
}
