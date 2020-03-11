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
 * Chase (Changes) 16/02/2020
 * Added journal entries and basic checklists for each puzzle
 * Chase (Changes) 26/2/2020
 * Added an update for monitor interaction text as it was not getting accessed otherwise. Also changed the ways in which they were called for easier
 * transitions with voiceovers.
 * Chase (Changes) 4/3/2020
 * Tidied up script, added regions and got all puzzles linked
 */

/**
* \class InitiatePuzzles_CW
* 
* \brief Sets up a puzzle when it is inititalised from GameTesting_CW
* 
* InititateSetUpRitualPuzzle() is used at the start of the game
* In Update monitorInteractions is used to identify what journal logs and tasks are needed and set the state and variables of GOs
* Initiate<puzzleName>() activates the right GOs for a puzzle to commence
* 
* \author Chase
* 
* \date Last Modified: 04/03/2020
*/

using UnityEngine;

public class InitiatePuzzles_CW : MonoBehaviour
{
    public static InitiatePuzzles_CW instance;

    #region PUZZLE_REFERENCES
    private Subtitles_HR subtitles;
    private SetUpRitual_CW ritualSetUp;
    private HiddenMech_CW hiddenMech;
    private ColourMatchingPuzzle_CW colourMatch;
    private Fusebox_CW fusebox;
    private ChessBoard_DR chessboard;
    private KeypadUI_DR keypad;
    private BallButtonLogic_HR throwing;
    private Journal_DR journal;
    private ScalesPuzzleScript_AG scales;
    private CorrectOrder_CW correctOrder;
    internal int ballCounter = 0;
    #endregion
    #region VOICEOVER_BOOLS
    private bool[] voiceovers = { false, false, false, false, false };
    internal bool[] monitorInteractions = { false, false, false, false, false, false, false, false, false, false };
    internal bool[] monitorInteractionsUsed = { false, false, false, false, false, false, false, false };
    #endregion
    #region TRIGGER_REFS
    private TriggerScript_CW correctOrderTrigger;
    private TriggerScript_CW hiddenMechTrigger;
    private Door_DR hiddenMechDoor;
  
    #endregion

    /// <summary>
    /// Inititalise variables
    /// </summary>
    private void Awake()
    {
        instance = this;
        #region INITIALISATION
        journal = Journal_DR.instance;
        ritualSetUp = GetComponent<SetUpRitual_CW>();
        hiddenMech = GetComponent<HiddenMech_CW>();
        colourMatch = GameObject.Find("ColourMatchingDoor").GetComponent<ColourMatchingPuzzle_CW>();
        fusebox = GameObject.Find("FuseboxUI").GetComponent<Fusebox_CW>();
        chessboard = GameObject.Find("ChessBoard").GetComponent<ChessBoard_DR>();
        keypad = GameObject.Find("KeypadUI").GetComponent<KeypadUI_DR>(); //might need to edit this
        scales = GameObject.Find("Scales").GetComponent<ScalesPuzzleScript_AG>();
        subtitles = GameObject.Find("FirstPersonCharacter").GetComponent<Subtitles_HR>();
        correctOrder = GameObject.Find("Correct Order PC").GetComponent<CorrectOrder_CW>();
        correctOrderTrigger = GameObject.Find("CorrectOrderTrigger").GetComponent<TriggerScript_CW>();
        hiddenMechTrigger = GameObject.Find("HiddenMechTrigger").GetComponent<TriggerScript_CW>();
        hiddenMechDoor = GameObject.Find("HiddenMechDoor").GetComponent<Door_DR>();
        
        #endregion
    }
 
