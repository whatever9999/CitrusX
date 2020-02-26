﻿/*
 * Script: WeightScript
 * Created By: Adam Gordon
 * Created On: 09/02/2020
 * 
 * Summary: Gives weights a retrievable weight in grammes, for use in balance puzzle.
 *          Defines whether a weight is moveable by the player.
 */

using UnityEngine;

public class WeightScript_AG : MonoBehaviour
{
    [SerializeField] private GameObject Scales;
    [SerializeField] private GameObject ScalePanLeft;
    [SerializeField] private GameObject WeightRack;
    private ScalesPuzzleScript_AG scalesScript;
    // Determines mass of weight
    [SerializeField] private enum WeightMass { OneHundred, TwoHundred, ThreeHundred, FourHundred, FiveHundred, OneThousand };
    [SerializeField] private WeightMass weightMass;

    // Determines if player can move weight
    [SerializeField] private bool isMovable;

    // Mass for calculations
    private int massInGrammes;
    private Journal_DR journal;
    private Subtiles_HR subtitles;

    // Is the weight on the scales
    private bool onScales;

    // If we are puting them around the room first
    [SerializeField] bool playerHasFound;

    // Start is called before the first frame update
    void Awake()
    {
        journal = Journal_DR.instance;
        subtitles = GameObject.Find("FirstPersonCharacter").GetComponent<Subtiles_HR>();
        scalesScript = Scales.GetComponent<ScalesPuzzleScript_AG>();
        switch (weightMass)
        {
            case WeightMass.OneHundred:
                massInGrammes = 100;
                break;
            case WeightMass.TwoHundred:
                massInGrammes = 200;
                break;
            case WeightMass.ThreeHundred:
                massInGrammes = 300;
                break;
            case WeightMass.FourHundred:
                massInGrammes = 400;
                break;
            case WeightMass.FiveHundred:
                massInGrammes = 500;
                break;
            case WeightMass.OneThousand:
                massInGrammes = 1000;
                break;
            default:
                Debug.Log("Error Assigning Mass to Weight: " + gameObject.name.ToString());
                break;
        }
    }

    /// <summary>
    /// Retreives the mass of the assigned weight
    /// </summary>
    public int GetMass()
    {
        return massInGrammes;
    }

    /// <summary>
    /// Add the attatched weight to the scales
    /// </summary>
    private void AddToScales()
    {
        // Set scales as parent
        transform.SetParent(ScalePanLeft.transform);

        // Mark as being on the scales
        onScales = true;
    }

    /// <summary>
    /// Add the weight to the rack. If the player hadn't previously found it, it will now be marked as found.
    /// </summary>
    private void AddToRack()
    {
        // Assign rack as parent
        transform.SetParent(WeightRack.transform);

        // If was hidden
        if (!playerHasFound)
        {
            // Mark as found
            playerHasFound = true;
        }

        // If was on scales
        if (onScales)
        {
            // Mark as Off Scales
            onScales = false;
        }
    }

    /// <summary>
    /// Adds the weight to the rack or to the scales, dependant on current location/state
    /// </summary>
    public void MoveWeight()
    {
        // If weight is on the scales or hidden in the room
        if ((isMovable) && (onScales || !playerHasFound))
        {
            // Add it to the rack
            AddToRack();
        }

        // If on the rack
        if (isMovable && playerHasFound && !onScales)
        {
            // Add it to the scales
            AddToScales();
        }

        // Move it to the correct location
        transform.position = transform.parent.position; //TODO - Assign a more specific location (e.g. if 100g -> (rack.x - 1, rack.y, rack.z), if 200g -> (rack.x - 0.8....) etc.)

        //Update Scales
        scalesScript.ReviewWeight();
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            MoveWeight();
            journal.TickOffTask("Balance scales");
            subtitles.PlayAudio(Subtiles_HR.ID.P5_LINE3);
            GameTesting_CW.instance.arePuzzlesDone[4] = true;
        }
    }
}

// V0.1.0 - Last Update: 2020/02/09 @ 19:25 by AG
// V0.1.0 - Last Update: 2020/02/18 @ 18:30 by AG - Summary: Replaced Start with Awake to solve issues when loading assets. Originally changed a week ago, but change seems to have been lost in version control.
