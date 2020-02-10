/*
 * Dominique
 * 
 * Spawns a SFX game object for the length of the audio clip or for a specified time (on loop)
 */

using UnityEngine;

public class SFXManager_DR : MonoBehaviour
{
    public static SFXManager_DR instance;

    public SoundEffect[] soundEffects;

    public GameObject SFXPrefab;

    public float volume;

    private bool sfxOn = true;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    /*
     * When a sound effect is played the enum name for it is passed in
     * This is used in a for loop to find it from the list of SFXs available
     * A SFX prefab (AudioSource) is then spawned and its clip is set to the correct clip
     * The clip is then played and the prefab is destroyed once the clip has finished
     */
    public void PlayEffect(SoundEffectNames name)
    {
        //Only works if SFX are in use
        if(sfxOn)
        {
            for (int i = 0; i < soundEffects.Length + 1; i++)
            {
                if (soundEffects[i].name == name)
                {
                    GameObject currentSFX = Instantiate(SFXPrefab);
                    AudioSource currentAS = currentSFX.GetComponent<AudioSource>();

                    currentAS.clip = soundEffects[i].clip;
                    currentAS.volume = volume;
                    currentAS.Play();

                    Destroy(currentSFX, currentAS.clip.length);

                    break;
                }
            }
        }
    }

    //Plays an effect on loop for however many seconds are passed in
    public void PlayEffect(SoundEffectNames name, float secondsToPlay)
    {
        //Only works if SFX are in use
        if (sfxOn)
        {
            for (int i = 0; i < soundEffects.Length + 1; i++)
            {
                if (soundEffects[i].name == name)
                {
                    GameObject currentSFX = Instantiate(SFXPrefab);
                    AudioSource currentAS = currentSFX.GetComponent<AudioSource>();

                    currentAS.clip = soundEffects[i].clip;
                    currentAS.volume = volume;
                    currentAS.loop = true;
                    currentAS.Play();

                    Destroy(currentSFX, secondsToPlay);

                    break;
                }
            }
        }
    }

    public void ToggleSFX()
    {
        sfxOn = !sfxOn;
    }
}

public enum SoundEffectNames
{
    CORRECT,
    INCORRECT,
    BUTTON
}

[System.Serializable]
public class SoundEffect
{
    public SoundEffectNames name;
    public AudioClip clip;
}