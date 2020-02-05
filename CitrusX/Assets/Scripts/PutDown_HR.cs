/* Hugo
 * 
 * What the script does...
 * 
 * This Script allows the player whenever he is in range of the table and has all the items 
 * the ability to put everything on the table.
 * 
 */

using UnityEngine;
using UnityEngine.UI;

public class PutDown_HR : MonoBehaviour
{
    private int tableChildren;

    public void setItemsDown() 
    {
        tableChildren = transform.childCount;
        for (int i = 0; i < tableChildren; ++i)
            transform.GetChild(i).GetComponent<MeshRenderer>().enabled = true;
    }

}
