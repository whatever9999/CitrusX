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
 */
using UnityEngine;
using UnityEngine.UI;

public class Interact_HR : MonoBehaviour
{
    public int rayRange = 5;
    public KeyCode InteractKey = KeyCode.E;

    private RaycastHit hit;
    private Text notificationText;
    private Journal_DR journal;
    private KeypadUI_DR keypad;
    private GameObject paper;
    private Text paperText;
    private Image paperBackground;

    private void Awake()
    {
        paper = GameObject.Find("PaperUI");
        paperText = paper.GetComponentInChildren<Text>();
        paperBackground = paper.GetComponent<Image>();
        keypad = GameObject.Find("KeypadUI").GetComponent<KeypadUI_DR>();
        notificationText = GameObject.Find("NotificationText").GetComponent<Text>();
        journal = GameObject.Find("FPSController").GetComponent<Journal_DR>();
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
                if (!keypadItem.door.GetUnlocked())
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

                if(door.GetUnlocked())
                {
                    notificationText.text = "Press E to open";

                    if (Input.GetKeyDown(InteractKey))
                    {
                        notificationText.text = "";
                        door.Open();
                        //Player can't interact with door when it is open
                        door.tag = "Untagged";
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
                    paperText.text = paperItem.text;
                    paperBackground.sprite = paperItem.background;
                    paper.SetActive(true);
                }
            }
        }
        else
        {
            notificationText.text = "";
        }
    }
}
