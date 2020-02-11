/*Chase Wilding 9/2/2020
 * This script can be called during gameplay to set up the next puzzle, so when the player finds a locked door, it can be called/
 * when a disturbance occurs. Some puzzles such as ColourMatching only need this segment to work.
 * 
 * Dominique (Changes) 10/02/2020
 * Added chess puzzle setup
 * Turned the class into a singleton so it can be called as an instance from anywhere
 * 
 * Chase (Changes) 10/02/2020
 * Added the hidden mech puzzle
 * Chase (Changes) 11/02/2020
 * Added the ritual puzzle and colour matching set up
 */
using UnityEngine;

public class InitiatePuzzles_CW : MonoBehaviour
{
    public static InitiatePuzzles_CW instance;
    #region PUZZLE_REFERENCES
    private SetUpRitual_CW ritualSetUp;
    private HiddenMech_CW hiddenMechSetUp;
    private ColourMatchingPuzzle_CW colourMatch;
    private Fusebox_CW fusebox;
    private Journal_DR journal;
    #endregion

    private void Awake()
    {
        instance = this;
        journal = Journal_DR.instance;
        ritualSetUp = GetComponent<SetUpRitual_CW>();
        hiddenMechSetUp = GetComponent<HiddenMech_CW>();
        colourMatch = GetComponent<ColourMatchingPuzzle_CW>();
        fusebox = GetComponent<Fusebox_CW>();
    }
    public void InitiateSetUpRitualPuzzle()
    {
        journal.AddJournalLog("I've got all the things I need in the room. I'll quickly pick them up so I can set up the game...");
        journal.ChangeTasks(new string[] { "Candles", "Salt", "Bowl", "Water jug", "Coins" });
        //this is here to stop the strings playing constantly as called from Game's update
        ritualSetUp.SetActive(true);
    }
    public void InitiateFuseboxPuzzle()
    {
        journal.AddJournalLog("The cameras have gone out, I should check that fusebox.");
        journal.ChangeTasks(new string[] { "Fix fusebox" });
        fusebox.SetActive(true);
    }
    public void InitiateColourMatchingPuzzle()
    {
        journal.AddJournalLog("This door looks like it needs a key...maybe I should try the garage");
        journal.ChangeTasks(new string[] { "key part 1", "key part 2" });
        colourMatch.SetActive(true);
    }
    public void InitiateHiddenMechanismPuzzle()
    {
        journal.AddJournalLog("Hmm...maybe if I find some sort of mechanism I can open this door...");
        journal.ChangeTasks(new string[] { "open door", "book" });
    }

    public void InitiateChessBoardPuzzle()
    {
        journal.AddJournalLog("I think that book might explain what I'm supposed to do with this board. A piece seems missing though.");
        journal.ChangeTasks(new string[] { "Pawn" });
    }
}
