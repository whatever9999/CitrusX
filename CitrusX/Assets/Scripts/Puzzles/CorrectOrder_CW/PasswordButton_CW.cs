/*
 * Chase - holds the correct order puzzles boxes. Flash() controls the lights based off of values set and ChangeColour() will change the password
 * box's colour when clicked on.
 * Chase (Changes) 11/3/2020
 * Fixed known issue of original red not registering. Added two extra rounds to improve the puzzle.
 */

/**
* \class PasswordButton_CW
* 
* \brief Makes the password boxes for the correct order puzzle flash and lets the player change the colour of the password buttons by clicking on them.
* 
* \author Chase
* 
* \date Last Modified: 18/02/2020
*/

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PasswordButton_CW : MonoBehaviour
{
    #region VARIABLES
    private CorrectOrder_CW correctOrderPuzzle;
    private Image thisImage;
    private Color originalColor;
    public enum WHICH_BOX
    {
        FIRST_BOX,
        SECOND_BOX,
        THIRD_BOX,
        FOURTH_BOX,
        PASSWORD_ONE,
        PASSWORD_TWO,
        PASSWORD_THREE,
        PASSWORD_FOUR
    };
    public WHICH_BOX box;
    public bool isPasswordBox = false;
    private float flashTimer = 0;
    private float flashNow;
   
    #endregion
    #region GET_BOX_NO
    private int GetBoxNumber()
    {
        switch (box)
        {
            case WHICH_BOX.FIRST_BOX:
                {
                    return 0;
                    
                } 
            case WHICH_BOX.SECOND_BOX:
                {
                    return 1;
                }
            case WHICH_BOX.THIRD_BOX:
                {
                    return 2;
                }
            case WHICH_BOX.FOURTH_BOX:
                {
                    return 3;
                }
            case WHICH_BOX.PASSWORD_ONE:
                {
                    return 4;
                }
            case WHICH_BOX.PASSWORD_TWO:
                {
                    return 5;
                }
            case WHICH_BOX.PASSWORD_THREE:
                {
                    return 6;
                }
            case WHICH_BOX.PASSWORD_FOUR:
                {
                    return 7;
                }
            default:
                return 0;
        }
       
    }
#endregion

    /// <summary>
    /// Inititialise variables and set the flash time of the box according to what box it is
    /// </summary>
    private void Awake()
    {
        correctOrderPuzzle = GameObject.Find("CorrectOrderUI").GetComponent<CorrectOrder_CW>();
        thisImage = GetComponent<Image>();
        originalColor = thisImage.color;
    }
    
    /// <summary>
    /// Is placed on a button so that its colour changes to the next one along when the player clicks on it
    /// </summary>
    public void ChangeColour()
    {
       if(thisImage.color == Color.red)
       {
            thisImage.color = Color.green;
            correctOrderPuzzle.AssignBoxColour(GetBoxNumber(), Color.green);
       }
       else if (thisImage.color == Color.green)
       {
            thisImage.color = Color.yellow;
            correctOrderPuzzle.AssignBoxColour(GetBoxNumber(), Color.yellow);
        }
       else if (thisImage.color == Color.yellow)
       {
            thisImage.color = Color.cyan;
            correctOrderPuzzle.AssignBoxColour(GetBoxNumber(), Color.cyan);
        }
       else if (thisImage.color == Color.cyan)
       {
            thisImage.color = Color.red;
            correctOrderPuzzle.AssignBoxColour(GetBoxNumber(),Color.red);
        }
       else if(thisImage.color == originalColor)
       {
            thisImage.color = Color.red;
            correctOrderPuzzle.AssignBoxColour(GetBoxNumber(), Color.red);
        }
    }
    /// <summary>
    /// If the button isn't the password box a timer runs for it to make it flash
    /// </summary>
    private void Update()
    {
        if(!isPasswordBox)
        {
            switch (box)
            {
                case WHICH_BOX.FIRST_BOX:
                    {
                        if (correctOrderPuzzle.whichRound[0])
                        {
                            flashNow = 2f;
                            
                        }
                        else if(correctOrderPuzzle.whichRound[1])
                        {
                            flashNow = 3f;
                        }
                        else if(correctOrderPuzzle.whichRound[2])
                        {
                            flashNow = 0.5f;
                        }
                            break;
                    }
                case WHICH_BOX.SECOND_BOX:
                    {
                        if (correctOrderPuzzle.whichRound[0])
                        {
                            flashNow = 4f;

                        }
                        else if (correctOrderPuzzle.whichRound[1])
                        {
                            flashNow = 2f;
                        }
                        else if (correctOrderPuzzle.whichRound[2])
                        {
                            flashNow = 8f;
                        }
                        
                        break;
                    }
                case WHICH_BOX.THIRD_BOX:
                    {
                        if (correctOrderPuzzle.whichRound[0])
                        {
                            flashNow = 8f;

                        }
                        else if (correctOrderPuzzle.whichRound[1])
                        {
                            flashNow = 5f;
                        }
                        else if (correctOrderPuzzle.whichRound[2])
                        {
                            flashNow = 2f;
                        }
                        
                        break;
                    }
                case WHICH_BOX.FOURTH_BOX:
                    {
                        if (correctOrderPuzzle.whichRound[0])
                        {
                            flashNow = 10f;

                        }
                        else if (correctOrderPuzzle.whichRound[1])
                        {
                            flashNow = 1f;
                        }
                        else if (correctOrderPuzzle.whichRound[2])
                        {
                            flashNow = 0.4f;
                        }
                        
                        break;
                    }
                default:
                    break;
            }

            if (flashTimer >= flashNow)
            {
                Flash();
                flashTimer = 0;
            }
            else
            {
                flashTimer += Time.deltaTime;
            }
        } 
    }
    /// <summary>
    /// The box flashes to a colour according to what box it is then goes back to white using the coroutine BackToWhite()
    /// </summary>
    private void Flash()
    {
        switch (box)
        {
            case WHICH_BOX.FIRST_BOX:
                {
                    if(correctOrderPuzzle.whichRound[0])
                    {
                        thisImage.color = Color.red;
                        correctOrderPuzzle.AssignBoxColour(GetBoxNumber(), Color.red);
                    }
                    else if(correctOrderPuzzle.whichRound[1])
                    {
                        thisImage.color = Color.green;
                        correctOrderPuzzle.AssignBoxColour(GetBoxNumber(), Color.green);
                    }
                    else if(correctOrderPuzzle.whichRound[2])
                    {
                        thisImage.color = Color.yellow;
                        correctOrderPuzzle.AssignBoxColour(GetBoxNumber(), Color.yellow);
                    }
                    
                }
                break;
            case WHICH_BOX.SECOND_BOX:
                {
                    if (correctOrderPuzzle.whichRound[0])
                    {
                        thisImage.color = Color.red;
                        correctOrderPuzzle.AssignBoxColour(GetBoxNumber(), Color.red);
                    }
                    else if (correctOrderPuzzle.whichRound[1])
                    {
                        thisImage.color = Color.green;
                        correctOrderPuzzle.AssignBoxColour(GetBoxNumber(), Color.green);
                    }
                    else if (correctOrderPuzzle.whichRound[2])
                    {
                        thisImage.color = Color.yellow;
                        correctOrderPuzzle.AssignBoxColour(GetBoxNumber(), Color.yellow);
                    }
                    thisImage.color = Color.yellow;
                    correctOrderPuzzle.AssignBoxColour(GetBoxNumber(), Color.yellow);
                }
                break;
            case WHICH_BOX.THIRD_BOX:
                {
                    if (correctOrderPuzzle.whichRound[0])
                    {
                        thisImage.color = Color.green;
                        correctOrderPuzzle.AssignBoxColour(GetBoxNumber(), Color.green);
                    }
                    else if (correctOrderPuzzle.whichRound[1])
                    {
                        thisImage.color = Color.green;
                        correctOrderPuzzle.AssignBoxColour(GetBoxNumber(), Color.green);
                    }
                    else if (correctOrderPuzzle.whichRound[2])
                    {
                        thisImage.color = Color.cyan;
                        correctOrderPuzzle.AssignBoxColour(GetBoxNumber(), Color.cyan);
                    }
                    
                }
                break;
            case WHICH_BOX.FOURTH_BOX:
                {
                    if (correctOrderPuzzle.whichRound[0])
                    {
                        thisImage.color = Color.cyan;
                        correctOrderPuzzle.AssignBoxColour(GetBoxNumber(), Color.cyan);
                    }
                    else if (correctOrderPuzzle.whichRound[1])
                    {
                        thisImage.color = Color.red;
                        correctOrderPuzzle.AssignBoxColour(GetBoxNumber(), Color.red);
                    }
                    else if (correctOrderPuzzle.whichRound[2])
                    {
                        thisImage.color = Color.yellow;
                        correctOrderPuzzle.AssignBoxColour(GetBoxNumber(), Color.yellow);
                    }
                 
                }
                break;
            default:
                break;
        }
        StartCoroutine(BackToWhite());
    } 
    /// <summary>
    /// Changes the image colour to white
    /// </summary>
    private IEnumerator BackToWhite()
    {
        yield return new WaitForSeconds(0.5f);
        thisImage.color = Color.white;
        yield return new WaitForSeconds(0.5f);
    }
}
