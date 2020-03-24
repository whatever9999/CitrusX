/*
 * Dominique
 * 
 * Saves and Loads game data
 * There is a different save location according to if the game is played in the editor or as an executable
 * A binary formatter has been used to save a .dat file
 * There is a single file for the save
 * Atm saving is on F5
 */

/**
* \class SaveSystem_DR
* 
* \brief Saves positions of moveable objects and the state of the game into a .dat file and loads it upon start (if there is a save file)
* 
* When the player presses f5 the current state of the game is saved into a .dat file.
* When they load up the game again it will use that save file to load the game. If there is no save file the game will start at the beginning and when the player next saves one will be created.
* When the player completes the game their save file is deleted so the game will start from the beginning when they next play.
* 
* \author Dominique
* 
* \date Last Modified: 18/02/2020
*/

using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Collections;

public class SaveSystem_DR: MonoBehaviour
{
    public static SaveSystem_DR instance;

    private Text saveNotificationText;
    internal GameData_DR loadedGD;
    internal bool loaded;

    #region TransformsAndActiveStates
    internal Transform playerT;
    internal GameObject journalGO;
    internal Transform fuseboxT;
    internal Transform baronT;
    internal Transform chessBoardT;
    internal Transform gardenTableT;
    internal Transform ritualTableT;
    internal Transform bowlT;
    internal Transform candlesT;
    internal Transform coinsT;
    internal Transform saltT;
    internal Transform hiddenMechNoteT;
    internal Transform monitorT;
    internal Transform keypadT;
    internal Transform throwingBoxT;
    internal Transform ball1T;
    internal Transform ball2T;
    internal Transform ball3T;
    internal Transform weight1T;
    internal Transform weight2T;
    internal Transform weight3T;
    internal Transform keyHandle1T;
    internal Transform keyHandle2T;
    internal Transform keyHandle3T;
    internal Transform keyHandle4T;
    internal Transform keyBit2T;
    internal Transform keyBit3T;
    internal Transform jewellery;
    internal Transform pendant;
    internal Transform necklace;
    internal Transform bracelet;
    #endregion

    #region ScriptVariables
    internal Cinematics_DR cinematics;
    internal Interact_HR interact;
    internal InitiatePuzzles_CW initiatePuzzles;
    internal GameTesting_CW gameTesting;
    internal Baron_DR baron;
    internal WaterBowl_DR waterBowl;
    internal KeypadUI_DR keyPadUI;
    internal Journal_DR journal;
    internal EventManager_CW eventManager;
    internal Inventory_HR inventory;

    internal TriggerScript_CW chessTrigger;
    internal TriggerScript_CW correctOrderTrigger;
    internal TriggerScript_CW gardenTrigger;
    internal TriggerScript_CW hiddenMechanismTrigger;
    internal TriggerScript_CW ritualTrigger;
    internal TriggerScript_CW throwingTrigger;

    internal PutDown_HR ritualPutDown;
    internal PutDown_HR gardenPutDown;
    internal PutDown_HR chessPutDown;

    internal Table_CW ritualTable;
    internal Table_CW gardenTable;
    internal Table_CW chessTable;

    internal Paper_DR chessPaper;
    internal Paper_DR hiddenMechanismPaper;
    internal Paper_DR keypadPaper;
    internal Paper_DR keysPaper;

    internal Door_DR colourMatchingDoor;
    internal Door_DR correntOrderDoor;
    internal Door_DR leftFrontDoor;
    internal Door_DR rightFrontDoor;
    internal Door_DR pantryDoor;
    internal Door_DR gymDoor;
    internal Door_DR garageDoor;
    internal Door_DR downstairsBathroomDoor;
    internal Door_DR diningRoomDoor;
    internal Door_DR safeDoor;
    internal Door_DR hiddenMechDoor;
    internal Door_DR chessDoor;
    internal Door_DR scalesDoor;

    internal HoldandThrow_HR ball1;
    internal HoldandThrow_HR ball2;
    internal HoldandThrow_HR ball3;
    internal HoldandThrow_HR weight1;
    internal HoldandThrow_HR weight2;
    internal HoldandThrow_HR weight3;
    internal HoldandThrow_HR keyHandle1;
    internal HoldandThrow_HR keyHandle2;
    internal HoldandThrow_HR keyHandle3;
    internal HoldandThrow_HR keyHandle4;
    internal HoldandThrow_HR keyBit2;
    internal HoldandThrow_HR keyBit3;

    internal BallButtonLogic_HR button1;
    internal BallButtonLogic_HR button2;
    internal BallButtonLogic_HR button3;

    internal SetUpRitual_CW setUpRitual;
    internal HiddenMech_CW hiddenMech;
    internal Fusebox_CW fusebox;
    internal CorrectOrder_CW correctOrder;
    internal ColourMatchingPuzzle_CW colourMatchingPuzzle;
    internal ChessBoard_DR chessBoard;
    internal ScalesPuzzleScript_AG scalesPuzzleScript;

    internal ChessPiece_DR knight;
    internal ChessPiece_DR king;
    internal ChessPiece_DR queen;
    internal ChessPiece_DR pawn;
    #endregion

