/*Chase Wilding 11/2/2020
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

/**
* \class Table_CW
* 
* \brief If the items on the table have been put down then the game state is updated accordingly using the table enum identifier
* 
* \author Chase
* 
* \date Last Modified: 12/02/2020
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

    /// <summary>
    /// Inititalise variables
    /// </summary>
    private void Awake()
    {
        ritualSetUp = GameObject.Find("FirstPersonCharacter").GetComponent<SetUpRitual_CW>();
    }

    /// <summary>
    /// Check what table it is and if the items have been put down on it and update the game state according to this
    /// </summary>
    private void Update()
    {

        if (hasBeenPlaced)
        {

            if (currentTable == TABLES.RITUAL_TABLE && hasBeenPlaced)
            {
                ritualSetUp.ritualSteps[0] = true;
                hasBeenPlaced = false;
            }
            else if (currentTable == TABLES.GARDEN_TABLE && hasBeenPlaced)
            {
                ritualSetUp.ritualSteps[4] = true;
                ritualSetUp.ritualSteps[5] = true;
                hasBeenPlaced = false;

            }
        }
    }
}
