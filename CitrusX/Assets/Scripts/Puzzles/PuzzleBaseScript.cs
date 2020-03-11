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
    internal TriggerScript_CW ritualTrigger;
    internal bool isActive = false;
    internal GameTesting_CW game;

    internal void SetActive(bool value) { isActive = value; }

    internal void Start()
    {
        fpsController = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
        subtitles = GameObject.Find("FirstPersonCharacter").GetComponent<Subtitles_HR>();
        ritualTrigger = GameObject.Find("RitualTrigger").GetComponent<TriggerScript_CW>();
        gardenTrigger = GameObject.Find("GardenTrigger").GetComponent<TriggerScript_CW>();
        game = GameTesting_CW.instance;
    }
    
   
}
