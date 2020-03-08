/*
 * Dominique
 * 
 * Spawns a SFX game object for the length of the audio clip or for a specified time (on loop)
 */

/**
* \class SFXManager_DR
* 
* \brief Instantiates a prefab in the centre of the scene that plays a sound effect
* 
* This class is a don't destroy singleton and should be added to a start scene that will persist throughout the game and can be accessed using SFXManager_DR.instance
* Sound effects are instantiated using an enum. This is linked to an audio clip passed in through the inspector.
* The class handles the destuction of the prefab.
* PlayEffect(name) plays an effect for its length then destroys it
* PlayEffect(name, secondsToPlay) plays an effect on a loop and destroys it after secondsToPlay
* 
* \author Dominique
* 
* \date Last Modified: 28/01/2020
*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class SFXManager_DR : MonoBehaviour
{
    public static SFXManager_DR instance;

    public SoundEffect_DR[] soundEffects;

    public GameObject SFXPrefab;

    public float volume;

    private bool sfxOn = true;

    /// <summary>
    /// Set the class as don't destroy and initialise variables, then load the next scene (probably the menu)
    /// </summary>
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
        //WHEN TESTING THIS IS COMMENTED OUT TO AVOID THE ERROR - IT IS NECESSARY FOR THE START TO GO INTO THE MAIN MENU IN THE FINAL BUILD
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

     /// <summary>
     /// When a sound effect is played the enum name for it is passed in
     /// This is used in a for loop to find it from the list of SFXs available 
     /// A SFX prefab(AudioSource) is then spawned and its clip is set to the correct clip
     /// The clip is then played and the prefab is destroyed once the clip has finished
     /// </summary>
     /// <param name="name - an enum identifier of the prefab, linked in the inspector"></param>
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

    /// <summary>
    /// As with PlayEffect(name) plays an effect on loop for however many seconds are passed in
    /// </summary>
    /// <param name="name - an enum identifier of the sound effect passed in through the inspector"></param>
    /// <param name="secondsToPlay - how long to loop the effect for"></param>
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

    /// <summary>
    /// Prefabs won't be instantiated if sfxOn is false. This procedure toggles the boolean on and off.
    /// </summary>
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


/**
* \class SoundEffect_DR
* 
* \brief Links an enum identifier for a sound effect to an audio clip of that effect
* 
* \author Dominique
* 
* \date Last Modified: 28/01/2020
*/
[System.Serializable]
public class SoundEffect_DR
{
    public SoundEffectNames name;
    public AudioClip clip;
}