    /// <summary>
    /// Inititalise variables and load the game
    /// </summary>
    private void Awake()
    {
        instance = this;

        saveNotificationText = GameObject.Find("SaveNotification").GetComponent<Text>();

        #region Initialisations
        playerT = GameObject.Find("FPSController").GetComponent<Transform>();
        journalGO = GameObject.Find("JournalBackground");
        fuseboxT = GameObject.Find("Fusebox").GetComponent<Transform>();
        baronT = GameObject.Find("Baron").GetComponent<Transform>();
        chessBoardT = GameObject.Find("ChessBoard").GetComponent<Transform>();
        gardenTableT = GameObject.Find("GardenTable").GetComponent<Transform>();
        ritualTableT = GameObject.Find("RitualTable").GetComponent<Transform>();
        bowlT = GameObject.Find("Bowl").GetComponent<Transform>();
        candlesT = GameObject.Find("Candles").GetComponent<Transform>();
        coinsT = GameObject.Find("Coins").GetComponent<Transform>();
        saltT = GameObject.Find("Salt").GetComponent<Transform>();
        hiddenMechNoteT = GameObject.Find("HiddenMechNote").GetComponent<Transform>();
        monitorT = GameObject.Find("Monitor").GetComponent<Transform>();
        keypadT = GameObject.Find("KeypadUI").GetComponent<Transform>();
        throwingBoxT = GameObject.Find("ThrowingBox").GetComponent<Transform>();
        ball1T = GameObject.Find("1Ball").GetComponent<Transform>();
        ball2T = GameObject.Find("2Ball").GetComponent<Transform>();
        ball3T = GameObject.Find("3Ball").GetComponent<Transform>();
        weight1T = GameObject.Find("Weight200").GetComponent<Transform>();
        weight2T = GameObject.Find("Weight400").GetComponent<Transform>();
        weight3T = GameObject.Find("Weight500").GetComponent<Transform>();
        keyHandle1T = GameObject.Find("KeyHandle1").GetComponent<Transform>();
        keyHandle2T = GameObject.Find("KeyHandle2").GetComponent<Transform>();
        keyHandle3T = GameObject.Find("KeyHandle3").GetComponent<Transform>();
        keyHandle4T = GameObject.Find("KeyHandle4").GetComponent<Transform>();
        keyBit2T = GameObject.Find("KeyBit1").GetComponent<Transform>();
        keyBit3T = GameObject.Find("KeyBit2").GetComponent<Transform>();
        jewellery = GameObject.Find("Jewellery Box").GetComponent<Transform>();
        pendant = GameObject.Find("Pendant").GetComponent<Transform>();
        necklace = GameObject.Find("Necklace").GetComponent<Transform>();
        bracelet = GameObject.Find("Bracelet").GetComponent<Transform>();

        cinematics = GameObject.Find("Cinematics").GetComponent<Cinematics_DR>();
        interact = GameObject.Find("FPSController").GetComponentInChildren<Interact_HR>();
        initiatePuzzles = GameObject.Find("FPSController").GetComponentInChildren<InitiatePuzzles_CW>();
        gameTesting = GameObject.Find("FPSController").GetComponentInChildren<GameTesting_CW>();
        baron = GameObject.Find("Baron").GetComponent<Baron_DR>();
        waterBowl = GameObject.Find("WaterBowl").GetComponent<WaterBowl_DR>();
        keyPadUI = GameObject.Find("KeypadUI").GetComponent<KeypadUI_DR>();
        journal = GameObject.Find("FPSController").GetComponentInChildren<Journal_DR>();
        eventManager = GameObject.Find("Managers").GetComponent<EventManager_CW>();
        inventory = GameObject.Find("FPSController").GetComponentInChildren<Inventory_HR>();

        chessTrigger = GameObject.Find("ChessboardTrigger").GetComponent<TriggerScript_CW>();
        correctOrderTrigger = GameObject.Find("CorrectOrderTrigger").GetComponent<TriggerScript_CW>();
        gardenTrigger = GameObject.Find("GardenTrigger").GetComponent<TriggerScript_CW>();
        hiddenMechanismTrigger = GameObject.Find("HiddenMechTrigger").GetComponent<TriggerScript_CW>();
        ritualTrigger = GameObject.Find("RitualTrigger").GetComponent<TriggerScript_CW>();
        throwingTrigger = GameObject.Find("ThrowingTrigger").GetComponent<TriggerScript_CW>();

        ritualPutDown = GameObject.Find("RitualTable").GetComponent<PutDown_HR>();
        gardenPutDown = GameObject.Find("GardenTable").GetComponent<PutDown_HR>();
        chessPutDown = GameObject.Find("ChessBoard").GetComponent<PutDown_HR>();

        ritualTable = GameObject.Find("RitualTable").GetComponent<Table_CW>();
        gardenTable = GameObject.Find("GardenTable").GetComponent<Table_CW>();
        chessTable = GameObject.Find("ChessBoard").GetComponent<Table_CW>();

        chessPaper = GameObject.Find("Chess Note").GetComponent<Paper_DR>();
        hiddenMechanismPaper = GameObject.Find("HiddenMechNote").GetComponent<Paper_DR>();
        keypadPaper = GameObject.Find("KeyPadDoc").GetComponent<Paper_DR>();
        keysPaper = GameObject.Find("KeysNote").GetComponent<Paper_DR>();

        colourMatchingDoor = GameObject.Find("ColourMatchingDoor").GetComponentInChildren<Door_DR>();
        correntOrderDoor = GameObject.Find("CorrectOrderDoor").GetComponentInChildren<Door_DR>();
        leftFrontDoor = GameObject.Find("LeftFrontDoor").GetComponentInChildren<Door_DR>();
        rightFrontDoor = GameObject.Find("RightFrontDoor").GetComponentInChildren<Door_DR>();
        pantryDoor = GameObject.Find("PantryDoor").GetComponentInChildren<Door_DR>();
        gymDoor = GameObject.Find("GymDoor").GetComponentInChildren<Door_DR>();
        garageDoor = GameObject.Find("GarageDoor").GetComponentInChildren<Door_DR>();
        downstairsBathroomDoor = GameObject.Find("DownstairsBathroomDoor").GetComponentInChildren<Door_DR>();
        diningRoomDoor = GameObject.Find("DiningRoomDoor").GetComponentInChildren<Door_DR>();
        safeDoor = GameObject.Find("Safe").GetComponentInChildren<Door_DR>();
        hiddenMechDoor = GameObject.Find("HiddenMechDoor").GetComponentInChildren<Door_DR>();
        chessDoor = GameObject.Find("ChessDoor").GetComponentInChildren<Door_DR>();
        scalesDoor = GameObject.Find("ScalesDoor").GetComponentInChildren<Door_DR>();

        ball1 = GameObject.Find("1Ball").GetComponent<HoldandThrow_HR>();
        ball2 = GameObject.Find("2Ball").GetComponent<HoldandThrow_HR>();
        ball3 = GameObject.Find("3Ball").GetComponent<HoldandThrow_HR>();
        weight1 = GameObject.Find("Weight200").GetComponent<HoldandThrow_HR>();
        weight2 = GameObject.Find("Weight400").GetComponent<HoldandThrow_HR>();
        weight3 = GameObject.Find("Weight500").GetComponent<HoldandThrow_HR>();
        keyHandle1 = GameObject.Find("KeyHandle1").GetComponent<HoldandThrow_HR>();
        keyHandle2 = GameObject.Find("KeyHandle2").GetComponent<HoldandThrow_HR>();
        keyHandle3 = GameObject.Find("KeyHandle3").GetComponent<HoldandThrow_HR>();
        keyHandle4 = GameObject.Find("KeyHandle4").GetComponent<HoldandThrow_HR>();
        keyBit2 = GameObject.Find("KeyBit1").GetComponent<HoldandThrow_HR>();
        keyBit3 = GameObject.Find("KeyBit2").GetComponent<HoldandThrow_HR>();

        button1 = GameObject.Find("1Button").GetComponentInChildren<BallButtonLogic_HR>();
        button2 = GameObject.Find("2Button").GetComponentInChildren<BallButtonLogic_HR>();
        button3 = GameObject.Find("3Button").GetComponentInChildren<BallButtonLogic_HR>();

        setUpRitual = GameObject.Find("FPSController").GetComponentInChildren<SetUpRitual_CW>();
        hiddenMech = GameObject.Find("FPSController").GetComponentInChildren<HiddenMech_CW>();
        GameObject fuseboxUI = GameObject.Find("FuseboxUI");
        fusebox = fuseboxUI.GetComponent<Fusebox_CW>();
        correctOrder = GameObject.Find("CorrectOrderUI").GetComponent<CorrectOrder_CW>();
        colourMatchingPuzzle = GameObject.Find("ColourMatchingDoor").GetComponent<ColourMatchingPuzzle_CW>();
        chessBoard = GameObject.Find("ChessBoard").GetComponent<ChessBoard_DR>();
        scalesPuzzleScript = GameObject.Find("Scales").GetComponent<ScalesPuzzleScript_AG>();

        knight = GameObject.Find("BoardKnight").GetComponent<ChessPiece_DR>();
        king = GameObject.Find("BoardKing").GetComponent<ChessPiece_DR>();
        queen = GameObject.Find("BoardQueen").GetComponent<ChessPiece_DR>();
        pawn = GameObject.Find("BoardPawn").GetComponent<ChessPiece_DR>();
        pawn.gameObject.SetActive(false);
        #endregion

        //The character controller stops the player's position from being changed so it's temporarily disabled
        CharacterController characterController = playerT.GetComponent<CharacterController>();
        characterController.enabled = false;
        Load();
        characterController.enabled = true;
    }

