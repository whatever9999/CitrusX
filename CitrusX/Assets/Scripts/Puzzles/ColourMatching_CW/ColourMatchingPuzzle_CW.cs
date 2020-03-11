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

internal class ColourMatchingPuzzle_CW : PuzzleBaseScript
{
    #region VARIABLES
    internal bool[] isDoorInteractedWith = { false, false };
    private bool hasKeyPart1 = false;
    internal bool hasKeyPart2 = false;
    private Door_DR door;
    #endregion
    /// <summary>
    /// Inititalise variables
    /// </summary>
    private void Awake()
    {
        journal = Journal_DR.instance;
        subtitles = GameObject.Find("FirstPersonCharacter").GetComponent<Subtitles_HR>();
    }
    /// <summary>
    /// According to the state of the puzzle play the right subtitles and update the journal
    /// </summary>
    private void Update()
    {
        if (isActive)
        {
            if (!voiceovers[9])
            {
                //subtitles.PlayAudio(Subtiles_HR.ID.P3_LINE2);
                voiceovers[9] = true;
            }
            else if (isDoorInteractedWith[0] && !voiceovers[10])
            {
                subtitles.PlayAudio(Subtitles_HR.ID.P3_LINE3);
                journal.AddJournalLog("It needs a key? Where can I find a key?");
                journal.ChangeTasks(new string[] { "Bathroom Key" });
                voiceovers[10] = true;
            }
            else if (isDoorInteractedWith[0] && !hasKeyPart1 && voiceovers[10])
            {
                if (journal.AreTasksComplete())
                {
                    if (!voiceovers[11])
                    {
                        subtitles.PlayAudio(Subtitles_HR.ID.P3_LINE4);
                        voiceovers[11] = true;
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
                    if (!voiceovers[12])
                    {
                        subtitles.PlayAudio(Subtitles_HR.ID.P3_LINE5);
                        voiceovers[12] = true;
                        hasKeyPart2 = true;
                    }
                }
            }
            else if (hasKeyPart2)
            {
                if (isDoorInteractedWith[1])
                {
                    if (!voiceovers[13])
                    {
                        subtitles.PlayAudio(Subtitles_HR.ID.P3_LINE6);
                        voiceovers[13] = true;

                        journal.AddJournalLog("Was that a ghost?! I better go back and see.");
                        ritualTrigger.allowedToBeUsed = true;
                        game.arePuzzlesDone[2] = true;
                    }
                }

            }
        }


    }


}
