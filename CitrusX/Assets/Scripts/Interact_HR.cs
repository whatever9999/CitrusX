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
 * Dominique (Changes) 25/02/2020
 * Door now opens AND closes!
 * 
 * Chase(Changes) 26/2/2020
 * Added monitor interaction ifs for voiceovers and edited colour-matching door segment, added segments for PC, scales and indepth paper interaction for
 * voiceovers
 * 
 * Dominique (Changes) 03/03/2020
 * Table items being put down now make a call to the animation manager
 * Candles script is gotten as the parent of the candle selected as there are multiple candles now
 * 
 * Chase (Changes) 2/3/2020
 * Added interaction for paintings, box and added a bool for paper closure
 * 
 * Chase (Changes) 4/3/2020
 * Added interaction with hidden mech and correct order doors, also added regions to tidy up
 * 
 * Chase (Changes) 11/3/2020
 * Added interaction for symbols of scarcity
 * 
 * Dominique (Changes) 18/03/2020
 * Chess pieces now rotate themselves
 * 
 * Chase (Changes) 18/3/2020
 * Added link to cinematics for the end puzzle. Also created if barriers for certain objects to ensure they can only be interacted with at the correct moment
 * to remove issues with puzzles being played in the wrong order
 */

/**
 * \class Interact_HR
 * 
 * \brief Placed on the player, this class handles interactions using tags.
 * 
 * This class is placed on the player so that they may interact with items according to their tag.
 * It does this using raycasting. If the player is looking at an interactable object the class changes the material of that object so it has a glow shader.
 * The class also contains a record of how many coins the player has collected from the water bowl and has a check to see if they have all of the coins (this can be used to see if the game is over).
 * 
 * \author Hugo
 * 
 * \date Last Modified: 04/03/2020
 */
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Interact_HR : MonoBehaviour
{
    #region ESEENTIAL_VARS
    public const int zoomedFOV = 20;
    public const int defaultFOV = 60;
    public int rayRange = 6;
    public KeyCode InteractKey = KeyCode.E;
    public Material outlineMaterial;
    private Cinematics_DR cinematics;

    private List<Material> matArray;
    private Material[] originalMaterials;
    private MeshRenderer targetRenderer;
    private MeshRenderer currRenderer;
    private bool zoomedIn = false;
    private RaycastHit hit;
    private Text notificationText;
    #endregion
    #region REFERENCES_TO_OBJS
    private Journal_DR journal;
    private KeypadUI_DR keypad;
    private GameObject paper;
    private GameObject fuseboxUI;
    private Text paperText;
    private Image paperBackground;
    private Camera playerCamera;
    [HideInInspector]
    public int numberCoinsCollected;
    private GameObject correctOrderUI;
    private Inventory_HR inventoryManager;
    private WaterBowl_DR waterBowl;
    private Subtitles_HR subtitles;
    private ScalesPuzzleScript_AG scales;
    private Baron_DR baron;
    internal bool paperIsClosed = false;
    HiddenMech_CW hiddenMech;
    private GameObject pawn;
    private IdleVoiceover_CW idleVos;
    #endregion
    #region VARS_FOR_PUZZLES
    private ColourMatchingPuzzle_CW colourMatch;
    private SetUpRitual_CW ritual;
    #endregion

    ///<summary>
    ///Initialisation of variables
    ///</summary>
    private void Awake()
    {
        #region INITIALISATION
        inventoryManager = GetComponent<Inventory_HR>();
        paper = GameObject.Find("PaperUI");
        fuseboxUI = GameObject.Find("FuseboxUI");
        paperText = paper.GetComponentInChildren<Text>();
        paperBackground = paper.GetComponent<Image>();
        keypad = GameObject.Find("KeypadUI").GetComponent<KeypadUI_DR>();
        notificationText = GameObject.Find("NotificationText").GetComponent<Text>();
        journal = Journal_DR.instance;
        playerCamera = GetComponent<Camera>();
        correctOrderUI = GameObject.Find("CorrectOrderUI");
        waterBowl = GameObject.Find("WaterBowl").GetComponent<WaterBowl_DR>();
        colourMatch = GameObject.Find("Upstairs Bathroom Door").GetComponent<ColourMatchingPuzzle_CW>();
        subtitles = GetComponent<Subtitles_HR>();
        scales = GameObject.Find("Scales").GetComponent<ScalesPuzzleScript_AG>();
        ritual = GetComponent<SetUpRitual_CW>();
        baron = GameObject.Find("Baron").GetComponent<Baron_DR>();
        cinematics = GameObject.Find("Cinematics").GetComponent<Cinematics_DR>();
        hiddenMech = GameObject.Find("Painting_AG").GetComponent<HiddenMech_CW>();
        pawn = GameObject.Find("Pawn");
        idleVos = GameObject.Find("Managers").GetComponent<IdleVoiceover_CW>();

        #endregion
    }

    ///<summary>
    ///Update casts a raycast from the player. If this hits an interactable then the object is given a glow shader and the player is permitted to interact with it using the interact buttons.
    ///</summary>
    void Update()
    {
        //Reset text
        notificationText.text = "";
        //RayCast Forward see if the player is in range of anything
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayRange))
        {
            //Get the current Renderer for the object
            if (hit.transform.gameObject.GetComponent<MeshRenderer>())
            {
                currRenderer = hit.transform.gameObject.GetComponent<MeshRenderer>();
            }

            //If the object is not the same as the previous object then revert to the original material
            //and change the new object to the outline material
            if (targetRenderer && currRenderer.materials != targetRenderer.materials && hit.transform.tag != "Keypad")
            {

                targetRenderer.materials = originalMaterials;
                originalMaterials = currRenderer.materials;
                matArray = new List<Material>(currRenderer.materials);
                matArray.Add(outlineMaterial);
                currRenderer.materials = matArray.ToArray();
                targetRenderer = currRenderer;
            }
            else
            {
                originalMaterials = currRenderer.materials;
                matArray = new List<Material>(currRenderer.materials);
                matArray.Add(outlineMaterial);
                currRenderer.materials = matArray.ToArray();
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
                    idleVos.interactedWith = true;
                    idleVos.interactedWith = false;
                    //inventoryManager.AddItem(Inventory_HR.Names.WaterJug);
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
                        if (GetComponent<SetUpRitual_CW>().ritualSteps[0])
                        {
                            notificationText.text = "Press E to put down your items";
                            //If he presses the key then pick up the object
                            if (Input.GetKeyDown(InteractKey) || Input.GetButtonDown("Interact"))
                            {
                                idleVos.interactedWith = true;
                                idleVos.interactedWith = false;
                                AnimationManager_DR.instance.TriggerAnimation(AnimationManager_DR.AnimationName.PLACERITUALITEMS);
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
                        if (GetComponent<SetUpRitual_CW>().ritualSteps[4])
                        {
                            notificationText.text = "Press E to put down your items";
                            //If he presses the key then pick up the object
                            if (Input.GetKeyDown(InteractKey) || Input.GetButtonDown("Interact"))
                            {
                                idleVos.interactedWith = true;
                                idleVos.interactedWith = false;
                                AnimationManager_DR.instance.TriggerAnimation(AnimationManager_DR.AnimationName.PLACEJEWELLERY);
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
                                idleVos.interactedWith = true;
                                idleVos.interactedWith = false;
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
                if(GameTesting_CW.instance.arePuzzlesDone[2])
                {
                    //Get the keypad we're looking at
                    KeypadItem_DR keypadItem = hit.transform.gameObject.GetComponent<KeypadItem_DR>();
                    //If the door isn't unlocked yet then open the keypad UI
                    if (!keypadItem.door.unlocked)
                    {
                        notificationText.text = "Press E to use the keypad";

                        if (Input.GetKeyDown(InteractKey) || Input.GetButtonDown("Interact"))
                        {
                            idleVos.interactedWith = true;
                            idleVos.interactedWith = false;
                            //Open the keypad UI using this keypad (makes sure the password can be changed between different keypads)
                            keypad.OpenKeypad(keypadItem);

                            //Hide the notification text when the keypad is open
                            notificationText.text = "";
                        }
                    }
                }

            }
            else if (hit.transform.tag == "Door")
            {
                Door_DR door = hit.transform.gameObject.GetComponent<Door_DR>();

                if (door.unlocked)
                {
                    notificationText.text = "Press E to open the " + hit.transform.parent.name;

                    if (Input.GetKeyDown(InteractKey) || Input.GetButtonDown("Interact"))
                    {
                        idleVos.interactedWith = true;
                        idleVos.interactedWith = false;
                        notificationText.text = "";
                        door.ToggleOpen();
                    }
                }
                else if (door.requiresKey)
                {
                    notificationText.text = "It's locked. I should check my journal.";
                    if(GameTesting_CW.instance.arePuzzlesDone[1])
                    {
                        if (door.type == Door_DR.DOOR_TYPE.COLOUR_MATCHING)
                        {
                            notificationText.text = "Press E to try handle";

                            if (Input.GetKeyDown(InteractKey) || Input.GetButtonDown("Interact"))
                            {
                                idleVos.interactedWith = true;
                                idleVos.interactedWith = false;
                                if (colourMatch.isActive && !colourMatch.isDoorInteractedWith[0])
                                {
                                    colourMatch.isDoorInteractedWith[0] = true;
                                    journal.TickOffTask("Check bathroom door");
                                }
                                else if (!colourMatch.hasKeyPart2)
                                {
                                    notificationText.text = "It's locked. I should check my journal.";
                                }
                                else if (!colourMatch.isDoorInteractedWith[1] && colourMatch.hasKeyPart2)
                                {
                                    notificationText.text = "Press E to open door";
                                    colourMatch.isDoorInteractedWith[1] = true;
                                    door.unlocked = true;
                                    if (Input.GetKeyDown(InteractKey) || Input.GetButtonDown("Interact"))
                                    {
                                        idleVos.interactedWith = true;
                                        idleVos.interactedWith = false;
                                        notificationText.text = "";

                                        journal.AddJournalLog("What was that on my screen? That couldn’t have been what I thought it was…could it?");
                                        journal.ChangeTasks(new string[] { "Return to ritual" });
                                        door.ToggleOpen();
                                        door.tag = "Untagged";
                                    }
                                }
                            }
                        }
                    if(GameTesting_CW.instance.arePuzzlesDone[6])
                        {
                            if (door.type == Door_DR.DOOR_TYPE.HIDDEN_MECH)
                            {
                                notificationText.text = "It's locked. I should check my journal.";

                                if (GameTesting_CW.instance.arePuzzlesDone[7])
                                {
                                    notificationText.text = "Press E to open";

                                    if (Input.GetKeyDown(InteractKey) || Input.GetButtonDown("Interact"))
                                    {
                                        if (Input.GetKeyDown(InteractKey) || Input.GetButtonDown("Interact"))
                                        {
                                            idleVos.interactedWith = true;
                                            idleVos.interactedWith = false;
                                            notificationText.text = "";
                                            door.ToggleOpen();
                                            door.tag = "Untagged";
                                        }
                                    }
                                }

                            }
                        }
                        if(GameTesting_CW.instance.arePuzzlesDone[7])
                        {
                            if (door.type == Door_DR.DOOR_TYPE.CORRECT_ORDER)
                            {
                                notificationText.text = "It's locked. I should check my journal.";

                                if (GameTesting_CW.instance.arePuzzlesDone[8])
                                {
                                    notificationText.text = "Press E to open";
                                    if (Input.GetKeyDown(InteractKey) || Input.GetButtonDown("Interact"))
                                    {
                                        idleVos.interactedWith = true;
                                        idleVos.interactedWith = false;
                                        notificationText.text = "";
                                        door.ToggleOpen();
                                        door.tag = "Untagged";
                                    }

                                }

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
                    idleVos.interactedWith = true;
                    idleVos.interactedWith = false;
                    Paper_DR paperItem = hit.transform.GetComponent<Paper_DR>();
                    notificationText.text = "";
                    //Set paper text and background according to the object
                    paperText.text = paperItem.text;
                    paperText.fontSize = paperItem.textSize;
                    paperBackground.sprite = paperItem.background;
                    paper.SetActive(true);
                    //if note is in the safe, let safe know
                    #region PAPER_TYPES_VOICEOVERS
                    if (paperItem.nameOfNote == Paper_DR.NOTE_NAME.KEY_PAD_DOCUMENT && !paperItem.hasBeenRead )
                    {
                        subtitles.PlayAudio(Subtitles_HR.ID.P4_LINE7);
                        journal.TickOffTask("Read note");
                        journal.AddJournalLog("This baron seems like he was quite the character…weird though, why would this be locked away?");
                        journal.ChangeTasks(new string[] { "Return to ritual" });
                        paperItem.hasBeenRead = true;
                    }
                    else if (paperItem.nameOfNote == Paper_DR.NOTE_NAME.CHESSBOARD_INSTRUCT && !paperItem.hasBeenRead)
                    {
                        if(GameTesting_CW.instance.arePuzzlesDone[4])
                        {
                            subtitles.PlayAudio(Subtitles_HR.ID.P6_LINE3);
                            journal.TickOffTask("Read book");
                            journal.AddJournalLog("The pawn? The Queen? This looks like a complex riddle.");
                            journal.ChangeTasks(new string[] { "Pawn" });
                            pawn.SetActive(true);
                            paperItem.hasBeenRead = true;
                        }
                        
                    }
                    else if (paperItem.nameOfNote == Paper_DR.NOTE_NAME.CHESSBOARD_DOC && !paperItem.hasBeenRead && !paperIsClosed)
                    {
                        subtitles.PlayAudio(Subtitles_HR.ID.P6_LINE6);
                        journal.AddJournalLog("The Baron seems to have ruined a lot of people’s lives…");
                        journal.TickOffTask("Read note");
                        journal.ChangeTasks(new string[] { "Return to ritual" });
                        paperItem.hasBeenRead = true;
                    }
                    else if (paperItem.nameOfNote == Paper_DR.NOTE_NAME.PHOTOGRAPH_REVERSE && !paperItem.hasBeenRead && !paperIsClosed)
                    {
                        subtitles.PlayAudio(Subtitles_HR.ID.P7_LINE5);
                        journal.AddJournalLog("A photograph of a family…the Baron’s family.");
                        journal.ChangeTasks(new string[] { "Return to ritual" });
                        paperItem.hasBeenRead = true;
                    }
                    else if (paperItem.nameOfNote == Paper_DR.NOTE_NAME.DEATH_CERTIFICATE && !paperItem.hasBeenRead &&!paperIsClosed)
                    {
                        subtitles.PlayAudio(Subtitles_HR.ID.P8_LINE7);
                        GameTesting_CW.instance.arePuzzlesDone[7] = true;
                        journal.AddJournalLog("The baron reached a grizzly death it appears.");
                        journal.TickOffTask("Read note");
                        journal.ChangeTasks(new string[] { "Return to ritual" });
                        paperItem.hasBeenRead = true;
                    }
                    else if (paperItem.nameOfNote == Paper_DR.NOTE_NAME.DEATH_CERTIFICATE && paperIsClosed)
                    {
                        subtitles.PlayAudio(Subtitles_HR.ID.P8_LINE8);
                    }
                    else if (paperItem.nameOfNote == Paper_DR.NOTE_NAME.CHESSBOARD_DOC && paperIsClosed)
                    {
                        subtitles.PlayAudio(Subtitles_HR.ID.P6_LINE7);
                    }
                    else if(paperItem.nameOfNote == Paper_DR.NOTE_NAME.KEY_PAD_NOTE && !paperItem.hasBeenRead && !paperIsClosed)
                    {
                        journal.TickOffTask("Find clue");
                        journal.AddJournalLog("Maths, birthdays and items – there must be something real important in this safe.");
                        journal.ChangeTasks(new string[] { "Solve the password" });
;                    }
                   
                    #endregion
                }
            }
            else if (hit.transform.tag == "Fusebox")
            {
                if(InitiatePuzzles_CW.instance.monitorInteractionsUsed[0])
                {
                    notificationText.text = "Press E to open the fuse box";

                    if (Input.GetKeyDown(InteractKey) || Input.GetButtonDown("Interact"))
                    {
                        idleVos.interactedWith = true;
                        idleVos.interactedWith = false;
                        fuseboxUI.GetComponent<Fusebox_CW>().OpenFusebox();
                    }
                }
               
            }
            else if (hit.transform.tag == "Monitor")
            {
                if (!zoomedIn)
                {
                    notificationText.text = "Press E to zoom in";
                    if (Input.GetKeyDown(InteractKey) || Input.GetButtonDown("Interact"))
                    {
                        idleVos.interactedWith = true;
                        idleVos.interactedWith = false;
                        playerCamera.transform.LookAt(hit.transform);
                        playerCamera.fieldOfView = zoomedFOV;
                        zoomedIn = true;
                        #region MONITOR_INTERACTION_IFS
                        if(!InitiatePuzzles_CW.instance.monitorInteractions[9] && ritual.ritualSteps[1])
                        {
                            journal.TickOffTask("Check the monitor");
                            journal.AddJournalLog("The cameras seem to link to every room…this could be useful.");
                            InitiatePuzzles_CW.instance.monitorInteractions[9] = true;
                        }
                        else if (!InitiatePuzzles_CW.instance.monitorInteractions[0] && GameTesting_CW.instance.arePuzzlesDone[0])
                        {
                            InitiatePuzzles_CW.instance.monitorInteractions[0] = true;
                        }
                        else if (!InitiatePuzzles_CW.instance.monitorInteractions[1] && GameTesting_CW.instance.arePuzzlesDone[1])
                        {
                            InitiatePuzzles_CW.instance.monitorInteractions[1] = true;
                        }
                        else if (!InitiatePuzzles_CW.instance.monitorInteractions[2] && GameTesting_CW.instance.arePuzzlesDone[2])
                        {
                            InitiatePuzzles_CW.instance.monitorInteractions[2] = true;
                        }
                        else if (!InitiatePuzzles_CW.instance.monitorInteractions[3] && GameTesting_CW.instance.arePuzzlesDone[3])
                        {
                            InitiatePuzzles_CW.instance.monitorInteractions[3] = true;
                        }
                        else if (!InitiatePuzzles_CW.instance.monitorInteractions[4] && GameTesting_CW.instance.arePuzzlesDone[4])
                        {
                            InitiatePuzzles_CW.instance.monitorInteractions[4] = true;
                        }
                        else if (!InitiatePuzzles_CW.instance.monitorInteractions[5] && GameTesting_CW.instance.arePuzzlesDone[5])
                        {
                            InitiatePuzzles_CW.instance.monitorInteractions[5] = true;
                        }
                        else if (!InitiatePuzzles_CW.instance.monitorInteractions[6] && GameTesting_CW.instance.arePuzzlesDone[6])
                        {
                            InitiatePuzzles_CW.instance.monitorInteractions[6] = true;
                        }
                        else if (!InitiatePuzzles_CW.instance.monitorInteractions[7] && GameTesting_CW.instance.arePuzzlesDone[7])
                        {
                            InitiatePuzzles_CW.instance.monitorInteractions[7] = true;
                        }
                        else if (!InitiatePuzzles_CW.instance.monitorInteractions[8] && GameTesting_CW.instance.arePuzzlesDone[8])
                        {
                            InitiatePuzzles_CW.instance.monitorInteractions[8] = true;
                        }
                        #endregion
                    }

                }
                else
                {
                    notificationText.text = "Press E to zoom out";

                    if (Input.GetKeyDown(InteractKey) || Input.GetButtonDown("Interact"))
                    {
                        idleVos.interactedWith = true;
                        idleVos.interactedWith = false;
                        if (!ritual.ritualSteps[1] && ritual.ritualSteps[0])
                        {
                            subtitles.PlayAudio(Subtitles_HR.ID.P1_LINE9);
                        }
                        zoomedIn = false;
                        playerCamera.fieldOfView = defaultFOV;
                    }
                }

            }           
            else if (hit.transform.tag == "ChessPiece")
            {
                if(GameTesting_CW.instance.arePuzzlesDone[4])
                {
                    notificationText.text = "Press E to rotate the " + hit.transform.name;

                    if (Input.GetKeyDown(InteractKey) || Input.GetButtonDown("Interact"))
                    {
                        idleVos.interactedWith = true;
                        idleVos.interactedWith = false;
                        hit.transform.GetComponent<ChessPiece_DR>().Rotate();
                    }
                }
                
            }
            else if (hit.transform.tag == "WaterBowl")
            {
                if(GameTesting_CW.instance.arePuzzlesDone[0])
                {
                    notificationText.text = "Press E to take a coin";
                    if (GameTesting_CW.instance.arePuzzlesDone[8])
                    {
                        subtitles.PlayAudio(Subtitles_HR.ID.P10_LINE2);
                    }

                    if (Input.GetKeyDown(InteractKey))
                    {
                        WaterBowl_DR waterBowl = hit.transform.GetComponent<WaterBowl_DR>();

                        if (baron.isActiveAndEnabled)
                        {
                            if (waterBowl.RemoveCoin())
                            {
                                numberCoinsCollected++;
                                baron.gameObject.SetActive(false);
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
                
            }
            else if (hit.transform.tag == "Scales")
            {
                if(GameTesting_CW.instance.arePuzzlesDone[3])
                {
                    notificationText.text = "Press E to observe scales";
                    bool interactedWith = false;
                    
                    if (Input.GetKeyDown(InteractKey) && !interactedWith)
                    {
                        idleVos.interactedWith = true;
                        idleVos.interactedWith = false;
                        subtitles.PlayAudio(Subtitles_HR.ID.P5_LINE2);
                        journal.TickOffTask("Check the scales");
                        journal.AddJournalLog("I could use items from the pantry to balance the scales.");
                        journal.ChangeTasks(new string[] { "Balance scales" });
                        interactedWith = true;
                    }
                }
                
            }
            else if (hit.transform.tag == "Candles")
            {
                if(GameTesting_CW.instance.arePuzzlesDone[8])
                {
                    notificationText.text = "Press " + InteractKey.ToString() + " to blow out";
                    if (Input.GetKeyDown(InteractKey))
                    {
                        idleVos.interactedWith = true;
                        idleVos.interactedWith = false;
                        hit.transform.parent.GetComponent<CandleScript_AG>().BlowOut();
                    }
                }
               
            }
            else if (hit.transform.tag == "PC")
            {
                if(GameTesting_CW.instance.arePuzzlesDone[7])
                {
                    notificationText.text = "Press E to open the PC";

                    if (Input.GetKeyDown(InteractKey) || Input.GetButtonDown("Interact"))
                    {
                        idleVos.interactedWith = true;
                        idleVos.interactedWith = false;
                        subtitles.PlayAudio(Subtitles_HR.ID.P9_LINE3);
                        journal.TickOffTask("Find a way out");
                        journal.ChangeTasks(new string[] { "Solve puzzle" });
                        correctOrderUI.GetComponent<CorrectOrder_CW>().OpenPC();
                    }
                }
              
            }
            else if (hit.transform.tag == "Box")
            {
                notificationText.text = "Press E to open the Box";
                bool hasBeenOpened = false;

                if (Input.GetKeyDown(InteractKey) || Input.GetButtonDown("Interact"))
                {
                    idleVos.interactedWith = true;
                    idleVos.interactedWith = false;
                    if (!hasBeenOpened)
                    {
                        //play box anim
                        subtitles.PlayAudio(Subtitles_HR.ID.P7_LINE5);
                        journal.ChangeTasks(new string[] { "Look at photo" });
                        hasBeenOpened = true;
                    }
                    else if (hasBeenOpened)
                    {
                        //box slam anim
                        subtitles.PlayAudio(Subtitles_HR.ID.P7_LINE6);
                    }

                }

            }
            else if (hit.transform.tag == "Painting")
            {
                if(GameTesting_CW.instance.arePuzzlesDone[6])
                {
                    notificationText.text = "Press E to look at the Painting";
                    bool hasBeenInteracted = false;

                    if (Input.GetKeyDown(InteractKey) || Input.GetButtonDown("Interact"))
                    {
                        idleVos.interactedWith = true;
                        idleVos.interactedWith = false;
                        if (!hasBeenInteracted)
                        {
                            //play box anim
                            subtitles.PlayAudio(Subtitles_HR.ID.P8_LINE5);
                            journal.TickOffTask("Find clue");
                            journal.AddJournalLog("I need to find the red accounting book.");
                            journal.ChangeTasks(new string[] { "Find correct book" });
                            hasBeenInteracted = true;
                        }
                    }
                }
               
            }
            else if (hit.transform.tag == "Book")
            {
                if(GameTesting_CW.instance.arePuzzlesDone[6])
                {
                    notificationText.text = "Press E to interact with the book";
                    Book_CW book = hit.transform.GetComponent<Book_CW>();

                    if (Input.GetKeyDown(InteractKey) || Input.GetButtonDown("Interact"))
                    {
                        idleVos.interactedWith = true;
                        idleVos.interactedWith = false;
                        if (book.type == Book_CW.BOOK_TYPE.HIDDEN_MECH_BOOK)
                        {
                            journal.TickOffTask("Find correct book");
                            subtitles.PlayAudio(Subtitles_HR.ID.P8_LINE6);
                            journal.ChangeTasks(new string[] { "Read note" });
                            hiddenMech.complete = true;
                            GameTesting_CW.instance.arePuzzlesDone[7] = true;
                        }

                    }
                    else
                    {
                        playerCamera.fieldOfView = defaultFOV;
                    }
                }
            

            }
            else if(hit.transform.tag == "Pawn")
            {
                if (Input.GetKeyDown(InteractKey) || Input.GetButtonDown("Interact"))
                {
                    idleVos.interactedWith = true;
                    idleVos.interactedWith = false;
                    //inventoryManager.AddItem(Inventory_HR.Names.WaterJug);

                    //  inventoryManager.AddItem(Inventory_HR.Names.WaterJug);

                    hit.transform.gameObject.SetActive(false);
                    notificationText.text = "";
                    journal.TickOffTask("Find the Pawn");
                    journal.ChangeTasks(new string[] { "Solve the puzzle" });
                }
            }
            else if (hit.transform.tag == "SymbolOfScarcity")
            {
                notificationText.text = "Press E to interact with the " + hit.transform.name;

                if (Input.GetKeyDown(InteractKey) || Input.GetButtonDown("Interact"))
                {
                    idleVos.interactedWith = true;
                    idleVos.interactedWith = false;
                    subtitles.PlayAudio(Subtitles_HR.ID.A_LINE6);
                }
            }
            else
            {
                zoomedIn = false;
                playerCamera.fieldOfView = defaultFOV;
                notificationText.text = "";
            }
        }
        else if (targetRenderer)
        {
            targetRenderer.materials = originalMaterials;
            targetRenderer = null;
            zoomedIn = false;
            playerCamera.fieldOfView = defaultFOV;
            notificationText.text = "";
        }
        

        /*
         * Play the good or bad cinematic after the player blows the candles out according to if they've won or not
         */
       
    }

    ///<summary>
    ///Check if the player has the total amount of coins required. If not, they have lost. If they do, they have won.
    ///</summary>
    public void EndGameCheck()
    {
        Color blackOutColour;
        blackOutColour.r = 0;
        blackOutColour.g = 0;
        blackOutColour.b = 0;
        blackOutColour.a = 0;
        if (numberCoinsCollected == waterBowl.numberOfCoins)
        {
          //  GameObject.Find("BlackoutScreen").GetComponent<Image>().color = blackOutColour;
            cinematics.PlayEndCinematic(Cinematics_DR.END_CINEMATICS.GOOD);
        }
        else
        {
          //  GameObject.Find("BlackoutScreen").GetComponent<Image>().color = blackOutColour;
            cinematics.PlayEndCinematic(Cinematics_DR.END_CINEMATICS.BAD);
        }
    }
}