    /// <summary>
    /// Set the journal log and tasks for the ritual and set the puzzle to active
    /// </summary>
    public void InitiateSetUpRitualPuzzle()
    {
        journal.AddJournalLog("Reddit said I need to find several items: coins, candles, salt, a water jug and a bowl.");
        journal.ChangeTasks(new string[] { "Candles", "Salt", "Bowl", "Water jug", "Coins" });
        //this is here to stop the strings playing constantly as called from Game's update
        ritualSetUp.SetActive(true);
    }
    /// <summary>
    /// Check the monitorInteractions array to check the status of a puzzle play subtitles, add journal logs, change tasks and set puzzles active accordingly
    /// </summary>
    private void Update()
    {
        if (monitorInteractions[0] && !monitorInteractionsUsed[0])
        {
            subtitles.PlayAudio(Subtitles_HR.ID.P2_LINE2);
            journal.AddJournalLog("The camera has gone out, I should check that fuse box.");
            journal.ChangeTasks(new string[] { "Check fusebox" });
            fusebox.SetGameActive(true);
            monitorInteractionsUsed[0] = true;
        }
        else if (monitorInteractions[1] && !monitorInteractionsUsed[1])
        {
            subtitles.PlayAudio(Subtitles_HR.ID.P3_LINE2);
            journal.AddJournalLog("That door upstairs wasn’t closed before. I should check it out.");
            journal.ChangeTasks(new string[] { "Check bathroom door" });
            colourMatch.SetActive(true);
            monitorInteractionsUsed[1] = true;
        }
        else if (monitorInteractions[2] && !monitorInteractionsUsed[2])
        {
            subtitles.PlayAudio(Subtitles_HR.ID.P4_LINE3);
            journal.AddJournalLog("That safe wasn't there before, I wonder what's in it...");
            journal.ChangeTasks(new string[] { "Check safe" });
            keypad.SetActive(true);
            monitorInteractionsUsed[2] = true;
        }
        else if (monitorInteractions[3] && !monitorInteractionsUsed[3])
        {
            subtitles.PlayAudio(Subtitles_HR.ID.P5_LINE1);
            journal.TickOffTask("Return to ritual");
            journal.AddJournalLog("Those scales have some weird aura…are they haunted? God this ritual is getting to my head");
            journal.ChangeTasks(new string[] { "Check the scales " });
            scales.SetActive(true);
            monitorInteractionsUsed[3] = true;
        }
        else if (monitorInteractions[4] && !monitorInteractionsUsed[4])
        {
            subtitles.PlayAudio(Subtitles_HR.ID.P6_LINE1);
            journal.TickOffTask("Return to ritual");
            journal.AddJournalLog("How did the pieces blow off with the window closed?");
            journal.ChangeTasks(new string[] {"Check the study"});
            chessboard.SetActive(true);
            monitorInteractionsUsed[4] = true;
        }
        else if (monitorInteractions[5] && !monitorInteractionsUsed[5])
        {
            subtitles.PlayAudio(Subtitles_HR.ID.P7_LINE1);
            journal.TickOffTask("Return to ritual");
            journal.AddJournalLog("How many coins am I at? I should be about half way…maybe?");
            journal.AddJournalLog("What's going on in the gym?");
            journal.ChangeTasks(new string[] { "Check the gym" });
            monitorInteractionsUsed[5] = true;
        }
        else if (monitorInteractions[6] && !monitorInteractionsUsed[6])
        {
            subtitles.PlayAudio(Subtitles_HR.ID.P8_LINE1);
            journal.TickOffTask("Return to ritual");
            journal.ChangeTasks(new string[] { "Check out library" });
            hiddenMechTrigger.allowedToBeUsed = true;
            hiddenMech.SetActive(true);
            monitorInteractionsUsed[6] = true;
        }
        else if (monitorInteractions[7] && !monitorInteractionsUsed[7])
        {
            subtitles.PlayAudio(Subtitles_HR.ID.P9_LINE1);
            journal.TickOffTask("Return to ritual");
            journal.ChangeTasks(new string[] { "Check master bedroom" });
            journal.AddJournalLog("That PC wasn’t like that before was it?");
            correctOrderTrigger.allowedToBeUsed = true;
            monitorInteractionsUsed[7] = true;
        }
      
    }
    public void InitiateFuseboxPuzzle()
    {
        fusebox.SetGameActive(true);
    }
    public void InitiateColourMatchingPuzzle()
    {
        colourMatch.SetActive(true);
    }
    public void InitiateHiddenMechanismPuzzle()
    {
        hiddenMech.SetActive(true);
    }

    public void InitiateChessBoardPuzzle()
    {
        chessboard.SetActive(true);
    }
    public void InitiateKeycodePuzzle()
    {
        keypad.SetActive(true);
    }
    public void InitiateBalancePuzzle()
    {
        scales.SetActive(true);
    }
    public void InitiateThrowingPuzzle()
    {
        journal.AddJournalLog("These buttons have some weird barrier, maybe I can throw something to hit them.");
        journal.ChangeTasks(new string[] { "button 1", "button 2", "button 3" });
    }
    public void InitiateCorrectOrderPuzzle()
    {
        subtitles.PlayAudio(Subtitles_HR.ID.P9_LINE1);
    }
    public void InitiateCoinCountPuzzle()
    {
        
    }

}
