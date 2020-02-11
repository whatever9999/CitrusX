/*Chase Wilding 11/2/2020
 * This script sees what type of table this is so that it can only take certain items and then lets the ritual script know
 * if they have items on, this works with the interaction script
 * 
 * Dominique (Changes) 11/02/2020
 * Removed unused imports
 * Changed start to awake for collection of SetUpRitual
 * Chase Wilding 11/2/2020
 * Changed bools into enums based on Dominique's feedback
 */
using UnityEngine;

public class Table_CW : MonoBehaviour
{
    public enum TABLES
    {
        RITUAL_TABLE,
        GARDEN_TABLE
    };
    public TABLES currentTables;
    internal bool hasBeenPlaced;
    private SetUpRitual_CW ritualSetUp;

    // Start is called before the first frame update
    void Awake()
    {
        ritualSetUp = GameObject.Find("FirstPersonController").GetComponent<SetUpRitual_CW>();
    }

    // Update is called once per frame
    void Update()
    {

        if (hasBeenPlaced)
        {
            if (isRitualTable)
            {
                ritualSetUp.ritualSetUpPlaced = true;
                hasBeenPlaced = false;
            }
            else if (isGardenTable)
            {
                ritualSetUp.jewelleryPlaced = true;
                hasBeenPlaced = false;
            }

            if (currentTables == TABLES.RITUAL_TABLE && hasBeenPlaced)
            {
                ritualSetUp.ritualSetUpPlaced = true;
                hasBeenPlaced = false;
            }
            else if (currentTables == TABLES.GARDEN_TABLE && hasBeenPlaced)
            {
                ritualSetUp.jewelleryPlaced = true;
                hasBeenPlaced = false;

            }
        }
    }
}
