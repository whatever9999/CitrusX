/*Chase 26/2/2020
 * - A script to hold trigger information, to set them and to see what they are, handy for voiceovers
 * 
 * Chase (Changes) 1/2/2020
 * Added to the enum for the later puzzles
 * Added ifs for hidden mech and correct order and added an else if for ritual
 * 
 * Chase (Changes) 4/3/2020
 * Added door functionality and edited triggers
 * 
 * Chase (Changes) 9/3/2020
 * Added new journal entries/tasks and added a new trigger "Chessboard Extra" which is for the room that the chessboard puzzle opens
 * Chase (Changes) 16/3/2020
 * Added a few disturbances upon entrance
 * Chase (Changes) 25/3/2020
 * Earlier in the week I removed the Chess Exit trigger and the Chess Extra trigger. I also have added in the baron's room appearances.
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
        CORRECT_ORDER,
        CHESSBOARD_EXTRA_ROOM
    };
    public TRIGGER_TYPE type;
    public Door_DR relatedDoor;
    private Subtitles_HR subtitles;
    private Journal_DR journal;
    private Baron_DR baron;
    private Transform ritualRoom;
    private Transform gymRoom;
    
    /// <summary>
    /// Inititalise variables
    /// </summary>
    private void Awake()
    {
        subtitles = GameObject.Find("FirstPersonCharacter").GetComponent<Subtitles_HR>();
        journal = Journal_DR.instance;
        baron = GameObject.Find("Baron").GetComponent<Baron_DR>();
        ritualRoom = GameObject.Find("RitualBaronLocation").GetComponent<Transform>();
        gymRoom = GameObject.Find("GymBaronLocation").GetComponent<Transform>();
    }

    /// <summary>
    /// get trigger type, see if active, play relevant audio if so
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (type == TRIGGER_TYPE.GARDEN && allowedToBeUsed)
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
                journal.TickOffTask("Return to ritual");
                journal.AddJournalLog("I can’t take anymore, blowing out the candles will end the ritual. But have I counted the right amount of coins?");
                journal.ChangeTasks(new string[] { "Blow out candles" });
            }
        }
        if (type == TRIGGER_TYPE.CHESSBOARD && allowedToBeUsed)
        {
            if(GameTesting_CW.instance.arePuzzlesDone[4] && !GameTesting_CW.instance.arePuzzlesDone[5])
            {
                DisturbanceHandler_DR.instance.TriggerDisturbance(DisturbanceHandler_DR.DisturbanceName.PAWNFALL);
                DisturbanceHandler_DR.instance.TriggerDisturbance(DisturbanceHandler_DR.DisturbanceName.BOOKTURNPAGE);
                journal.TickOffTask("Check study");
                baron.AppearStill(ritualRoom,3.0f);
                journal.AddJournalLog("This book might have some information");
                journal.ChangeTasks(new string[] { "Read book" });
                baron.GetCoin();
                subtitles.PlayAudio(Subtitles_HR.ID.P6_LINE2);
                allowedToBeUsed = false;
            }
        }
        if (type == TRIGGER_TYPE.THROWING && allowedToBeUsed)
        {
            if (GameTesting_CW.instance.arePuzzlesDone[5])
            {
                subtitles.PlayAudio(Subtitles_HR.ID.P7_LINE2);
                journal.TickOffTask("Check the gym");
                journal.AddJournalLog("This is the same aura I got from the scales…I need to get rid of it now.");
                baron.AppearStill(gymRoom, 5.0f);
                journal.ChangeTasks(new string[] { "Button 1", "Button 2", "Button 3" });
                allowedToBeUsed = false;
            }
        }
        if(type == TRIGGER_TYPE.HIDDEN_MECH && allowedToBeUsed)
        {
            if(relatedDoor.GetState())
            {
                relatedDoor.ToggleOpen();
            }
            relatedDoor.unlocked = false;
            journal.TickOffTask("Check out library");
            subtitles.PlayAudio(Subtitles_HR.ID.P8_LINE2);
            journal.AddJournalLog("The door locked on its own but there must be something somewhere that’ll tell me how to get out.");
            journal.ChangeTasks(new string[] { "Find clue" });
           
            allowedToBeUsed = false;
        }
        if(type == TRIGGER_TYPE.CORRECT_ORDER && allowedToBeUsed)
        {
            if(relatedDoor.GetState())
            {
                relatedDoor.ToggleOpen();
            }
            relatedDoor.unlocked = false;
            subtitles.PlayAudio(Subtitles_HR.ID.P9_LINE2);
            journal.AddJournalLog("Locked in again? I should’ve seen it coming.");
            journal.ChangeTasks(new string[] { "Find a way out" });
            allowedToBeUsed = false;
        }
    }

    /// <summary>
    /// get trigger type, see if active, play relevant audio if so
    /// </summary>

}
