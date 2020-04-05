/* Chase Wilding : Stores values of the book for the hidden mech puzzle
 * 
 * Dominique (Changes) 05/04/2020
 * Added canInteractWith bool
 */

/**
* \class Book_CW
* 
* \brief Stores values of the book for the hidden mech puzzle
* 
* \author Chase
* 
* \date Last Modified: 17/02/2020
*/

using UnityEngine;

public class Book_CW : MonoBehaviour
{
    public enum BOOK_TYPE
    {
        HIDDEN_MECH_BOOK,
        BLUE_BOOK,
        FOURTH_EAST,
        TWO_NW,
        DEFAULT
    };
    public BOOK_TYPE type;
    public bool canInteractWith = false;
}
