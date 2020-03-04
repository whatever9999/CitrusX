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
 */
using UnityEngine;

public class InitiatePuzzles_CW : MonoBehaviour
{
    public static InitiatePuzzles_CW instance;
    private Subtiles_HR subtitles;
    #region PUZZLE_REFERENCES
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
    internal bool[] monitorInteractions = { false, false, false, false, false, false, false, false, false };
    internal bool[] monitorInteractionsUsed = { false, false, false, false, false, false, false, false };
    #endregion
    #region TRIGGER_REFS
    TriggerScript_CW correctOrderTrigger;
    TriggerScript_CW hiddenMechTrigger;
    Door_DR hiddenMechDoor;
  
    #endregion

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
        subtitles = GameObject.Find("FirstPersonCharacter").GetComponent<Subtiles_HR>();
      //  correctOrder = GameObject.Find("PC").GetComponent<CorrectOrder_CW>();
        correctOrderTrigger = GameObject.Find("CorrectOrderTrigger").GetComponent<TriggerScript_CW>();
        hiddenMechTrigger = GameObject.Find("HiddenMechTrigger").GetComponent<TriggerScript_CW>();
       // hiddenMechDoor = GameObject.Find("HiddenMechDoor").GetComponent<Door_DR>();
        
        #endregion
    }
 
    public void InitiateSetUpRitualPuzzle()
    {
        journal.AddJournalLog("I've got all the things I need in the room. I'll quickly pick them up so I can set up the game...");
        journal.ChangeTasks(new string[] { "Candles", "Salt", "Bowl", "Water jug", "Coins" });
        //this is here to stop the strings playing constantly as called from Game's update
        ritualSetUp.SetActive(true);
    }
    private void Update()
    {
        if (monitorInteractions[0] && !monitorInteractionsUsed[0])
        {
            subtitles.PlayAudio(Subtiles_HR.ID.P2_LINE2);
            journal.AddJournalLog("The cameras have gone out, I should check that fusebox.");
            journal.ChangeTasks(new string[] { "Fix fusebox" });
            fusebox.SetGameActive(true);
            monitorInteractionsUsed[0] = true;
        }
        else if (monitorInteractions[1] && !monitorInteractionsUsed[1])
        {
            subtitles.PlayAudio(Subtiles_HR.ID.P3_LINE2);
            colourMatch.SetActive(true);
            monitorInteractionsUsed[1] = true;
        }
        else if (monitorInteractions[2] && !monitorInteractionsUsed[2])
        {
            subtitles.PlayAudio(Subtiles_HR.ID.P4_LINE3);
            journal.AddJournalLog("That safe wasn't there before, I wonder what's in it...");
            journal.ChangeTasks(new string[] { "unlock safe" });
            keypad.SetActive(true);
            monitorInteractionsUsed[2] = true;
        }
        else if (monitorInteractions[3] && !monitorInteractionsUsed[3])
        {
            subtitles.PlayAudio(Subtiles_HR.ID.P5_LINE1);
            scales.SetActive(true);
            monitorInteractionsUsed[3] = true;
        }
        else if (monitorInteractions[4] && !monitorInteractionsUsed[4])
        {
            subtitles.PlayAudio(Subtiles_HR.ID.P6_LINE1);
            journal.ChangeTasks(new string[] { "Pawn" });
            chessboard.SetActive(true);
            monitorInteractionsUsed[4] = true;
        }
        else if (monitorInteractions[5] && !monitorInteractionsUsed[5])
        {
            subtitles.PlayAudio(Subtiles_HR.ID.P7_LINE1);
            //throwing.SetActive(true);
            monitorInteractionsUsed[5] = true;
        }
        else if (monitorInteractions[6] && !monitorInteractionsUsed[6])
        {
            subtitles.PlayAudio(Subtiles_HR.ID.P8_LINE1);
            hiddenMechTrigger.allowedToBeUsed = true;
           // hiddenMechDoor.ToggleOpen();
           
            hiddenMech.SetActive(true);
            monitorInteractionsUsed[6] = true;
        }
        else if (monitorInteractions[7] && !monitorInteractionsUsed[7])
        {
            subtitles.PlayAudio(Subtiles_HR.ID.P9_LINE1);
            correctOrderTrigger.allowedToBeUsed = true;
          //  correctOrder.SetActive(true);
            monitorInteractionsUsed[7] = true;
        }
      
    }
    public void InitiateFuseboxPuzzle()
    {
        if (monitorInteractions[0])
        {
            fusebox.SetGameActive(true);
        }
    }
    public void InitiateColourMatchingPuzzle()
    {
        if (monitorInteractions[1])
        {

            colourMatch.SetActive(true);
        }
    }
    public void InitiateHiddenMechanismPuzzle()
    {
        //check monitor
        if (monitorInteractions[7])
        {
            //subtitles.PlayAudio(Subtiles_HR.ID.P8_LINE1);
        }

       
        hiddenMech.SetActive(true);
    }

    public void InitiateChessBoardPuzzle()
    {
        if (monitorInteractions[5])
        {
            subtitles.PlayAudio(Subtiles_HR.ID.P6_LINE1);
        }
        //enter room
        //VOICEOVER 6-2
        //interact with book
        //VOICEOVER 6-3
       
        chessboard.SetActive(true);
    }
    public void InitiateKeycodePuzzle()
    {
        //this is triggered by being in room for a certain amount of time

        if (monitorInteractions[3])
        {
            subtitles.PlayAudio(Subtiles_HR.ID.P4_LINE4);
            keypad.SetActive(true);
        }

    }
    public void InitiateBalancePuzzle()
    {
        if (monitorInteractions[4])
        {

            scales.SetActive(true);
            //journal for check out kitchen?

        }
    }
    public void InitiateThrowingPuzzle()
    {
        if (monitorInteractions[6])
        {
            subtitles.PlayAudio(Subtiles_HR.ID.P7_LINE1);
        }
        //enter game room
        //VOICEOVER 7-2
        //pick up ball
        //VOICEOVER 7-3
        journal.AddJournalLog("These buttons have some weird barrier, maybe I can throw something to hit them.");
        journal.ChangeTasks(new string[] { "button 1", "button 2", "button 3" });
      //  throwing.SetActive(true);
    }
    public void InitiateCorrectOrderPuzzle()
    {
        if (monitorInteractions[8])
        {
            subtitles.PlayAudio(Subtiles_HR.ID.P9_LINE1);
        }

    }
    public void InitiateCoinCountPuzzle()
    {
        
    }

}
