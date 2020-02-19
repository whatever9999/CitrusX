﻿/*Chase Wilding 11/2/2020
 * This script sees what type of table this is so that it can only take certain items and then lets the ritual script know
 * if they have items on, this works with the interaction script
 * 
 * Dominique (Changes) 11/02/2020
 * Removed unused imports
 * Changed start to awake for collection of SetUpRitual
 * Chase Wilding 11/2/2020
 * Changed bools into enums based on Dominique's feedback
 * 
 * Dominique (Changes) 12/02/2020
 * Added a curly bracket and swapped some old bools around with enums to test after merge conflict (on fps controller)
 */
using UnityEngine;

public class Table_CW : MonoBehaviour
{
    public enum TABLES
    {
        RITUAL_TABLE,
        GARDEN_TABLE,
        CHESS_BOARD
    }
    public TABLES currentTable;
    internal bool hasBeenPlaced = false;
    private SetUpRitual_CW ritualSetUp;

    // Start is called before the first frame update
    void Awake()
    {
        ritualSetUp = GameObject.Find("FirstPersonCharacter").GetComponent<SetUpRitual_CW>();
    }

    // Update is called once per frame
    void Update()
    {

        if (hasBeenPlaced)
        {

            if (currentTable == TABLES.RITUAL_TABLE && hasBeenPlaced)
            {
                ritualSetUp.ritualSetUpCollected = true;
                hasBeenPlaced = false;
            }
            else if (currentTable == TABLES.GARDEN_TABLE && hasBeenPlaced)
            {
                ritualSetUp.jewelleryCollected = true;
                hasBeenPlaced = false;

            }
        }
    }
}
