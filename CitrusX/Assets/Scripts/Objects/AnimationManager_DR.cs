/*
 * Dominique
 * 
 * Animations for puzzles happen here
 * If an animation is not here or in the DisturbanceHandler then it is handled by the object itself (this is usually the case when the player directly interacts with the object)
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

    //TEST
    private void Start()
    {
        StartCoroutine(Test());
    }

    private IEnumerator Test()
    {
        yield return new WaitForSeconds(1);
    }

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

    private IEnumerator HandAnimation()
    {
        hand.SetActive(true);
        //handAnimator.SetTrigger("PutDown");
        yield return new WaitForSeconds(handAnimator.GetCurrentAnimatorStateInfo(0).length);
        hand.SetActive(false);
    }
}
