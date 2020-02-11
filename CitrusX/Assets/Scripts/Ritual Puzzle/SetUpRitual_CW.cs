/*Chase Wilding 11/2/2020
 * This puzzle sets up the ritual scene and allows for progression into the second puzzle
 * 
 * Chase Wilding (Changes) 11/2/2020
 * Changed to else if statements based off Dominique's feedback
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpRitual_CW : MonoBehaviour
{
    #region BOOLS
    private bool isActive = false;

    internal bool jewelleryCollectionInitiated = false;
    internal bool ritualSetUpCollected = false;
    internal bool ritualSetUpPlaced = false;
    internal bool jewelleryCollected = false;
    internal bool jewelleryPlaced= false;
    #endregion
    internal void SetActive(bool value) { isActive = value; }
    void Update()
    {
        //if nothing has been collected
        if(!ritualSetUpCollected)
        {
            //check until the tasks are done
            if (Journal_DR.instance.AreTasksComplete())
            {
                //change tasks
                Journal_DR.instance.AddJournalLog("Now I need to place it on the table");
                Journal_DR.instance.ChangeTasks(new string[] { "Place on table"});
                //mark step as complete
                ritualSetUpCollected = true;
            }
            
        }
        else if(ritualSetUpCollected && !ritualSetUpPlaced)
        {
            if (Journal_DR.instance.AreTasksComplete())
            {
                Journal_DR.instance.AddJournalLog("Now I need to get the jewellery and move it to the house...");
                Journal_DR.instance.ChangeTasks(new string[] { "necklace", "jewellery box", "bracelet", "pendant" });
                ritualSetUpPlaced = true;
            }
        }
        else if (ritualSetUpPlaced && !jewelleryCollected)
        {
            if (Journal_DR.instance.AreTasksComplete())
            {
                Journal_DR.instance.AddJournalLog("I should put this in the garden box");
                Journal_DR.instance.ChangeTasks(new string[] { "Place in garden" });
                jewelleryCollected = true;
            }
        }
        else if(jewelleryCollected && !jewelleryPlaced)
        {
            //if these final tasks are done
            if(Journal_DR.instance.AreTasksComplete())
            {//tell the game the puzzle is complete
                GameTesting_CW.instance.arePuzzlesDone[0] = true;
            }
        }
    }
}
