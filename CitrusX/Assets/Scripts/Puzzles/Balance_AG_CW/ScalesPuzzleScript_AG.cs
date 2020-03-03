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
 *  Edited the puzzle to make it playable and linked it to interact for the mean time by manipulating the weights
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalesPuzzleScript_AG : MonoBehaviour
{
    public Vector2[] whereToPutWeightsAccordingToPan;

    // Each side of scales
    private Transform leftPan;
    private Transform rightPan;
    private Door_DR door;
    private Subtiles_HR subtitles;

    // Puzzle State
    private bool isComplete = false;

    // Weights to compare
    public int leftMass;
    public int rightMass;

    private Journal_DR journal;
    private bool isActive = false;
    private float zPosOfWeights;

    public void SetActive(bool value) { isActive = value; }

    private void Awake()
    {
        leftPan = GameObject.Find("LeftPan").transform;
        rightPan = GameObject.Find("RightPan").transform;
        door = GameObject.Find("ScalesDoor").GetComponent<Door_DR>();
        journal = Journal_DR.instance;
        subtitles = GameObject.Find("FirstPersonCharacter").GetComponent<Subtiles_HR>();

        zPosOfWeights = GameObject.Find("WeightToGetZPosition").transform.localPosition.z;
    }

    public void MoveWeight(Transform weight)
    {
        weight.parent = leftPan;

        //Get a random position for the weight to be in, making sure that it doesn't collide with any others
        Vector3 newPosition = Vector3.zero;
        newPosition.z = zPosOfWeights;
        newPosition.y = whereToPutWeightsAccordingToPan[leftPan.childCount - 1].y;
        newPosition.x = whereToPutWeightsAccordingToPan[leftPan.childCount - 1].x;

        weight.localPosition = newPosition;

        //You can't move the weight anymore
        weight.tag = "Untagged";
        weight.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

        ComparePans();
    }

    public void ComparePans()
    {
        if (rightPan.childCount == leftPan.childCount)
        {
            print("COMPLETE");
            // if equal - puzzle complete
            isComplete = true;
            //  journal.TickOffTask("Balance scales");
            subtitles.PlayAudio(Subtiles_HR.ID.P5_LINE3);
            GameTesting_CW.instance.arePuzzlesDone[4] = true;

            door.ToggleOpen();
        }
    }
}

// V0.1.0 - Last Update: 2020/02/09 @ 19:25 by AG
