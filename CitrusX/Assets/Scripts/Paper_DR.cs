/*
 * Dominique 
 * 
 * Holds the text and background of the paper object so it can change the paper UI accordingly
 *
 * Chase (Changes) 22/2/2020
 * Added enum for types of notes for interaction purposes
 */
using UnityEngine;

public class Paper_DR : MonoBehaviour
{
    public string text;
    public Sprite background;
    public enum NOTE_NAME
    {
        KEY_PAD_NOTE,
        KEY_PAD_DOCUMENT
    };
    public NOTE_NAME nameOfNote;
}
