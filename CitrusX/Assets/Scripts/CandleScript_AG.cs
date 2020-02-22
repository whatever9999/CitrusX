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
 */
using UnityEngine;

public class CandleScript_AG : MonoBehaviour
{
    private ParticleSystem flame;
    private Interact_HR player;

    private void Awake()
    {
        flame = GetComponentInChildren<ParticleSystem>();
        player = GameObject.Find("FirstPersonCharacter").GetComponent<Interact_HR>();
    }

    public void BlowOut()
    {
        flame.Stop();
        player.EndGameCheck();
    }
}
