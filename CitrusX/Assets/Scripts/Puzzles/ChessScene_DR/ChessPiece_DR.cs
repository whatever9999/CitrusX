﻿using UnityEngine;

public class ChessPiece_DR : MonoBehaviour
{
    public ChessBoard_DR.POSITION currentPosition;
    public ChessBoard_DR.POSITION desiredPosition;

    private ChessBoard_DR chessBoard;

    private void Awake()
    {
        chessBoard = GameObject.Find("ChessBoard").GetComponent<ChessBoard_DR>();
    }

    public void Rotate()
    {
        transform.Rotate(0, 0, 90);

        switch (currentPosition)
        {
            case ChessBoard_DR.POSITION.LEFT:
                currentPosition = ChessBoard_DR.POSITION.UP;
                break;
            case ChessBoard_DR.POSITION.UP:
                currentPosition = ChessBoard_DR.POSITION.RIGHT;
                break;
            case ChessBoard_DR.POSITION.RIGHT:
                currentPosition = ChessBoard_DR.POSITION.DOWN;
                break;
            case ChessBoard_DR.POSITION.DOWN:
                currentPosition = ChessBoard_DR.POSITION.LEFT;
                break;
        }

        chessBoard.CheckPieces();
    }
}
