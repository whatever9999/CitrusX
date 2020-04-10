/*
 * Script: CandleScript
 * Created By: Adam Gordon
 * Created On: 18/02/2020
 * 
 * Summary: Used by the candles to light/blow out
 * 
 * Dominique Changes 20/02/20
 * Candles only need to be blown out and then the good or bad cinematic will play according to if the player took all the coins or not
 * 
 * Chase Changes 22/2/2020
 * Added voiceover lines and bools for voiceovers
 * 
 * Chase (Changes) 1/3/2020
 * Added voiceover for final line, added connection to subtitles script
 */

/**
* \class CandleScript_AG
* 
* \brief Lets the player blow out the candles and updates the game state when this happens
* 
* \author Adam
* 
* \date Last Modified: 01/03/2020
*/
using UnityEngine;
using UnityEngine.UI;

public class CandleScript_AG : Interactable_DR
{
    private ParticleSystem[] flames;
    private Subtitles_HR subtitles;

    /// <summary>
    /// Inititalise variables
    /// </summary>
    private void Awake()
    {
        flames = GetComponentsInChildren<ParticleSystem>();
        subtitles = GameObject.Find("FirstPersonCharacter").GetComponent<Subtitles_HR>();
    }

    /// <summary>
    /// All the candle flames are stopped and Interact_HR.EndGameCheck() is called
    /// </summary>
    public void BlowOut()
    {
        for(int i = 0; i < flames.Length; i++)
        {
            flames[i].Stop();
        }
        subtitles.PlayAudio(Subtitles_HR.ID.P10_LINE3);
    }
  
}
