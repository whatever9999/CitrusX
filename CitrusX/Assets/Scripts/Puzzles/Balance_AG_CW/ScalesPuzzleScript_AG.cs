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

/**
* \class ScalesPuzzleScript_AG
* 
* \brief Compares the children of the left and right pan of the scales to see if they are balanced
* 
* \author Adam
* 
* \date Last Modified: 09/02/2020
*/

using UnityEngine;

public class ScalesPuzzleScript_AG : MonoBehaviour
{
    public Vector2[] whereToPutWeightsAccordingToPan;

    // Each side of scales
    private Transform leftPan;
    private Transform rightPan;
    private Door_DR door;
    private Subtitles_HR subtitles;

    // Puzzle State
    private bool isComplete = false;

    // Weights to compare
    public int leftMass;
    public int rightMass;

    private Journal_DR journal;
    private bool isActive = false;
    private float zPosOfWeights;

    public void SetActive(bool value) { isActive = value; }

    /// <summary>
    /// Initialise the variables
    /// </summary>
    private void Awake()
    {
        leftPan = GameObject.Find("LeftPan").transform;
        rightPan = GameObject.Find("RightPan").transform;
        door = GameObject.Find("ScalesDoor").GetComponent<Door_DR>();
        journal = Journal_DR.instance;
        subtitles = GameObject.Find("FirstPersonCharacter").GetComponent<Subtitles_HR>();

        zPosOfWeights = GameObject.Find("WeightToGetZPosition").transform.localPosition.z;
    }

    /// <summary>
    /// When a weight is moved its parent is set along with its position on the pan
    /// Then the pans are compared to see if they're balanced
    /// </summary>
    /// <param name="weight - the transform of the weight so its position can be moved and parent can be set"></param>
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

    /// <summary>
    /// The number of children of the left pan is compared to the right
    /// If they are equal the game state is updated
    /// </summary>
    public void ComparePans()
    {
        if (rightPan.childCount == leftPan.childCount)
        {
            print("COMPLETE");
            // if equal - puzzle complete
            isComplete = true;
            //  journal.TickOffTask("Balance scales");
            subtitles.PlayAudio(Subtitles_HR.ID.P5_LINE3);
            GameTesting_CW.instance.arePuzzlesDone[4] = true;

            door.ToggleOpen();
        }
    }
}

// V0.1.0 - Last Update: 2020/02/09 @ 19:25 by AG
