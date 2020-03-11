/*
 * Dominique
 *
 * Holds the text and background of the paper object so it can change the paper UI accordingly
 *
 * Chase(Changes) 22/2/2020
 * Added enum for types of notes for interaction purposes
 * Chase(Changes) 24/2/2020
 * Added text size for notes
 * Chase(Changes) 26/2/2020
 * Added more to the enum and added a bool for whether it had been read or not
 * Chase(Changes) 11/3/2020
 * Added a new enum member
 */

/**
* \class Paper_DR
* 
* \brief Each paper object has its own name, text, textSize and background that appear on the PaperUI_DR when used
* 
* \author Dominique
* 
* \date Last Modified: 26/02/2020
*/
using UnityEngine;

public class Paper_DR : MonoBehaviour
{
    public string text;
    public int textSize;
    public Sprite background;
    public enum NOTE_NAME
    {
        KEY_PAD_NOTE,
        KEY_PAD_DOCUMENT,
        CHESSBOARD_INSTRUCT,
        CHESSBOARD_DOC,
        PHOTOGRAPH_REVERSE,
        DEATH_CERTIFICATE,
        CORRECT_ORDER_ARTICLE,
        RITUAL_ARTICLE,
        COLOUR_MATCH_CLUE
    };
    public NOTE_NAME nameOfNote;
    public bool hasBeenRead = false;
}
