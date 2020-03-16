/* Chase Wilding 16/3/2020
 * This script controls the events that occur in the house such as the doors opening and disturbances occuring. This is in place
 * to stop the game state script from getting too crowded
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager_CW : MonoBehaviour
{
    #region DOOR_REFERENCES
    private Door_DR kitchenDoor;
    private Door_DR upstairsBathroomDoor;
    private Door_DR studyDoor; //hidden mech door
    private Door_DR loungeDoor; //chessboard door
    private Door_DR masterBedroomDoor;
    private Door_DR[] otherDownstairsDoors;
    #endregion
    #region TRIGGER_REFERENCES
    private TriggerScript_CW ritualTrigger;
    private TriggerScript_CW chessTrigger;
    private TriggerScript_CW throwingTrigger;
    private TriggerScript_CW correctOrderTrigger;
    #endregion
    #region GAMEOBJECT_REFERENCES
    private GameObject throwingBox;
    private GameObject hiddenMechDoc;
    private GameObject bathroomKeyPart1;
    private GameObject bathroomKeyPart2;
    #endregion
    #region DISTURBANCES
    private DisturbanceHandler_DR disturbances;
    private Baron_DR baron;
    #endregion
    #region BOOLS
    private bool[] triggersSet = { false, false, false, false, false };
    private bool[] itemsSet = { false, false };
    private bool[] disturbancesSet = { false, false };
    #endregion
    private GameTesting_CW game;

    private void Awake()
    {
        game = GameTesting_CW.instance;
        disturbances = DisturbanceHandler_DR.instance;
        throwingBox = GameObject.Find("ThrowingBox");
        hiddenMechDoc = GameObject.Find("HiddenMechNote");
        ritualTrigger = GameObject.Find("RitualTrigger").GetComponent<TriggerScript_CW>();
        chessTrigger = GameObject.Find("ChessboardTrigger").GetComponent<TriggerScript_CW>();
        throwingTrigger = GameObject.Find("ThrowingTrigger").GetComponent<TriggerScript_CW>();
        correctOrderTrigger = GameObject.Find("CorrectOrderTrigger").GetComponent<TriggerScript_CW>();
    }
    private void Start()
    {
        throwingBox.SetActive(false);
        hiddenMechDoc.SetActive(false);
    }
    private void Update()
    {
        if(game.arePuzzlesDone[1] && !triggersSet[0])
        {
            ritualTrigger.allowedToBeUsed = true;
            triggersSet[0] = true;
        }
        else if(game.arePuzzlesDone[4] && !triggersSet[1])
        {
            chessTrigger.allowedToBeUsed = true;
            triggersSet[1] = true;
        }
        else if(game.arePuzzlesDone[5] && !triggersSet[2])
        {
            chessTrigger.allowedToBeUsed = true;
            throwingTrigger.allowedToBeUsed = true;
            disturbances.TriggerDisturbance(DisturbanceHandler_DR.DisturbanceName.DOORCREAK);
            triggersSet[2] = true;
        }
        else if(game.arePuzzlesDone[6] && !itemsSet[0])
        {
            throwingBox.SetActive(true);
            disturbances.TriggerDisturbance(DisturbanceHandler_DR.DisturbanceName.LAMPWOBBLE);
            itemsSet[0] = true;
        }
        else if(game.arePuzzlesDone[7] && (!itemsSet[1] || !triggersSet[3]))
        {
            hiddenMechDoc.SetActive(true);
            correctOrderTrigger.allowedToBeUsed = true;
            disturbances.TriggerDisturbance(DisturbanceHandler_DR.DisturbanceName.DOORCREAK);
            itemsSet[1] = true;
            triggersSet[3] = true;
        }
        else if(game.arePuzzlesDone[8] && !triggersSet[4])
        {
            ritualTrigger.allowedToBeUsed = true;
            triggersSet[4] = true;
        }
    }
}
