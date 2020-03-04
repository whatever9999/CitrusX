/*Chase Wilding 11/2/2020
 * This puzzle calls the scripts and starts the next puzzle when one is completed
 * Chase (Changes) 16/2/2020
 * Set up the bool system for all puzzles so the game can be played in order
 * Chase (Changes) 26/2/2020
 * Set up trigger references, as these can be activated from here to activate triggers for events such as subtitles
 * 
 * Chase (Changes) 2/3/2020
 * Added disturbances, changed initiate to a reference instead of calling the instance every time
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTesting_CW : MonoBehaviour
{
    public static GameTesting_CW instance;
    private TriggerScript_CW ritualTrigger;
    private TriggerScript_CW chessTrigger;
    private TriggerScript_CW throwingTrigger;
    private bool[] setUpPuzzle = { false, false, false, false, false, false, false, false, false, false };
    internal bool[] arePuzzlesDone = { false, false, false, false, false, false, false, false, false, false, false };
    private bool[] cutscenes = { false, false, false };
    private bool[] cutscenesDone = { false, false, false };
    private DisturbanceHandler_DR disturbance;
    private InitiatePuzzles_CW initiate;
    GameObject throwingBox;

    private void Awake()
    {
        instance = this;
        disturbance = DisturbanceHandler_DR.instance;
        initiate = InitiatePuzzles_CW.instance;
        ritualTrigger = GameObject.Find("RitualTrigger").GetComponent<TriggerScript_CW>();
        chessTrigger = GameObject.Find("ChessboardTrigger").GetComponent<TriggerScript_CW>();
        throwingTrigger = GameObject.Find("ThrowingTrigger").GetComponent<TriggerScript_CW>();
        throwingBox = GameObject.Find("ThrowingBox");
    }
    private void Start()
    {
        throwingBox.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        //play start cutscene
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
            disturbance.TriggerDisturbance(DisturbanceHandler_DR.DisturbanceName.LAMPWOBBLE);
            initiate.InitiateHiddenMechanismPuzzle();
        }
        else if (arePuzzlesDone[7] && !setUpPuzzle[8])
        {
            setUpPuzzle[8] = true;
            disturbance.TriggerDisturbance(DisturbanceHandler_DR.DisturbanceName.DOORCREAK);
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
