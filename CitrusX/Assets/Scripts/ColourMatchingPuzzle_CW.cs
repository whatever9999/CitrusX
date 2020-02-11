/*Chase Wilding
 * Colour matching script
 * This scripts main function 'Start colour Matching puzzle' can be called during game play to add it to the journal and start the process
 * when this puzzle begins
 * Attach this script anywhere in the scene for it to work
 * 
 * Dominique (Changes) 11/02/2020
 * Removed unused package imports
 */
using UnityEngine;

public class ColourMatchingPuzzle_CW : MonoBehaviour
{
    private Journal_DR journal;
    public void Awake()
    {
        journal = GameObject.Find("FPSController").GetComponent<Journal_DR>();
        StartColourMatchingPuzzle(); //only used for testing
    }
    internal void StartColourMatchingPuzzle()
    {
        journal.AddJournalLog("This door looks like it needs a key...maybe I should try the garage");
        journal.ChangeTasks(new string[] { "key part 1", "key part 2" });
    }
}
