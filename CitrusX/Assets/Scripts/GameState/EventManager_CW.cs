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
    private GameObject keypile1;
    private GameObject keypile2;
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
    ParticleSystem hiddenMechEffect;
    ParticleSystem bathroomNoteEffect;
    #endregion
    #region BOOLS
    internal bool[] triggersSet = { false, false, false, false, false, false };
    internal bool[] itemsSet = { false, false, false, false, false, false, false };
    internal bool[] disturbancesSet = { false, false };
    #endregion
    private GameTesting_CW game;
    private Cinematics_DR cinematics;

    #region Interacts
    private Interactable_DR ritualTable;
    private Interactable_DR gardenTable;
    private Interactable_DR chessTable;

    private Interactable_DR[] foodItems;

    private Interactable_DR[] chessPieces;

    private Interactable_DR scales;

    private Interactable_DR keypad;

    private Interactable_DR candles;
    #endregion

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
        hiddenMechEffect = GameObject.Find("Painting_AG").GetComponentInChildren<ParticleSystem>();
        bathroomNoteEffect = GameObject.Find("KeysNote").GetComponentInChildren<ParticleSystem>();
        laptopScreen = GameObject.Find("LaptopScreen");
        #endregion

        #region INITIATE_TRIGGERS
        ritualTrigger = GameObject.Find("RitualTrigger").GetComponent<TriggerScript_CW>();
        chessTrigger = GameObject.Find("ChessboardTrigger").GetComponent<TriggerScript_CW>();
        throwingTrigger = GameObject.Find("ThrowingTrigger").GetComponent<TriggerScript_CW>();
        correctOrderTrigger = GameObject.Find("CorrectOrderTrigger").GetComponent<TriggerScript_CW>();
        gardenTrigger = GameObject.Find("GardenTrigger").GetComponent<TriggerScript_CW>();
        #endregion

        #region Initiate Interacts
        GameObject[] foodItemGOs = GameObject.FindGameObjectsWithTag("FoodItem");
        foodItems = new Interactable_DR[foodItemGOs.Length];
        for(int i = 0; i < foodItemGOs.Length; i++)
        {
            foodItems[i] = foodItemGOs[i].GetComponent<Interactable_DR>();
        }

        GameObject[] chessPieceGOs = GameObject.FindGameObjectsWithTag("ChessPiece");
        chessPieces = new Interactable_DR[chessPieceGOs.Length];
        for (int i = 0; i < chessPieceGOs.Length; i++)
        {
            chessPieces[i] = chessPieceGOs[i].GetComponent<Interactable_DR>();
        }

        scales = GameObject.Find("ScalesInteract").GetComponent<Interactable_DR>();

        keypad = GameObject.FindGameObjectWithTag("Keypad").GetComponent<Interactable_DR>();

        candles = GameObject.Find("RitualCandles").GetComponent<Interactable_DR>();
        #endregion
    }
    private void Start()
    {
        game = GameTesting_CW.instance;
        #region ACTIVATE_OBJECTS
        if (SaveSystem_DR.instance.loaded)
        {
            throwingBox.SetActive(SaveSystem_DR.instance.loadedGD.throwingBoxActivated);
            hiddenMechDoc.SetActive(SaveSystem_DR.instance.loadedGD.hiddenMechNoteActivated);
            bathroomKeyPart1.SetActive(SaveSystem_DR.instance.loadedGD.bathroomKeyPartOneActive);
            bathroomKeyPart2.SetActive(SaveSystem_DR.instance.loadedGD.bathroomKeyPartTwoActive);
            safe.SetActive(SaveSystem_DR.instance.loadedGD.keypadTableActivated);
            balls1.SetActive(SaveSystem_DR.instance.loadedGD.ball1IsActive);
            balls2.SetActive(SaveSystem_DR.instance.loadedGD.ball2IsActive);
            balls3.SetActive(SaveSystem_DR.instance.loadedGD.ball3IsActive);
            chessNote.SetActive(SaveSystem_DR.instance.loadedGD.chessNoteActivated);
            keypadDoc.SetActive(SaveSystem_DR.instance.loadedGD.keypadNoteActive);
            laptopScreen.SetActive(SaveSystem_DR.instance.loadedGD.laptopOn);
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
            candles.gameObject.SetActive(false);
        }
        #endregion
    }
    private void Update()
    {
        if(game.arePuzzlesDone[0] && !triggersSet[5])
        {
            gardenTrigger.allowedToBeUsed = true;
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
            ritualTrigger.allowedToBeUsed = true;
            triggersSet[0] = true;
            bathroomNoteEffect.Play();
        }
        else if(game.arePuzzlesDone[1] && itemsSet[5] && !itemsSet[6])
        {
            bathroomKeyPart1.SetActive(true);
            bathroomKeyPart2.SetActive(true);
            bathroomKeyPart2.name = "Bathroom Key";
           
            itemsSet[6] = true;
        }
        else if(triggersSet[0] && !game.arePuzzlesDone[2] && itemsSet[6])
        {
            if(!bathroomKeyPart2.activeInHierarchy)
            {
                bathroomKeyPart1.name = "Bathroom Key Part 2";
                bathroomNoteEffect.Stop();
            }
            else if(!bathroomKeyPart1.activeInHierarchy)
            {
                bathroomKeyPart2.name = "Bathroom Key Part 2";
                bathroomNoteEffect.Stop();
            }
        }
        else if(game.arePuzzlesDone[2] && !itemsSet[2])
        {
            keypad.canInteractWith = true;

            baron.GetCoin();
            safe.SetActive(true);
            itemsSet[2] = true;
        }
        else if(game.arePuzzlesDone[3] && !itemsSet[3])
        {
            keypad.canInteractWith = false;
            for(int i = 0; i < foodItems.Length; i++)
            {
                foodItems[i].canInteractWith = true;
            }
            scales.canInteractWith = true;

            baron.GetCoin();
            keypadDoc.SetActive(true);
            scalesEffect.Play();
            itemsSet[3] = true;
        }
        else if(game.arePuzzlesDone[4] && !triggersSet[1])
        {
            for (int i = 0; i < foodItems.Length; i++)
            {
                foodItems[i].canInteractWith = false;
            }
            for (int i = 0; i < chessPieces.Length; i++)
            {
                chessPieces[i].canInteractWith = true;
            }

            baron.GetCoin();
            chessBookEffect.Play();
            chessTrigger.allowedToBeUsed = true;
            triggersSet[1] = true;
        }
        else if(game.arePuzzlesDone[5] && !triggersSet[2])
        {
            for (int i = 0; i < chessPieces.Length; i++)
            {
                chessPieces[i].canInteractWith = false;
            }
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
            hiddenMechEffect.Play();
            throwingBox.SetActive(true);
            DisturbanceHandler_DR.instance.TriggerDisturbance(DisturbanceHandler_DR.DisturbanceName.LAMPWOBBLE);
            itemsSet[0] = true;
        }
        else if(game.arePuzzlesDone[7] && (!itemsSet[1] || !triggersSet[3]))
        {
            hiddenMechEffect.Stop();
            hiddenMechDoc.SetActive(true);
            laptopScreen.SetActive(true);
            correctOrderTrigger.allowedToBeUsed = true;
            itemsSet[1] = true;
            triggersSet[3] = true;
        }
        else if(game.arePuzzlesDone[8] && !triggersSet[4])
        {
            candles.canInteractWith = true;
            baron.gameIsEnding = true;
            baron.AppearStill(baronRitualLocation.transform, 0);
            ritualTrigger.allowedToBeUsed = true;
            triggersSet[4] = true;
        }
    }
}
