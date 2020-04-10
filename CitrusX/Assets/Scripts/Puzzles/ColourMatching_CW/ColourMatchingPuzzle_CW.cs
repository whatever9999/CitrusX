/*Chase Wilding
 * Colour matching script
 * This scripts main function 'Start colour Matching puzzle' can be called during game play to add it to the journal and start the process
 * when this puzzle begins

 * 
 * Dominique (Changes) 11/02/2020
 * Removed unused package imports
 * 
 * Chase (Changes) 22/2/2020
 * Added voiceover bools and comments, linked puzzle to door and fleshed
 * out the entire puzzle
 * 
 * Chase(Changes) 11/3/2020
 * Inherited from puzzlebase and tidied up

 */

/**
* \class ColourMatchingPuzzle_CW
* 
* \brief Start the colour matching puzzle and update the journal tasks and logs and subtitles accordingly
* 
* \author Chase
* 
* \date Last Modified: 22/02/2020
*/

using UnityEngine;

internal class ColourMatchingPuzzle_CW : MonoBehaviour
{
    #region VARIABLES
    internal bool[] isDoorInteractedWith = { false, false };
    internal bool hasKeyPart1 = false;
    internal bool hasKeyPart2 = false;
    private Door_DR door;
    internal bool isActive = false;
    internal Journal_DR journal;
    internal Subtitles_HR subtitles;
    internal bool[] voiceovers = { false, false, false, false, false };
    internal TriggerScript_CW ritualTrigger;
    public Door_DR workshopDoor;

    internal void SetActive(bool value) { isActive = value; }
    #endregion
    /// <summary>
    /// Inititalise variables
    /// </summary>
    private void Awake()
    {
        journal = GameObject.Find("FirstPersonCharacter").GetComponent<Journal_DR>();
        subtitles = GameObject.Find("FirstPersonCharacter").GetComponent<Subtitles_HR>();
        ritualTrigger = GameObject.Find("RitualTrigger").GetComponent<TriggerScript_CW>();
        door = GetComponentInChildren<Door_DR>();
    }
    /// <summary>
    /// According to the state of the puzzle play the right subtitles and update the journal
    /// </summary>
    private void Update()
    {
        if (isActive)
        {
            if (!voiceovers[0])
            {
                if(door.isOpen) door.ToggleOpen();
                door.unlocked = false;
                door.requiresKey = true;
                if (!workshopDoor.isOpen) workshopDoor.ToggleOpen();
                workshopDoor.unlocked = true;
                workshopDoor.requiresKey = false;
                voiceovers[0] = true;
            }
            else if (isDoorInteractedWith[0] && !voiceovers[1])
            {
                subtitles.PlayAudio(Subtitles_HR.ID.P3_LINE3);
                journal.AddJournalLog("It needs a key? Where can I find a key?");
                journal.ChangeTasks(new string[] { "Bathroom Key" });
                voiceovers[1] = true;
            }
            else if (isDoorInteractedWith[0] && !hasKeyPart1 && voiceovers[1])
            {
                if (journal.AreTasksComplete())
                {
                    if (!voiceovers[2])
                    {
                        subtitles.PlayAudio(Subtitles_HR.ID.P3_LINE4);
                        voiceovers[2] = true;
                        journal.AddJournalLog("Half a key? Who breaks their keys into two halves?");
                        journal.ChangeTasks(new string[] { "Bathroom Key Part 2" });
                        hasKeyPart1 = true;
                    }
                }
            }
            else if (!hasKeyPart2 && hasKeyPart1)
            {
                if (journal.AreTasksComplete())
                {
                    if (!voiceovers[3])
                    {
                        subtitles.PlayAudio(Subtitles_HR.ID.P3_LINE5);
                        voiceovers[3] = true;
                        hasKeyPart2 = true;
                    }
                }
            }
            else if (hasKeyPart2)
            {
                if (isDoorInteractedWith[1])
                {
                    if (!voiceovers[4])
                    {
                        subtitles.PlayAudio(Subtitles_HR.ID.P3_LINE6);
                        voiceovers[4] = true;

                        journal.AddJournalLog("Was that a ghost?! I better go back and see.");
                        ritualTrigger.allowedToBeUsed = true;
                        GameTesting_CW.instance.arePuzzlesDone[2] = true;
                    }
                }

            }
        }


    }


}
