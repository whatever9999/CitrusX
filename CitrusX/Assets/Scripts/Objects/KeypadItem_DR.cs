/* 
 * Dominique
 * 
 * Keypad item holds the password for a specific keypad and what door it's connected to
 */

/**
* \class KeypadItem_DR
* 
* \brief When a player interacts with a keypad item the UI will open up and use the keypad for that item and open the door connected to it too
* 
* \author Dominique
* 
* \date Last Modified: 11/03/2020
*/
using UnityEngine;

public class KeypadItem_DR : MonoBehaviour
{
    public string password = "1234";
    public Door_DR door;
}
