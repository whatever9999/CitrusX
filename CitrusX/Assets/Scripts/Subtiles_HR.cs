/*
 * Hugo
 * 
 * Other scripts can call the function with the id of the clip and subtitles they wanna show on screen
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Subtiles_HR : MonoBehaviour
{

    //Change this chase
    public enum ID
    {
        P1_LINE1,
        P1_LINE2,
        P1_LINE3,
        P1_LINE4,
        P1_LINE5,
        P1_LINE6,
        P1_LINE7,
        P2_LINE1,
        P2_LINE2,
        P2_LINE3,
        P3_LINE1,
        P3_LINE2,
        P3_LINE3,
        P3_LINE4,
        P3_LINE5,
        P3_LINE6,
        P4_LINE1,
        P4_LINE2,
        P4_LINE3,
        P4_LINE4,
        P4_LINE5,
        P4_LINE6,
        P4_LINE7,
        P5_LINE1,
        P5_LINE2,
        P5_LINE3,
        P6_LINE1,
        P6_LINE2,
        P6_LINE3,
        P6_LINE4,
        P6_LINE5,
        P6_LINE6,
        P6_LINE7,
        P7_LINE1,
        P7_LINE2,
        P7_LINE3,
        P7_LINE4,
        P7_LINE5,
        P7_LINE6,
        P8_LINE1,
        P8_LINE2,
        P8_LINE3,
        P8_LINE4,
        P8_LINE5,
        P8_LINE6,
        P8_LINE7,
        P8_LINE8,
        P9_LINE1,
        P9_LINE2,
        P9_LINE3,
        P9_LINE4,
        P9_LINE5,
        P9_LINE6,
        P10_LINE1,
        P10_LINE2,
        P10_LINE3,
        A_LINE1,
        A_LINE2,
        A_LINE3,
        A_LINE4,
        A_LINE5,
        S_LINE1,
        S_LINE2,
        S_LINE3,
        S_LINE4,
        S_LINE5,
        S_LINE6,
        S_LINE7,
        S_LINE8,
        S_LINE9,
        G_LINE1,
        G_LINE2,
        G_LINE3,
        G_LINE4,
        G_LINE5,
        B_LINE1,
        B_LINE2,
        SIGH,
        GASP
    }

    //With the same key (the enumerator) you can acess both subtitles and clips
    public List<ID> audioID = new List<ID>();
    public List<AudioClip> voiceClips = new List<AudioClip>();
    public List<string> subtitles = new List<string>();

    private Dictionary<ID, string> subtitlesDictionary = new Dictionary<ID, string>();
    private Dictionary<ID, AudioClip> clipDictionary = new Dictionary<ID, AudioClip>();
    private Text subtitleText;
    private AudioSource voiceSource;
    
    void Awake()
    {
        voiceSource = GetComponent<AudioSource>();
        subtitleText = GameObject.Find("Subtitles").GetComponent<Text>();

        //Load both subtitles and clips
        for (int i = 0; i < voiceClips.Count; i++)
        {
            clipDictionary.Add(audioID[i], voiceClips[i]);
        }

        for (int i = 0; i < subtitles.Count; i++)
        {
            subtitlesDictionary.Add(audioID[i], subtitles[i]);
        }

        subtitleText.text = "";
    }

    //THE BIG FUNCTION TO CALL CHASE
    public void PlayAudio(ID id) 
    {
        voiceSource.clip = clipDictionary[id];
        subtitleText.text = subtitlesDictionary[id];
        voiceSource.Play();
        SubtitleReset(voiceSource.clip.length);
       
    }


    //Wait for the clip to be over to remove text
    IEnumerator SubtitleReset(float timeToWait) 
    {
        yield return new WaitForSeconds(timeToWait);
        subtitleText.text = "";
    }
}
