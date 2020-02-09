using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPuzzleJournal_DR : MonoBehaviour
{
    private void Start()
    {
        Journal_DR.instance.ChangeTasks(new string[] { "Pawn" });
    }
}
