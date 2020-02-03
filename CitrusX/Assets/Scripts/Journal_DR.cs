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
 */
using UnityEngine;
using UnityEngine.UI;

public class Journal_DR : MonoBehaviour
{
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

    private void Start()
    {
        journal = GameObject.Find("JournalBackground");
        journalTaskList = GameObject.Find("JournalTasks");
        journalTasks = journalTaskList.GetComponentsInChildren<Text>();

        journalLogContentBox = GameObject.Find("JournalContentBox").GetComponent<RectTransform>();
        GameObject journalLogGO = GameObject.Find("JournalLogs");
        journalLogTextBox = journalLogGO.GetComponent<RectTransform>();
        journalLogText = journalLogGO.GetComponent<Text>();

        journal.SetActive(false);
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
            foreach (KeyCode KC in journalCloseKeys)
            {
                if(Input.GetKeyDown(KC))
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
        journalLogText.text += "\n\n" + text;
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
        foreach(Text T in journalTasks)
        {
            if(T.text.Equals(task))
            {
                T.text += " ✓";
            }
        }
    }

    public bool AreTasksComplete()
    {
        bool complete = true;

        foreach (Text T in journalTasks)
        {
            //Check that the last character on the task is a tick as long as its not an empty task
            if (!T.text.Equals("") && !T.text[T.text.Length - 1].Equals('✓'))
            {
                complete = false;
                break;
            }
        }

        return complete;
    }
}
