/*
 * Dominique
 * 
 * The door knows if it is unlocked or not and opens itself through an animation when told to Open()
 * 
 * Chase (Changes) 08/02/2020
 * 
 * Added a public bool for if the door requires a key
 * 
 * Chase (Changes) 22/2/2020
 * Added public enum to signify what kind of door it is
 * 
 * Dominique (Changes) 25/02/2020
 * Door has a ToggleOpen instead of just open now - can open AND close
 * 
 * Chase (Changes) 1/2/2020
 * Added new type for correct order
 */

/**
* \class Door_DR
* 
* \brief The door is linked to an animator so it can open and close and can be unlocked, locked or require a key
* 
* \author Dominique
* 
* \date Last Modified: 01/02/2020
*/

using UnityEngine;

public class Door_DR : MonoBehaviour
{
    public bool unlocked;
    public bool requiresKey;
    public bool isOpen = false;

    public enum DOOR_TYPE
    {
        COLOUR_MATCHING,
        HIDDEN_MECH,
        SAFE,
        CORRECT_ORDER
    };
    public DOOR_TYPE type;

    private Animator animator;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    public void ToggleOpen()
    {
        isOpen = !isOpen;
        animator.SetBool("Open", isOpen);
    }
}
