/* 
 * Dominique
 * 
 * Checks if the pieces are in the correct position on an interval (every 1 second) if the door isn't unlocked yet
 */
using UnityEngine;

public class ChessBoard_DR : MonoBehaviour
{
    public Door_DR door;
    public ChessPiece[] chessPieces;

    private const float checkBoardInterval = 1;
    private float currentCheckBoardInterval;

    private void Start()
    {
        Journal_DR.instance.ChangeTasks(new string[] { "Pawn" });
    }

    private void Update()
    {
        //If the door isn't unlocked
        if(!door.unlocked)
        {
            //Run a timer to see if we should check the position of the pieces
            if (currentCheckBoardInterval >= checkBoardInterval)
            {
                //Unlock the door if the pieces are in position
                if (CheckPieces())
                {
                    door.unlocked = true;
                }

                currentCheckBoardInterval = 0;
            }
            else
            {
                currentCheckBoardInterval += Time.deltaTime;
            }
        }
    }

    public bool CheckPieces()
    {
        bool inPosition = true;
        for(int i = 0; i < chessPieces.Length; i++)
        {
            //If the chess piece isn't in the right position or isn't active (isn't on the board yet) then the door cannot open
            if(chessPieces[i].chessPieceTransform.rotation.eulerAngles != chessPieces[i].desiredPosition)
            {
                inPosition = false;
                break;
            } else if (!chessPieces[i].chessPieceTransform.gameObject.activeInHierarchy)
            {
                inPosition = false;
                break;
            }
        }
        return inPosition;
    }
}

[System.Serializable]
public class ChessPiece
{
    public Transform chessPieceTransform;
    public Vector3 desiredPosition;
}