/* Hugo
 * 
 * What the script does...
 * 
 * This Script allows the player whenever he is in range of a pickable object to pick it up
 * 
 * Dominique (Changes) 04/02/2020
 * Specified Hit and UI as private for a consistent coding standard
 * Got rid of unused imported namespaces (System.Collections and System.Collections.Generic)
 * Renamed variable (and game object in hierarchy) to NotificationText
 * Set rayRange to 5 in script
 * Optimising after 'if(Input.GetKeyDown(KeyCode.E))' -> Unnecessary if statement that checked if the Vector3 mouse position equaled a Vector2 cast of itself
 * 
 * Dominique (Changes) 05/02/2020
 * Made sure that if the table already has the items the text doesn't pop up anymore
 * The name of an item is shown when you are looking at it after 'Press E to pick up the '
 * If the player picks up an item it is ticked off on their checklist (by name)
 * Instead of destroying it the object is deactivated
 * 
 * Dominique (Changes) 07/02/2020
 * Player can now interact with door, keypad and paper
 * Door interaction will tell the player if they can or can't open it (and let them do so if they can)
 * Keypad interaction enables the player to use the cursor to enter a keycode that is connected to a locked door
 * Paper opens up with a specified background and text according to the paper object the player interacts with
 * 
 * Chase (Changes) 08/02/2020
 * When interacting with a door, it checks to see if the door needs a key and whether or not they have the key
 * If they can't open the door and the door requires a key it hints at the player to check their journal
 * 
 * Hugo (Changes) 08/02/2020
 * Player can interact with the monitor
 * This will allow the player to zoom in on the big screen to get a better view of the house
 * 
 * Dominique (Changes) 10/02/2020
 * Player can interact with chess pieces -> this makes them rotate (and then checks that if they have gone past 360 degrees this will be changed to 0 since the angles need to be checked as in position)
  */
using UnityEngine;
using UnityEngine.UI;

public class Interact_HR : MonoBehaviour
{
    public const int zoomedFOV = 20;
    public const int defaultFOV = 60;
    public int rayRange = 5;
    public KeyCode InteractKey = KeyCode.E;

    private bool zoomedIn = false;
    private RaycastHit hit;
    private Text notificationText;
    private Journal_DR journal;
    private KeypadUI_DR keypad;
    private GameObject paper;
    private GameObject fuseboxUI;
    private Text paperText;
    private Image paperBackground;
    private Camera playerCamera;

    private void Awake()
    {
        paper = GameObject.Find("PaperUI");
        fuseboxUI = GameObject.Find("FuseboxUI");
        paperText = paper.GetComponentInChildren<Text>();
        paperBackground = paper.GetComponent<Image>();
        keypad = GameObject.Find("KeypadUI").GetComponent<KeypadUI_DR>();
        notificationText = GameObject.Find("NotificationText").GetComponent<Text>();
        journal = GameObject.Find("FPSController").GetComponent<Journal_DR>();
        playerCamera = GetComponent<Camera>();
    }

