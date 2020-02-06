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
 */
using UnityEngine;
using UnityEngine.UI;

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

    //This is an underestimate so that more space would be given as opposed to less (since this would mean the player couldn't read the entry)
    private const float numberOfCharactersPerLine = 14;

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

        journal.SetActive(false);

        ChangeTasks(new string[] { "Candles", "Book", "Bowl", "Water jug", "Coins" }); //Test for prototype
        AddJournalLog("I've got all the things I need in the room. I'll quickly pick them up so I can set up the game...");
    }

    /*
     * Opening and closing the journal
     */

    private void Update()
    {
        if (!journal.activeInHierarchy && Input.GetKeyDown(journalOpenKey))
        {
            journal.SetActive(true);
        } else if (journal.activeInHierarchy)
        {
            for(int i = 0; i < journalCloseKeys.Length; i++)
            {
                if (Input.GetKeyDown(journalCloseKeys[i]))
                {
                    journal.SetActive(false);
                }
            }
        }
    }

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
        journalLogText.text = text + "\n\n" + journalLogText.text; ;
    }

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

    public void TickOffTask(int taskNumber)
    {
        journalTasks[taskNumber].text = journalTasks[taskNumber].text + " ✓";
    }

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
}
