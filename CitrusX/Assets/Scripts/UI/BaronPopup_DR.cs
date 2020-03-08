/*
 * Dominique
 * 
 * The UI image that swoops towards the player on the screen to spoop them
 */

/**
* \class BaronPopup_DR
* 
* \brief Makes a UI image of the baron appear and move towards the camera
* 
* Use SpoopyScare() to activate the object and play the animation.
* This starts a coroutine that makes it go away after the length of the animation.
* 
* \author Dominique
* 
* \date Last Modified: 02/03/2020
*/

using System.Collections;
using UnityEngine;

public class BaronPopup_DR : MonoBehaviour
{
    public static BaronPopup_DR instance;

    GameObject baronPopup;
    Animation popupAnimation;

    /// <summary>
    /// Inititalise variables and set the object to false
    /// </summary>
    private void Awake()
    {
        instance = this;
        baronPopup = GameObject.Find("BaronPopup");
        popupAnimation = baronPopup.GetComponent<Animation>();
        baronPopup.SetActive(false);
    }

    /// <summary>
    /// Activate the gameobject and play the animation. Start the GoAway(seconds) coroutine after to make the object disable once the animation is done.
    /// </summary>
    public void SpoopyScare()
    {
        baronPopup.SetActive(true);
        popupAnimation.Play();
        StartCoroutine(GoAway(popupAnimation.clip.length));
    }

    /// <summary>
    /// Make the GO go away once the animation has played
    /// </summary>
    private IEnumerator GoAway(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        baronPopup.SetActive(false);
    }
}
