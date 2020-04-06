/*Chase Wilding Pipes script 09/02/2020
* This script controls both parts of the fuse puzzle. It uses buttons and public variables which must be set in the inspector
* depending on the layout chosen in the UI. It allows for easier manipulation when building the level.
* It controls the rotation for the pipes and the connections of wires.
* 
* Dominique (Changes) 11/02/2020
* Removed unused package imports
* Moved rotate in switch case to before (stops repetition)
* 
* Chase (Changes) 19/2/2020
* Commented out all wire code and stuck to just pipes for the time being
* 
* Dominique (Changes) 03/03/2020
* Seperated pipes and wires and simplified script so most is handled by Fusebox
* 
* Dominique (Changes) 09/03/2020
* Pipes now have a before and after sprite so the colour can change (and go back if incorrect)
*/

/**
* \class Pipes_CW
* 
* \brief Each pipe has this script. It knows what the current state of the pipe is and what it should be and allows it to be rotated and checked for its position.
* 
* GetIsInPosition() compares the currentPosition of the pipe to the desiredPosition
* Update() resets the pipes if the player presses X while the UI is open, if it's not complete yet
* Rotate() moves the UI by 90 degrees and updates the currentPosition of the pipe
* 
* \author Chase
* 
* \date Last Modified: 09/03/2020
*/

using UnityEngine;
using UnityEngine.UI;

public class Pipes_CW : MonoBehaviour
{
    public enum Directions
    {
        HORIZONTAL,
        VERTICAL,
        RIGHT_DOWN_BEND,
        LEFT_DOWN_BEND,
        RIGHT_UP_BEND,
        LEFT_UP_BEND
    };
    public Directions startPosition;
    internal Directions currentPosition;
    public Directions desiredPosition;

    public Sprite incompletePipe;
    public Sprite completePipe;
    private Color startColour;
    private Color completeColour;

    private const int degreesToMove = 90;
    private Fusebox_CW fusebox;
    private Image image;
    private bool canBeRotated = true;
    private IdleVoiceover_CW idleVos;
    private bool beingReset = false;

    public bool GetIsInPosition() {
        if (currentPosition == desiredPosition)
        {
            return true;
        } else
        {
            return false;
        }
    }

    /// <summary>
    /// Inititalise variables and set currentPosition to the startPosition
    /// </summary>
    private void Awake()
    {
        fusebox = GameObject.Find("FuseboxUI").GetComponent<Fusebox_CW>();
        currentPosition = startPosition;
        image = GetComponent<Image>();
        startColour = GetComponent<Image>().color;
        completeColour.r = 238;
        completeColour.g = 255;
        completeColour.b = 0;
        completeColour.a = 255;
        idleVos = GameObject.Find("Managers").GetComponent<IdleVoiceover_CW>();
    }
    
    /// <summary>
    /// If the player presses X and the fusebox puzzle isn't solved yet the position of the pipe is set to its start position by rotating it until it reaches that point
    /// </summary>
    private void Update()
    {
        //if X, reset puzzle to default colours and state (make sure they can't do this if they've already solved it)
        if(!fusebox.isFuseboxSolved && Input.GetKeyDown(fusebox.resetPipesKey))
        {
            beingReset = true;
            while (currentPosition != startPosition)
            {
                Rotate();
            }
            beingReset = false;
        }
    }

    /// <summary>
    /// Rotate the GO by 90 degrees and use the enum Fusebox_CW.Directions to identify what the new direction of the pipe is given its old direction
    /// </summary>
    public void Rotate()
    {
        idleVos.interactedWith = true;
        idleVos.interactedWith = false;
        if(!beingReset) SFX_Manager_HR.instance.PlaySFX(SFX_Manager_HR.SoundEffectNames.ROTATE_PIPE, transform.position);
        if (canBeRotated)
        {
            gameObject.transform.Rotate(0, 0, degreesToMove);

            switch (currentPosition)
            {
                case Directions.HORIZONTAL:
                    {
                        currentPosition = Directions.VERTICAL;
                    }
                    break;
                case Directions.VERTICAL:
                    {
                        currentPosition = Directions.HORIZONTAL;
                    }
                    break;
                case Directions.RIGHT_DOWN_BEND:
                    {
                        currentPosition = Directions.RIGHT_UP_BEND;
                    }
                    break;
                case Directions.LEFT_DOWN_BEND:
                    {
                        currentPosition = Directions.RIGHT_DOWN_BEND;
                    }
                    break;
                case Directions.RIGHT_UP_BEND:
                    {
                        currentPosition = Directions.LEFT_UP_BEND;
                    }
                    break;
                case Directions.LEFT_UP_BEND:
                    {
                        currentPosition = Directions.LEFT_DOWN_BEND;
                    }
                    break;
            }
        }
    }
    /// <summary>
    /// Set the image sprite to the pipe when it's complete. It can't be rotated after this.
    /// </summary>
    public void ChangeColour()
    {
        image.color = completeColour;
        canBeRotated = false;
    }
    /// <summary>
    /// Set the image sprite to the pipe when it's incomplete
    /// </summary>
    public void ResetColour()
    {
        image.color = startColour;
        canBeRotated = true;
    }
}
