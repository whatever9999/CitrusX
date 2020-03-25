/*
 * Hugo
 * 
 * Change volume through the pause menu with sliders
 */


using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControls : MonoBehaviour
{
    private Slider slider;
    public AudioMixer audioMixer;
    public enum Groups
    {
        MASTER,
        AMBIENCE,
        BGM,
        SFX,
        DIALOG
    }

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    public Groups group;

    //Slider will call this function every time the user changes the value. Changing the volune of each respective group
    public void changeVolume() 
    {

        switch (group)
        {
            case Groups.MASTER:
                audioMixer.SetFloat("Master",slider.value);
                break;
            case Groups.AMBIENCE:
                audioMixer.SetFloat("Ambience", slider.value);
                break;
            case Groups.BGM:
                audioMixer.SetFloat("BGM", slider.value);
                break;
            case Groups.SFX:
                audioMixer.SetFloat("SFX", slider.value);
                break;
            case Groups.DIALOG:
                audioMixer.SetFloat("Dialog", slider.value);
                break;
            default:
                break;
        }



    }

}
