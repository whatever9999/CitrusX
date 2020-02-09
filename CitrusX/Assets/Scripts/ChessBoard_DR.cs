using UnityEngine;

public class ChessBoard_DR : MonoBehaviour
{
    public Door_DR door;
    public ChessPiece[] chessPieces;

    private const float checkBoardInterval = 1;
    private float currentCheckBoardInterval;

    private void Update()
    {
        if(!door.unlocked)
        {
            if (currentCheckBoardInterval >= checkBoardInterval)
            {
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
            if(chessPieces[i].chessPieceTransform.rotation.eulerAngles != chessPieces[i].desiredPosition)
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