    /// <summary>
    /// If the player presses F5 they save the game
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            Save();
        }
    }

    /// <summary>
    /// Use a binary formatter to create a save file at the Application.persistentDataPath + "/save.dat"
    /// </summary>
    public void Save()
    {
        saveNotificationText.text = "Saving...";
        //Save data path
#if UNITY_EDITOR
        string path = Application.dataPath + "/save.dat";
#else
        string path = Application.persistentDataPath + "/save.dat";
#endif
        FileStream file;

        //If the file exists then rewrite it, otherwise make a new file
        if (File.Exists(path))
        {
            file = File.OpenWrite(path);
        }
        else
        {
            file = File.Create(path);
        }

        //Save the game state data into the file
        GameData_DR gameData = new GameData_DR(this);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, gameData);
        file.Close();

        StartCoroutine(NotifyPlayer("Save Complete"));
    }

    private IEnumerator NotifyPlayer(string notification)
    {
        saveNotificationText.text = notification;
        yield return new WaitForSeconds(2);
        saveNotificationText.text = "";
    }

    /// <summary>
    /// Load the save file from the Application.persistentDataPath + "/save.dat" by collecting the data and placing it in the appropriate variables
    /// </summary>
    public void Load()
    {
       //Load data path
#if UNITY_EDITOR
       string path = Application.dataPath + "/save.dat";
#else
       string path = Application.persistentDataPath + "/save.dat";
#endif

        FileStream file;

        //If the file exists then open it, otherwise there is no file to be read
        if (File.Exists(path))
        {
            file = File.OpenRead(path);
            loaded = true;
        }
        else
        {
            //No save file available
            return;
        }

        //Deserialize the file
        BinaryFormatter bf = new BinaryFormatter();
        GameData_DR gameData = (GameData_DR)bf.Deserialize(file);
        file.Close();

        //Put file data into variables
        UpdateVariables(gameData);

        loadedGD = gameData;

        StartCoroutine(NotifyPlayer("Loaded"));
    }

    /// <summary>
    /// Takes values from a GameData class and puts them in the appropriate variables within the current game.
    /// </summary>
    /// <param name="GD - a class that stores all of the data for the game that needs to be saved"></param>
    public void UpdateVariables(GameData_DR GD)
    {
        //Player
        playerT.position = new Vector3(GD.playerPosition[0], GD.playerPosition[1], GD.playerPosition[2]);
        playerT.rotation = new Quaternion(GD.playerRotation[0], GD.playerRotation[1], GD.playerRotation[2], GD.playerRotation[3]);

        //JournalUI
        Text[] journalTexts = journalGO.GetComponentsInChildren<Text>();
        for (int i = 0; i < journalTexts.Length; i++)
        {
            switch (journalTexts[i].name)
            {
                case "Task":
                    journalTexts[i].text = GD.journalTasks[0];
                    break;
                case "Task (1)":
                    journalTexts[i].text = GD.journalTasks[1];
                    break;
                case "Task (2)":
                    journalTexts[i].text = GD.journalTasks[2];
                    break;
                case "Task (3)":
                    journalTexts[i].text = GD.journalTasks[3];
                    break;
                case "Task (4)":
                    journalTexts[i].text = GD.journalTasks[4];
                    break;
                case "JournalLogs":
                    journalTexts[i].text = GD.journalLog;
                    break;
            }
        }
        Vector2 journalLogSize = new Vector2(GD.journalBoxSize[0], GD.journalBoxSize[1]);
        GameObject.Find("JournalLogs").GetComponent<RectTransform>().sizeDelta = journalLogSize;
        //Make sure the text box width doesn't affect the content box width (otherwise text moves to the right)
        journalLogSize.x = 0;
        GameObject.Find("JournalContentBox").GetComponent<RectTransform>().sizeDelta = journalLogSize;

        //Baron
        baronT.position = new Vector3(GD.baronPosition[0], GD.baronPosition[1], GD.baronPosition[2]);
        baronT.rotation = new Quaternion(GD.baronRotation[0], GD.baronRotation[1], GD.baronRotation[2], GD.baronRotation[3]);

        //Chessboard
        chessBoardT.gameObject.SetActive(GD.chessTableActivated);

        Transform[] chessPieces = chessBoard.GetComponentsInChildren<Transform>();
        for (int i = 0; i < chessPieces.Length; i++)
        {
            switch (chessPieces[i].name)
            {
                case "BoardKnight":
                    chessPieces[i].rotation = new Quaternion(GD.chessKnightRotation[0], GD.chessKnightRotation[1], GD.chessKnightRotation[2], GD.chessKnightRotation[3]);
                    break;
                case "BoardKing":
                    chessPieces[i].rotation = new Quaternion(GD.chessKingRotation[0], GD.chessKingRotation[1], GD.chessKingRotation[2], GD.chessKingRotation[3]);
                    break;
                case "BoardQueen":
                    chessPieces[i].rotation = new Quaternion(GD.chessQueenRotation[0], GD.chessQueenRotation[1], GD.chessQueenRotation[2], GD.chessQueenRotation[3]);
                    break;
                case "BoardPawn":
                    chessPieces[i].rotation = new Quaternion(GD.chessPawnRotation[0], GD.chessPawnRotation[1], GD.chessPawnRotation[2], GD.chessPawnRotation[3]);
                    break;
            }
        }

        //GardenTable
        gardenTableT.gameObject.SetActive(GD.gardenTableActivated);

        //RitualTable
        ritualTableT.gameObject.SetActive(GD.ritualTableActivated);

        //RitualItems
        bowlT.gameObject.SetActive(GD.bowlNotPickedUp);
        candlesT.gameObject.SetActive(GD.candlesNotPickedUp);
        coinsT.gameObject.SetActive(GD.coinsNotPickedUp);
        saltT.gameObject.SetActive(GD.saltNotPickedUp);

        //Monitor
        saltT.gameObject.SetActive(GD.monitorOn);
        for(int i = 0; i < monitorT.childCount; i++)
        {
            monitorT.GetChild(i).gameObject.SetActive(GD.monitorOn);
        }

        //KeypadTable
        keypadT.gameObject.SetActive(GD.keypadTableActivated);

        //Balls
        ball1T.position = new Vector3(GD.ball1TPosition[0], GD.ball1TPosition[1], GD.ball1TPosition[2]);

        ball2T.position = new Vector3(GD.ball2TPosition[0], GD.ball2TPosition[1], GD.ball2TPosition[2]);

        ball3T.position = new Vector3(GD.ball3TPosition[0], GD.ball3TPosition[1], GD.ball3TPosition[2]);

        //Weights
        weight1T.position = new Vector3(GD.weight1TPosition[0], GD.weight1TPosition[1], GD.weight1TPosition[2]);

        weight2T.position = new Vector3(GD.weight2TPosition[0], GD.weight2TPosition[1], GD.weight2TPosition[2]);

        weight3T.position = new Vector3(GD.weight3TPosition[0], GD.weight3TPosition[1], GD.weight3TPosition[2]);

        //KeyPieces
        keyHandle1T.position = new Vector3(GD.keyHandle1TPosition[0], GD.keyHandle1TPosition[1], GD.keyHandle1TPosition[2]);

        keyHandle2T.position = new Vector3(GD.keyHandle2TPosition[0], GD.keyHandle2TPosition[1], GD.keyHandle2TPosition[2]);

        keyHandle3T.position = new Vector3(GD.keyHandle3TPosition[0], GD.keyHandle3TPosition[1], GD.keyHandle3TPosition[2]);

        keyHandle4T.position = new Vector3(GD.keyHandle4TPosition[0], GD.keyHandle4TPosition[1], GD.keyHandle4TPosition[2]);

        keyBit2T.position = new Vector3(GD.keyBit2TPosition[0], GD.keyBit2TPosition[1], GD.keyBit2TPosition[2]);

        keyBit3T.position = new Vector3(GD.keyBit3TPosition[0], GD.keyBit3TPosition[1], GD.keyBit3TPosition[2]);

        //JewelleryItems
        jewellery.gameObject.SetActive(GD.jewelleryNotPickedUp);
        pendant.gameObject.SetActive(GD.pendantNotPickedUp);
        necklace.gameObject.SetActive(GD.necklaceNotPickedUp);
        bracelet.gameObject.SetActive(GD.braceletNotPickedUp);

        //Inventory
        for (int i = 0; i < GD.inventory.Length; i++)
        {
            inventory.AddItem(GD.inventory[i]);
        }

        //Cinematics
        cinematics.playStartCinematic = GD.playStartCinematic;

        //Interact
        interact.numberCoinsCollected = GD.numberCoinsCollected;

        //InitiatePuzzles
        initiatePuzzles.ballCounter = GD.ballCounter;
        initiatePuzzles.voiceovers = GD.puzzleVoiceovers;
        initiatePuzzles.monitorInteractions = GD.monitorInteractions;
        initiatePuzzles.monitorInteractionsUsed = GD.monitorInteractionsUsed;

        //GameTesting
        gameTesting.setUpPuzzle = GD.setUpPuzzle;
        gameTesting.arePuzzlesDone = GD.arePuzzlesDone;
        gameTesting.cutscenes = GD.cutscenes;
        gameTesting.cutscenesDone = GD.cutscenesDone;
        gameTesting.controlsSeen = GD.controlsSeen;

        //Baron
        baron.appearanceTimer = GD.appearanceTimer;

        //WaterBowl
        for(int i = waterBowl.numberOfCoins; i > GD.coinsLeft; i--)
        {
            waterBowl.RemoveCoin();
        }

        //KeypadUI
        keyPadUI.interactedWithSafe = GD.interactedWithSafe;
        keyPadUI.hasAlreadyInteractedWithSafe = GD.hasAlreadyInteractedWithSafe;
        keyPadUI.playerInteractsWithDoc = GD.playerInteractsWithDoc;
        keyPadUI.voiceovers = GD.keypadVoiceovers;
        keyPadUI.isActive = GD.isActive;

        //EventManager
        eventManager.triggersSet = GD.triggersSet;
        eventManager.itemsSet = GD.itemsSet;
        eventManager.disturbancesSet = GD.disturbancesSet;

        //TriggerScripts
        chessTrigger.allowedToBeUsed = GD.chessAllowedToBeUsed;
        chessTrigger.activated = GD.chessActivated;
        correctOrderTrigger.allowedToBeUsed = GD.correctOrderAllowedToBeUsed;
        correctOrderTrigger.activated = GD.correctOrderActivated;
        gardenTrigger.allowedToBeUsed = GD.gardenAllowedToBeUsed;
        gardenTrigger.activated = GD.gardenActivated;
        hiddenMechanismTrigger.allowedToBeUsed = GD.hiddenMechanismAllowedToBeUsed;
        hiddenMechanismTrigger.activated = GD.hiddenMechanismActivated;
        ritualTrigger.allowedToBeUsed = GD.ritualAllowedToBeUsed;
        ritualTrigger.activated = GD.ritualActivated;
        throwingTrigger.allowedToBeUsed = GD.throwingAllowedToBeUsed;
        throwingTrigger.activated = GD.throwingActivated;

        //PutDown
        ritualPutDown.SetBeenUsed(GD.ritualPDBeenUsed);
        gardenPutDown.SetBeenUsed(GD.gardenPDBeenUsed);
        chessPutDown.SetBeenUsed(GD.chessPDBeenUsed);

        //Table
        ritualTable.hasBeenPlaced = GD.ritualTHasBeenPlaced;
        gardenTable.hasBeenPlaced = GD.gardenTHasBeenPlaced;
        chessTable.hasBeenPlaced = GD.chessTHasBeenPlaced;

        //Paper
        chessPaper.hasBeenRead = GD.chessPHasBeenRead;
        hiddenMechanismPaper.hasBeenRead = GD.hiddenMechanismPHasBeenRead;
        keypadPaper.hasBeenRead = GD.keypadPHasBeenRead;
        keysPaper.hasBeenRead = GD.keysPHasBeenRead;

        //Door
        colourMatchingDoor.unlocked = GD.colourMatchingDoorUnlocked;
        colourMatchingDoor.isOpen = GD.colourMatchingDoorIsOpen;
        correntOrderDoor.unlocked = GD.correntOrderDoorUnlocked;
        correntOrderDoor.isOpen = GD.correntOrderDoorIsOpen;
        leftFrontDoor.unlocked = GD.leftFrontDoorUnlocked;
        leftFrontDoor.isOpen = GD.leftFrontDoorIsOpen;
        rightFrontDoor.unlocked = GD.rightFrontDoorUnlocked;
        rightFrontDoor.isOpen = GD.rightFrontDoorIsOpen;
        pantryDoor.unlocked = GD.pantryDoorUnlocked;
        pantryDoor.isOpen = GD.pantryDoorIsOpen;
        gymDoor.unlocked = GD.gymDoorUnlocked;
        gymDoor.isOpen = GD.gymDoorIsOpen;
        garageDoor.unlocked = GD.garageDoorUnlocked;
        garageDoor.isOpen = GD.garageDoorIsOpen;
        downstairsBathroomDoor.unlocked = GD.downstairsBathroomDoorUnlocked;
        downstairsBathroomDoor.isOpen = GD.downstairsBathroomDoorIsOpen;
        diningRoomDoor.unlocked = GD.diningRoomDoorUnlocked;
        diningRoomDoor.isOpen = GD.diningRoomDoorIsOpen;
        safeDoor.unlocked = GD.safeDoorUnlocked;
        safeDoor.isOpen = GD.safeDoorIsOpen;
        scalesDoor.unlocked = GD.scalesDoorUnlocked;
        scalesDoor.isOpen = GD.scalesDoorIsOpen;
        hiddenMechDoor.unlocked = GD.hiddenMechDoorUnlocked;
        hiddenMechDoor.isOpen = GD.hiddenMechDoorIsOpen;
        chessDoor.unlocked = GD.chessDoorUnlocked;
        chessDoor.isOpen = GD.chessDoorIsOpen;

        //HoldandThrow
        ball1.canHold = GD.canHoldBall1;
        ball1.isFirstTime = GD.ball1IsFirstTime;
        ball2.canHold = GD.canHoldBall2;
        ball2.isFirstTime = GD.ball2IsFirstTime;
        ball3.canHold = GD.canHoldBall3;
        ball3.isFirstTime = GD.ball3IsFirstTime;

        weight1.canHold = GD.canHoldWeight1;
        weight1.isFirstTime = GD.weight1IsFirstTime;
        weight2.canHold = GD.canHoldWeight2;
        weight2.isFirstTime = GD.weight2IsFirstTime;
        weight3.canHold = GD.canHoldWeight3;
        weight3.isFirstTime = GD.weight3IsFirstTime;

        keyHandle1.canHold = GD.canHoldKeyHandle1;
        keyHandle1.isFirstTime = GD.keyHandle1IsFirstTime;
        keyHandle2.canHold = GD.canHoldKeyHandle2;
        keyHandle2.isFirstTime = GD.keyHandle2IsFirstTime;
        keyHandle3.canHold = GD.canHoldKeyHandle3;
        keyHandle3.isFirstTime = GD.keyHandle3IsFirstTime;
        keyHandle4.canHold = GD.canHoldKeyHandle4;
        keyHandle4.isFirstTime = GD.keyHandle4IsFirstTime;

        keyBit2.canHold = GD.canHoldKeyBit2;
        keyBit2.isFirstTime = GD.keyBit2IsFirstTime;
        keyBit3.canHold = GD.canHoldKeyBit3;
        keyBit3.isFirstTime = GD.keyBit3IsFirstTime;

        //BallButtonLogic
        button1.isActive = GD.button1IsActive;
        button2.isActive = GD.button2IsActive;
        button3.isActive = GD.button3IsActive;

        //SetUpRitual
        setUpRitual.ritualSteps = GD.ritualSteps;
        setUpRitual.isActive = GD.ritualIsActive;
        setUpRitual.voiceovers = GD.ritualVoiceovers;

        //HiddenMech
        hiddenMech.isActive = GD.hiddenMechIsActive;
        //Fusebox
        fusebox.isFuseboxSolved = GD.isFuseboxSolved;
        fusebox.voiceovers = GD.fuseboxVoiceovers;
        fusebox.isActive = GD.fuseboxActive;
        //CorrectOrder
        correctOrder.isActive = GD.correctOrderIsActive;
        correctOrder.whichRound = GD.correctOrderWhichRound;
        //ColourMatchingPuzzle
        colourMatchingPuzzle.isActive = GD.colourMatchingPuzzleIsActive;
        colourMatchingPuzzle.isDoorInteractedWith = GD.isDoorInteractedWith;
        colourMatchingPuzzle.hasKeyPart1 = GD.hasKeyPart1;
        colourMatchingPuzzle.hasKeyPart2 = GD.hasKeyPart2;
        colourMatchingPuzzle.voiceovers = GD.colourMatchingVoiceovers;
        //Chessboard
        chessBoard.isActive = GD.chessBoardIsActive;
        //ScalesPuzzleScript
        scalesPuzzleScript.isActive = GD.scalesPuzzleIsActive;
        scalesPuzzleScript.isComplete = GD.scalesPuzzleIsComplete;

        //ChessPieces
        while(knight.currentPosition != GD.knightCurrentPosition)
        {
            knight.Rotate();
        }
        while (king.currentPosition != GD.knightCurrentPosition)
        {
            king.Rotate();
        }
        while (queen.currentPosition != GD.knightCurrentPosition)
        {
            queen.Rotate();
        }
        while (pawn.currentPosition != GD.knightCurrentPosition)
        {
            pawn.Rotate();
        }
    }
}

