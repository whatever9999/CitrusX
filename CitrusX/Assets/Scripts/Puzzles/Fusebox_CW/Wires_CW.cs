/*
 * Dominique
 * This is Chase's code for the wires part of the fusebox puzzle
 */

/**
* \class Wires_CW
* 
* \brief Chase's code for the wires part of the fusebox puzzle
* 
* \author Chase
* 
* \date Last Modified: 02/03/2020
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wires_CW : MonoBehaviour
{
    //#region CONNECTING_WIRES
    //public Button northWire;
    //public Button southWire;
    //public Button eastWire;
    //public Button westWire;
    //public Pipes_CW northWireScript;
    //public Pipes_CW southWireScript;
    //public Pipes_CW eastWireScript;
    //private Pipes_CW westWireScript;
    //#endregion
    //
    //public COLOURS wireEndColour;
    //
    //public enum COLOURS
    //{
    //    RED,
    //    BLUE,
    //    GREEN
    //
    //}
    //
    //public void Start()
    //{
    //    if (wiresConnectedTo[0])
    //    {
    //        northWireScript = northWire.GetComponent<Pipes_CW>();
    //    }
    //    if (wiresConnectedTo[1])
    //    {
    //        eastWireScript = eastWire.GetComponent<Pipes_CW>();
    //    }
    //    if (wiresConnectedTo[2])
    //    {
    //        southWireScript = southWire.GetComponent<Pipes_CW>();
    //    }
    //    if (wiresConnectedTo[3])
    //    {
    //        westWireScript = westWire.GetComponent<Pipes_CW>();
    //    }
    //
    //
    //}
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
