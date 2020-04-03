/*
 * Dominique
 */

/**
* \class ScalePan_DR
* 
* \brief If an object collides with the pan or leaves it, it becomes a child and the pans are checked to see if they are balanced
* 
* \author Dominique
* 
* \date Last Modified: 09/03/2020
*/

using UnityEngine;

public class ScalePan_DR : MonoBehaviour
{
    ScalesPuzzleScript_AG scales;
    TextMesh text;

    /// <summary>
    /// Initialise variables
    /// </summary>
    private void Awake()
    {
        scales = GameObject.Find("Scales").GetComponent<ScalesPuzzleScript_AG>();
        text = GetComponentInChildren<TextMesh>();
    }
    /// <summary>
    /// Set the parent of the object that has collided with the pan to the pan
    /// Then make the scales check if the pans are balanced yet
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.parent = transform;
        scales.ComparePans();

    }

    /// <summary>
    /// If the object leaving the pan has it as a parent its parent is now null
    /// Then make the scales check if the pans are balanced yet
    /// </summary>
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.parent == transform) collision.transform.parent = null;
        scales.ComparePans();
    }

    /// <summary>
    /// Change the text that is shown on the screen for the weight on the pan
    /// </summary>
    /// <param name="value - the value that should be shown (will have '00g' added to the end)"></param>
    public void UpdateText(float value)
    {
        text.text = value + "00g";
    }
}
