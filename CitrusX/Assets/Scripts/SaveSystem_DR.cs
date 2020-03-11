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

public class SaveSystem_DR: MonoBehaviour
{
    public static SaveSystem_DR instance;

    #region ObjectsToSave
    private Transform player;
    #endregion

    /// <summary>
    /// Inititalise variables and load the game
    /// </summary>
    private void Awake()
    {
        instance = this;

        player = GameObject.Find("FPSController").GetComponent<Transform>();

        //The character controller stops the player's position from being changed so it's temporarily disabled
        CharacterController characterController = player.GetComponent<CharacterController>();
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
        GameData_DR gameData = new GameData_DR(player);
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
        player.position = new Vector3(GD.playerPosition[0], GD.playerPosition[1], GD.playerPosition[2]);
        player.rotation = new Quaternion(GD.playerRotation[0], GD.playerRotation[1], GD.playerRotation[2], GD.playerRotation[3]);
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
    public float[] playerPosition = new float[3];
    public float[] playerRotation = new float[4];
    #endregion

    public GameData_DR(Transform player)
    {
        playerPosition[0] = player.position.x;
        playerPosition[1] = player.position.y;
        playerPosition[2] = player.position.z;

        playerRotation[0] = player.rotation.x;
        playerRotation[1] = player.rotation.y;
        playerRotation[2] = player.rotation.z;
        playerRotation[3] = player.rotation.w;
    }
}