﻿/*
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
 */
 

using UnityEngine;

public class Door_DR : MonoBehaviour
{
    public bool unlocked;
    public bool requiresKey;
    public enum DOOR_TYPE
    {
        COLOUR_MATCHING,
        HIDDEN_MECH
    };
    public DOOR_TYPE type;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Open()
    {
        animator.SetBool("Open", true);
    }
}
