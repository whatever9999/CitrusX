/*
 * Hugo
 * 
 * Other scripts call this one to set an audio clip into the pool objects and bring them to the desired location playing them afterwards
 */

using UnityEngine;

public class SFX_Manager_HR : MonoBehaviour
{
    public enum SoundEffectNames
    {
        CORRECT,
        INCORRECT,
        BUTTON
    }

    public SoundEffect_HR[] soundEffects;
    Pooler_HR pooler;
    AudioSource soundSource;


    public float volume;

    //Get the Pooler instance
    void Awake()
    {
        pooler = Pooler_HR.instance;
    }


    //Scripts will call this function and pass the name, the position and optionally the amount of time to play
    public void PlaySFX(SoundEffectNames clipName, Vector3 position, double seconds = 0)
    {
        //Calls the pooler script to dequeue a pool object 
        soundSource = pooler.SpawnFromPool(Pooler_HR.Tags.SFX, position, soundEffects[(int)clipName].clip).GetComponent<AudioSource>();
        soundSource.volume = volume;

        //If time is given
        if (seconds != 0)
            soundSource.PlayScheduled(seconds);
        else
            soundSource.Play();

    }
    [System.Serializable]
    public class SoundEffect_HR
    {
        public SoundEffectNames name;
        public AudioClip clip;
    }

}