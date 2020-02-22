/*Chase Wilding 11/2/2020
 * This puzzle sets up the ritual scene and allows for progression into the second puzzle
 * 
 * Dominique (Changes) 11/02/2020
 * Removed unused imports
 * Got a reference to journal to reduce repetition of 'Journal_DR.instance...'
 * 
 * Chase Wilding (Changes) 11/2/2020
 * Changed to else if statements based off Dominique's feedback
 */
using UnityEngine;

public class SetUpRitual_CW : MonoBehaviour
{
    Journal_DR journal;
    #region BOOLS
    private bool isActive = false;

    internal bool jewelleryCollectionInitiated = false;
    internal bool ritualSetUpCollected = false;
    internal bool ritualSetUpPlaced = false;
    internal bool jewelleryCollected = false;
    internal bool jewelleryPlaced = false;
    #endregion

    private void Awake()
    {
        journal = Journal_DR.instance;
    }

    private bool[] voiceovers = { false, false, false, false, false, false, false, false, false };
    internal void SetActive(bool value) { isActive = value; }

    void Update()
    {
        //if nothing has been collected
        if(isActive)
        {
            if(!voiceovers[0])
            {
                //VOICEOVER 1-1
                voiceovers[0] = true;
            }
            if (!ritualSetUpCollected)
            {
                //check until the tasks are done
                if (journal.AreTasksComplete())
                {
                    if(!voiceovers[2])
                    {
                        //VOICEOVER 1-3
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
                    if(!voiceovers[3])
                    {
                        //VOICEOVER 1-4
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
                        //VOICEOVER 1-5
                        voiceovers[4] = true;
                    }
                    journal.AddJournalLog("I should put this in the garden box");
                    journal.ChangeTasks(new string[] { "Place in garden" });
                    jewelleryCollected = true;
                }
            }
            else if (jewelleryCollected && !jewelleryPlaced)
            {
              
                //if these final tasks are done
                if (journal.AreTasksComplete())
                {//tell the game the puzzle is complete
                    if (!voiceovers[5])
                    {
                        //VOICEOVER 1-6
                        voiceovers[5] = true;
                    }
                    if (!voiceovers[6])
                    {
                        //VOICEOVER 1-7
                        voiceovers[6] = true;
                    }
                    GameTesting_CW.instance.arePuzzlesDone[0] = true;
                }
            }
        }
    }
        
}
