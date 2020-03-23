/* Chase Wilding - 23/3/2020
 * After a period of inactivity, the game plays a line to hint to the player what to do next
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleVoiceover_CW : MonoBehaviour
{
    private bool completeLoop = false;
    internal bool interactedWith = false;
    private Subtitles_HR subtitles;
    private TriggerScript_CW hiddenMechTrigger;
    internal enum VOICEOVERS
    {
        QUICK,
        JOURNAL,
        WHY,
        CAMERAS,
        RITUAL,
        HIDDEN_MECH
    };
    internal VOICEOVERS vos;

    private void Awake()
    {
        subtitles = GameObject.Find("FirstPersonCharacter").GetComponent<Subtitles_HR>();
        hiddenMechTrigger = GameObject.Find("HiddenMechTrigger").GetComponent<TriggerScript_CW>();
    }
    void Update()
    {
        if(!GameTesting_CW.instance.cutscenes[0])
        {
            interactedWith = true;
        }
        else if(GameTesting_CW.instance.cutscenes[0])
        {
            interactedWith = false;
        }
        //find out which voiceover is most appropriate based on current game state
        FindWhichLine();
        //if no recent interactions
        if(!interactedWith)
        {
            //and not already in a loop
            if(!completeLoop)
            {
                //wait 90 then call line
                StartCoroutine(IdleLine());
            }
        }
        if(interactedWith)
        {
            StopCoroutine(IdleLine());
        }
    }
    IEnumerator IdleLine()
    {
        completeLoop = true;
        yield return new WaitForSeconds(120);
        switch (vos)
        {
            case VOICEOVERS.QUICK:
                {
                    subtitles.PlayAudio(Subtitles_HR.ID.A_LINE1);
                    break;
                }
            case VOICEOVERS.JOURNAL:
                {
                    subtitles.PlayAudio(Subtitles_HR.ID.A_LINE2);
                    break;
                }
            case VOICEOVERS.WHY:
                {
                    subtitles.PlayAudio(Subtitles_HR.ID.A_LINE3);
                    break;
                }
            case VOICEOVERS.CAMERAS:
                {
                    subtitles.PlayAudio(Subtitles_HR.ID.A_LINE4);
                    break;
                }
            case VOICEOVERS.RITUAL:
                {
                    subtitles.PlayAudio(Subtitles_HR.ID.A_LINE5);
                    break;
                }
            default:
                break;
        }
        completeLoop = false;
    }
    int FindWhichLine()
    {
        if(!GameTesting_CW.instance.arePuzzlesDone[0] && GameTesting_CW.instance.cutscenes[0])
        {
            vos = VOICEOVERS.JOURNAL;
        }
        else if(GameTesting_CW.instance.arePuzzlesDone[0] && !InitiatePuzzles_CW.instance.monitorInteractionsUsed[0])
        {
            vos = VOICEOVERS.CAMERAS;
        }
        else if (GameTesting_CW.instance.arePuzzlesDone[1] && !InitiatePuzzles_CW.instance.monitorInteractionsUsed[1])
        {
            vos = VOICEOVERS.CAMERAS;
        }
        else if (GameTesting_CW.instance.arePuzzlesDone[2] && !InitiatePuzzles_CW.instance.monitorInteractionsUsed[2])
        {
            vos = VOICEOVERS.CAMERAS;
        }
        else if (GameTesting_CW.instance.arePuzzlesDone[3] && !InitiatePuzzles_CW.instance.monitorInteractionsUsed[3])
        {
            vos = VOICEOVERS.CAMERAS;
        }
        else if (GameTesting_CW.instance.arePuzzlesDone[4] && !InitiatePuzzles_CW.instance.monitorInteractionsUsed[4])
        {
            vos = VOICEOVERS.CAMERAS;
        }
        else if (GameTesting_CW.instance.arePuzzlesDone[5] && !InitiatePuzzles_CW.instance.monitorInteractionsUsed[5])
        {
            vos = VOICEOVERS.CAMERAS;
        }
        else if (GameTesting_CW.instance.arePuzzlesDone[6] && !InitiatePuzzles_CW.instance.monitorInteractionsUsed[6])
        {
            vos = VOICEOVERS.CAMERAS;
        }
        else if (GameTesting_CW.instance.arePuzzlesDone[7] && !InitiatePuzzles_CW.instance.monitorInteractionsUsed[7])
        {
            vos = VOICEOVERS.CAMERAS;
        }
     
        else if(InitiatePuzzles_CW.instance.monitorInteractions[0] && !InitiatePuzzles_CW.instance.monitorInteractions[1])
        {
            vos = VOICEOVERS.JOURNAL;
        }
        else if (InitiatePuzzles_CW.instance.monitorInteractions[1] && !InitiatePuzzles_CW.instance.monitorInteractions[2])
        {
            vos = VOICEOVERS.JOURNAL;
        }
        else if (InitiatePuzzles_CW.instance.monitorInteractions[2] && !InitiatePuzzles_CW.instance.monitorInteractions[3])
        {
            vos = VOICEOVERS.JOURNAL;
        }
        else if (InitiatePuzzles_CW.instance.monitorInteractions[3] && !InitiatePuzzles_CW.instance.monitorInteractions[4])
        {
            vos = VOICEOVERS.JOURNAL;
        }
        else if (InitiatePuzzles_CW.instance.monitorInteractions[4] && !InitiatePuzzles_CW.instance.monitorInteractions[5])
        {
            vos = VOICEOVERS.JOURNAL;
        }
        else if (InitiatePuzzles_CW.instance.monitorInteractions[5] && !InitiatePuzzles_CW.instance.monitorInteractions[6])
        {
            vos = VOICEOVERS.JOURNAL;
        }
        else if (InitiatePuzzles_CW.instance.monitorInteractions[6] && !hiddenMechTrigger.allowedToBeUsed && !InitiatePuzzles_CW.instance.monitorInteractions[7])
        {
            vos = VOICEOVERS.HIDDEN_MECH;
        }
        else if (InitiatePuzzles_CW.instance.monitorInteractions[7] && !InitiatePuzzles_CW.instance.monitorInteractions[8])
        {
            vos = VOICEOVERS.JOURNAL;
        }
        else if (InitiatePuzzles_CW.instance.monitorInteractions[8])
        {
            vos = VOICEOVERS.JOURNAL;
        }



        return 0;
    }
}
