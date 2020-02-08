using UnityEngine;

public class ChessBoard_DR : MonoBehaviour
{
    private Transform[] chessPieces;

    void Start()
    {
        Journal_DR.instance.ChangeTasks(new string[]{"Pawn",});

        //Get the transforms of all the chess pieces (don't include the board itself)
        Transform[] transforms = GetComponentsInChildren<Transform>();
        chessPieces = new Transform[transforms.Length - 1];
        int count = 0;
        for(int i = 0; i < transforms.Length; i++)
        {
            if(transforms[i].tag == "ChessPiece")
            {
                chessPieces[count] = transforms[i];
                ++count;
            }
        }
    }

    //Rotate pieces by 90 degrees
    public void RotatePiece(string name)
    {
        for (int i = 0; i < chessPieces.Length; i++)
        {
            if(chessPieces[i].name == name)
            {
                chessPieces[i].Rotate(90, 0, 0);
            }
        }
    }
}
