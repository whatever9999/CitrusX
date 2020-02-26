﻿/*Chase Wilding 11/2/2020
 * This puzzle calls the scripts and starts the next puzzle when one is completed
 * Chase (Changes) 16/2/2020
 * Set up the bool system for all puzzles so the game can be played in order
 * Chase (Changes) 26/2/2020
 * Set up trigger references, as these can be activated from here to activate triggers for events such as subtitles
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

    private void Awake()
    {
        instance = this;
        ritualTrigger = GameObject.Find("Ritual Trigger").GetComponent<TriggerScript_CW>();
        chessTrigger = GameObject.Find("Chessboard Trigger").GetComponent<TriggerScript_CW>();
        throwingTrigger = GameObject.Find("Throwing Trigger").GetComponent<TriggerScript_CW>();
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
            InitiatePuzzles_CW.instance.InitiateSetUpRitualPuzzle();
        }
        else if (arePuzzlesDone[0] && !setUpPuzzle[1])
        {
            setUpPuzzle[1] = true;
            InitiatePuzzles_CW.instance.InitiateFuseboxPuzzle();
        }
        else if (arePuzzlesDone[1] && !setUpPuzzle[2])
        {
            setUpPuzzle[2] = true;
            ritualTrigger.allowedToBeUsed = true;
            InitiatePuzzles_CW.instance.InitiateColourMatchingPuzzle();
        }
        else if (arePuzzlesDone[2] && !setUpPuzzle[3])
        {
            setUpPuzzle[3] = true;
            InitiatePuzzles_CW.instance.InitiateKeycodePuzzle();
        }
        else if (arePuzzlesDone[3] && !setUpPuzzle[4])
        {
            setUpPuzzle[4] = true;
            InitiatePuzzles_CW.instance.InitiateBalancePuzzle();
        }
        else if (arePuzzlesDone[4] && !setUpPuzzle[5])
        {
            setUpPuzzle[5] = true;
            chessTrigger.allowedToBeUsed = true;
            InitiatePuzzles_CW.instance.InitiateChessBoardPuzzle();
        }
        else if (arePuzzlesDone[5] && !setUpPuzzle[6])
        {
            setUpPuzzle[6] = true;
            throwingTrigger.allowedToBeUsed = true;
            InitiatePuzzles_CW.instance.InitiateThrowingPuzzle();
        }
        else if (arePuzzlesDone[6] && !setUpPuzzle[7])
        {
            setUpPuzzle[7] = true;
            InitiatePuzzles_CW.instance.InitiateHiddenMechanismPuzzle();
        }
        else if (arePuzzlesDone[7] && !setUpPuzzle[8])
        {
            setUpPuzzle[8] = true;
            InitiatePuzzles_CW.instance.InitiateCorrectOrderPuzzle();
        }
        else if (arePuzzlesDone[8] && !setUpPuzzle[9])
        {
            setUpPuzzle[9] = true;
            InitiatePuzzles_CW.instance.InitiateCoinCountPuzzle();
        }
        else if (arePuzzlesDone[9])
        {
            //DISTURBANCES DONE
        }
    }
}
