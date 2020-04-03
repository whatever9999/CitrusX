/*
 * Hugo
 * 
 * Other scripts call this one to set an audio clip into the pool objects and bring them to the desired location playing them afterwards
 * 
 * Changes (Dominique) 04/04/2020
 * Turned into a singleton
 */

using UnityEngine;

public class SFX_Manager_HR : MonoBehaviour
{
    public enum SoundEffectNames
    {
        KEYPAD_CORRECT,
        KEYPAD_INCORRECT,
        KEYPAD_BUTTON,
        CANDLE_BLOW,
        COMPUTER_ON_OFF,
        LOUD_DRIP,
        MEDIUM_DRIP,
        QUIET_DRIP,
        FUSEBOX_DOOR,
        PC_CORRECT,
        PC_INCORRECT,
        PICK_UP_OBJECT,
        PUT_DOWN_JEWELLERY,
        PUT_DOWN_PAWN,
        PUT_DOWN_RITUAL,
        ROTATE_PAWN,
        ROTATE_PIPE,
        PUT_DOWN_SCALES,
        NOTE,
        PICK_UP_COIN,
        PIPE_CORRECT,
        PIPE_INCORRECT
    }

    public static SFX_Manager_HR instance;
    public SoundEffect_HR[] soundEffects;
    Pooler_HR pooler;
    AudioSource soundSource;

    //Get the Pooler instance
    void Awake()
    {
        instance = this;
        pooler = Pooler_HR.instance;
    }

    //Scripts will call this function and pass the name, the position and optionally the amount of time to play
    public void PlaySFX(SoundEffectNames clipName, Vector3 position, double seconds = 0)
    {
        //Calls the pooler script to dequeue a pool object 
        soundSource = pooler.SpawnFromPool(Pooler_HR.Tags.SFX, position, soundEffects[(int)clipName].clip).GetComponent<AudioSource>();

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