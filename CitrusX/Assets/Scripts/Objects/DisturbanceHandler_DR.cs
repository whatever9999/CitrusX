﻿/*
 * Dominique
 * Use TriggerDisturbance() to call a disturbance by name and nearly everything is handled for you ;)
 * You will have to set where you want the baron to appear by giving him a position with MoveBaron(Vector3 position) - if you don't he'll appear in the main room
 */

/**
* \class DisturbanceHandler_DR
* 
* \brief Using an enum and TriggerDisturbance(name) an animation/sound will happen to progress the story
* 
* MoveBaron(position) is used to set the position of the baron for if he needs to appear somewhere in particular (his original position is still stored)
* DisappearBaron() is a coroutine that prevents him from going to the water bowl and makes him appear idle somewhere for numberOfSecondsForBaronAppearance seconds
* 
* \author Dominique
* 
* \date Last Modified: 02/03/2020
*/
using System.Collections;
using UnityEngine;

public class DisturbanceHandler_DR : MonoBehaviour
{
    public static DisturbanceHandler_DR instance;

    #region Disturbance Components
    private Animator pawn;
    private Animator slamBook;
    private Animator boxFall;
    private Door_DR creakyDoor;
    private GameObject baron;
    private Animator baronAnimator;
    private Baron_DR baronAI;
    private WaterBowl_DR baronTimer;
    private Animator boxMove;
    private Animator bookFall;
    private Animator lampWobble;
    #endregion

    //Used to make sure the baron is back in his start position after he is moved somewhere for an appearance
    private Vector3 baronStart;

    public enum DisturbanceName
    {
        PAWNFALL,
        BOXFALL,
        BARONCLOSEUP,
        BOOKTURNPAGE,
        DOORCREAK,
        BOXMOVE,
        BOOKFALL,
        LAMPWOBBLE
    }

    private void Awake()
    {
        #region Initialisations
        instance = this;

        pawn = GameObject.Find("Pawn").GetComponent<Animator>();
        boxFall = GameObject.Find("BoxFall").GetComponent<Animator>();
        slamBook = GameObject.Find("TurnPageBook").GetComponent<Animator>();
        baron = GameObject.Find("Baron");
        baronAnimator = baron.GetComponent<Animator>();
        baronAI = baron.GetComponent<Baron_DR>();
        baronTimer = GameObject.Find("WaterBowl").GetComponent<WaterBowl_DR>();
        baronStart = baron.transform.position;
        boxMove = GameObject.Find("BoxMove").GetComponent<Animator>();
        bookFall = GameObject.Find("BookFall").GetComponent<Animator>();
        lampWobble = GameObject.Find("LampWobble").GetComponent<Animator>();
        #endregion
    }

    public void TriggerDisturbance(DisturbanceName name)
    {
        switch (name)
        {
            case DisturbanceName.PAWNFALL:
                pawn.SetTrigger("Fall");
                break;
            case DisturbanceName.BOXFALL:
                boxFall.SetTrigger("Fall");
                break;
            case DisturbanceName.BARONCLOSEUP:
                BaronPopup_DR.instance.SpoopyScare();
                break;
            case DisturbanceName.BOOKTURNPAGE:
                slamBook.SetTrigger("TurnPage");
                break;
            case DisturbanceName.BOXMOVE:
                boxMove.SetTrigger("Shufft");
                break;
            case DisturbanceName.BOOKFALL:
                bookFall.SetTrigger("Fall");
                break;
            case DisturbanceName.LAMPWOBBLE:
                lampWobble.SetTrigger("Wobble");
                break;
            default:
                break;
        }
    }

    public void MoveBaron(Vector3 position)
    {
        baron.transform.position = position;
    }
}
