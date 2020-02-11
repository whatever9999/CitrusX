/*Chase Wilding Pipes script 09/02/2020
* This script controls both parts of the fuse puzzle. It uses buttons and public variables which must be set in the inspector
* depending on the layout chosen in the UI. It allows for easier manipulation when building the level.
* It controls the rotation for the pipes and the connections of wires.
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
    public Button previousWire;
    public Button previousWire2;
    public Button matchingEnd;
    private bool isInPosition = false;
    private bool isWireConnected = false;
    private const int degreesToMove = 90;
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
        //if X, reset puzzle to default colours and state
        if(Input.GetKeyDown(theFusebox.resetPipesKey))
        {
            GetComponent<Button>().image.color = defaultBoxColour;
            if(!isWireEnd)
            {
                isWireConnected = false;
            }  
        }
    }
    public void Rotate()
    {
        //if in the desired position set to true so it will not move
        if(desiredDirection == currentDirection)
        {
            isInPosition = true;
            GetComponent<Button>().enabled = false;
            theFusebox.pipeCompletedCount++;
        }

        //if not in correct position, rotate it
        if (desiredDirection != currentDirection)
        {
            switch (currentDirection)
            {
                case DIRECTIONS.HORIZONTAL:
                    { 
                        gameObject.transform.Rotate(0, 0, degreesToMove);
                        currentDirection = DIRECTIONS.VERTICAL; 
                    }
                    break;
                case DIRECTIONS.VERTICAL:
                    {
                        gameObject.transform.Rotate(0, 0, degreesToMove);
                        currentDirection = DIRECTIONS.HORIZONTAL;
                    }
                    break;
                case DIRECTIONS.RIGHT_DOWN_BEND:
                    {
                        gameObject.transform.Rotate(0, 0, degreesToMove);
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
        //check if it is a wireend (this is ticked in the inspector)
        if(isWireEnd)
        {
            //signify that it is already connected
            isWireConnected = true;
            //check for what colour it represents
            switch (wireEndColour)
            {
                case COLOURS.RED:
                    {
                        //then set the drawcolour to the correct colour
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
            if (previousWire.GetComponent<Pipes_CW>().wireEndColour == wireEndColour || previousWire2.GetComponent<Pipes_CW>().wireEndColour == wireEndColour)
            {
                //if the previous tile is the same colour as the wire end, both the original and last pipe end will change colour to show
                //completion
                GetComponent<Button>().image.color = Color.yellow;
                matchingEnd.image.color = Color.yellow;
                previousWire.GetComponent<Pipes_CW>().isWireConnected = true;
                theFusebox.wireCompletedCount += 2;
            }    
        }
        if(!isWireEnd)
        {
            //if the tile hasn't been manipulated
            if(GetComponent<Button>().image.color == defaultBoxColour)
            {
                //check if the previous pipe has been used
                if(previousWire.GetComponent<Pipes_CW>().isWireConnected || previousWire2.GetComponent<Pipes_CW>().isWireConnected)
                {
                    //set the wireEndColour for the comparison for wireEnds
                    if (theFusebox.drawColour == Color.red)
                    {
                        wireEndColour = COLOURS.RED;
                    }
                    else if (theFusebox.drawColour == Color.green)
                    {
                        wireEndColour = COLOURS.GREEN;
                    }
                    else if (theFusebox.drawColour == Color.blue)
                    {
                        wireEndColour = COLOURS.BLUE;
                    }
                    //check for colour
                    if (previousWire.GetComponent<Pipes_CW>().wireEndColour == wireEndColour || previousWire2.GetComponent<Pipes_CW>().wireEndColour == wireEndColour)
                    {
                        //draw the correct colour tile and signify as connected
                        GetComponent<Button>().image.color = theFusebox.drawColour;
                        isWireConnected = true;
                    }
                    
                    
                   
                }
               

            }
            
        }
    }
}
