/*
 * Dominique
 *
 * Holds the text and background of the paper object so it can change the paper UI accordingly
 *
 * Chase(Changes) 22/2/2020
 * Added enum for types of notes for interaction purposes
 * Chase(Changes) 24/2/2020
 * Added text size for notes
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
        RITUAL_ARTICLE
    };
    public NOTE_NAME nameOfNote;
    public bool hasBeenRead = false;
}
