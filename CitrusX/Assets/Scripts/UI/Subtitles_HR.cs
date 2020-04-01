/*
 * Hugo
 * 
 * Other scripts can call the function with the id of the clip and subtitles they wanna show on screen
 * 
 * Dominique (Changes) 25/02/2020
 * Changed subtitles from two dictionaries into a struct so this is easier to use in the inspector and less open for error
 * Chase (Changes) 9/3/2020
 * Added lines p1_line8 -> p1_line11 for new dialogue for introducing cameras and phone - need VOs for them still
 * Chase (Changes) 11/3/2020
 * Added line A_Line6
 */

/**
* \class Subtitles_HR
* 
* \brief Makes subtitle appear that corresponds to an audio clip from a struct and disappear once the clip has ended
* 
* PlayAudio(id) plays a subtitle according to an enum that links to an audio clip and the text for the subtitle.
* SubtitleReset(timeToWait) makes the subtitle text blank after timeToWait seconds.
* 
* \author Hugo
* 
* \date Last Modified: 25/02/2020
*/

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Subtitles_HR : MonoBehaviour
{
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
        S_LINE10,
        G_LINE1,
        G_LINE2,
        G_LINE3,
        G_LINE4,
        G_LINE5,
        B_LINE1,
        B_LINE2,
        SIGH,
        GASP,
        P1_LINE8,
        P1_LINE9,
        P1_LINE10,
        P1_LINE11,
        A_LINE6
    }

    //This array will show in the inspector for subtitle data to be passed in to
    public Subtitle_DR[] subtitles;

    private Text subtitleText;
    private AudioSource voiceSource;

    private ID currentCutsceneSubtitle;
    private ID currentGoodCutsceneSubtitle;
    private ID currentBadCutsceneSubtitle;

    private Coroutine currentCoroutine;
    private bool coroutineRunning = false;

    /// <summary>
    /// Initialise variables
    /// </summary>
    void Awake()
    {
        voiceSource = GetComponent<AudioSource>();
        subtitleText = GameObject.Find("Subtitles").GetComponent<Text>();

        subtitleText.text = "";

        currentCutsceneSubtitle = ID.S_LINE1;
        currentBadCutsceneSubtitle = ID.B_LINE1;
        currentGoodCutsceneSubtitle = ID.G_LINE1;
    }

    //THE BIG FUNCTION TO CALL CHASE
    /// <summary>
    /// Goes through subtitles using the id to find the right one. Plays this audio from the player AudioSource and sets the text to the corresponding text
    /// Starts the SubtitleReset(timeToWait) coroutine using the clip length
    /// </summary>
    /// <param name="id - the enum linked to the audio clip/subtitle"></param>
    public void PlayAudio(ID id)
    {
        for (int i = 0; i < subtitles.Length; i++)
        {
            if (subtitles[i].id == id)
            {
                voiceSource.clip = subtitles[i].clip;
                subtitleText.text = subtitles[i].text;
                break;
            }
        }
        voiceSource.Play();

        //If the coroutine for resetting the subtitles is already running stop it before this subtitle plays (for overlapped subtitles)
        if(coroutineRunning)
        {
            StopCoroutine(currentCoroutine);
            coroutineRunning = false;
        }
        currentCoroutine = StartCoroutine(SubtitleReset(voiceSource.clip.length));
    }

    /// <summary>
    /// Wait for the clip to be over to remove text
    /// </summary>
    /// <param name="timeToWait - the length of the audio clip linked to the text"></param>
    /// <returns></returns>
    IEnumerator SubtitleReset(float timeToWait)
    {
        coroutineRunning = true;
        yield return new WaitForSeconds(timeToWait);
        subtitleText.text = "";
        coroutineRunning = false;
    }

    //Set lines for cutscenes
    public void StartCutsceneSubtitle()
    {
        PlayAudio(currentCutsceneSubtitle++);
    }
    public void GoodEndCutsceneSubtitle()
    {
        PlayAudio(currentGoodCutsceneSubtitle++);
    }
    public void BadEndCutsceneSubtitle()
    {
        PlayAudio(currentBadCutsceneSubtitle++);
    }
    public void Gasp()
    {
        PlayAudio(ID.GASP);
    }
    public void Sigh()
    {
        PlayAudio(ID.SIGH);
    }
}

/**
* \class Subtitle_DR
* 
* \brief Each subtitle has an ID, clip and the text that links to the clip
* 
* \author Dominique
* 
* \date Last Modified: 25/02/2020
*/
[System.Serializable]
public struct Subtitle_DR
{
    public Subtitles_HR.ID id;
    public AudioClip clip;
    public string text;
}