using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;

public class PuzzleBaseScript : MonoBehaviour
{
    internal Journal_DR journal;
    internal Subtitles_HR subtitles;
    internal FirstPersonController fpsController;
    internal bool[] voiceovers = new bool[76];
    internal TriggerScript_CW gardenTrigger;
    internal bool isActive = false;

    internal void SetActive(bool value) { isActive = value; }

    internal void Start()
    {
        fpsController = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
        subtitles = GameObject.Find("FirstPersonCharacter").GetComponent<Subtitles_HR>();
        
        gardenTrigger = GameObject.Find("GardenTrigger").GetComponent<TriggerScript_CW>();
    }
    
   
}
