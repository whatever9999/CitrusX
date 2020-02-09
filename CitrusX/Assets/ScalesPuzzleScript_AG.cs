/*
 * Script: ScalesPuzzleScript 
 * Created By: Adam Gordon
 * Created On: 09/02/2020
 * 
 * Summary: Script used for the balance puzzle. Will assess the total weight placed on each pan of the scales.
 *          Once the target weight has been reached, will allow access to the reward.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalesPuzzleScript_AG : MonoBehaviour
{
    // Each side of scales
    [SerializeField] private GameObject leftPan;
    [SerializeField] private GameObject rightPan;
    [SerializeField] private GameObject puzzleDoor;
    private Door_DR doorScript;

    // Puzzle State
    private bool isComplete = false;

    // Weights to compare
    private int leftMass;
    private int rightMass;

    // Start is called before the first frame update
    void Start()
    {
        // Get Door script
        doorScript = puzzleDoor.GetComponent<Door_DR>();

        // get all weights in right pan (pre-set weights)
        var seatedWeights = rightPan.GetComponentsInChildren<WeightScript_AG>();

        // Iterate and combijne masses to find total needed
        foreach (WeightScript_AG weightScript in seatedWeights)
        {
            rightMass += weightScript.GetMass();
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
            doorScript.Open();
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
