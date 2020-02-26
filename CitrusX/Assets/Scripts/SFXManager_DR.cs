/*
 * Dominique
 * 
 * Spawns a SFX game object for the length of the audio clip or for a specified time (on loop)
 * 
 * 26/02/2020 - Adam
 * Added Enums for currently added sfx clips
 */

using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
    CORRECT_ALTERNATIVE,
    INCORRECT,
    INCORRECT_ALTERNATIVE,
    BUTTON,
    BARON_BEHIND_YOU,
    BARON_HERE,
    BARON_SEE_YOU,
    CANDLE_BLOW,
    CANDLE_LIT,
    COIN_DROP,
    COIN_DROP_BAG,
    DOOR_LOCKED,
    DOOR_CLOSE,
    DOOR_CLOSE_ALT,
    DOOR_OPEN,
    DOOR_OPEN_ALTERNATIVE,
    INVENTORY_IN,
    INVENTORY_OUT,
    LIGHTER_SPARK,
    LIGHTSWITCH,
    UI_CLICK,
    UI_SNAP,
    WATER_DROP,
    WATER_FILL,
    WATER_SPILL,
    AMBIENT_OUTDOORS,
    WILHELM_SCREAM
}

[System.Serializable]
public class SoundEffect
{
    public SoundEffectNames name;
    public AudioClip clip;
}