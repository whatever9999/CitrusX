/*
 * Dominique
 * 
 * The UI image that swoops towards the player on the screen to spoop them
 */

using System.Collections;
using UnityEngine;

public class BaronPopup_DR : MonoBehaviour
{
    public static BaronPopup_DR instance;

    GameObject baronPopup;
    Animation popupAnimation;

    private void Awake()
    {
        instance = this;
        baronPopup = GameObject.Find("BaronPopup");
        popupAnimation = baronPopup.GetComponent<Animation>();
        baronPopup.SetActive(false);
    }

    public void SpoopyScare()
    {
        //Activate the gameobject and play the animation
        baronPopup.SetActive(true);
        popupAnimation.Play();
        StartCoroutine(GoAway(popupAnimation.clip.length));
    }

    //Make the GO go away once the animation has played
    private IEnumerator GoAway(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        baronPopup.SetActive(false);
    }
}
