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

public class SaveSystem_DR: MonoBehaviour
{
    public static SaveSystem_DR instance;

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
    internal GameObject inventory;
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

    internal Pipes_CW pipe1;
    internal Pipes_CW pipe2;
    internal Pipes_CW pipe3;
    internal Pipes_CW pipe4;
    internal Pipes_CW pipe5;
    internal Pipes_CW pipe6;
    internal Pipes_CW pipe7;
    internal Pipes_CW pipe8;
    internal Pipes_CW pipe9;
    internal Pipes_CW pipe10;
    internal Pipes_CW pipe11;
    internal Pipes_CW pipe12;

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
        inventory = GameObject.Find("Inventory");

        cinematics = GameObject.Find("Cinematics").GetComponent<Cinematics_DR>();
        interact = GameObject.Find("FPSController").GetComponentInChildren<Interact_HR>();
        initiatePuzzles = GameObject.Find("FPSController").GetComponentInChildren<InitiatePuzzles_CW>();
        gameTesting = GameObject.Find("FPSController").GetComponentInChildren<GameTesting_CW>();
        baron = GameObject.Find("Baron").GetComponent<Baron_DR>();
        waterBowl = GameObject.Find("WaterBowl").GetComponent<WaterBowl_DR>();
        keyPadUI = GameObject.Find("KeypadUI").GetComponent<KeypadUI_DR>();
        journal = GameObject.Find("FPSController").GetComponentInChildren<Journal_DR>();
        eventManager = GameObject.Find("Managers").GetComponent<EventManager_CW>();

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

        button1 = GameObject.Find("1Button").GetComponent<BallButtonLogic_HR>();
        button2 = GameObject.Find("2Button").GetComponent<BallButtonLogic_HR>();
        button3 = GameObject.Find("3Button").GetComponent<BallButtonLogic_HR>();

        setUpRitual = GameObject.Find("FPSController").GetComponent<SetUpRitual_CW>();
        hiddenMech = GameObject.Find("FPSController").GetComponent<HiddenMech_CW>();
        fusebox = GameObject.Find("Fusebox").GetComponent<Fusebox_CW>();
        correctOrder = GameObject.Find("CorrectOrderUI").GetComponent<CorrectOrder_CW>();
        colourMatchingPuzzle = GameObject.Find("ColourMatchingDoor").GetComponent<ColourMatchingPuzzle_CW>();
        chessBoard = GameObject.Find("ChessBoard").GetComponent<ChessBoard_DR>();
        scalesPuzzleScript = GameObject.Find("Scales").GetComponent<ScalesPuzzleScript_AG>();

        pipe1 = GameObject.Find("Pipe 1").GetComponent<Pipes_CW>();
        pipe2 = GameObject.Find("Pipe 2").GetComponent<Pipes_CW>();
        pipe3 = GameObject.Find("Pipe 3").GetComponent<Pipes_CW>();
        pipe4 = GameObject.Find("Pipe 4").GetComponent<Pipes_CW>();
        pipe5 = GameObject.Find("Pipe 5").GetComponent<Pipes_CW>();
        pipe6 = GameObject.Find("Pipe 6").GetComponent<Pipes_CW>();
        pipe7 = GameObject.Find("Pipe 7").GetComponent<Pipes_CW>();
        pipe8 = GameObject.Find("Pipe 8").GetComponent<Pipes_CW>();
        pipe9 = GameObject.Find("Pipe 9").GetComponent<Pipes_CW>();
        pipe10 = GameObject.Find("Pipe 10").GetComponent<Pipes_CW>();
        pipe11 = GameObject.Find("Pipe 11").GetComponent<Pipes_CW>();
        pipe12 = GameObject.Find("Pipe 12").GetComponent<Pipes_CW>();

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
        //Save data path
#if UNITY_EDITOR
        string path = Application.dataPath + "/SaveFiles/save.dat";
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

        Debug.Log("Saved");
    }

    /// <summary>
    /// Load the save file from the Application.persistentDataPath + "/save.dat" by collecting the data and placing it in the appropriate variables
    /// </summary>
    public void Load()
    {
       //Load data path
#if UNITY_EDITOR
       string path = Application.dataPath + "/SaveFiles/save.dat";
#else
       string path = Application.persistentDataPath + "/save.dat";
#endif

        FileStream file;

        //If the file exists then open it, otherwise there is no file to be read
        if (File.Exists(path)) file = File.OpenRead(path);
        else
        {
            Debug.Log("File not found");
            return;
        }

        //Deserialize the file
        BinaryFormatter bf = new BinaryFormatter();
        GameData_DR gameData = (GameData_DR)bf.Deserialize(file);
        file.Close();

        //Put file data into variables
        UpdateVariables(gameData);

        Debug.Log("Loaded");
    }

    /// <summary>
    /// Takes values from a GameData class and puts them in the appropriate variables within the current game.
    /// </summary>
    /// <param name="GD - a class that stores all of the data for the game that needs to be saved"></param>
    public void UpdateVariables(GameData_DR GD)
    {
        playerT.position = new Vector3(GD.playerPosition[0], GD.playerPosition[1], GD.playerPosition[2]);
        playerT.rotation = new Quaternion(GD.playerRotation[0], GD.playerRotation[1], GD.playerRotation[2], GD.playerRotation[3]);
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
    //HiddenMech
    internal bool hiddenMechIsActive;
    //Fusebox
    internal bool isFuseboxSolved;
    //CorrectOrder
    internal bool correctOrderIsActive;
    internal bool[] correctOrderWhichRound;
    //ColourMatchingPuzzle
    internal bool colourMatchingPuzzleIsActive;
    internal bool[] isDoorInteractedWith;
    internal bool hasKeyPart1;
    internal bool hasKeyPart2;
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
        Text[] journalTexts = saveData.GetComponentsInChildren<Text>();
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

        Transform[] chessPieces = saveData.GetComponentsInChildren<Transform>();
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
    //HiddenMech
    internal bool hiddenMechIsActive;
    //Fusebox
    internal bool isFuseboxSolved;
    //CorrectOrder
    internal bool correctOrderIsActive;
    internal bool[] correctOrderWhichRound;
    //ColourMatchingPuzzle
    internal bool colourMatchingPuzzleIsActive;
    internal bool[] isDoorInteractedWith;
    internal bool hasKeyPart1;
    internal bool hasKeyPart2;
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
}
}