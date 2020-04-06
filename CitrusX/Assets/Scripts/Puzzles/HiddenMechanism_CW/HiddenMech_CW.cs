/*Chase Wilding - Hidden Mechanism Puzzle 10/02/2020
 * This puzzle sets off an animation when the correct book is picked up
 * 
 * Dominique (Changes) 11/02/2020
 * Removed unused imports
 * 
 * Chase (Changes) 4/3/2020
 * Tidied up script
 * 
 * Dominique (Changes) 05/03/2020
 * Made it so you can only interact with books in order
 */

/**
* \class HiddenMech_CW
* 
* \brief Checks to see if the journal's tasks are complete and opens the door if so
* 
* \author Chase
* 
* \date Last Modified: 05/03/2020
*/
using UnityEngine;

public class HiddenMech_CW : MonoBehaviour
{
    public Door_DR door;
    internal bool isActive = false;
    internal bool complete = false;
    internal bool clueRead = false;
    internal bool[] steps = { false, false, false, false };
    public void SetActive(bool value) { isActive = value; }

    internal Book_CW blueBook;
    internal Book_CW FourEastBook;
    internal Book_CW TwoNorthWestBook;
    internal Book_CW HiddenMechBook;

    private void Awake()
    {
        blueBook = GameObject.Find("BlueBook").GetComponent<Book_CW>();
        FourEastBook = GameObject.Find("FourEastBook").GetComponent<Book_CW>();
        TwoNorthWestBook = GameObject.Find("TwoNorthWestBook").GetComponent<Book_CW>();
        HiddenMechBook = GameObject.Find("HiddenMechBook").GetComponent<Book_CW>();
    }

    private void Update()
    {
        if(complete)
        {
            door.unlocked = true;
            door.ToggleOpen();
            complete = false;
        }

        if(!FourEastBook.canInteractWith && steps[0])
        {
            blueBook.canInteractWith = false;
            FourEastBook.canInteractWith = true;
        } else if (!TwoNorthWestBook.canInteractWith && steps[1])
        {
            FourEastBook.canInteractWith = false;
            TwoNorthWestBook.canInteractWith = true;
        } else if(!HiddenMechBook.canInteractWith && steps[2])
        {
            TwoNorthWestBook.canInteractWith = false;
            HiddenMechBook.canInteractWith = true;
        }
    }
}