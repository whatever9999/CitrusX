﻿/* Chase Wilding 16/3/2020
 * This script controls the events that occur in the house such as the doors opening and disturbances occuring. This is in place
 * to stop the game state script from getting too crowded
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager_CW : MonoBehaviour
{
    #region DOOR_REFERENCES
    private Door_DR kitchenDoor;
    private Door_DR upstairsBathroomDoor;
    private Door_DR studyDoor; //hidden mech door
    private Door_DR loungeDoor; //chessboard door
    private Door_DR masterBedroomDoor;
    private Door_DR[] otherDownstairsDoors;
    #endregion
    #region TRIGGER_REFERENCES
    private TriggerScript_CW ritualTrigger;
    private TriggerScript_CW chessTrigger;
    private TriggerScript_CW throwingTrigger;
    private TriggerScript_CW correctOrderTrigger;
    private TriggerScript_CW gardenTrigger;
    #endregion
    #region GAMEOBJECT_REFERENCES
    private GameObject throwingBox;
    private GameObject hiddenMechDoc;
    private GameObject bathroomKeyPart1;
    private GameObject bathroomKeyPart2;
    private GameObject safe;
    private GameObject balls1;
    private GameObject balls2;
    private GameObject balls3;
    private GameObject buttons1;
    private GameObject buttons2;
    private GameObject buttons3;
    private GameObject pawn;
    private GameObject chessNote;
    private GameObject keypadDoc;
    private GameObject baronRitualLocation;
    private GameObject laptopScreen;
   
    #endregion
    #region DISTURBANCES
    private Baron_DR baron;
    #endregion
    #region PARTICLE_EFFECTS
    ParticleSystem scalesEffect;
    ParticleSystem chessBookEffect;
    ParticleSystem button1Effect;
    ParticleSystem button2Effect;
    ParticleSystem button3Effect;
    #endregion
    #region BOOLS
    internal bool[] triggersSet = { false, false, false, false, false, false };
    internal bool[] itemsSet = { false, false, false, false, false };
    internal bool[] disturbancesSet = { false, false };
    #endregion
    private GameTesting_CW game;
    private Cinematics_DR cinematics;

    private void Awake()
    {
        baron = GameObject.Find("Baron").GetComponent<Baron_DR>();
        cinematics = GameObject.Find("Cinematics").GetComponent<Cinematics_DR>();
        #region INITIATE_GOs
        throwingBox = GameObject.Find("ThrowingBox");
        hiddenMechDoc = GameObject.Find("HiddenMechNote");
        bathroomKeyPart1 = GameObject.Find("Bathroom Key");
        bathroomKeyPart2 = GameObject.Find("Bathroom Key Part 2");
        balls1 = GameObject.Find("1Ball");
        balls2 = GameObject.Find("2Ball");
        balls3 = GameObject.Find("3Ball");
        buttons1 = GameObject.Find("1Button");
        buttons2 = GameObject.Find("2Button");
        buttons3 = GameObject.Find("3Button");
        safe = GameObject.Find("Safe");
        pawn = GameObject.Find("Pawn");
        chessNote = GameObject.Find("Chess Note");
        keypadDoc = GameObject.Find("KeyPadDoc");
        masterBedroomDoor = GameObject.Find("Bedroom Door").GetComponent<Door_DR>();
        baronRitualLocation = GameObject.Find("RitualBaronLocation");
        scalesEffect = GameObject.Find("Scales").GetComponentInChildren<ParticleSystem>();
        chessBookEffect = GameObject.Find("ChessBook").GetComponentInChildren<ParticleSystem>();
        button1Effect = GameObject.Find("1Button").GetComponentInChildren<ParticleSystem>();
        button2Effect = GameObject.Find("2Button").GetComponentInChildren<ParticleSystem>();
        button3Effect = GameObject.Find("3Button").GetComponentInChildren<ParticleSystem>();
        laptopScreen = GameObject.Find("LaptopScreen");



        #endregion
        #region INITIATE_TRIGGERS
        ritualTrigger = GameObject.Find("RitualTrigger").GetComponent<TriggerScript_CW>();
        chessTrigger = GameObject.Find("ChessboardTrigger").GetComponent<TriggerScript_CW>();
        throwingTrigger = GameObject.Find("ThrowingTrigger").GetComponent<TriggerScript_CW>();
        correctOrderTrigger = GameObject.Find("CorrectOrderTrigger").GetComponent<TriggerScript_CW>();
        gardenTrigger = GameObject.Find("GardenTrigger").GetComponent<TriggerScript_CW>();
        #endregion
    }
    private void Start()
    {
        game = GameTesting_CW.instance;
        #region ACTIVATE_OBJECTS
        if (SaveSystem_DR.instance.loaded)
        {
            throwingBox.SetActive(SaveSystem_DR.instance.loadedGD.throwingActivated);
            hiddenMechDoc.SetActive(SaveSystem_DR.instance.loadedGD.hiddenMechNoteActivated);
            bathroomKeyPart1.SetActive(SaveSystem_DR.instance.loadedGD.bathroomKeyPartOneActive);
            bathroomKeyPart2.SetActive(SaveSystem_DR.instance.loadedGD.bathroomKeyPartTwoActive);
            safe.SetActive(SaveSystem_DR.instance.loadedGD.keypadTableActivated);
            balls1.SetActive(SaveSystem_DR.instance.loadedGD.ball1IsActive);
            balls2.SetActive(SaveSystem_DR.instance.loadedGD.ball2IsActive);
            balls3.SetActive(SaveSystem_DR.instance.loadedGD.ball3IsActive);
            buttons1.SetActive(SaveSystem_DR.instance.loadedGD.button1IsActive);
            buttons2.SetActive(SaveSystem_DR.instance.loadedGD.button2IsActive);
            buttons3.SetActive(SaveSystem_DR.instance.loadedGD.button3IsActive);
            chessNote.SetActive(SaveSystem_DR.instance.loadedGD.chessNoteActivated);
            keypadDoc.SetActive(SaveSystem_DR.instance.loadedGD.keypadNoteActive);
        }
        else
        {
            throwingBox.SetActive(false);
            hiddenMechDoc.SetActive(false);
            bathroomKeyPart1.SetActive(false);
            bathroomKeyPart2.SetActive(false);
            safe.SetActive(false);
            balls1.SetActive(false);
            balls2.SetActive(false);
            balls3.SetActive(false);
            buttons1.SetActive(false);
            buttons2.SetActive(false);
            buttons3.SetActive(false);
            chessNote.SetActive(false);
            keypadDoc.SetActive(false);
        }
        #endregion
    }
    private void Update()
    {
        if(game.arePuzzlesDone[0] && !triggersSet[5])
        {
            gardenTrigger.allowedToBeUsed = true;
            laptopScreen.SetActive(false);
            triggersSet[5] = true;
        }
        else if(game.arePuzzlesDone[0] && !game.arePuzzlesDone[1] && !itemsSet[4])
        {
            cinematics.ToggleMonitor();
            itemsSet[4] = true;
        }
        else if(game.arePuzzlesDone[1] && !triggersSet[0])
        {
            cinematics.ToggleMonitor();
            bathroomKeyPart1.SetActive(true);
            bathroomKeyPart2.SetActive(true);
            bathroomKeyPart2.name = "Bathroom Key"; 
            ritualTrigger.allowedToBeUsed = true;
            triggersSet[0] = true;
        }
        else if(triggersSet[0] && !game.arePuzzlesDone[2])
        {
            if(!bathroomKeyPart2.activeInHierarchy)
            {
                bathroomKeyPart1.name = "Bathroom Key Part 2";
            }
            else if(!bathroomKeyPart1.activeInHierarchy)
            {
                bathroomKeyPart2.name = "Bathroom Key Part 2";
            }
        }
        else if(game.arePuzzlesDone[2] && !itemsSet[2])
        {
            baron.GetCoin();
            safe.SetActive(true);
            itemsSet[2] = true;
        }
        else if(game.arePuzzlesDone[3] && !itemsSet[3])
        {
            baron.GetCoin();
            keypadDoc.SetActive(true);
            scalesEffect.Play();
            itemsSet[3] = true;
        }
        else if(game.arePuzzlesDone[4] && !triggersSet[1])
        {
            baron.GetCoin();
            chessBookEffect.Play();
            chessTrigger.allowedToBeUsed = true;
            triggersSet[1] = true;
        }
        else if(game.arePuzzlesDone[5] && !triggersSet[2])
        {
            baron.GetCoin();
            chessNote.SetActive(true);
            chessTrigger.allowedToBeUsed = true;
            throwingTrigger.allowedToBeUsed = true;
            #region ACTIVATE_THROWING
            balls1.SetActive(true);
            balls2.SetActive(true);
            balls3.SetActive(true);
            buttons1.SetActive(true);
            buttons2.SetActive(true);
            buttons3.SetActive(true);
            button1Effect.Play();
            button2Effect.Play();
            button3Effect.Play();
            #endregion
            triggersSet[2] = true;
        }
        else if(game.arePuzzlesDone[6] && !itemsSet[0])
        {
            throwingBox.SetActive(true);
            DisturbanceHandler_DR.instance.TriggerDisturbance(DisturbanceHandler_DR.DisturbanceName.LAMPWOBBLE);
            itemsSet[0] = true;
        }
        else if(game.arePuzzlesDone[7] && (!itemsSet[1] || !triggersSet[3]))
        {
            hiddenMechDoc.SetActive(true);
            laptopScreen.SetActive(true);
            correctOrderTrigger.allowedToBeUsed = true;
            itemsSet[1] = true;
            triggersSet[3] = true;
        }
        else if(game.arePuzzlesDone[8] && !triggersSet[4])
        {
            baron.gameIsEnding = true;
            baron.AppearStill(baronRitualLocation.transform, 0);
            ritualTrigger.allowedToBeUsed = true;
            triggersSet[4] = true;
        }
    }
}
