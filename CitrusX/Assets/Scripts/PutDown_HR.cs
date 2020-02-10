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
 * Dominique (Changes) 
 * Changed it so that game objects are set active instead of mesh renderers being enabled
 */

using UnityEngine;

public class PutDown_HR : MonoBehaviour
{
    private int tableChildren;
    private bool beenUsed;

    public bool getBeenUsed() { return beenUsed; }

    public void setItemsDown() 
    {
        tableChildren = transform.childCount;
        for (int i = 0; i < tableChildren; ++i)
            transform.GetChild(i).gameObject.SetActive(true);
        beenUsed = true;
    }

}
