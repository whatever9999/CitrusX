/*Chase Wilding Pipes script 09/02/2020
* This script controls both parts of the fuse puzzle. It uses buttons and public variables which must be set in the inspector
* depending on the layout chosen in the UI. It allows for easier manipulation when building the level.
* It controls the rotation for the pipes and the connections of wires.
* 
* Dominique (Changes) 11/02/2020
* Removed unused package imports
* Moved rotate in switch case to before (stops repetition)
* Chase (Changes) 19/2/2020
* Commented out all wire code and stuck to just pipes for the time being
*/

using UnityEngine;
using UnityEngine.UI;

public class Pipes_CW : MonoBehaviour
{
    #region CONNECTING_WIRES
    public Button northWire;
    public Button southWire;
    public Button eastWire;
    public Button westWire;
    public Pipes_CW northWireScript;
    public Pipes_CW southWireScript;
    public Pipes_CW eastWireScript;
    private Pipes_CW westWireScript;
    #endregion
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
    public bool isPipe;
    public Button matchingEnd;
    private Button thisPipe;
    public bool[] wiresConnectedTo =  { false, false, false, false};
    private bool isInPosition = false;
    private bool isWireConnected = false;
    private const int degreesToMove = 90;
    private Color wireColour;
    Color defaultBoxColour;
    private Fusebox_CW theFusebox;
    #endregion
    public bool GetCompletionState() { return isInPosition; }
    public void Awake()
    {
        theFusebox = GameObject.Find("FuseboxUI").GetComponent<Fusebox_CW>();
        defaultBoxColour = GetComponent<Button>().image.color;
        wireColour = GetComponent<Button>().image.color;
        thisPipe = GetComponent<Button>();
    }
    public void Start()
    {
        if(wiresConnectedTo[0])
        {
            northWireScript = northWire.GetComponent<Pipes_CW>(); 
        }
        if(wiresConnectedTo[1])
        {
            eastWireScript = eastWire.GetComponent<Pipes_CW>();
        }
        if(wiresConnectedTo[2])
        {
            southWireScript = southWire.GetComponent<Pipes_CW>();
        }
        if(wiresConnectedTo[3])
        {
            westWireScript = westWire.GetComponent<Pipes_CW>();
        }
       

    }
    public void Update()
    {
        //if X, reset puzzle to default colours and state
        if(Input.GetKeyDown(theFusebox.resetPipesKey))
        {
            wireColour = defaultBoxColour;
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
            thisPipe.enabled = false;
            theFusebox.pipeCompletedCount++;
        }

        //if not in correct position, rotate it
        if (desiredDirection != currentDirection)
        {
            gameObject.transform.Rotate(0, 0, degreesToMove);

            switch (currentDirection)
            {
                case DIRECTIONS.HORIZONTAL:
                    { 
                        currentDirection = DIRECTIONS.VERTICAL; 
                    }
                    break;
                case DIRECTIONS.VERTICAL:
                    {
                        currentDirection = DIRECTIONS.HORIZONTAL;
                    }
                    break;
                case DIRECTIONS.RIGHT_DOWN_BEND:
                    {
                        currentDirection = DIRECTIONS.LEFT_DOWN_BEND;
                    }
                    break;
                case DIRECTIONS.LEFT_DOWN_BEND:
                    {
                        currentDirection = DIRECTIONS.LEFT_UP_BEND;
                    }
                    break;
                case DIRECTIONS.RIGHT_UP_BEND:
                    {
                        currentDirection = DIRECTIONS.RIGHT_DOWN_BEND;
                    }
                    break;
                case DIRECTIONS.LEFT_UP_BEND:
                    {
                        currentDirection = DIRECTIONS.RIGHT_UP_BEND;
                    }
                    break;
                default:
                    break;
            }
        }
       
    }
    //public void ConnectWires()
    //{
    //    //check if it is a wireend (this is ticked in the inspector)
    //    if(isWireEnd)
    //    {
    //        //signify that it is already connected
    //        isWireConnected = true;
    //        //check for what colour it represents
    //        switch (wireEndColour)
    //        {
    //            case COLOURS.RED:
    //                {
    //                    //then set the drawcolour to the correct colour
    //                    theFusebox.drawColour = Color.red;
    //                }
    //                break;
    //            case COLOURS.BLUE:
    //                {
    //                    theFusebox.drawColour = Color.blue;
    //                }
    //                break;
    //            case COLOURS.GREEN:
    //                {
    //                    theFusebox.drawColour = Color.green;
    //                }
    //                break;
    //            default:
    //                break;
    //        }
    //        if ((wiresConnectedTo[0] && northWireScript.wireEndColour == wireEndColour) || (wiresConnectedTo[1] && eastWireScript.wireEndColour == wireEndColour) ||(wiresConnectedTo[2] && southWireScript.wireEndColour == wireEndColour) || (wiresConnectedTo[3] && westWireScript.wireEndColour == wireEndColour))
    //        {
    //            //if the previous tile is the same colour as the wire end, both the original and last pipe end will change colour to show
    //            //completion
    //            wireColour = Color.yellow;
    //            matchingEnd.image.color = Color.yellow;
    //            northWireScript.isWireConnected = true;
    //            southWireScript.isWireConnected = true;
    //            eastWireScript.isWireConnected = true;
    //            westWireScript.isWireConnected = true;
    //            theFusebox.wireCompletedCount += 1;
    //        }    
    //    }
    //    if(!isWireEnd)
    //    {
    //        //if the tile hasn't been manipulated
    //        if(wireColour == defaultBoxColour)
    //        {
    //            //check if the previous pipe has been used
    //            if(( wiresConnectedTo[1] && eastWireScript.isWireConnected) || ( wiresConnectedTo[0] && northWireScript.isWireConnected) ||
    //                (wiresConnectedTo[2] && southWireScript.isWireConnected) || (wiresConnectedTo[3] && eastWireScript.isWireConnected))
    //            {
    //                //set the wireEndColour for the comparison for wireEnds
    //                if (theFusebox.drawColour == Color.red)
    //                {
    //                    wireEndColour = COLOURS.RED;
                        
    //                }
    //                else if (theFusebox.drawColour == Color.green)
    //                {
    //                    wireEndColour = COLOURS.GREEN;
    //                }
    //                else if (theFusebox.drawColour == Color.blue)
    //                {
    //                    wireEndColour = COLOURS.BLUE;
    //                }
    //                //check for colour
    //                if ((wiresConnectedTo[0] && northWireScript.wireEndColour == wireEndColour) || (wiresConnectedTo[1] && eastWireScript.wireEndColour == wireEndColour) 
    //                    || (wiresConnectedTo[2] && southWireScript.wireEndColour == wireEndColour) || (wiresConnectedTo[3] && westWireScript.wireEndColour == wireEndColour))
    //                {
    //                    //draw the correct colour tile and signify as connected
    //                    wireColour = theFusebox.drawColour;
    //                    thisPipe.image.color = theFusebox.drawColour;
    //                    isWireConnected = true;
    //                }
                   
    //            }
    //        }
            
    //    }
    //}
}
