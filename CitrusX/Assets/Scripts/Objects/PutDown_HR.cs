/* Hugo
 * 
 * What the script does...
 * 
 * This Script allows the player whenever he is in range of the table and has all the items 
 * the ability to put everything on the table.
 * 
 * Dominique (Changes) 05/02/2020
 * Added a variable to check if the table has had the items put down on it or not yet
 * Got rid of unused namespace UnityEngine.UI
 * 
 * Dominique (Changes) 11/02/2020
 * Changed it so that game objects are set active instead of mesh renderers being enabled
 * Renamed GetBeenUsed and PutItemsDown
 * Added curly brackets to for loop (I find this makes code more readable for me)
 */

/**
* \class PutDown_HR
* 
* \brief When the player interacts with an object that has this script all children of the object are activated
* 
* GetBeenUsed() identifies if the items are already down or not
* PutItemsDown() activates all children of the object
* 
* \author Hugo
* 
* \date Last Modified: 11/02/2020
*/
using UnityEngine;

public class PutDown_HR : MonoBehaviour
{
    private int tableChildren;
    private bool beenUsed;

    public bool GetBeenUsed() { return beenUsed; }
    public void SetBeenUsed(bool newValue) { beenUsed = newValue; }

    /// <summary>
    /// Activate all children of the object and set been used to true
    /// </summary>
    public void PutItemsDown() 
    {
        tableChildren = transform.childCount;
        for (int i = 0; i < tableChildren; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        beenUsed = true;
    }

}
