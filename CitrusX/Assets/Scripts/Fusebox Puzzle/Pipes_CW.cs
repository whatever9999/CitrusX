/*Chase Wilding 09/02/2020
* Script for moving the individual pipe pieces into the correct position, they can be rotated by 90 degrees but when they're in the
* correct position they stop being moveable.
* The current and desired position are set in the Inspector so they can be manipulated for different layouts.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pipes_CW : MonoBehaviour
{
    #region VARIABLES
    public enum DIRECTIONS
    {
        HORIZONTAL,
        VERTICAL,
        RIGHT_DOWN_BEND,
        LEFT_DOWN_BEND,
        RIGHT_UP_BEND,
        LEFT_UP_BEND
    }
    public enum COLOURS
    {
        RED,
        BLUE,
        GREEN
    }
    public DIRECTIONS currentDirection;
    public DIRECTIONS desiredDirection;
    public COLOURS wireEndColour;
    public bool isWireEnd;
    private bool isInPosition = false;
    Color defaultBoxColour;
    private Fusebox_CW theFusebox;
    #endregion
    public bool GetCompletionState() { return isInPosition; }
    public void Awake()
    {
        theFusebox = GameObject.Find("FuseboxUI").GetComponent<Fusebox_CW>();
        defaultBoxColour = GetComponent<Button>().image.color;
    }
    public void Update()
    {
        if(Input.GetKeyDown(theFusebox.resetPipesKey))
        {
            GetComponent<Button>().image.color = defaultBoxColour;
        }
    }
    public void Rotate()
    {
        //if in the desired position set to true so it will not move
        if(desiredDirection == currentDirection)
        {
            isInPosition = true;
        }

        //if not in correct position, rotate it
        if (desiredDirection != currentDirection)
        {
            switch (currentDirection)
            {
                case DIRECTIONS.HORIZONTAL:
                    { 
                        gameObject.transform.Rotate(0, 0, 90);
                        currentDirection = DIRECTIONS.VERTICAL; 
                    }
                    break;
                case DIRECTIONS.VERTICAL:
                    {
                        gameObject.transform.Rotate(0, 0, 90);
                        currentDirection = DIRECTIONS.HORIZONTAL;
                    }
                    break;
                case DIRECTIONS.RIGHT_DOWN_BEND:
                    {
                        gameObject.transform.Rotate(0, 0, 90);
                        currentDirection = DIRECTIONS.LEFT_DOWN_BEND;
                    }
                    break;
                case DIRECTIONS.LEFT_DOWN_BEND:
                    {
                        gameObject.transform.Rotate(0, 0, 90);
                        currentDirection = DIRECTIONS.LEFT_UP_BEND;
                    }
                    break;
                case DIRECTIONS.RIGHT_UP_BEND:
                    {
                        gameObject.transform.Rotate(0, 0, 90);
                        currentDirection = DIRECTIONS.RIGHT_DOWN_BEND;
                    }
                    break;
                case DIRECTIONS.LEFT_UP_BEND:
                    {
                        gameObject.transform.Rotate(0, 0, 90);
                        currentDirection = DIRECTIONS.RIGHT_UP_BEND;
                    }
                    break;
                default:
                    break;
            }
        }
       
    }
    public void ConnectWires()
    {
        if(isWireEnd)
        {
            switch (wireEndColour)
            {
                case COLOURS.RED:
                    {
                        theFusebox.drawColour = Color.red;
                    }
                    break;
                case COLOURS.BLUE:
                    {
                        theFusebox.drawColour = Color.blue;
                    }
                    break;
                case COLOURS.GREEN:
                    {
                        theFusebox.drawColour = Color.green;
                    }
                    break;
                default:
                    break;
            }
            
        }
        if(!isWireEnd)
        {
            
            if(GetComponent<Button>().image.color != defaultBoxColour)
            {
               
            }
            else if(GetComponent<Button>().image.color == defaultBoxColour)
            {
                GetComponent<Button>().image.color = theFusebox.drawColour;
            }
            
        }
    }
}