/**
* \class GameData_DR
* 
* \brief Holds all variables that need to be saved for the player to restart from their current point in the game
* 
* Contains variables that need to be saved in order for the game to be loaded in the same state.
* To be initialised it takes in all the classes that have data that needs to be saved and uses these classes to initialise its variables.
* GameData_DR gameData = new GameData_DR(playerTransform) -> this object will contain the player's current position to be formatted with a binary formatter and written to a file.
* 
* \author Dominique
* 
* \date Last Modified: 18/02/2020
*/
[System.Serializable]
public class GameData_DR
{
    #region VariablesToSave
    //Player
    internal float[] playerPosition = new float[3];
    internal float[] playerRotation = new float[4];
    //JournalUI
    internal string[] journalTasks;
    internal string journalLog;
    internal float[] journalBoxSize = new float[2];
    //Baron
    internal bool baronActive;
    internal float[] baronPosition = new float[3];
    internal float[] baronRotation = new float[4];
    //Chessboard
    internal bool chessTableActivated;
    internal float[] chessPawnRotation;
    internal float[] chessKnightRotation;
    internal float[] chessKingRotation;
    internal float[] chessQueenRotation;
    //GardenTable
    internal bool gardenTableActivated;
    //RitualTable
    internal bool ritualTableActivated;
    //RitualItems
    internal bool bowlNotPickedUp;
    internal bool candlesNotPickedUp;
    internal bool coinsNotPickedUp;
    internal bool saltNotPickedUp;
    //HiddenMechNote
    internal bool hiddenMechNoteActivated;
    //Monitor
    internal bool monitorOn;
    //Keypad
    internal bool keypadTableActivated;
    //ThrowingBox
    internal bool throwingBoxActivated;
    //Balls
    internal float[] ball1TPosition = new float[3];
    internal float[] ball1TRotation = new float[4];
    internal float[] ball2TPosition = new float[3];
    internal float[] ball2TRotation = new float[4];
    internal float[] ball3TPosition = new float[3];
    internal float[] ball3TRotation = new float[4];
    //Weights
    internal float[] weight1TPosition = new float[3];
    internal float[] weight1TRotation = new float[4];
    internal float[] weight2TPosition = new float[3];
    internal float[] weight2TRotation = new float[4];
    internal float[] weight3TPosition = new float[3];
    internal float[] weight3TRotation = new float[4];
    //KeyPieces
    internal float[] keyHandle1TPosition = new float[3];
    internal float[] keyHandle1TRotation = new float[4];
    internal float[] keyHandle2TPosition = new float[3];
    internal float[] keyHandle2TRotation = new float[4];
    internal float[] keyHandle3TPosition = new float[3];
    internal float[] keyHandle3TRotation = new float[4];
    internal float[] keyHandle4TPosition = new float[3];
    internal float[] keyHandle4TRotation = new float[4];
    internal float[] keyBit2TPosition = new float[3];
    internal float[] keyBit2TRotation = new float[4];
    internal float[] keyBit3TPosition = new float[3];
    internal float[] keyBit3TRotation = new float[4];
    //JewelleryItems
    internal bool jewelleryNotPickedUp;
    internal bool pendantNotPickedUp;
    internal bool necklaceNotPickedUp;
    internal bool braceletNotPickedUp;
    //Inventory
    internal Inventory_HR.Names[] inventory;

