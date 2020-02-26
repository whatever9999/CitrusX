﻿/*Chase Wilding 9/2/2020
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
    internal int ballCounter = 0;
    #endregion
    #region VOICEOVER_BOOLS
    private bool[] voiceovers = { false, false, false, false, false};
    #endregion
    private void Awake()
    {
        instance = this;
        journal = Journal_DR.instance;
        ritualSetUp = GetComponent<SetUpRitual_CW>();
        hiddenMech = GetComponent<HiddenMech_CW>();
        colourMatch = GetComponent<ColourMatchingPuzzle_CW>();
        fusebox = GameObject.Find("FuseboxUI").GetComponent<Fusebox_CW>();
        chessboard = GameObject.Find("ChessBoard").GetComponent<ChessBoard_DR>();
        throwing = GetComponent<BallButtonLogic_HR>();
        keypad = GetComponent<KeypadUI_DR>(); //might need to edit this
        scales = GetComponent<ScalesPuzzleScript_AG>();
        subtitles = GameObject.Find("FirstPersonCharacter").GetComponent<Subtiles_HR>();
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
        //player enters ritual room - trigger box? Game script?
        //disturbance occurs (loud sound?)
        if(!voiceovers[1])
        {
            subtitles.PlayAudio(Subtiles_HR.ID.P2_LINE1);
            voiceovers[1] = true;
            Debug.Log("reached");
            journal.AddJournalLog("The cameras have gone out, I should check that fusebox.");
            journal.ChangeTasks(new string[] { "Fix fusebox" });
            fusebox.SetGameActive(true);
        }
       //if interact with monitor then
        
    }
    public void InitiateColourMatchingPuzzle()
    {
        //player in ritual room
        //disturbance occurs
        if(!voiceovers[2])
        {
            subtitles.PlayAudio(Subtiles_HR.ID.P3_LINE1);
            voiceovers[2] = true;
        }
        //if player interacts with monitor then

        colourMatch.SetActive(true);
       
    }
    public void InitiateHiddenMechanismPuzzle()
    {
        //check monitor
        //VOICEOVER 8-1
        //enter library
        //door slams
        //VOICEOVER 8-2
        //VOICEOVER 8-3

        journal.AddJournalLog("Hmm...maybe if I find some sort of mechanism I can open this door...");
        journal.ChangeTasks(new string[] { "open door", "book" });
        hiddenMech.SetActive(true);
    }

    public void InitiateChessBoardPuzzle()
    {
        //interact with monitor
        //VOICEOVER 6-1
        //enter room
        //VOICEOVER 6-2
        //interact with book
        //VOICEOVER 6-3
        journal.AddJournalLog("I think that book might explain what I'm supposed to do with this board. A piece seems missing though.");
        journal.ChangeTasks(new string[] { "Pawn" });
        chessboard.SetActive(true);
    }
    public void InitiateKeycodePuzzle()
    {
        //this is triggered by being in room for a certain amount of time
        if(!voiceovers[3])
        {
            subtitles.PlayAudio(Subtiles_HR.ID.P4_LINE1);
            voiceovers[3] = true;
        }
        if(!voiceovers[4])
        {
            subtitles.PlayAudio(Subtiles_HR.ID.P4_LINE2);
            voiceovers[4] = true;
        }
        //if player interacts with monitor
        keypad.SetActive(true);
    }
    public void InitiateBalancePuzzle()
    {
        //in room, checks monitor
        if(!voiceovers[5])
        {
            subtitles.PlayAudio(Subtiles_HR.ID.P5_LINE1);
            voiceovers[5] = true;
            scales.SetActive(true);
            //journal for check out kitchen?
        }
        
        

    }
    public void InitiateThrowingPuzzle()
    {
        //check monitor
        //VOICEOVER 7-1
        //enter game room
        //VOICEOVER 7-2
        //pick up ball
        //VOICEOVER 7-3
        journal.AddJournalLog("These buttons have some weird barrier, maybe I can throw something to hit them.");
        journal.ChangeTasks(new string[] { "button 1", "button 2", "button 3" });
        throwing.SetActive(true);
    }
    public void InitiateCorrectOrderPuzzle()
    {
        //interact with monitor
        //VOICEOVER 9-1
        //enter room
        //door shut
        //VOICEOVER 9-2
        //interact PC
        //VOICEOVER 9-3
        journal.AddJournalLog("Is there some kind of pattern here? Maybe I could recreate it.");
        journal.ChangeTasks(new string[] { "repeat the sequence" });
    }
    public void InitiateCoinCountPuzzle()
    {
        //stand infront of ritual table
        //VOICEOVER 10-1
        //VOICEOVER 10-2
        journal.AddJournalLog("That should be it. Have I counted enough coins? I should blow out the candles if I have.");
        journal.ChangeTasks(new string[] { "blow out candles" });
    }

}
