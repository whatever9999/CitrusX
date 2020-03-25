/*
 * Dominique
 * 
 * Animations for puzzles happen here
 * If an animation is not here or in the DisturbanceHandler then it is handled by the object itself (this is usually the case when the player directly interacts with the object)
 */

 /**
  * \class AnimationManager_DR
  * 
  * \brief Enables the triggering of animations on objects around the game using an enum.
  * 
  * Using the instance of this class you can call TriggerAnimation() with an enum identifier of a particular animation to make it start.
  * There is also a FadeToBlack() coroutine that will make an animation happen as the screen is fading back in from black to signify time passing.
  * 
  * \author Dominique
  * 
  * \date Last Modified: 03/03/2020
  */
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class AnimationManager_DR : MonoBehaviour
{
    public static AnimationManager_DR instance;

    #region Animation Components
    private Image blackScreen;
    private PutDown_HR ritualTable;
    private PutDown_HR gardenTable;
    private Animator handAnimator;
    private GameObject hand;
    private FirstPersonController controller;
    #endregion

    public enum AnimationName
    {
        PLACERITUALITEMS,
        PLACEJEWELLERY,
        OPENCHESSBOARDBOOK,
        OPENGAMESROOMBOX,
        SLAMGAMESROOMBOX
    }

    /// <summary>
    /// Initialise variables
    /// </summary>
    private void Awake()
    {
        #region Initialisations
        instance = this;

        blackScreen = GameObject.Find("BlackScreen").GetComponent<Image>();
        ritualTable = GameObject.Find("RitualTable").GetComponent<PutDown_HR>();
        gardenTable = GameObject.Find("GardenTable").GetComponent<PutDown_HR>();
        hand = GameObject.Find("Hand");
        handAnimator = hand.GetComponent<Animator>();
        controller = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
        #endregion

        hand.SetActive(false);
    }

    /// <summary>
    /// Uses a switch case to use the name parameter to trigger a specific animation
    /// </summary>
    /// <param name="name - an enum that identifies what animation needs to be carried out"></param>
    public void TriggerAnimation(AnimationName name)
    {
        switch (name)
        {
            case AnimationName.PLACERITUALITEMS:
                StartCoroutine(FadeToBlack(name));
                break;
            case AnimationName.PLACEJEWELLERY:
                StartCoroutine(FadeToBlack(name));
                break;
            case AnimationName.OPENCHESSBOARDBOOK:
                break;
            case AnimationName.OPENGAMESROOMBOX:
                break;
            case AnimationName.SLAMGAMESROOMBOX:
                break;
            default:
                break;
        }
    }

    public void StartCinematicFade()
    {
        StartCoroutine(CinematicFade());
    }

    private IEnumerator CinematicFade()
    {
        //Fade to black
        while (blackScreen.color.a < 1f)
        {
            Color newColor = blackScreen.color;
            newColor.a += Time.deltaTime * 2;
            //Emphasise the fade
            yield return new WaitForSeconds(Time.deltaTime);
            blackScreen.color = newColor;
        }

        //Black screen for a second
        yield return new WaitForSeconds(1);

        //Fade back from black
        while (blackScreen.color.a > 0f)
        {
            Color newColor = blackScreen.color;
            newColor.a -= Time.deltaTime * 2;
            yield return new WaitForSeconds(Time.deltaTime);
            blackScreen.color = newColor;
        }
    }


    /// <summary>
    /// The player's movement is temporarily disabled as a fade to black occurs.
    /// The fade increases the alpha of a black UI image to 1, makes any effects of the animation happen, waits, reduces the alpha again and when the alpha reaches 0.95 triggers another animation if there is one for the player to see happening.
    /// </summary>
    /// <param name="name - an enum that identifies what animation needs to be carried out"></param>
    private IEnumerator FadeToBlack(AnimationName name)
    {
        //Make sure player can't move during animation
        controller.enabled = false;

        //Fade to black
        while (blackScreen.color.a < 1f)
        {
            Color newColor = blackScreen.color;
            newColor.a += Time.deltaTime;
            //Emphasise the fade
            yield return new WaitForSeconds(Time.deltaTime);
            blackScreen.color = newColor;
        }

        //Make items appear
        switch (name)
        {
            case AnimationName.PLACERITUALITEMS:
                ritualTable.PutItemsDown();
                break;
            case AnimationName.PLACEJEWELLERY:
                gardenTable.PutItemsDown();
                break;
        }

        //Black screen for a second
        yield return new WaitForSeconds(1);

        bool animationHasPlayed = false;
        //Fade back from black
        while (blackScreen.color.a > 0f)
        {
            Color newColor = blackScreen.color;
            newColor.a -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
            blackScreen.color = newColor;

            //When the black screen is see through enough start animations
            if (!animationHasPlayed && newColor.a < 0.95f)
            {
                animationHasPlayed = true;
                //Animations that happen after the black screen (if there are any)
                //Default is for hand to be putting down something 
                switch (name)
                {
                    default:
                        StartCoroutine(HandAnimation());
                        break;
                }
            }
        }

        //Let player move again
        controller.enabled = true;
    }

    /// <summary>
    /// Activates the hand object (coming off of the player), makes it animate and once it is complete deactivates it again.
    /// </summary>
    private IEnumerator HandAnimation()
    {
        hand.SetActive(true);
        handAnimator.SetTrigger("PutDown");
        yield return new WaitForSeconds(handAnimator.GetCurrentAnimatorStateInfo(0).length);
        hand.SetActive(false);
    }
}