    //Cinematics
    internal bool playStartCinematic;
    //Interact
    internal int numberCoinsCollected;
    //InitiatePuzzles
    internal int ballCounter;
    internal bool[] puzzleVoiceovers;
    internal bool[] monitorInteractions;
    internal bool[] monitorInteractionsUsed;
    //GameTesting
    internal bool[] setUpPuzzle;
    internal bool[] arePuzzlesDone;
    internal bool[] cutscenes;
    internal bool[] cutscenesDone;
    internal bool controlsSeen;
    //Baron
    internal float appearanceTimer;
    //Water Bowl
    internal int coinsLeft;
    //KeypadUI
    internal bool interactedWithSafe;
    internal bool hasAlreadyInteractedWithSafe;
    internal bool playerInteractsWithDoc;
    internal bool[] keypadVoiceovers;
    internal bool isActive;
    //EventManager
    internal bool[] triggersSet;
    internal bool[] itemsSet;
    internal bool[] disturbancesSet;

    //TriggerScripts
    internal bool chessAllowedToBeUsed;
    internal bool chessActivated;
    internal bool correctOrderAllowedToBeUsed;
    internal bool correctOrderActivated;
    internal bool gardenAllowedToBeUsed;
    internal bool gardenActivated;
    internal bool hiddenMechanismAllowedToBeUsed;
    internal bool hiddenMechanismActivated;
    internal bool ritualAllowedToBeUsed;
    internal bool ritualActivated;
    internal bool throwingAllowedToBeUsed;
    internal bool throwingActivated;

    //PutDown
    internal bool ritualPDBeenUsed;
    internal bool gardenPDBeenUsed;
    internal bool chessPDBeenUsed;

    //Table
    internal bool ritualTHasBeenPlaced;
    internal bool gardenTHasBeenPlaced;
    internal bool chessTHasBeenPlaced;

    //Paper
    internal bool chessPHasBeenRead;
    internal bool hiddenMechanismPHasBeenRead;
    internal bool keypadPHasBeenRead;
    internal bool keysPHasBeenRead;

