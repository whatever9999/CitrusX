/*
 * Script: ScalesPuzzleScript 
 * Created By: Adam Gordon
 * Created On: 09/02/2020
 * 
 * Summary: Script used for the balance puzzle. Will assess the total weight placed on each pan of the scales.
 *          Once the target weight has been reached, will allow access to the reward.
 *          
 *  Chase (changes) 17/2/2020
 *  Added journal and setActive and linked to game script.
 *  
 *  Chase (changes) 22/2/2020
 *  Changed serialized fields to private and found objects through get component. Added bools for voiceover.
 *  Removed start and replaced with awake. Changed var to the relevant script.
 *  
 *  Chase (changes) 24/2/2020
 *  Edited the puzzle to make it playable and linked it to interact
 *  
 *  
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalesPuzzleScript_AG : MonoBehaviour
{
    // Each side of scales
    private GameObject leftPan;
    private GameObject rightPan;
    private GameObject puzzleDoor;
    private Door_DR doorScript;

    // Puzzle State
    private bool isComplete = false;

    // Weights to compare
    private int leftMass;
    private int rightMass;

    private Journal_DR journal;
    private bool isActive = false;
    
    public void SetActive(bool value) { isActive = value; }

    private void Awake()
    {
        leftPan = GameObject.Find("Left Pan");
        rightPan = GameObject.Find("Right Pan");
        journal = Journal_DR.instance;
        // doorScript = puzzleDoor.GetComponent<Door_DR>();
        //WeightScript_AG seatedWeights = rightPan.GetComponentInChildren<WeightScript_AG>();
        //foreach (WeightScript_AG weightScript in seatedWeights)
        //{
        //    rightMass += weightScript.GetMass();
        //}
    }

    private void Update()
    {
        if(isActive)
        {
            
        }
    }
    /// <summary>
    /// Compare the weight on each side to see if they match
    /// </summary>
    private void CompareSides()
    {
        // Compare
        if(leftMass == rightMass)
        {
            // if equal - puzzle complete
            isComplete = true;
            journal.TickOffTask("balance scales");
            GameTesting_CW.instance.arePuzzlesDone[4] = true;
            
            doorScript.ToggleOpen();
        }
    }

    /// <summary>
    /// Combine the mass of all weights in the left pan
    /// </summary>
    private void CalculateLeftMass()
    {
        // Reset mass
        leftMass = 0;

        // Get all weight scripts in left pan
        var addedWeights = leftPan.GetComponentsInChildren<WeightScript_AG>();

        // Add each weights mass to the leftMass var
        foreach (WeightScript_AG weight in addedWeights)
        {
            leftMass += weight.GetMass();
        }
    }

    /// <summary>
    /// Calculate and compare weight when a weight is added (Called Externally)
    /// </summary>
    public void ReviewWeight()
    {
        CalculateLeftMass();
        CompareSides();
    }
}

// V0.1.0 - Last Update: 2020/02/09 @ 19:25 by AG
