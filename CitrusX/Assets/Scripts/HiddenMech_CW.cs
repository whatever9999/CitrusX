﻿/*Chase Wilding - Hidden Mechanism Puzzle 10/02/2020
 * This puzzle sets off an animation when the correct book is picked up
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenMech_CW : MonoBehaviour
{
    Door_DR door;
    private bool isActive = false;
    public void SetActive(bool value) { isActive = value; }

    private void Awake()
    {
        door = GameObject.Find("Hidden Mech Door").GetComponent<Door_DR>(); 
    }
    private void HiddenMechPuzzle()
    {
        if(Journal_DR.instance.AreTasksComplete())
        {
            door.Open();
            GameTesting_CW.instance.arePuzzlesDone[7] = true;
        }
    }
}
