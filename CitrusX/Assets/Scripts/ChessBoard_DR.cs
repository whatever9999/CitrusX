using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoard_DR : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Journal_DR.instance.ChangeTasks(new string[]{"Pawn",});
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
