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
 * Chase (Changes) 11/3/2020
 * Tidied script and got it to inherit from a base class to remove repetition between classes
 * Chase (Changes) 18/3/2020
 * Added symbols of scarcity and doors to this puzzle so that they show at the right times.
 */

/**
* \class SetUpRitual_CW
* 
* \brief This puzzle sets up the ritual scene and allows for progression into the next puzzle
* 
* \author Chase
* 
* \date Last Modified: 26/02/2020
*/
using System.Collections;
using UnityEngine;

internal class SetUpRitual_CW : PuzzleBaseScript
{

    #region BOOLS
    internal bool[] ritualSteps = { false, false, false, false, false, false, false };
    #endregion
    #region GAME_OBJECTS AND DOORS
    private GameObject necklace;
    private GameObject pendant;
    private GameObject jewelleryBox;
    private GameObject bracelet;
    private GameObject symbol1;
    private GameObject symbol2;
    private GameObject symbol3;
    private GameObject symbol4;
    public Door_DR door1;
    public Door_DR door2;
    public Door_DR door3;
    public Door_DR door4;
    public Door_DR door5;
    private GameObject laptopScreen;
    #endregion
    /// <summary>
    /// Inititalise variables
    /// </summary>
    private void Awake()
    {
        #region INITIALISATION
        journal = GameObject.Find("FirstPersonCharacter").GetComponent<Journal_DR>();
        necklace = GameObject.Find("Necklace");
        pendant = GameObject.Find("Pendant");
        jewelleryBox = GameObject.Find("Jewellery Box");
        bracelet = GameObject.Find("Bracelet");

        symbol1 = GameObject.Find("Bathroom Symbol of Scarcity");
        symbol2 = GameObject.Find("Dining Room Symbol of Scarcity");
        symbol3 = GameObject.Find("Living Room Symbol of Scarcity");
        symbol4 = GameObject.Find("Study Symbol of Scarcity");
        laptopScreen = GameObject.Find("LaptopScreen");
        #endregion
    }

    /// <summary>
    /// If the puzzle is active then the voiceover for it is played.
    /// If the setup isn't complete yet a check is made on the tasks in the journal. Once it is complete then the log and tasks are updated and the next puzzle is started
    /// This is done for the ritual setup and the jewellery setup
    /// </summary>
    private void Update()
    {
        //if nothing has been collected
        if (isActive)
        {
            if (!voiceovers[0])
            {
                #region ITEMS_TO_ACTIVATE
                symbol1.SetActive(false);
                symbol2.SetActive(false);
                symbol3.SetActive(false);
                symbol4.SetActive(false);
                jewelleryBox.SetActive(false);
                necklace.SetActive(false);
                pendant.SetActive(false);
                bracelet.SetActive(false);
                #endregion

                subtitles.PlayAudio(Subtitles_HR.ID.P1_LINE1);
                voiceovers[0] = true;
            }
            if (!ritualSteps[0])
            {
                //check until the tasks are done
                if (journal.AreTasksComplete())
                {
                    if (!voiceovers[1])
                    {
                        subtitles.PlayAudio(Subtitles_HR.ID.P1_LINE3);
                        voiceovers[1] = true;
                    }
                    //change tasks
                    journal.AddJournalLog("Now I need to place it on the table");
                    journal.ChangeTasks(new string[] { "Place on table" });
                    //mark step as complete
                    ritualSteps[0] = true;
                }

            }
            else if (ritualSteps[0] && !ritualSteps[1])
            {
                if (journal.AreTasksComplete())
                {
                   if(!voiceovers[2])
                    {
                        #region RITUAL_SETUP
                        if (!door1.GetState())
                        {
                            door1.ToggleOpen();
                        }
                        if(!door2.GetState())
                        {
                            door2.ToggleOpen();
                        }
                        if(!door3.GetState())
                        {
                            door3.ToggleOpen();
                        }
                        if(!door4.GetState())
                        {
                            door4.ToggleOpen();
                        }
                        if(!door5.GetState())
                        {
                            door5.ToggleOpen();
                        }
                        symbol1.SetActive(true);
                        symbol2.SetActive(true);
                        symbol3.SetActive(true);
                        symbol4.SetActive(true);
                        laptopScreen.SetActive(false);
                        #endregion
                        subtitles.PlayAudio(Subtitles_HR.ID.P1_LINE8);
                        voiceovers[2] = true;
                    }
                    journal.AddJournalLog("I’ve set that up and put the items of scarcity around to ward off the Baron, now I should check out the security system and see how it works.");
                    journal.ChangeTasks(new string[] { "Check the monitor" });
                    ritualSteps[1] = true;
                }
            }
            else if(ritualSteps[1] && !ritualSteps[2])
            {
                if(journal.AreTasksComplete())
                {
                    if (!voiceovers[3])
                    {
                        subtitles.PlayAudio(Subtitles_HR.ID.P1_LINE10);
                        voiceovers[3] = true;
                    }
                    journal.AddJournalLog("I need to keep the doors downstairs open otherwise the ritual ends, I can keep an eye on this room from my phone camera.");
                    journal.AddJournalLog("I can press C to open my phone if I remember correctly");
                    journal.ChangeTasks(new string[] { "Check phone camera" });
                    ritualSteps[2] = true;
                }
            }
            else if(ritualSteps[2] && !ritualSteps[3])
            {
                if(journal.AreTasksComplete())
                {
                    if (!voiceovers[4])
                    {
                        subtitles.PlayAudio(Subtitles_HR.ID.P1_LINE4);
                        voiceovers[4] = true;
                    }
                    journal.AddJournalLog("Now I need to get the jewellery and move it to the house...");
                    bool setJewellery = false;
                    if(!setJewellery)
                    {
                        pendant.SetActive(true);
                        necklace.SetActive(true);
                        jewelleryBox.SetActive(true);
                        bracelet.SetActive(true);
                        setJewellery = true;
                    }
                    
                    journal.ChangeTasks(new string[] { "necklace", "jewellery box", "bracelet", "pendant" });
                    ritualSteps[3] = true;
                }
            }
            else if (ritualSteps[3] && !ritualSteps[4])
            {
                if (journal.AreTasksComplete())
                {
                    if (!voiceovers[5])
                    {
                        subtitles.PlayAudio(Subtitles_HR.ID.P1_LINE5);
                        voiceovers[5] = true;
                    }
                    journal.AddJournalLog("I should put this in the garden box");
                    journal.ChangeTasks(new string[] { "Place in garden" });
                    ritualSteps[4] = true;
                }
            }
            else if (ritualSteps[4] && ritualSteps[5])
            {

                //if these final tasks are done
                if (journal.AreTasksComplete())
                {//tell the game the puzzle is complete
                    if (!voiceovers[6])
                    {
                        subtitles.PlayAudio(Subtitles_HR.ID.P1_LINE6);
                        voiceovers[6] = true;
                        if (!voiceovers[7])
                        {
                            subtitles.PlayAudio(Subtitles_HR.ID.P1_LINE7);
                            //allow ritual trigger to be active
                            gardenTrigger.allowedToBeUsed = true;
                            journal.ChangeTasks(new string[] { "Return to ritual" });
                            ritualTrigger.allowedToBeUsed = true;
                            GameTesting_CW.instance.arePuzzlesDone[0] = true;
                        }
                    }


                }
            }

        }
    }
}
