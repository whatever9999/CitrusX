//Chase Wilding
//script for moving the individual pipe pieces into the correct position

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pipes_CW : MonoBehaviour
{
    public enum DIRECTIONS
    {
        HORIZONTAL,
        VERTICAL,
        RIGHT_DOWN_BEND,
        LEFT_DOWN_BEND,
        RIGHT_UP_BEND,
        LEFT_UP_BEND
    }
    public DIRECTIONS currentDirection;
    public DIRECTIONS desiredDirection;
    private Sprite pipeSprite;

    private void Awake()
    {
        pipeSprite = gameObject.GetComponent<Image>().sprite;
        
    }
    public void Rotate()
    {
        //check it isn't in the correct position
        if(desiredDirection != currentDirection)
        {
            switch (currentDirection)
            {
                case DIRECTIONS.HORIZONTAL:
                    {
                        gameObject.transform.Rotate(Vector3.left);
                    }
                    break;
                case DIRECTIONS.VERTICAL:
                    {
                        gameObject.transform.Rotate(Vector3.left);
                    }
                    break;
                case DIRECTIONS.RIGHT_DOWN_BEND:
                    {
                        gameObject.transform.Rotate(Vector3.left);
                    }
                    break;
                case DIRECTIONS.LEFT_DOWN_BEND:
                    {
                        gameObject.transform.Rotate(Vector3.left);
                    }
                    break;
                case DIRECTIONS.RIGHT_UP_BEND:
                    {
                        gameObject.transform.Rotate(Vector3.left);
                    }
                    break;
                case DIRECTIONS.LEFT_UP_BEND:
                    {
                        gameObject.transform.Rotate(Vector3.left);
                    }
                    break;
                default:
                    break;
            }
        }
       
    }

}
