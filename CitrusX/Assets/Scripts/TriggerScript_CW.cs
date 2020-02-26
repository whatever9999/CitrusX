using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript_CW : MonoBehaviour
{
    public enum TRIGGER_TYPE
    {
        GARDEN,
        RITUAL,
        CHESSBOARD
    };
    public TRIGGER_TYPE type;
    private Subtiles_HR subtitles;
    public bool allowedToBeUsed;
    public bool activated;

    private void Awake()
    {
        subtitles = GameObject.Find("FirstPersonCharacter").GetComponent<Subtiles_HR>();
    }
 
    private void OnTriggerEnter(Collider other)
    {
        if (type == TRIGGER_TYPE.GARDEN && !activated && allowedToBeUsed)
        {

            subtitles.PlayAudio(Subtiles_HR.ID.P2_LINE1);
        }
        if(type == TRIGGER_TYPE.RITUAL && allowedToBeUsed)
        {
            if(GameTesting_CW.instance.arePuzzlesDone[1])
            {
                subtitles.PlayAudio(Subtiles_HR.ID.P3_LINE1);
                allowedToBeUsed = false;
            }
            if(GameTesting_CW.instance.arePuzzlesDone[2])
            {
                subtitles.PlayAudio(Subtiles_HR.ID.P4_LINE1);
                allowedToBeUsed = false;
            }
        }
        if (type == TRIGGER_TYPE.CHESSBOARD && allowedToBeUsed)
        {
            if(GameTesting_CW.instance.arePuzzlesDone[4])
            {
                subtitles.PlayAudio(Subtiles_HR.ID.P6_LINE2);
                allowedToBeUsed = false;
            }
            else if(GameTesting_CW.instance.arePuzzlesDone[5])
            {
                subtitles.PlayAudio(Subtiles_HR.ID.P6_LINE5);
                allowedToBeUsed = false;
            }
            
        }
    }
}
