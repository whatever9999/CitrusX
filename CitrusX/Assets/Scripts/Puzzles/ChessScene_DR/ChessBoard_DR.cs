/* 
 * Dominique
 * 
 * Checks if the pieces are in the correct position on an interval (every 1 second) if the door isn't unlocked yet
 * 
 * Chase (changes) 17/2/2020
 * Removed start as this is now in initiate puzzles, added journal reference, set active function and linked to game script.
 * Chase(Changes) 26/2/2020
 * Added subtitle functionality
 */

/**
* \class ChessBoard_DR
* 
* \brief checks if the chess pieces are all in position and unlocks the door if so
* 
* \author Dominique
* 
* \date Last Modified: 26/02/2020
*/
using UnityEngine;

public class ChessBoard_DR : MonoBehaviour
{
    public Door_DR door;
    public ChessPiece_DR[] chessPieces;

    private const float checkBoardInterval = 1;
    private float currentCheckBoardInterval;
    private Journal_DR journal;
    private bool isActive = false;
    private Subtitles_HR subtitles;
    private TriggerScript_CW chessTrigger;
    public void SetActive(bool value) { isActive = value; }

    /// <summary>
    /// Initialise variables
    /// </summary>
    private void Awake()
    {
        journal = Journal_DR.instance;
        subtitles = GameObject.Find("FirstPersonCharacter").GetComponent<Subtitles_HR>();
        chessTrigger = GameObject.Find("ChessboardTrigger").GetComponent<TriggerScript_CW>();
    }

    /// <summary>
    /// If the door isn't unlocked then a timer is running to check the position of the pieces every half a second
    /// The game state is updated when the pieces are in place and the door is unlocked
    /// </summary>
    private void Update()
    {
        
            //If the door isn't unlocked
            if (!door.unlocked)
            {
                //Run a timer to see if we should check the position of the pieces
                if (currentCheckBoardInterval >= checkBoardInterval)
                {
                    //Unlock the door if the pieces are in position
                    if (CheckPieces() == true)
                    {
                         Debug.Log("plz");
                         subtitles.PlayAudio(Subtitles_HR.ID.P6_LINE4);
                         chessTrigger.allowedToBeUsed = true;
                         GameTesting_CW.instance.arePuzzlesDone[5] = true;
                         door.unlocked = true;    
                    }
                    else if(CheckPieces() == false)
                    {
                         //Debug.Log("oh no");
                    }

                    currentCheckBoardInterval = 0;
                }
                else
                {
                    currentCheckBoardInterval += Time.deltaTime;
                }
            }
        

    }

    /// <summary>
    /// Check the position of the pieces compared to their desired position
    /// </summary>
    /// <returns>A bool that shows if the pieces are all in place or not</returns>
    public bool CheckPieces()
    {
        bool inPosition = true;
        for (int i = 0; i < chessPieces.Length; i++)
        {
            //If the chess piece isn't in the right position or isn't active (isn't on the board yet) then the door cannot open
            if (chessPieces[i].chessPieceTransform.localEulerAngles != chessPieces[i].desiredPosition)
            {
                inPosition = false;
                break;
            }
            else if (!chessPieces[i].chessPieceTransform.gameObject.activeInHierarchy)
            {
                inPosition = false;
                break;
            }
            else //HERE FOR TESTING PURPS ONLY
            {
                return true;
            }
        }
        return inPosition;

    }
}

/**
 * \class ChessPiece_DR
 * 
 * \brief Holds the transform of a piece and the desired position for it to be at
 * 
 * \author Dominique
 * 
 * \date Last Modified: 04/02/2020
 */
[System.Serializable]
public class ChessPiece_DR
{
    public Transform chessPieceTransform;
    public Vector3 desiredPosition;
}