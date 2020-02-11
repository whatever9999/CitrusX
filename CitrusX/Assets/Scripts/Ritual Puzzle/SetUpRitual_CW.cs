/*Chase Wilding 11/2/2020
 * This puzzle sets up the ritual scene and allows for progression into the second puzzle
 * 
 * Dominique (Changes) 11/02/2020
 * Removed unused imports
 * Got a reference to journal to reduce repetition of 'Journal_DR.instance...'
 */
using UnityEngine;

public class SetUpRitual_CW : MonoBehaviour
{
    Journal_DR journal;
    #region BOOLS
    internal bool SetActive = false;
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
    void Update()
    {
        //if nothing has been collected
        if(!ritualSetUpCollected)
        {
            //check until the tasks are done
            if (journal.AreTasksComplete())
            {
                //change tasks
                journal.AddJournalLog("Now I need to place it on the table");
                journal.ChangeTasks(new string[] { "Place on table"});
                //mark step as complete
                ritualSetUpCollected = true;
            }
            
        }
        if(ritualSetUpCollected && !ritualSetUpPlaced)
        {
            if (journal.AreTasksComplete())
            {
                journal.AddJournalLog("Now I need to get the jewellery and move it to the house...");
                journal.ChangeTasks(new string[] { "necklace", "jewellery box", "bracelet", "pendant" });
                ritualSetUpPlaced = true;
            }
        }
        if (ritualSetUpPlaced && !jewelleryCollected)
        {
            if (journal.AreTasksComplete())
            {
                journal.AddJournalLog("I should put this in the garden box");
                journal.ChangeTasks(new string[] { "Place in garden" });
                jewelleryCollected = true;
            }
        }
        if(jewelleryCollected && !jewelleryPlaced)
        {
            //if these final tasks are done
            if(journal.AreTasksComplete())
            {//tell the game the puzzle is complete
                GameTesting_CW.instance.arePuzzlesDone[0] = true;
            }
            
        }


    }
}
