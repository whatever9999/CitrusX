/*
 * Hugo
 * 
 * Other scripts can call the function with the id of the clip and subtitles they wanna show on screen
 * 
 * Dominique (Changes) 25/02/2020
 * Changed subtitles from two dictionaries into a struct so this is easier to use in the inspector and less open for error
 */

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Subtiles_HR : MonoBehaviour
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

    //This array will show in the inspector for subtitle data to be passed in to
    public Subtitle[] subtitles;

    private Text subtitleText;
    private AudioSource voiceSource;

    void Awake()
    {
        voiceSource = GetComponent<AudioSource>();
        subtitleText = GameObject.Find("Subtitles").GetComponent<Text>();

        subtitleText.text = "";
    }

    void Start()
    {
        //TEST
        //PlayAudio(ID.P1_LINE1);
    }

    //THE BIG FUNCTION TO CALL CHASE
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
        SubtitleReset(voiceSource.clip.length);
    }


    //Wait for the clip to be over to remove text
    IEnumerator SubtitleReset(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        subtitleText.text = "";
    }
}

//Each subtitle has an ID, clip and the text that links to the clip
[System.Serializable]
public struct Subtitle
{
    public Subtiles_HR.ID id;
    public AudioClip clip;
    public string text;
}