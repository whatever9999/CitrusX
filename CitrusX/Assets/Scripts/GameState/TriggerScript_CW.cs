﻿/*Chase 26/2/2020
 * - A script to hold trigger information, to set them and to see what they are, handy for voiceovers
 * 
 * Chase (Changes) 1/2/2020
 * Added to the enum for the later puzzles
 * Added ifs for hidden mech and correct order and added an else if for ritual
 * 
 * Chase (Changes) 4/3/2020
 * Added door functionality and edited triggers
 */

/**
* \class TriggerScript_CW
* 
* \brief When a player enters the trigger it will cause disturbances or start audio clips according to the state of the game for storytelling
* 
* The trigger behaves differently according to an enum that identifies where it is
* 
* \author Chase
* 
* \date Last Modified: 04/03/2020
*/

using UnityEngine;

public class TriggerScript_CW : MonoBehaviour
{
    #region DOORS_AND_BOOLS
    private Door_DR hiddenMechDoor;
    private Door_DR correctOrderDoor;
    public bool allowedToBeUsed;
    public bool activated;
    #endregion
    public enum TRIGGER_TYPE
    {
        GARDEN,
        RITUAL,
        CHESSBOARD,
        THROWING,
        HIDDEN_MECH,
        CORRECT_ORDER
    };
    public TRIGGER_TYPE type;
    private Subtitles_HR subtitles;
    private Journal_DR journal;
    
    /// <summary>
    /// Inititalise variables
    /// </summary>
    private void Awake()
    {
        subtitles = GameObject.Find("FirstPersonCharacter").GetComponent<Subtitles_HR>();
        journal = Journal_DR.instance;
        hiddenMechDoor = GameObject.Find("HiddenMechDoor").GetComponent<Door_DR>();
        correctOrderDoor = GameObject.Find("CorrectOrderDoor").GetComponent<Door_DR>();
    }

    /// <summary>
    /// get trigger type, see if active, play relevant audio if so
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (type == TRIGGER_TYPE.GARDEN && !activated && allowedToBeUsed)
        {
            DisturbanceHandler_DR.instance.TriggerDisturbance(DisturbanceHandler_DR.DisturbanceName.BOXFALL);
            subtitles.PlayAudio(Subtitles_HR.ID.P2_LINE1);
            allowedToBeUsed = false;
        }
        if(type == TRIGGER_TYPE.RITUAL && allowedToBeUsed)
        {
            if(GameTesting_CW.instance.arePuzzlesDone[1])
            {
                journal.TickOffTask("Return to ritual");
                subtitles.PlayAudio(Subtitles_HR.ID.P3_LINE1);
                allowedToBeUsed = false;
            }
            else if(GameTesting_CW.instance.arePuzzlesDone[2])
            {
                journal.TickOffTask("Return to ritual");
                subtitles.PlayAudio(Subtitles_HR.ID.P4_LINE1);
                journal.AddJournalLog("When I hear that water ripple, I should check my phone’s camera");
                allowedToBeUsed = false;
            }
            else if(GameTesting_CW.instance.arePuzzlesDone[8])
            {
                subtitles.PlayAudio(Subtitles_HR.ID.P10_LINE2);
                journal.AddJournalLog("That should be it. Have I counted enough coins? I should blow out the candles if I have.");
                journal.ChangeTasks(new string[] { "Blow out candles" });
            }
        }
        if (type == TRIGGER_TYPE.CHESSBOARD && allowedToBeUsed)
        {
            if(GameTesting_CW.instance.arePuzzlesDone[4])
            {
                DisturbanceHandler_DR.instance.TriggerDisturbance(DisturbanceHandler_DR.DisturbanceName.BOOKTURNPAGE);
                journal.TickOffTask("Check study");
                journal.AddJournalLog("This book might have some information");
                journal.ChangeTasks(new string[] { "Read the book" });
                subtitles.PlayAudio(Subtitles_HR.ID.P6_LINE2);
                allowedToBeUsed = false;
            }
        }
        if (type == TRIGGER_TYPE.THROWING && allowedToBeUsed)
        {
            if (GameTesting_CW.instance.arePuzzlesDone[5])
            {
                subtitles.PlayAudio(Subtitles_HR.ID.P7_LINE2);
                allowedToBeUsed = false;
            }
        }
        if(type == TRIGGER_TYPE.HIDDEN_MECH && allowedToBeUsed)
        {
            journal.ChangeTasks(new string[] { "Find Book" });
            subtitles.PlayAudio(Subtitles_HR.ID.P8_LINE2);
            journal.AddJournalLog("Hmm...maybe if I find some sort of mechanism I can open this door...");
           
            allowedToBeUsed = false;
        }
        if(type == TRIGGER_TYPE.CORRECT_ORDER && allowedToBeUsed)
        {
            correctOrderDoor.ToggleOpen();
            correctOrderDoor.unlocked = false;
            subtitles.PlayAudio(Subtitles_HR.ID.P9_LINE2);
            journal.AddJournalLog("Is there some kind of pattern here? Maybe I could recreate it.");
            journal.ChangeTasks(new string[] { "repeat the sequence" });
            allowedToBeUsed = false;
        }
    }

    /// <summary>
    /// get trigger type, see if active, play relevant audio if so
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if(type == TRIGGER_TYPE.CHESSBOARD && allowedToBeUsed)
        {
            if (GameTesting_CW.instance.arePuzzlesDone[5])
            {
                subtitles.PlayAudio(Subtitles_HR.ID.P6_LINE5);
                allowedToBeUsed = false;
            }
        }
    }
}
