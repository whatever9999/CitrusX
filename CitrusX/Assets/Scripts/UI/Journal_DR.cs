/*
 * Dominique
 * 
 * Opens and closes the journal on the UI using keycodes
 * From another script you can use the Journal class to add a log to the journal, change the tasks available, tick off tasks and check if all the tasks are complete or not
 * 
 * Example use:
 * AddJournalLog("Well that went great...");
 * ChangeTasks(new string[] { "Pineapple pie", "Rice" });
 * TickOffTask(0);
 * AreTasksComplete();
 * 
 * 
 * Hugo (Changes) 04/02/2020'
 * 
 * Getcomponents and Gameobjects.Find moved to awake instead of start
 * 
 * 
 * Dominique (Changes) 05/02/2020
 * 
 * Changed foreach loops into for loops
 * Made new logs appear at the top of the journal instead of the bottom (easier to read new logs for player)
 * Made sure that capitilisation doesn't matter on tasks put in as strings to be ticked off
 * 
 * Hugo (Changes) 10/02/2020'
 * 
 * Added controller functionality
 * 
 * Chase (Changes) 11/2/2020
 * Moved original journal tasks to initiate puzzles script
 * 
 * Alex (changes) 08/04/2020
 * Added to code to show cursor when the journal is opened
 * 
 * Chase (Changes) 08/04/2020
 * Fixed journal cursor issues
 */

/**
* \class Journal_DR
* 
* \brief Shows a log of the players activity in a scrollRect and has a list of tasks for the player to complete.
* 
* Use AddJournalLog(text) to add the next to the top of the scrollRect that shows the journal log.
* Use ChangeTasks(tasks) to pass in up to 5 strings in an array to be used as the new tasks. If under 5 are passed in the rest of the tasks will be made blank.
* Use TickOffTask(taskNumber) or TickOffTask(task) to tick off the task that corresponds to that number in the list or has the text shown in the task variable. (A literal tick appears next to the task and this is used later to check if the tasks are complete)
* Use AreTasksComplete() to check if all tasks have been ticked off. Returns a bool for yes or no.
* 
* \author Dominique
* 
* \date Last Modified: 11/02/2020
*/

using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Journal_DR : MonoBehaviour
{
    public static Journal_DR instance;

    public KeyCode journalOpenKey = KeyCode.J;
    public KeyCode[] journalCloseKeys = { KeyCode.J, KeyCode.Escape };

    private GameObject journal;

    private GameObject journalTaskList;
    private Text[] journalTasks;

    private RectTransform journalLogContentBox;
    private RectTransform journalLogTextBox;
    private Text journalLogText;

    private FirstPersonController firstPersonController;

    //This is an underestimate so that more space would be given as opposed to less (since this would mean the player couldn't read the entry)
    private const float numberOfCharactersPerLine = 11;

    /// <summary>
    /// Inititalise variables and ensure the journal GO is disabled
    /// </summary>
    private void Awake()
    {
        instance = this;

        journal = GameObject.Find("JournalBackground");
        journalTaskList = GameObject.Find("JournalTasks");
        journalTasks = journalTaskList.GetComponentsInChildren<Text>();

        journalLogContentBox = GameObject.Find("JournalContentBox").GetComponent<RectTransform>();
        GameObject journalLogGO = GameObject.Find("JournalLogs");
        journalLogTextBox = journalLogGO.GetComponent<RectTransform>();
        journalLogText = journalLogGO.GetComponent<Text>();

        firstPersonController = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
    }

    private void Start()
    {
        journal.SetActive(false);
    }

    /// <summary>
    /// Open and close the journal (using J)
    /// </summary>
    private void Update()
    {
        if (!journal.activeInHierarchy && Input.GetKeyDown(journalOpenKey) || Input.GetButtonDown("Journal"))
        {
            JournalOpen();
        } 
        else if (journal.activeInHierarchy)
        {
            for(int i = 0; i < journalCloseKeys.Length; i++)
            {
                if (Input.GetKeyDown(journalCloseKeys[i]) || Input.GetButtonDown("Cancel"))
                {
                    JournalClose();
                }
            }
        }
    }

    /// <summary>
    /// Calculate the size that the contents and text box will need to increase to show all of the text then add the text to the bo including new lines.
    /// </summary>
    /// <param name="text - the log text"></param>
    public void AddJournalLog(string text)
    {
        int numLines = (int)Mathf.Ceil(text.Length / numberOfCharactersPerLine);

        //Increase the size of the content and text box so they fit the text
        Vector2 journalLogSize = journalLogTextBox.sizeDelta;
        journalLogSize.y += (journalLogText.fontSize * numLines);
        journalLogTextBox.sizeDelta = journalLogSize;

        //Make sure the text box width doesn't affect the content box width (otherwise text moves to the right)
        journalLogSize.x = 0;
        journalLogContentBox.sizeDelta = journalLogSize;

        //Add the text to the text box
        journalLogText.text = journalLogText.text + "\n\n" + text;
    }


    /// <summary>
    /// Set the tasks to those in the string array. If the array has less than 5 tasks set the rest of the task text boxes to be blank.
    /// </summary>
    /// <param name="newTasks - an array of up to 5 strings that show the tasks the player needs to carry out"></param>
    public void ChangeTasks(string[] newTasks)
    {
        for(int i = 0; i < newTasks.Length; i++)
        {
            journalTasks[i].text = newTasks[i];
        }

        //If there are fewer tasks passed in than text boxes available make sure the rest of the boxes are made empty
        for (int i = newTasks.Length; i < journalTasks.Length; i++) {
            journalTasks[i].text = "";
        }
    }

    /// <summary>
    /// Place a tick next to the taskNumberth task
    /// </summary>
    /// <param name="taskNumber - the number of the task from top to bottom, 0 indexed"></param>
    public void TickOffTask(int taskNumber)
    {
        journalTasks[taskNumber].text = journalTasks[taskNumber].text + " ✓";
    }

    /// <summary>
    /// Place a tick next to the task that matches the task string exactly
    /// </summary>
    /// <param name="task - the task text"></param>
    public void TickOffTask(string task)
    {
        //Make sure both strings are lower case so capitilisation doesn't matter
        for(int i = 0; i < journalTasks.Length; i++)
        {
            if (task.ToLower().Equals(journalTasks[i].text.ToLower()))
            {
                journalTasks[i].text += " ✓";
            }
        }
    }

    /// <summary>
    /// Go through all the tasks and check if they end with a tick (skips blank tasks)
    /// </summary>
    /// <returns>A boolean for yes or no if all the tasks are complete</returns>
    public bool AreTasksComplete()
    {
        bool complete = true;

        for(int i = 0; i < journalTasks.Length; i++)
        {
            //Check that the last character on the task is a tick as long as its not an empty task
            if (!journalTasks[i].text.Equals("") && !journalTasks[i].text[journalTasks[i].text.Length - 1].Equals('✓'))
            {
                complete = false;
                break;
            }
        }

        return complete;
    }
    private void JournalOpen()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        journal.SetActive(true);
        
        firstPersonController.enabled = false;
    }

    private void JournalClose()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        journal.SetActive(false);
        
        firstPersonController.enabled = true;
    }
}
