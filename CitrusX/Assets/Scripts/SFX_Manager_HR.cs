using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Manager_HR : MonoBehaviour
{
    
    public enum SoundEffectNames
    {
        CORRECT,
        INCORRECT,
        BUTTON
    }

    Pooler_HR pooler;
    AudioSource soundSource;
    public Dictionary<SoundEffectNames, AudioClip> soundEffects;
    public float volume;

    void Awake()
    {
         pooler = Pooler_HR.instance;
    }

    
    public void PlaySFX(SoundEffectNames clipName ,Vector3 position, double seconds = 0) 
    {

        soundSource = pooler.SpawnFromPool(Pooler_HR.Tags.SFX, position, soundEffects[clipName]).GetComponent<AudioSource>();
        soundSource.volume = volume;

        //If time is given
        if (seconds != 0)
            soundSource.PlayScheduled(seconds);
        else
            soundSource.Play();

    }
}

