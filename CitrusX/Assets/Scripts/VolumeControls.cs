/*
 * Hugo
 * 
 * Change volume through the pause menu with sliders
 * x
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
        switch (group)
        {
            case Groups.MASTER:
                slider.value = PlayerPrefs.GetFloat("Master");
                break;
            case Groups.AMBIENCE:
                slider.value = PlayerPrefs.GetFloat("Ambience");
                break;
            case Groups.BGM:
                slider.value = PlayerPrefs.GetFloat("BGM");
                break;
            case Groups.SFX:
                slider.value = PlayerPrefs.GetFloat("SFX");
                break;
            case Groups.DIALOG:
                slider.value = PlayerPrefs.GetFloat("Dialog");
                break;
            default:
                break;
        }
    }
    public Groups group;

    //Slider will call this function every time the user changes the value. Changing the volune of each respective groupX
    public void changeVolume() 
    {

        switch (group)
        {
            case Groups.MASTER:
                audioMixer.SetFloat("Master",slider.value);
                PlayerPrefs.SetFloat("Master", slider.value);
                break;
            case Groups.AMBIENCE:
                audioMixer.SetFloat("Ambience", slider.value);
                PlayerPrefs.SetFloat("Ambience", slider.value);
                break;
            case Groups.BGM:
                audioMixer.SetFloat("BGM", slider.value);
                PlayerPrefs.SetFloat("BGM", slider.value);
                break;
            case Groups.SFX:
                audioMixer.SetFloat("SFX", slider.value);
                PlayerPrefs.SetFloat("SFX", slider.value);
                break;
            case Groups.DIALOG:
                audioMixer.SetFloat("Dialog", slider.value);
                PlayerPrefs.SetFloat("Dialog", slider.value);
                break;
            default:
                break;
        }

        PlayerPrefs.Save();



    }

}
