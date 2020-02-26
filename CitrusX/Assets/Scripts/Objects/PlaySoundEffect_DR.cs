using System.Collections;
using System.Collections.Generic;
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
