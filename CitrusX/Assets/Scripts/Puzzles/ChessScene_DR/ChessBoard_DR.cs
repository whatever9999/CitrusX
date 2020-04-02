/* 
 * Dominique
 * 
 * Checks if the pieces are in the correct position on an interval (every 1 second) if the door isn't unlocked yet
 * 
 * Chase (changes) 17/2/2020
 * Removed start as this is now in initiate puzzles, added journal reference, set active function and linked to game script.
 * 
 * Chase(Changes) 26/2/2020
 * Added subtitle functionality
 * 
 * Chase (changes) 9/3/2020
 * Added new journal entries/tasks and added link to the room it unlocks with the trigger
 * 
 * Dominique (Changes) 18/03/2020
 * Simplified script a bit and did some bug fixing
 * Modified rotation so it matches Chase's pipes puzzle solution (left -> up -> right -> down)
 */

/**
* \class ChessBoard_DR
* 
* \brief checks if the chess pieces are all in position and unlocks the door if so
* 
* \author Dominique
* 
* \date Last Modified: 18/03/2020
*/
using UnityEngine;

public class ChessBoard_DR : MonoBehaviour
{
    public enum POSITION {
        LEFT,
        UP,
        RIGHT,
        DOWN
    }

    public Door_DR door;
    public ChessPiece_DR[] chessPieces;

    private Journal_DR journal;
    internal bool isActive = false;
    private Subtitles_HR subtitles;
    private TriggerScript_CW chessTrigger;
    private TriggerScript_CW chessExtraTrigger;
    public void SetActive(bool value) { isActive = value; }

    /// <summary>
    /// Initialise variables
    /// </summary>
    private void Awake()
    {
        journal = Journal_DR.instance;
        subtitles = GameObject.Find("FirstPersonCharacter").GetComponent<Subtitles_HR>();
        chessTrigger = GameObject.Find("ChessboardTrigger").GetComponent<TriggerScript_CW>();
       // chessExtraTrigger = GameObject.Find("ChessboardExtraTrigger").GetComponent<TriggerScript_CW>();
    }

    /// <summary>
    /// Check the position of the pieces compared to their desired position
    /// </summary>
    /// <returns>A bool that shows if the pieces are all in place or not</returns>
    public void CheckPieces()
    {
        bool inPosition = true;
        for (int i = 0; i < chessPieces.Length; i++)
        {
            //If the chess piece isn't in the right position or isn't active (isn't on the board yet) then the door cannot open
            if (chessPieces[i].currentPosition != chessPieces[i].desiredPosition)
            {
                inPosition = false;
                break;
            }
            else if (!chessPieces[i].gameObject.activeInHierarchy)
            {
                inPosition = false;
                break;
            }
        }

        if (inPosition)
        {
            //SOUND HERE a CLICK to signify completion
            subtitles.PlayAudio(Subtitles_HR.ID.P6_LINE4);
            chessTrigger.allowedToBeUsed = true;
            journal.TickOffTask("Solve Puzzle");
            GameTesting_CW.instance.arePuzzlesDone[5] = true;
            door.unlocked = true;
        }
    }
}