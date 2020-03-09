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
* This is done using the mass of the children on the scales
* The first child is ignored as this is to be a text mesh that shows the weight on the scale
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

        //Ensure that the text on the pans is updated
        ComparePans();
    }

    /// <summary>
    /// The mass of the children of each pan is compared
    /// </summary>
    public void ComparePans()
    {
        float leftPanMass = 0, rightPanMass = 0;

        //i starts at 1 so it skips the text showing how much weight is on the pan
        for (int i = 1; i < leftPan.childCount; i++)
        {
            leftPanMass += leftPan.GetChild(i).GetComponent<Rigidbody>().mass;

        }
        for (int i = 1; i < rightPan.childCount; i++)
        {
            rightPanMass += rightPan.GetChild(i).GetComponent<Rigidbody>().mass;
        }

        //Show the weights on the pan
        leftPan.GetComponent<ScalePan_DR>().UpdateText(leftPanMass);
        rightPan.GetComponent<ScalePan_DR>().UpdateText(rightPanMass);

        //Once the puzzle is complete the pans don't make the door open/close or make the subtitle play
        if (!isComplete) {
            if (leftPanMass == rightPanMass)
            {
                print("COMPLETE");
                // if equal - puzzle complete
                isComplete = true;
                journal.TickOffTask("Balance scales");
                journal.AddJournalLog("The scales seem different know…how much longer until this ritual is over?");
                journal.ChangeTasks(new string[] { "Return to ritual" });
                subtitles.PlayAudio(Subtitles_HR.ID.P5_LINE3);
                GameTesting_CW.instance.arePuzzlesDone[4] = true;

                door.ToggleOpen();
            }
        }
    }
}

// V0.1.0 - Last Update: 2020/02/09 @ 19:25 by AG
