using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBaseScript : MonoBehaviour
{
    internal Journal_DR journal;
    internal Subtitles_HR subtitles;
    internal bool[] voiceovers = new bool[76];
    internal TriggerScript_CW gardenTrigger;


    internal void Awake()
    {
        subtitles = GameObject.Find("FirstPersonCharacter").GetComponent<Subtitles_HR>();
        journal = Journal_DR.instance;
        gardenTrigger = GameObject.Find("GardenTrigger").GetComponent<TriggerScript_CW>();
    }
    
   
}
