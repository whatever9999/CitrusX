/*Chase Wilding 11/2/2020
 * This puzzle sets up the ritual scene and allows for progression into the second puzzle
 * 
 * Dominique (Changes) 11/02/2020
 * Removed unused imports
 * Got a reference to journal to reduce repetition of 'Journal_DR.instance...'
 * 
 * Chase Wilding (Changes) 11/2/2020
 * Changed to else if statements based off Dominique's feedback
 * 
 * Chase Wilding (Changes) 26/2/2020
 * added reference to trigger box, added bools and subtitles for a continous game state
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpRitual_CW : MonoBehaviour
{
    Journal_DR journal;
    Subtiles_HR subtitles;
    #region BOOLS
    private bool isActive = false;

    internal bool jewelleryCollectionInitiated = false;
    internal bool ritualSetUpCollected = false;
    internal bool ritualSetUpPlaced = false;
    internal bool jewelleryCollected = false;
    internal bool jewelleryPlaced = false;
    private TriggerScript_CW gardenTrigger;
    #endregion

    private void Awake()
    {
        journal = Journal_DR.instance;
        subtitles = GameObject.Find("FirstPersonCharacter").GetComponent<Subtiles_HR>();
        gardenTrigger = GameObject.Find("GardenTrigger").GetComponent<TriggerScript_CW>();
    }

    private bool[] voiceovers = { false, false, false, false, false, false, false, false, false };
    internal void SetActive(bool value) { isActive = value; }

    void Update()
    {
        //if nothing has been collected
        if (isActive)
        {
            if (!voiceovers[0])
            {
                subtitles.PlayAudio(Subtiles_HR.ID.P1_LINE1);
                voiceovers[0] = true;
            }
            if (!ritualSetUpCollected)
            {
                //check until the tasks are done
                if (journal.AreTasksComplete())
                {
                    if (!voiceovers[2])
                    {
                        subtitles.PlayAudio(Subtiles_HR.ID.P1_LINE3);
                        voiceovers[2] = true;
                    }
                    //change tasks
                    journal.AddJournalLog("Now I need to place it on the table");
                    journal.ChangeTasks(new string[] { "Place on table" });
                    //mark step as complete
                    ritualSetUpCollected = true;
                }

            }
            else if (ritualSetUpCollected && !ritualSetUpPlaced)
            {
                if (journal.AreTasksComplete())
                {
                    if (!voiceovers[3])
                    {
                        subtitles.PlayAudio(Subtiles_HR.ID.P1_LINE4);
                        voiceovers[3] = true;
                    }
                    journal.AddJournalLog("Now I need to get the jewellery and move it to the house...");
                    journal.ChangeTasks(new string[] { "necklace", "jewellery box", "bracelet", "pendant" });
                    ritualSetUpPlaced = true;
                }
            }
            else if (ritualSetUpPlaced && !jewelleryCollected)
            {
                if (journal.AreTasksComplete())
                {
                    if (!voiceovers[4])
                    {
                        subtitles.PlayAudio(Subtiles_HR.ID.P1_LINE5);
                        voiceovers[4] = true;
                    }
                    journal.AddJournalLog("I should put this in the garden box");
                    journal.ChangeTasks(new string[] { "Place in garden" });
                    jewelleryCollected = true;
                }
            }
            else if (jewelleryCollected && jewelleryPlaced)
            {

                //if these final tasks are done
                if (journal.AreTasksComplete())
                {//tell the game the puzzle is complete
                    if (!voiceovers[5])
                    {
                        subtitles.PlayAudio(Subtiles_HR.ID.P1_LINE6);
                        voiceovers[5] = true;
                        if (!voiceovers[6])
                        {
                            subtitles.PlayAudio(Subtiles_HR.ID.P1_LINE7);
                            //allow ritual trigger to be active
                            gardenTrigger.allowedToBeUsed = true;
                            GameTesting_CW.instance.arePuzzlesDone[0] = true;
                        }
                    }


                }
            }

        }
    }
    IEnumerator Pause()
    {
        yield return new WaitForSeconds(1.0f);
    }

}
