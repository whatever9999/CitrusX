/*Chase Wilding 11/2/2020
 * This script sees what type of table this is so that it can only take certain items and then lets the ritual script know
 * if they have items on, this works with the interaction script
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table_CW : MonoBehaviour
{
    public bool isRitualTable;
    public bool isGardenTable;
    internal bool hasBeenPlaced;
    private SetUpRitual_CW ritualSetUp;

    // Start is called before the first frame update
    void Start()
    {
        ritualSetUp = GameObject.Find("FPSController").GetComponent<SetUpRitual_CW>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isRitualTable && hasBeenPlaced)
        {
            ritualSetUp.ritualSetUpPlaced = true;
            hasBeenPlaced = false;
        }
        if(isGardenTable && hasBeenPlaced)
        {
            ritualSetUp.jewelleryPlaced = true;
            hasBeenPlaced = false;
        }
    }
}
