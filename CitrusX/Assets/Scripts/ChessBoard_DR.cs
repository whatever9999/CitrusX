﻿/* 
 * Dominique
 * 
 * Checks if the pieces are in the correct position on an interval (every 1 second) if the door isn't unlocked yet
 * 
 * Chase (changes) 17/2/2020
 * Removed start as this is now in initiate puzzles, added journal reference, set active function and linked to game script.
 */
using UnityEngine;

public class ChessBoard_DR : MonoBehaviour
{
    public Door_DR door;
    public ChessPiece[] chessPieces;

    private const float checkBoardInterval = 1;
    private float currentCheckBoardInterval;
    private Journal_DR journal;
    private bool isActive = false;
    private Subtiles_HR subtitles;
    private TriggerScript_CW chessTrigger;
    public void SetActive(bool value) { isActive = value; }

    private void Awake()
    {
        journal = Journal_DR.instance;
        subtitles = GameObject.Find("FirstPersonCharacter").GetComponent<Subtiles_HR>();
        chessTrigger = GameObject.Find("Chessboard Trigger").GetComponent<TriggerScript_CW>();
    }

    private void Update()
    {
        if (isActive)
        {
            //If the door isn't unlocked
            if (!door.unlocked)
            {
                //Run a timer to see if we should check the position of the pieces
                if (currentCheckBoardInterval >= checkBoardInterval)
                {
                    //Unlock the door if the pieces are in position
                    if (CheckPieces())
                    {
                        door.unlocked = true;
                        
                        subtitles.PlayAudio(Subtiles_HR.ID.P6_LINE4);
                        chessTrigger.allowedToBeUsed = true;
                        GameTesting_CW.instance.arePuzzlesDone[5] = true;
                    }

                    currentCheckBoardInterval = 0;
                }
                else
                {
                    currentCheckBoardInterval += Time.deltaTime;
                }
            }
        }

    }

    public bool CheckPieces()
    {
        if (isActive == true)
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
            }
            return inPosition;
        }
        return false;

    }
}

[System.Serializable]
public class ChessPiece
{
    public Transform chessPieceTransform;
    public Vector3 desiredPosition;
}