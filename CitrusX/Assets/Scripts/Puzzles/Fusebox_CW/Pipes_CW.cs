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
*/

using UnityEngine;

public class Pipes_CW : MonoBehaviour
{
    public Fusebox_CW.Directions startPosition;
    private Fusebox_CW.Directions currentPosition;
    public Fusebox_CW.Directions desiredPosition;

    private const int degreesToMove = 90;
    private Fusebox_CW fusebox;

    public bool GetIsInPosition() {
        if (currentPosition == desiredPosition)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public void Awake()
    {
        fusebox = GameObject.Find("FuseboxUI").GetComponent<Fusebox_CW>();
        currentPosition = startPosition;
    }
    
    public void Update()
    {
        //if X, reset puzzle to default colours and state (make sure they can't do this if they've already solved it)
        if(!fusebox.isFuseboxSolved && Input.GetKeyDown(fusebox.resetPipesKey))
        {
            while(currentPosition != startPosition)
            {
                Rotate();
            }
        }
    }

    public void Rotate()
    {
        gameObject.transform.Rotate(0, 0, degreesToMove);

        switch (currentPosition)
        {
            case Fusebox_CW.Directions.HORIZONTAL:
                {
                    currentPosition = Fusebox_CW.Directions.VERTICAL;
                }
                break;
            case Fusebox_CW.Directions.VERTICAL:
                {
                    currentPosition = Fusebox_CW.Directions.HORIZONTAL;
                }
                break;
            case Fusebox_CW.Directions.RIGHT_DOWN_BEND:
                {
                    currentPosition = Fusebox_CW.Directions.RIGHT_UP_BEND;
                }
                break;
            case Fusebox_CW.Directions.LEFT_DOWN_BEND:
                {
                    currentPosition = Fusebox_CW.Directions.RIGHT_DOWN_BEND;
                }
                break;
            case Fusebox_CW.Directions.RIGHT_UP_BEND:
                {
                    currentPosition = Fusebox_CW.Directions.LEFT_UP_BEND;
                }
                break;
            case Fusebox_CW.Directions.LEFT_UP_BEND:
                {
                    currentPosition = Fusebox_CW.Directions.LEFT_DOWN_BEND;
                }
                break;
        }
    }
}
