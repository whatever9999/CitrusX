/*Chase 26/2/2020
 * - A script to hold trigger information, to set them and to see what they are, handy for voiceovers
 * 
 * Chase (Changes) 1/2/2020
 * Added to the enum for the later puzzles
 * Added ifs for hidden mech and correct order and added an else if for ritual
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript_CW : MonoBehaviour
{
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
    private Subtiles_HR subtitles;
    public bool allowedToBeUsed;
    public bool activated;
    private Journal_DR journal;
    private Door_DR hiddenMechDoor;
    private Door_DR correctOrderDoor;

    private void Awake()
    {
        subtitles = GameObject.Find("FirstPersonCharacter").GetComponent<Subtiles_HR>();
        journal = Journal_DR.instance;
       // hiddenMechDoor = GameObject.Find("HiddenMechDoor").GetComponent<Door_DR>();
      //  correctOrderDoor = GameObject.Find("CorrectOrderDoor").GetComponent<Door_DR>();
    }
 //get type, see if active, play relevant audio if so
    private void OnTriggerEnter(Collider other)
    {
        if (type == TRIGGER_TYPE.GARDEN && !activated && allowedToBeUsed)
        {
            DisturbanceHandler_DR.instance.TriggerDisturbance(DisturbanceHandler_DR.DisturbanceName.BOXFALL);
            subtitles.PlayAudio(Subtiles_HR.ID.P2_LINE1);
        }
        if(type == TRIGGER_TYPE.RITUAL && allowedToBeUsed)
        {
            if(GameTesting_CW.instance.arePuzzlesDone[1])
            {
                subtitles.PlayAudio(Subtiles_HR.ID.P3_LINE1);
                allowedToBeUsed = false;
            }
            else if(GameTesting_CW.instance.arePuzzlesDone[2])
            {
                subtitles.PlayAudio(Subtiles_HR.ID.P4_LINE1);
                allowedToBeUsed = false;
            }
            else if(GameTesting_CW.instance.arePuzzlesDone[8])
            {
                subtitles.PlayAudio(Subtiles_HR.ID.P10_LINE2);
                journal.AddJournalLog("That should be it. Have I counted enough coins? I should blow out the candles if I have.");
                journal.ChangeTasks(new string[] { "Blow out candles" });
            }
        }
        if (type == TRIGGER_TYPE.CHESSBOARD && allowedToBeUsed)
        {
            if(GameTesting_CW.instance.arePuzzlesDone[4])
            {
                DisturbanceHandler_DR.instance.TriggerDisturbance(DisturbanceHandler_DR.DisturbanceName.BOOKTURNPAGE);
                subtitles.PlayAudio(Subtiles_HR.ID.P6_LINE2);
                allowedToBeUsed = false;
            }
  
        }
        if (type == TRIGGER_TYPE.THROWING && allowedToBeUsed)
        {
            if (GameTesting_CW.instance.arePuzzlesDone[5])
            {
                subtitles.PlayAudio(Subtiles_HR.ID.P7_LINE2);
                allowedToBeUsed = false;
            }
        }
        if(type == TRIGGER_TYPE.HIDDEN_MECH && allowedToBeUsed)
        {
            // hiddenMechDoor.ToggleOpen();
            // hiddenMechDoor.unlocked = false;
            //  hiddenMechDoor.requiresKey = true;
            journal.ChangeTasks(new string[] { "Find Book" });
            subtitles.PlayAudio(Subtiles_HR.ID.P8_LINE2);
            journal.AddJournalLog("Hmm...maybe if I find some sort of mechanism I can open this door...");
           
            allowedToBeUsed = false;
        }
        if(type == TRIGGER_TYPE.CORRECT_ORDER && allowedToBeUsed)
        {
           // correctOrderDoor.ToggleOpen();
           // correctOrderDoor.unlocked = false;
           // correctOrderDoor.requiresKey = true;

            subtitles.PlayAudio(Subtiles_HR.ID.P9_LINE2);
            journal.AddJournalLog("Is there some kind of pattern here? Maybe I could recreate it.");
            journal.ChangeTasks(new string[] { "repeat the sequence" });
            allowedToBeUsed = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(type == TRIGGER_TYPE.CHESSBOARD && allowedToBeUsed)
        {
            if (GameTesting_CW.instance.arePuzzlesDone[5])
            {
                subtitles.PlayAudio(Subtiles_HR.ID.P6_LINE5);
                allowedToBeUsed = false;
            }
        }
    }
}