    //Door
    internal bool colourMatchingDoorUnlocked;
    internal bool colourMatchingDoorIsOpen;
    internal bool correntOrderDoorUnlocked;
    internal bool correntOrderDoorIsOpen;
    internal bool leftFrontDoorUnlocked;
    internal bool leftFrontDoorIsOpen;
    internal bool rightFrontDoorUnlocked;
    internal bool rightFrontDoorIsOpen;
    internal bool pantryDoorUnlocked;
    internal bool pantryDoorIsOpen;
    internal bool gymDoorUnlocked;
    internal bool gymDoorIsOpen;
    internal bool garageDoorUnlocked;
    internal bool garageDoorIsOpen;
    internal bool downstairsBathroomDoorUnlocked;
    internal bool downstairsBathroomDoorIsOpen;
    internal bool diningRoomDoorUnlocked;
    internal bool diningRoomDoorIsOpen;
    internal bool safeDoorUnlocked;
    internal bool safeDoorIsOpen;
    internal bool scalesDoorUnlocked;
    internal bool scalesDoorIsOpen;
    internal bool chessDoorUnlocked;
    internal bool chessDoorIsOpen;
    internal bool hiddenMechDoorUnlocked;
    internal bool hiddenMechDoorIsOpen;

    //HoldandThrow
    internal bool canHoldBall1;
    internal bool ball1IsFirstTime;
    internal bool canHoldBall2;
    internal bool ball2IsFirstTime;
    internal bool canHoldBall3;
    internal bool ball3IsFirstTime;

    internal bool canHoldWeight1;
    internal bool weight1IsFirstTime;
    internal bool canHoldWeight2;
    internal bool weight2IsFirstTime;
    internal bool canHoldWeight3;
    internal bool weight3IsFirstTime;

    internal bool canHoldKeyHandle1;
    internal bool keyHandle1IsFirstTime;
    internal bool canHoldKeyHandle2;
    internal bool keyHandle2IsFirstTime;
    internal bool canHoldKeyHandle3;
    internal bool keyHandle3IsFirstTime;
    internal bool canHoldKeyHandle4;
    internal bool keyHandle4IsFirstTime;

    internal bool canHoldKeyBit2;
    internal bool keyBit2IsFirstTime;
    internal bool canHoldKeyBit3;
    internal bool keyBit3IsFirstTime;

    //BallButtonLogic
    internal bool button1IsActive;
    internal bool button2IsActive;
    internal bool button3IsActive;

    //SetUpRitual
    internal bool[] ritualSteps;
    internal bool ritualIsActive;
    internal bool[] ritualVoiceovers;

    //HiddenMech
    internal bool hiddenMechIsActive;
    //Fusebox
    internal bool isFuseboxSolved;
    internal bool[] fuseboxVoiceovers;
    internal bool fuseboxActive;
    //CorrectOrder
    internal bool correctOrderIsActive;
    internal bool[] correctOrderWhichRound;
    //ColourMatchingPuzzle
    internal bool colourMatchingPuzzleIsActive;
    internal bool[] isDoorInteractedWith;
    internal bool hasKeyPart1;
    internal bool hasKeyPart2;
    internal bool[] colourMatchingVoiceovers;
    //Chessboard
    internal bool chessBoardIsActive;
    //ScalesPuzzleScript
    internal bool scalesPuzzleIsActive;
    internal bool scalesPuzzleIsComplete;

    //Pipes
    internal Pipes_CW.Directions pipe1CurrentPosition;
    internal Pipes_CW.Directions pipe2CurrentPosition;
    internal Pipes_CW.Directions pipe3CurrentPosition;
    internal Pipes_CW.Directions pipe4CurrentPosition;
    internal Pipes_CW.Directions pipe5CurrentPosition;
    internal Pipes_CW.Directions pipe6CurrentPosition;
    internal Pipes_CW.Directions pipe7CurrentPosition;
    internal Pipes_CW.Directions pipe8CurrentPosition;
    internal Pipes_CW.Directions pipe9CurrentPosition;
    internal Pipes_CW.Directions pipe10CurrentPosition;
    internal Pipes_CW.Directions pipe11CurrentPosition;
    internal Pipes_CW.Directions pipe12CurrentPosition;

    //ChessPieces
    internal ChessBoard_DR.POSITION knightCurrentPosition;
    internal ChessBoard_DR.POSITION kingCurrentPosition;
    internal ChessBoard_DR.POSITION queenCurrentPosition;
    internal ChessBoard_DR.POSITION pawnCurrentPosition;
    #endregion

