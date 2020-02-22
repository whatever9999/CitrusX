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
 * zooms out if the player presses the key again or leaves the monitor
 * 
 * Dominique (Changes) 10/02/2020
 * Player can interact with chess pieces -> this makes them rotate (and then checks that if they have gone past 360 degrees this will be changed to 0 since the angles need to be checked as in position)
 * Player can interact with water bowl -> picks up a coin and resets the baron
 * 
 * Hugo (Changes) 10/02/2020
 * Fixed Raycasting bug
 * Fixed TextBox staying on screen
 * Added controller functionality
 * 
 * Chase (Changes) 11/02/2020
 * Added ritual and garden table interaction for the first puzzle as they progress the puzzles
 * 
 * Adam (Changes) 18/02/2020
 * Added moveable weights to list of interactable objects (seems to work fine, but unity didn't like it the first couple of times I looked at a weight... Now working fine, just a heads up)
 * 
 * Adam (Changes) 19/02/2020
 * Added Candles and bowl as interactable objects
 * 
 * Chase (Changes) 17/2/2020
 * Added PC for correct order puzzle
 * 
 * Hugo (Changes) 16/02/2020
 * Added a glow effect onto interactable objects
 * 
 * Dominique (Changes) 20/02/2020
 * Added a check to see if the player has won or lost the game
 * 
 * Chase (Changes) 22/2/2020 added more to door section for colour
 * matching puzzle as it relies on trying the lock for some voiceovers.
 * Added section to paper for certain notes for game progression in keycode puzzle
 * 
  */
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Interact_HR : MonoBehaviour
{
    public const int zoomedFOV = 20;
    public const int defaultFOV = 60;
    public int rayRange = 6;
    public KeyCode InteractKey = KeyCode.E;
    public Material outlineMaterial;

    private Material originalMaterial;
    private MeshRenderer targetRenderer;
    private MeshRenderer currRenderer;
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
    private int numberCoinsCollected;
    private GameObject correctOrderUI;
    private Inventory_HR inventoryManager;
    private WaterBowl_DR waterBowl;

    #region VARS_FOR_PUZZLES
    private ColourMatchingPuzzle_CW colourMatch;
    #endregion

    private void Awake()
    {
        inventoryManager = GetComponent<Inventory_HR>();
        paper = GameObject.Find("PaperUI");
        fuseboxUI = GameObject.Find("FuseboxUI");
        paperText = paper.GetComponentInChildren<Text>();
        paperBackground = paper.GetComponent<Image>();
        keypad = GameObject.Find("KeypadUI").GetComponent<KeypadUI_DR>();
        notificationText = GameObject.Find("NotificationText").GetComponent<Text>();
        journal = GameObject.Find("FPSController").GetComponent<Journal_DR>();
        playerCamera = GetComponent<Camera>();
        correctOrderUI = GameObject.Find("CorrectOrderUI");
        waterBowl = GameObject.Find("Water Bowl").GetComponent<WaterBowl_DR>();
        colourMatch = GameObject.Find("Colour Matching Door").GetComponent<ColourMatchingPuzzle_CW>();
    }

    void Update()
    {
        //Reset text
        notificationText.text = "";
        //RayCast Forward see if the player is in range of anything
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayRange))
        {
            //Get the current Renderer for the object
            currRenderer = hit.transform.gameObject.GetComponent<MeshRenderer>();

            //If the object is not the same as the previous object then revert to the original material
            //and change the new object to the outline material
            if (targetRenderer && currRenderer.material != targetRenderer.material)
            {
                targetRenderer.material = originalMaterial;
                originalMaterial = currRenderer.material;
                targetRenderer = currRenderer;
                currRenderer.material = outlineMaterial;
                
            }
            else
            {
                targetRenderer = currRenderer;
            }

            //Is in looking at an object?
            if (hit.transform.tag == "Object")
            {
                GameObject item = hit.transform.gameObject;
                notificationText.text = "Press E to pick up the " + item.name.ToLower();
                //If he presses the key then pick up the object
                if (Input.GetKeyDown(InteractKey) || Input.GetButtonDown("Interact"))
                {
                    inventoryManager.AddItem(Inventory_HR.Names.WaterJug);
                    hit.transform.gameObject.SetActive(false);
                    notificationText.text = "";
                    Journal_DR.instance.TickOffTask(item.name); //Or Journal_DR.instance.TickOffTask("Pick up block"); Test for prototype
                }
            }
            else if (hit.transform.tag == "Table")
            {
                //Check if the table already has the items or not yet
                PutDown_HR putDownScript = hit.transform.gameObject.GetComponent<PutDown_HR>();
                if (!putDownScript.GetBeenUsed())
                {
                    Table_CW table = hit.transform.gameObject.GetComponent<Table_CW>();
                    //if its the ritual table...
                    if (table.currentTable == Table_CW.TABLES.RITUAL_TABLE)
                    {
                        //check to see if its been set up
                        if (GetComponent<SetUpRitual_CW>().ritualSetUpCollected)
                        {
                            notificationText.text = "Press E to put down your items";
                            //If he presses the key then pick up the object
                            if (Input.GetKeyDown(InteractKey) || Input.GetButtonDown("Interact"))
                            {
                                putDownScript.PutItemsDown();
                                //let the table and journal know the items are put down
                                table.hasBeenPlaced = true;
                                Journal_DR.instance.TickOffTask("Place on table");
                                notificationText.text = "";
                            }
                        }
                    }
                    else if (table.currentTable == Table_CW.TABLES.GARDEN_TABLE)
                    {
                        //check to see if its been set up
                        if (GetComponent<SetUpRitual_CW>().jewelleryCollected)
                        {
                            notificationText.text = "Press E to put down your items";
                            //If he presses the key then pick up the object
                            if (Input.GetKeyDown(InteractKey) || Input.GetButtonDown("Interact"))
                            {
                                putDownScript.PutItemsDown();
                                //let the table and journal know the items are put down
                                table.hasBeenPlaced = true;
                                Journal_DR.instance.TickOffTask("Place in garden");
                                notificationText.text = "";
                            }
                        }
                    }
                    else if (table.currentTable == Table_CW.TABLES.CHESS_BOARD)
                    {
                        if (Journal_DR.instance.AreTasksComplete())
                        {
                            notificationText.text = "Press E to put down the pawn";
                            //If he presses the key then pick up the object
                            if (Input.GetKeyDown(InteractKey) || Input.GetButtonDown("Interact"))
                            {
                                putDownScript.PutItemsDown();
                                //let the table and journal know the items are put down
                                table.hasBeenPlaced = true;
                                notificationText.text = "";
                            }
                        }
                    }
                    else
                    {
                        notificationText.text = "You don't have all the items";
                    }
                }
            }
            else if (hit.transform.tag == "Keypad")
            {
                //Get the keypad we're looking at
                KeypadItem_DR keypadItem = hit.transform.gameObject.GetComponent<KeypadItem_DR>();
                //If the door isn't unlocked yet then open the keypad UI
                if (!keypadItem.door.unlocked)
                {
                    notificationText.text = "Press E to use the keypad";

                    if (Input.GetKeyDown(InteractKey) || Input.GetButtonDown("Interact"))
                    {
                        //Open the keypad UI using this keypad (makes sure the password can be changed between different keypads)
                        keypad.OpenKeypad(keypadItem);

                        //Hide the notification text when the keypad is open
                        notificationText.text = "";
                    }
                }
            }
            else if (hit.transform.tag == "Door")
            {
                Door_DR door = hit.transform.gameObject.GetComponent<Door_DR>();

                if (door.unlocked)
                {
                    notificationText.text = "Press E to open";

                    if (Input.GetKeyDown(InteractKey) || Input.GetButtonDown("Interact"))
                    {
                        notificationText.text = "";
                        door.Open();
                        //Player can't interact with door when it is already open
                        door.tag = "Untagged";
                    }
                }
                else if (door.requiresKey)
                {
                    if(door.type == Door_DR.DOOR_TYPE.COLOUR_MATCHING)
                    {
                            if(!colourMatch.isDoorInteractedWith[0])
                            {
                                colourMatch.isDoorInteractedWith[0] = true;
                            notificationText.text = "It's locked. I should check my journal.";
                            }
                            if(!colourMatch.isDoorInteractedWith[1] && colourMatch.hasKeyPart2)
                            {
                                colourMatch.isDoorInteractedWith[1] = true;
                                notificationText.text = "Press E to open";
                                door.unlocked = true;
                                 if (Input.GetKeyDown(InteractKey) || Input.GetButtonDown("Interact"))
                                 { 
                                     notificationText.text = "";
                                     door.Open();
                                      door.tag = "Untagged";
                                 }
                            }
                           
                    }
                }
                else
                {
                    notificationText.text = "How can I unlock this?";

                }
            }
            else if (hit.transform.tag == "Paper")
            {
                notificationText.text = "Press E to read";

                if (Input.GetKeyDown(InteractKey) || Input.GetButtonDown("Interact"))
                {
                    Paper_DR paperItem = hit.transform.GetComponent<Paper_DR>();
                    notificationText.text = "";
                    //Set paper text and background according to the object
                    paperText.text = paperItem.text;
                    paperBackground.sprite = paperItem.background;
                    paper.SetActive(true);
                    //if note is in the safe, let safe know
                    if(paperItem.nameOfNote == Paper_DR.NOTE_NAME.KEY_PAD_DOCUMENT)
                    {
                        keypad.playerInteractsWithDoc = true;
                    }
                }
            }
            else if (hit.transform.tag == "Fusebox")
            {
                notificationText.text = "Press E to open the fuse box";

                if (Input.GetKeyDown(InteractKey) || Input.GetButtonDown("Interact"))
                {
                    fuseboxUI.GetComponent<Fusebox_CW>().OpenFusebox();
                }
            }
            else if (hit.transform.tag == "Monitor")
            {
                if (!zoomedIn)
                {
                    notificationText.text = "Press E to zoom in";
                    if (Input.GetKeyDown(InteractKey) || Input.GetButtonDown("Interact"))
                    {
                        playerCamera.transform.LookAt(hit.transform);
                        playerCamera.fieldOfView = zoomedFOV;
                        zoomedIn = true;
                    }
                }
                else
                {
                    notificationText.text = "Press E to zoom out";

                    if (Input.GetKeyDown(InteractKey) || Input.GetButtonDown("Interact"))
                    {
                        zoomedIn = false;
                        playerCamera.fieldOfView = defaultFOV;
                    }
                }

            }
            else if (hit.transform.tag == "ChessPiece")
            {
                notificationText.text = "Press E to rotate the " + hit.transform.name;

                if (Input.GetKeyDown(InteractKey) || Input.GetButtonDown("Interact"))
                {
                    //Rotate 90 degrees in y axis
                    hit.transform.Rotate(0, 0, 90);
                }
            }
            else if (hit.transform.tag == "WaterBowl")
            {
                notificationText.text = "Press E to take a coin";

                if (Input.GetKeyDown(InteractKey))
                {
                    WaterBowl_DR waterBowl = hit.transform.GetComponent<WaterBowl_DR>();

                    if (waterBowl.GetBaronActive())
                    {
                        if (waterBowl.RemoveCoin())
                        {
                            numberCoinsCollected++;
                            waterBowl.ResetBaron();
                            Debug.Log("The player took a coin. They now have " + numberCoinsCollected + " coins");
                        }
                        else
                        {
                            Debug.Log("Player has lost by trying to take a coin when there weren't any in the bowl");
                            //TODO: There wasn't a coin for the player to take so they lose the game
                        }
                    }
                    else
                    {
                        Debug.Log("Player has lost by trying to take a coin when the baron wasn't present");
                        //TODO: Player tried to take a coin when water wasn't moving (baron wasn't present) so they lose the game
                    }

                }
            }
            else if (hit.transform.tag == "MovableWeight")
            {
                notificationText.text = "Press " + InteractKey.ToString() + " to use the weight";

                if (Input.GetKeyDown(InteractKey))
                {
                    WeightScript_AG weight = hit.transform.GetComponent<WeightScript_AG>();
                    weight.MoveWeight();
                }
            }
            else if (hit.transform.tag == "Candles")
            {
                notificationText.text = "Press " + InteractKey.ToString() + " to blow out";
                if (Input.GetKeyDown(InteractKey))
                {
                    CandleScript_AG candleScript = hit.transform.GetComponent<CandleScript_AG>();
                    candleScript.BlowOut();
                }
            }
            else if (hit.transform.tag == "PC")
            {
                notificationText.text = "Press E to open the PC";

                if (Input.GetKeyDown(InteractKey) || Input.GetButtonDown("Interact"))
                {
                    correctOrderUI.GetComponent<CorrectOrder_CW>().OpenPC();
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

    /*
     * Play the good or bad cinematic after the player blows the candles out according to if they've won or not
     */
    public void EndGameCheck()
    {
        if (numberCoinsCollected == waterBowl.numberOfCoins)
        {
            Debug.Log("Player has won");
        } else
        {
            Debug.Log("Player has lost");
        }
    }
}