/* Hugo
 * 
 * What script does...
 * 
 * Dominique (Changes) 04/02/2020
 * Specified Hit and UI as private for a consistent coding standard
 * Got rid of unused imported namespaces (System.Collections and System.Collections.Generic)
 * Renamed variable (and game object in hierarchy) to NotificationText
 * Set rayRange to 5 in script
 * Optimising after 'if(Input.GetKeyDown(KeyCode.E))' -> Unnecessary if statement that checked if the Vector3 mouse position equaled a Vector2 cast of itself
 */
using UnityEngine;
using UnityEngine.UI;

public class Interact_HR : MonoBehaviour
{
    public int rayRange = 5;

    private RaycastHit hit;
    private Text notificationText;

    private void Awake()
    {
        notificationText = GameObject.Find("NotificationText").GetComponent<Text>();
    }

    void Update()
    {
        if(Physics.Raycast(transform.position, transform.forward, out hit, rayRange))
        {
            if(hit.transform.tag == "Object")
            {
                notificationText.text = "Press E to pick up";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Destroy(hit.transform.gameObject);
                    notificationText.text = "";
                }
            }
        }
    }
}
