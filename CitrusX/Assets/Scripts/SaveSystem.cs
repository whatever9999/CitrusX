/*
 * Dominique
 * 
 * Saves and Loads game data
 * There is a different save location according to if the game is played in the editor or as an executable
 * A binary formatter has been used to save a .dat file
 * There is a single file for the save
 * Atm saving is on F5
 */

using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityStandardAssets.Characters.FirstPerson;

public class SaveSystem: MonoBehaviour
{
    public static SaveSystem instance;

    #region ObjectsToSave
    private Transform player;
    #endregion

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            Save();
        }
    }

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
        GameData gameData = new GameData(player);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, gameData);
        file.Close();

        Debug.Log("Saved");
    }

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
        GameData gameData = (GameData)bf.Deserialize(file);
        file.Close();

        //Put file data into variables
        UpdateVariables(gameData);

        Debug.Log("Loaded");
    }

    public void UpdateVariables(GameData GD)
    {
        player.position = new Vector3(GD.playerPosition[0], GD.playerPosition[1], GD.playerPosition[2]);
        player.rotation = new Quaternion(GD.playerRotation[0], GD.playerRotation[1], GD.playerRotation[2], GD.playerRotation[3]);
    }
}

[System.Serializable]
public class GameData
{
    #region VariablesToSave
    public float[] playerPosition = new float[3];
    public float[] playerRotation = new float[4];
    #endregion

    public GameData(Transform player)
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