    public GameData_DR(SaveSystem_DR saveData)
    {
        //Player
        playerPosition[0] = saveData.playerT.position.x;
        playerPosition[1] = saveData.playerT.position.y;
        playerPosition[2] = saveData.playerT.position.z;

        playerRotation[0] = saveData.playerT.rotation.x;
        playerRotation[1] = saveData.playerT.rotation.y;
        playerRotation[2] = saveData.playerT.rotation.z;
        playerRotation[3] = saveData.playerT.rotation.w;

        //JournalUI
        Text[] journalTexts = saveData.journalGO.GetComponentsInChildren<Text>();
        journalTasks = new string[5];
        for (int i = 0; i < journalTexts.Length; i++)
        {
            switch (journalTexts[i].name)
            {
                case "Task":
                    journalTasks[0] = journalTexts[i].text;
                    break;
                case "Task (1)":
                    journalTasks[1] = journalTexts[i].text;
                    break;
                case "Task (2)":
                    journalTasks[2] = journalTexts[i].text;
                    break;
                case "Task (3)":
                    journalTasks[3] = journalTexts[i].text;
                    break;
                case "Task (4)":
                    journalTasks[4] = journalTexts[i].text;
                    break;
                case "JournalLogs":
                    journalLog = journalTexts[i].text;
                    break;
            }
        }
        saveData.journalGO.SetActive(true);
        journalBoxSize[0] = GameObject.Find("JournalLogs").GetComponent<RectTransform>().sizeDelta[0];
        journalBoxSize[1] = GameObject.Find("JournalLogs").GetComponent<RectTransform>().sizeDelta[1];
        saveData.journalGO.SetActive(false);

        //Baron
        baronActive = saveData.baronT.gameObject.activeInHierarchy;

        baronPosition[0] = saveData.baronT.position.x;
        baronPosition[1] = saveData.baronT.position.y;
        baronPosition[2] = saveData.baronT.position.z;

        baronRotation[0] = saveData.baronT.rotation.x;
        baronRotation[1] = saveData.baronT.rotation.y;
        baronRotation[2] = saveData.baronT.rotation.z;
        baronRotation[3] = saveData.baronT.rotation.w;

        //Chessboard
        chessTableActivated = saveData.chessBoardT.gameObject.activeInHierarchy;

        Transform[] chessPieces = saveData.chessBoard.GetComponentsInChildren<Transform>();
        chessKnightRotation = new float[4];
        chessKingRotation = new float[4];
        chessQueenRotation = new float[4];
        chessPawnRotation = new float[4];
        for (int i = 0; i < chessPieces.Length; i++)
        {
            switch (chessPieces[i].name)
            {
                case "BoardKnight":
                    chessKnightRotation[0] = chessPieces[i].rotation.x;
                    chessKnightRotation[1] = chessPieces[i].rotation.y;
                    chessKnightRotation[2] = chessPieces[i].rotation.z;
                    chessKnightRotation[3] = chessPieces[i].rotation.w;
                    break;
                case "BoardKing":
                    chessKingRotation[0] = chessPieces[i].rotation.x;
                    chessKingRotation[1] = chessPieces[i].rotation.y;
                    chessKingRotation[2] = chessPieces[i].rotation.z;
                    chessKingRotation[3] = chessPieces[i].rotation.w;
                    break;
                case "BoardQueen":
                    chessQueenRotation[0] = chessPieces[i].rotation.x;
                    chessQueenRotation[1] = chessPieces[i].rotation.y;
                    chessQueenRotation[2] = chessPieces[i].rotation.z;
                    chessQueenRotation[3] = chessPieces[i].rotation.w;
                    break;
                case "BoardPawn":
                    chessPawnRotation[0] = chessPieces[i].rotation.x;
                    chessPawnRotation[1] = chessPieces[i].rotation.y;
                    chessPawnRotation[2] = chessPieces[i].rotation.z;
                    chessPawnRotation[3] = chessPieces[i].rotation.w;
                    break;
            }
        }

        //GardenTable
        gardenTableActivated = saveData.gardenTableT.gameObject.activeInHierarchy;

        //RitualTable
        ritualTableActivated = saveData.ritualTableT.gameObject.activeInHierarchy;

        //RitualItems
        bowlNotPickedUp = saveData.bowlT.gameObject.activeInHierarchy;
        candlesNotPickedUp = saveData.candlesT.gameObject.activeInHierarchy;
        coinsNotPickedUp = saveData.coinsT.gameObject.activeInHierarchy;
        saltNotPickedUp = saveData.saltT.gameObject.activeInHierarchy;

        //HiddenMechNote
        hiddenMechNoteActivated = saveData.hiddenMechNoteT.gameObject.activeInHierarchy;

        //Monitor
        monitorOn = saveData.monitorT.GetChild(0).gameObject.activeInHierarchy;

        //KeypadTable
        keypadTableActivated = saveData.keypadT.gameObject.activeInHierarchy;

        //ThrowingBox
        throwingBoxActivated = saveData.throwingBoxT.gameObject.activeInHierarchy;

        //Balls
        ball1TPosition[0] = saveData.baronT.position.x;
        ball1TPosition[1] = saveData.baronT.position.y;
        ball1TPosition[2] = saveData.baronT.position.z;

        ball2TPosition[0] = saveData.baronT.position.x;
        ball2TPosition[1] = saveData.baronT.position.y;
        ball2TPosition[2] = saveData.baronT.position.z;

        ball3TPosition[0] = saveData.baronT.position.x;
        ball3TPosition[1] = saveData.baronT.position.y;
        ball3TPosition[2] = saveData.baronT.position.z;

        //Weights
        weight1TPosition[0] = saveData.weight1T.position.x;
        weight1TPosition[1] = saveData.weight1T.position.y;
        weight1TPosition[2] = saveData.weight1T.position.z;

        weight2TPosition[0] = saveData.weight2T.position.x;
        weight2TPosition[1] = saveData.weight2T.position.y;
        weight2TPosition[2] = saveData.weight2T.position.z;

        weight3TPosition[0] = saveData.weight3T.position.x;
        weight3TPosition[1] = saveData.weight3T.position.y;
        weight3TPosition[2] = saveData.weight3T.position.z;

        //KeyPieces
        keyHandle1TPosition[0] = saveData.keyHandle1T.position.x;
        keyHandle1TPosition[1] = saveData.keyHandle1T.position.y;
        keyHandle1TPosition[2] = saveData.keyHandle1T.position.z;

        keyHandle2TPosition[0] = saveData.keyHandle2T.position.x;
        keyHandle2TPosition[1] = saveData.keyHandle2T.position.y;
        keyHandle2TPosition[2] = saveData.keyHandle2T.position.z;

        keyHandle3TPosition[0] = saveData.keyHandle3T.position.x;
        keyHandle3TPosition[1] = saveData.keyHandle3T.position.y;
        keyHandle3TPosition[2] = saveData.keyHandle3T.position.z;

        keyHandle4TPosition[0] = saveData.keyHandle4T.position.x;
        keyHandle4TPosition[1] = saveData.keyHandle4T.position.y;
        keyHandle4TPosition[2] = saveData.keyHandle4T.position.z;

        keyBit2TPosition[0] = saveData.keyBit2T.position.x;
        keyBit2TPosition[1] = saveData.keyBit2T.position.y;
        keyBit2TPosition[2] = saveData.keyBit2T.position.z;

        keyBit3TPosition[0] = saveData.keyBit3T.position.x;
        keyBit3TPosition[1] = saveData.keyBit3T.position.y;
        keyBit3TPosition[2] = saveData.keyBit3T.position.z;

        //JewelleryItems
        jewelleryNotPickedUp = saveData.jewellery.gameObject.activeInHierarchy;
        pendantNotPickedUp = saveData.pendant.gameObject.activeInHierarchy;
        necklaceNotPickedUp = saveData.necklace.gameObject.activeInHierarchy;
        braceletNotPickedUp = saveData.bracelet.gameObject.activeInHierarchy;

        //Inventory
        GameObject[] inventoryGOs = saveData.inventory.inventoryItems;
        int itemCount = 0;
        List<Inventory_HR.Names> tempInventory = new List<Inventory_HR.Names>();
        for(int i = 0; i < inventoryGOs.Length; i++)
        {
            string inventoryItem = inventoryGOs[i].GetComponentInChildren<Text>().text;
            if (inventoryItem.Trim() != "")
            {
                tempInventory.Add((Inventory_HR.Names)Enum.Parse(typeof(Inventory_HR.Names), inventoryItem));
                itemCount++;
            }
        }
        inventory = new Inventory_HR.Names[itemCount];
        for(int i = 0; i < itemCount; i++)
        {
            inventory[i] = tempInventory[i];
        }

        //Cinematics
        playStartCinematic = saveData.cinematics.playStartCinematic;

        //Interact
        numberCoinsCollected = saveData.interact.numberCoinsCollected;

        //InitiatePuzzles
        ballCounter = saveData.initiatePuzzles.ballCounter;
        puzzleVoiceovers = saveData.initiatePuzzles.voiceovers;
        monitorInteractions = saveData.initiatePuzzles.monitorInteractions;
        monitorInteractionsUsed = saveData.initiatePuzzles.monitorInteractionsUsed;

        //GameTesting
        setUpPuzzle = saveData.gameTesting.setUpPuzzle;
        arePuzzlesDone = saveData.gameTesting.arePuzzlesDone;
        cutscenes = saveData.gameTesting.cutscenes;
        cutscenesDone = saveData.gameTesting.cutscenesDone;
        controlsSeen = saveData.gameTesting.controlsSeen;

        //Baron
        appearanceTimer = saveData.baron.appearanceTimer;

        //WaterBowl
        coinsLeft = saveData.waterBowl.coins.Count;

        //KeypadUI
        interactedWithSafe = saveData.keyPadUI.interactedWithSafe;
        hasAlreadyInteractedWithSafe = saveData.keyPadUI.hasAlreadyInteractedWithSafe;
        playerInteractsWithDoc = saveData.keyPadUI.playerInteractsWithDoc;
        keypadVoiceovers = saveData.keyPadUI.voiceovers;
        isActive = saveData.keyPadUI.isActive;

        //EventManager
        triggersSet = saveData.eventManager.triggersSet;
        itemsSet = saveData.eventManager.itemsSet;
        disturbancesSet = saveData.eventManager.disturbancesSet;

        //TriggerScripts
        chessAllowedToBeUsed = saveData.chessTrigger.allowedToBeUsed;
        chessActivated = saveData.chessTrigger.activated;
        correctOrderAllowedToBeUsed = saveData.correctOrderTrigger.allowedToBeUsed;
        correctOrderActivated = saveData.correctOrderTrigger.activated;
        gardenAllowedToBeUsed = saveData.gardenTrigger.allowedToBeUsed;
        gardenActivated = saveData.gardenTrigger.activated;
        hiddenMechanismAllowedToBeUsed = saveData.hiddenMechanismTrigger.allowedToBeUsed;
        hiddenMechanismActivated = saveData.hiddenMechanismTrigger.activated;
        ritualAllowedToBeUsed = saveData.ritualTrigger.allowedToBeUsed;
        ritualActivated = saveData.ritualTrigger.activated;
        throwingAllowedToBeUsed = saveData.throwingTrigger.allowedToBeUsed;
        throwingActivated = saveData.throwingTrigger.activated;

        //PutDown
        ritualPDBeenUsed = saveData.ritualPutDown.GetBeenUsed();
        gardenPDBeenUsed = saveData.gardenPutDown.GetBeenUsed();
        chessPDBeenUsed = saveData.chessPutDown.GetBeenUsed();

        //Table
        ritualTHasBeenPlaced = saveData.ritualTable.hasBeenPlaced;
        gardenTHasBeenPlaced = saveData.gardenTable.hasBeenPlaced;
        chessTHasBeenPlaced = saveData.chessTable.hasBeenPlaced;

        //Paper
        chessPHasBeenRead = saveData.chessPaper.hasBeenRead;
        hiddenMechanismPHasBeenRead = saveData.hiddenMechanismPaper.hasBeenRead;
        keypadPHasBeenRead = saveData.keypadPaper.hasBeenRead;
        keysPHasBeenRead = saveData.keysPaper.hasBeenRead;

        //Door
        colourMatchingDoorUnlocked = saveData.colourMatchingDoor.unlocked;
        colourMatchingDoorIsOpen = saveData.colourMatchingDoor.isOpen;
        correntOrderDoorUnlocked = saveData.correntOrderDoor.unlocked;
        correntOrderDoorIsOpen = saveData.correntOrderDoor.isOpen;
        leftFrontDoorUnlocked = saveData.leftFrontDoor.unlocked;
        leftFrontDoorIsOpen = saveData.leftFrontDoor.isOpen;
        rightFrontDoorUnlocked = saveData.rightFrontDoor.unlocked;
        rightFrontDoorIsOpen = saveData.rightFrontDoor.isOpen;
        pantryDoorUnlocked = saveData.pantryDoor.unlocked;
        pantryDoorIsOpen = saveData.pantryDoor.isOpen;
        gymDoorUnlocked = saveData.gymDoor.unlocked;
        gymDoorIsOpen = saveData.gymDoor.isOpen;
        garageDoorUnlocked = saveData.garageDoor.unlocked;
        garageDoorIsOpen = saveData.garageDoor.isOpen;
        downstairsBathroomDoorUnlocked = saveData.downstairsBathroomDoor.unlocked;
        downstairsBathroomDoorIsOpen = saveData.downstairsBathroomDoor.isOpen;
        diningRoomDoorUnlocked = saveData.diningRoomDoor.unlocked;
        diningRoomDoorIsOpen = saveData.diningRoomDoor.isOpen;
        safeDoorUnlocked = saveData.safeDoor.unlocked;
        safeDoorIsOpen = saveData.safeDoor.isOpen;
        scalesDoorUnlocked = saveData.scalesDoor.unlocked;
        scalesDoorIsOpen = saveData.scalesDoor.isOpen;
        chessDoorUnlocked = saveData.chessDoor.unlocked;
        chessDoorIsOpen = saveData.chessDoor.isOpen;
        hiddenMechDoorUnlocked = saveData.hiddenMechDoor.unlocked;
        hiddenMechDoorIsOpen = saveData.hiddenMechDoor.isOpen;

        //HoldandThrow
        canHoldBall1 = saveData.ball1.canHold;
        ball1IsFirstTime = saveData.ball1.isFirstTime;
        canHoldBall2 = saveData.ball2.canHold;
        ball2IsFirstTime = saveData.ball2.isFirstTime;
        canHoldBall3 = saveData.ball3.canHold;
        ball3IsFirstTime = saveData.ball3.isFirstTime;

        canHoldWeight1 = saveData.weight1.canHold;
        weight1IsFirstTime = saveData.weight1.isFirstTime;
        canHoldWeight2 = saveData.weight2.canHold;
        weight2IsFirstTime = saveData.weight2.isFirstTime;
        canHoldWeight3 = saveData.weight3.canHold;
        weight3IsFirstTime = saveData.weight3.isFirstTime;

        canHoldKeyHandle1 = saveData.keyHandle1.canHold;
        keyHandle1IsFirstTime = saveData.keyHandle1.isFirstTime;
        canHoldKeyHandle2 = saveData.keyHandle2.canHold;
        keyHandle2IsFirstTime = saveData.keyHandle2.isFirstTime;
        canHoldKeyHandle3 = saveData.keyHandle3.canHold;
        keyHandle3IsFirstTime = saveData.keyHandle3.isFirstTime;
        canHoldKeyHandle4 = saveData.keyHandle4.canHold;
        keyHandle4IsFirstTime = saveData.keyHandle4.isFirstTime;

        canHoldKeyBit2 = saveData.keyBit2.canHold;
        keyBit2IsFirstTime = saveData.keyBit2.isFirstTime;
        canHoldKeyBit3 = saveData.keyBit3.canHold;
        keyBit3IsFirstTime = saveData.keyBit3.isFirstTime;

        //BallButtonLogic
        button1IsActive = saveData.button1.isActive;
        button2IsActive = saveData.button2.isActive;
        button3IsActive = saveData.button3.isActive;

        //SetUpRitual
        ritualSteps = saveData.setUpRitual.ritualSteps;
        ritualIsActive = saveData.setUpRitual.isActive;
        ritualVoiceovers = saveData.setUpRitual.voiceovers;
        //HiddenMech
        hiddenMechIsActive = saveData.hiddenMech.isActive;
        //Fusebox
        isFuseboxSolved = saveData.fusebox.isFuseboxSolved;
        fuseboxActive = saveData.fusebox.isActive;
        fuseboxVoiceovers = saveData.fusebox.voiceovers;
        //CorrectOrder
        correctOrderIsActive = saveData.correctOrder.isActive;
        correctOrderWhichRound = saveData.correctOrder.whichRound;
        //ColourMatchingPuzzle
        colourMatchingPuzzleIsActive = saveData.colourMatchingPuzzle.isActive;
        isDoorInteractedWith = saveData.colourMatchingPuzzle.isDoorInteractedWith;
        hasKeyPart1 = saveData.colourMatchingPuzzle.hasKeyPart1;
        hasKeyPart2 = saveData.colourMatchingPuzzle.hasKeyPart2;
        colourMatchingVoiceovers = saveData.colourMatchingPuzzle.voiceovers;
        //Chessboard
        chessBoardIsActive = saveData.chessBoard.isActive;
        //ScalesPuzzleScript
        scalesPuzzleIsActive = saveData.scalesPuzzleScript.isActive;
        scalesPuzzleIsComplete = saveData.scalesPuzzleScript.isComplete;

        //ChessPieces
        knightCurrentPosition = saveData.knight.currentPosition;
        kingCurrentPosition = saveData.king.currentPosition;
        queenCurrentPosition = saveData.queen.currentPosition;
        pawnCurrentPosition = saveData.pawn.currentPosition;
    }
}