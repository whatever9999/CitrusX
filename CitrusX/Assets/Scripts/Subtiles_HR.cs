/*
 * Hugo
 * 
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Subtiles_HR : MonoBehaviour
{

    public enum ID
    {
        START,
        FIRSTPUZZLE,
        SECONDPUZZLE,
        THIRDPUZZLE,
        FORTHPUZZLE,
        FIFTHPUZZLE,
        SIXTHPUZZLE,
        SEVENTHPUZZLE,
        EIGHTHPUZZLE,
        NINETHPUZZLE,
        TENTHPUZZLE,
        End
    }

    public List<ID> audioID = new List<ID>();
    public List<AudioClip> voiceClips = new List<AudioClip>();
    public List<string> subtitles = new List<string>();

    private Dictionary<ID, string> subtitlesDictionary = new Dictionary<ID, string>();
    private Dictionary<ID, AudioClip> clipDictionary = new Dictionary<ID, AudioClip>();
    private Text subtitleText;
    
    void Awake()
    {
        subtitleText = GameObject.Find("").GetComponent<Text>();

        //Load both subtitles and clips
        for (int i = 0; i < voiceClips.Count; i++)
        {
            clipDictionary.Add(audioID[i], voiceClips[i]);
        }

        for (int i = 0; i < subtitles.Count; i++)
        {
            subtitlesDictionary.Add(audioID[i], subtitles[i]);
        }
    }

    //THE BIG FUNCTION TO CALL CHASE
    public void PlayAudio(ID id) 
    {

        subtitleText.text = subtitlesDictionary[id];
    }
}
