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
        END
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
