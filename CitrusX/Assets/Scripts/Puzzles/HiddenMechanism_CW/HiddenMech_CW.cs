/*Chase Wilding - Hidden Mechanism Puzzle 10/02/2020
 * This puzzle sets off an animation when the correct book is picked up
 * 
 * Dominique (Changes) 11/02/2020
 * Removed unused imports
 * 
 * Chase (Changes) 4/3/2020
 * Tidied up script
 */

/**
* \class HiddenMech_CW
* 
* \brief Checks to see if the journal's tasks are complete and opens the door if so
* 
* \author Chase
* 
* \date Last Modified: 04/03/2020
*/
using UnityEngine;

public class HiddenMech_CW : MonoBehaviour
{
    public Door_DR door;
    internal bool isActive = false;
    internal bool complete = false;
    internal bool clueRead = false;
    internal bool[] steps = { false, false, false, false, false };
    public void SetActive(bool value) { isActive = value; }

    private void Update()
    {
        if(complete)
        {
            door.unlocked = true;
            door.ToggleOpen();
            complete = false;
        }
    }
}