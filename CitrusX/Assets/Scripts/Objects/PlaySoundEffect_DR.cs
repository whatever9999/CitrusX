/*
 * Dominique
 * 
 * Play a clip on the object
 */

/**
* \class PlaySoundEffect_DR
* 
* \brief Placed on an object, gets its AudioSource and plays a clip passed into PlayEffect(clip) from that source
* 
* \author Dominique
* 
* \date Last Modified: 03/03/2020
*/
using UnityEngine;

public class PlaySoundEffect_DR : MonoBehaviour
{
    AudioSource AS;

    private void Awake()
    {
        AS = GetComponent<AudioSource>();
    }

    public void PlayEffect(AudioClip clip)
    {
        AS.clip = clip;
        AS.Play();
    }
}
