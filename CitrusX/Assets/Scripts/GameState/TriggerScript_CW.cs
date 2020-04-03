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
 * 
 * Chase (Changes) 3/4/2020
 * Added in baron appearances in relevant rooms
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
    public bool entering = false;
    #endregion
    #region TRIGGER_VARS
    public enum TRIGGER_TYPE
    {
        GARDEN,
        RITUAL,
        CHESSBOARD,
        THROWING,
        HIDDEN_MECH,
        CORRECT_ORDER,
        CHESSBOARD_EXTRA_ROOM,
        DINING_TO_RITUAL,
        DINING_TO_KITCHEN,
        KITCHEN_TO_PANTRY,
        DINING_TO_GYM,
        GYM_TO_WORKSHOP,
        LOUNGE
    };
    public TRIGGER_TYPE type;
    #endregion
    #region GAME_VARIABLES
    public Door_DR relatedDoor;
    private Subtitles_HR subtitles;
    private Journal_DR journal;
    private Baron_DR baron;
    private float baronTime = 5.0f;
    #endregion
    #region BARON_LOCATIONS
    private GameObject loungeLocation;
    private GameObject diningRoomLocation;
    private GameObject kitchenLocation;
    private GameObject pantryLocation;
    private GameObject gymLocation;
    private GameObject workshopLocation;
    #endregion

    /// <summary>
    /// Inititalise variables
    /// </summary>
    private void Awake()
    {
        subtitles = GameObject.Find("FirstPersonCharacter").GetComponent<Subtitles_HR>();
        journal = Journal_DR.instance;
        baron = GameObject.Find("Baron").GetComponent<Baron_DR>();
        #region INITIALISATION_OF_LOCATIONS
        loungeLocation = GameObject.Find("LoungeBaronLocation");
        diningRoomLocation = GameObject.Find("DiningRoomBaronLocation");
        kitchenLocation = GameObject.Find("KitchenBaronLocation");
        pantryLocation = GameObject.Find("PantryBaronLocation");
        gymLocation = GameObject.Find("GymBaronLocation");
        workshopLocation = GameObject.Find("WorkshopBaronLocation");
        #endregion
    }

    /// <summary>
    /// get trigger type, see if active, play relevant audio if so
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {

            #region GAMESTATE_TRIGGERS
            if (type == TRIGGER_TYPE.GARDEN && allowedToBeUsed)
            {
                //SOUND HERE for LARGE BOX FALLING (or another loud noise, just something distracting)
                DisturbanceHandler_DR.instance.TriggerDisturbance(DisturbanceHandler_DR.DisturbanceName.BOXFALL);
                subtitles.PlayAudio(Subtitles_HR.ID.P2_LINE1);
                allowedToBeUsed = false;
                if (GameTesting_CW.instance.arePuzzlesDone[1] && !GameTesting_CW.instance.arePuzzlesDone[2])
                {
                    subtitles.PlayAudio(Subtitles_HR.ID.P3_LINE1);
                    allowedToBeUsed = false;
                }
            }
            else if (type == TRIGGER_TYPE.RITUAL && allowedToBeUsed)
            {
                if (GameTesting_CW.instance.arePuzzlesDone[1] && !GameTesting_CW.instance.arePuzzlesDone[2])

                {
                    if (GameTesting_CW.instance.arePuzzlesDone[1] && !GameTesting_CW.instance.arePuzzlesDone[2])
                    {
                        journal.TickOffTask("Return to ritual");
                        allowedToBeUsed = false;
                    }
                    else if (GameTesting_CW.instance.arePuzzlesDone[2] && !GameTesting_CW.instance.arePuzzlesDone[3])
                    {
                        journal.TickOffTask("Return to ritual");
                        subtitles.PlayAudio(Subtitles_HR.ID.P4_LINE2);
                        journal.AddJournalLog("When I hear that water ripple, I should check my phone’s camera");
                        allowedToBeUsed = false;
                    }
                    else if (GameTesting_CW.instance.arePuzzlesDone[8])
                    {
                        subtitles.PlayAudio(Subtitles_HR.ID.P10_LINE2);
                        journal.TickOffTask("Return to ritual");
                        journal.AddJournalLog("I can’t take anymore, blowing out the candles will end the ritual. But have I counted the right amount of coins?");
                        journal.ChangeTasks(new string[] { "Blow out candles" });
                    }
                }

            }
            else if (type == TRIGGER_TYPE.CHESSBOARD && allowedToBeUsed)
            {
                if (GameTesting_CW.instance.arePuzzlesDone[4] && !GameTesting_CW.instance.arePuzzlesDone[5])
                {
                    DisturbanceHandler_DR.instance.TriggerDisturbance(DisturbanceHandler_DR.DisturbanceName.PAWNFALL);
                    DisturbanceHandler_DR.instance.TriggerDisturbance(DisturbanceHandler_DR.DisturbanceName.BOOKTURNPAGE);
                    journal.TickOffTask("Check out lounge");
                    journal.AddJournalLog("This book might have some information");
                    journal.ChangeTasks(new string[] { "Read book" });
                    subtitles.PlayAudio(Subtitles_HR.ID.P6_LINE2);
                    allowedToBeUsed = false;
                }
            }
            else if (type == TRIGGER_TYPE.THROWING && allowedToBeUsed)
            {
                if (GameTesting_CW.instance.arePuzzlesDone[5])
                {
                    subtitles.PlayAudio(Subtitles_HR.ID.P7_LINE2);
                    journal.TickOffTask("Check the gym");
                    journal.AddJournalLog("This is the same aura I got from the scales…I need to get rid of it now.");
                    journal.ChangeTasks(new string[] { "Button 1", "Button 2", "Button 3" });
                    allowedToBeUsed = false;
                }
            }
            else if (type == TRIGGER_TYPE.HIDDEN_MECH && allowedToBeUsed)
            {
                if (GameTesting_CW.instance.arePuzzlesDone[4] && !GameTesting_CW.instance.arePuzzlesDone[5])
                {
                    if (relatedDoor.GetState())
                    {
                        relatedDoor.ToggleOpen();
                    }
                    relatedDoor.unlocked = false;
                    journal.TickOffTask("Check out library");
                    subtitles.PlayAudio(Subtitles_HR.ID.P8_LINE2);
                    journal.AddJournalLog("The door locked on its own but there must be something somewhere that’ll tell me how to get out.");
                    journal.ChangeTasks(new string[] { "Find a clue" });

                    allowedToBeUsed = false;
                }

            }
            else if (type == TRIGGER_TYPE.CORRECT_ORDER && allowedToBeUsed)
            {
                if (GameTesting_CW.instance.arePuzzlesDone[5])
                {
                    if (relatedDoor.GetState())
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
        #endregion
        #region BARON_TRIGGERS
            else if (type == TRIGGER_TYPE.DINING_TO_GYM && entering)
            {
                entering = false;
            }
            else if (type == TRIGGER_TYPE.DINING_TO_GYM && !entering)
            {
                if (!baron.gettingCoin)
                {
                    baron.AppearStill(gymLocation.transform, baronTime);
                }
                entering = true;
            }
            else if (type == TRIGGER_TYPE.DINING_TO_KITCHEN && entering)
            {
               entering = false;
            }
            else if (type == TRIGGER_TYPE.DINING_TO_KITCHEN && !entering)
            {
                if (!baron.gettingCoin)
                {
                    baron.AppearStill(kitchenLocation.transform, baronTime);
                }

                entering = true;
            }
            else if (type == TRIGGER_TYPE.DINING_TO_RITUAL && entering)
            {
                entering = false;
            }
            else if (type == TRIGGER_TYPE.DINING_TO_RITUAL && !entering)
            {
                if (!baron.gettingCoin)
                {
                    baron.AppearStill(diningRoomLocation.transform, baronTime);
                }
                entering = true;
            }
            else if (type == TRIGGER_TYPE.GYM_TO_WORKSHOP && entering)
            {
                entering = false;
            }
            else if (type == TRIGGER_TYPE.GYM_TO_WORKSHOP && !entering)
            {
                if (!baron.gettingCoin)
                {
                    baron.AppearStill(workshopLocation.transform, baronTime);
                }
                entering = true;
            }
            else if (type == TRIGGER_TYPE.LOUNGE && entering)
            {
                entering = false;
            }
            else if (type == TRIGGER_TYPE.LOUNGE && !entering)
            {
                if (!baron.gettingCoin)
                {
                    baron.AppearStill(loungeLocation.transform, baronTime);
                }
                entering = true;
            }
            else if (type == TRIGGER_TYPE.KITCHEN_TO_PANTRY && entering)
            {
                entering = false;
            }
            else if (type == TRIGGER_TYPE.KITCHEN_TO_PANTRY && !entering)
            {
                if (!baron.gettingCoin)
                {
                    baron.AppearStill(pantryLocation.transform, baronTime);
                }
                entering = true;
            } 
            #endregion
        



    }
    /// <summary>
    /// get trigger type, see if active, play relevant audio if so
    /// </summary>

}
