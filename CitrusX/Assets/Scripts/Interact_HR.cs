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
 */
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Interact_HR : MonoBehaviour
{
    public int rayRange = 5;
    public KeyCode InteractKey = KeyCode.E;
    

    private RaycastHit hit;
    private Text notificationText;
    private Journal_DR journal;

    private void Awake()
    {
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
                notificationText.text = "Press E to pick up";
                //If he presses the key then pick up the object
                if (Input.GetKeyDown(InteractKey))
                {
                    Destroy(hit.transform.gameObject);
                    notificationText.text = "";
                    Journal_DR.instance.TickOffTask(0); //Or Journal_DR.instance.TickOffTask("Pick up block"); Test for prototype
                }
            }

            if (hit.transform.tag == "Table")
            {
                if (journal.AreTasksComplete())
                {
                    notificationText.text = "Press E to put down your items";
                    //If he presses the key then pick up the object
                    if (Input.GetKeyDown(InteractKey))
                    {
                        hit.transform.gameObject.GetComponent<PutDown_HR>().setItemsDown();
                        notificationText.text = "";
                    }
                }
                else
                {
                    notificationText.text = "You don't have all the items";
                }

                
            }

        }
        else
        {
            notificationText.text = "";
        }
    }
}
