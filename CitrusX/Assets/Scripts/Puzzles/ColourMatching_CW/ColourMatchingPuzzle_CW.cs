/*Chase Wilding
 * Colour matching script
 * This scripts main function 'Start colour Matching puzzle' can be called during game play to add it to the journal and start the process
 * when this puzzle begins

 * 
 * Dominique (Changes) 11/02/2020
 * Removed unused package imports
 * 
 * Chase (Changes) 22/2/2020
 * Added voiceover bools and comments, linked puzzle to door and fleshed
 * out the entire puzzle

 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourMatchingPuzzle_CW : MonoBehaviour
{
    #region VARIABLES
    internal bool isActive = false;
    private bool[] voiceovers = { false, false, false, false, false };
    internal bool[] isDoorInteractedWith = { false, false };
    private bool hasKeyPart1 = false;
    internal bool hasKeyPart2 = false;
    private TriggerScript_CW ritualTrigger;

    private Journal_DR journal;
    private Door_DR door;
    private Subtiles_HR subtitles;
    #endregion
    internal void SetActive(bool value) { isActive = value; }
    public void Awake()
    {
        journal = Journal_DR.instance;
        subtitles = GameObject.Find("FirstPersonCharacter").GetComponent<Subtiles_HR>();
        ritualTrigger = GameObject.Find("RitualTrigger").GetComponent<TriggerScript_CW>();
    }
    private void Update()
    {
        journal = Journal_DR.instance;
        if (isActive)
        {
            if (!voiceovers[0])
            {
                //subtitles.PlayAudio(Subtiles_HR.ID.P3_LINE2);
                voiceovers[0] = true;
            }
            else if (isDoorInteractedWith[0] && !voiceovers[1])
            {
                subtitles.PlayAudio(Subtiles_HR.ID.P3_LINE3);
                journal.AddJournalLog("This door looks like it needs a key...maybe I should try the garage");
                journal.ChangeTasks(new string[] { "Bathroom Key" });
                voiceovers[1] = true;
            }
            else if (isDoorInteractedWith[0] && !hasKeyPart1 && voiceovers[1])
            {
                if (journal.AreTasksComplete())
                {
                    if (!voiceovers[2])
                    {
                        subtitles.PlayAudio(Subtiles_HR.ID.P3_LINE4);
                        voiceovers[2] = true;
                        journal.ChangeTasks(new string[] { "Bathroom Key Part 2" });
                        hasKeyPart1 = true;
                    }
                }
            }
            else if (!hasKeyPart2 && hasKeyPart1)
            {
                if (journal.AreTasksComplete())
                {
                    if (!voiceovers[3])
                    {
                        subtitles.PlayAudio(Subtiles_HR.ID.P3_LINE5);
                        voiceovers[3] = true;
                        hasKeyPart2 = true;
                    }
                }
            }
            else if (hasKeyPart2)
            {
                if (isDoorInteractedWith[1])
                {
                    if (!voiceovers[4])
                    {
                        subtitles.PlayAudio(Subtiles_HR.ID.P3_LINE6);
                        voiceovers[4] = true;

                        journal.AddJournalLog("Was that a ghost?! I better go back and see.");
                        ritualTrigger.allowedToBeUsed = true;
                        GameTesting_CW.instance.arePuzzlesDone[2] = true;
                    }
                }

            }
        }


    }


}