    void Update()
    {
        //RayCast Forward see if the player is in range of anything
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayRange))
        {
            //Is in looking at an object
            if (hit.transform.tag == "Object")
            {
                GameObject item = hit.transform.gameObject;
                notificationText.text = "Press E to pick up the " + item.name.ToLower();
                //If he presses the key then pick up the object
                if (Input.GetKeyDown(InteractKey))
                {
                    hit.transform.gameObject.SetActive(false);
                    notificationText.text = "";
                    Journal_DR.instance.TickOffTask(item.name); //Or Journal_DR.instance.TickOffTask("Pick up block"); Test for prototype
                }
            } else if (hit.transform.tag == "Table")
            {
                //Check if the table already has the items or not yet
                PutDown_HR putDownScript = hit.transform.gameObject.GetComponent<PutDown_HR>();
                if (!putDownScript.getBeenUsed())
                {
                    if (journal.AreTasksComplete())
                    {
                        notificationText.text = "Press E to put down your items";
                        //If he presses the key then pick up the object
                        if (Input.GetKeyDown(InteractKey))
                        {
                            putDownScript.setItemsDown();
                            notificationText.text = "";
                        }
                    }
                    else
                    {
                        notificationText.text = "You don't have all the items";
                    }
                }
            } else if (hit.transform.tag == "Keypad")
            {
                //Get the keypad we're looking at
                KeypadItem_DR keypadItem = hit.transform.gameObject.GetComponent<KeypadItem_DR>();
                //If the door isn't unlocked yet then open the keypad UI
                if (!keypadItem.door.unlocked)
                {
                    notificationText.text = "Press E to use the keypad";

                    if (Input.GetKeyDown(InteractKey))
                    {
                        //Open the keypad UI using this keypad (makes sure the password can be changed between different keypads)
                        keypad.OpenKeypad(keypadItem);

                        //Hide the notification text when the keypad is open
                        notificationText.text = "";
                    }
                }
            } else if (hit.transform.tag == "Door")
            {
                Door_DR door = hit.transform.gameObject.GetComponent<Door_DR>();

                if(door.unlocked)
                {
                    notificationText.text = "Press E to open";

                    if (Input.GetKeyDown(InteractKey))
                    {
                        notificationText.text = "";
                        door.Open();
                        //Player can't interact with door when it is already open
                        door.tag = "Untagged";
                    }
                } else if(door.requiresKey) 
                {
                    //if both key parts are found (in journal as it's for a colour matching puzzle)
                    if(journal.AreTasksComplete())
                    {
                        notificationText.text = "Press E to open";
                        door.unlocked = true;

                        if (Input.GetKeyDown(InteractKey))
                        {
                            notificationText.text = "";
                            door.Open();
                            door.tag = "Untagged";
                        }
                     
                    }
                    else //if tasks arent complete hint at player to read their journal
                    {
                       
                        notificationText.text = "It's locked. I should check my journal.";
                    }
                } else
                {
                    notificationText.text = "How can I unlock this?";
                  
                }
            } else if (hit.transform.tag == "Paper")
            {
                notificationText.text = "Press E to read";

                if (Input.GetKeyDown(InteractKey))
                {
                    Paper_DR paperItem = hit.transform.GetComponent<Paper_DR>();
                    notificationText.text = "";
                    //Set paper text and background according to the object
                    paperText.text = paperItem.text;
                    paperBackground.sprite = paperItem.background;
                    paper.SetActive(true);
                }
           }
            else if (hit.transform.tag == "Fusebox")
            {
                notificationText.text = "Press E to open the fuse box";

                if (Input.GetKeyDown(InteractKey))
                {
                    fuseboxUI.GetComponent<Fusebox_CW>().OpenFusebox();
                }
            }
            else if (hit.transform.tag == "Monitor")
            {
                if (!zoomedIn)
                {
                    notificationText.text = "Press E to zoom in";
                    if (Input.GetKeyDown(InteractKey))
                    {
                        playerCamera.transform.LookAt(hit.transform);
                        playerCamera.fieldOfView = zoomedFOV;
                        zoomedIn = true;
                    }
                }
                else
                {
                    notificationText.text = "Press E to zoom out";

                    if (Input.GetKeyDown(InteractKey))
                    {
                        zoomedIn = false;
                        playerCamera.fieldOfView = defaultFOV;
                    }
                }

            }
            else if (hit.transform.tag == "ChessPiece")
            {
                notificationText.text = "Press E to rotate the " + hit.transform.name;

                if (Input.GetKeyDown(InteractKey))
                {
                    //Rotate 90 degrees in y axis
                    hit.transform.Rotate(0, 90, 0);
                    //If angle is 360 degrees set it to 0 degrees
                    if (hit.transform.rotation.eulerAngles.y == 360)
                    {
                        hit.transform.Rotate(0, -360, 0);
                    }
                }
            }
            else
            {
                playerCamera.fieldOfView = defaultFOV;
            }
        }
        else
        {
            playerCamera.fieldOfView = defaultFOV;
            notificationText.text = "";
        }
    }
}
