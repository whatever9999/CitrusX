/*
 * Hugo
 * 
 * Script to make sure the button only works with the correct ball by comparing masses 
 * 
 * Chase (changes) 17/2/2020
 * Added a setactive function and linked it to the games puzzle. Also added the counter made in initiate puzzles to see if all
 * buttons have been pressed. Also added journal.
 * Chase(Changes) 26/2/2020
 * Began adding subtitle functionality
 * 
 */

/**
* \class BallButtonLogic_HR
* 
* \brief When the right weight ball hits the button it is destroyed and the game state is updated
* 
* \author Hugo
* 
* \date Last Modified: 26/02/2020
*/
using UnityEngine;

public class BallButtonLogic_HR : MonoBehaviour
{
    public int massRequired;
    private const int ballsRequired = 3;
    internal bool isActive;
    private Journal_DR journal;
    private InitiatePuzzles_CW puzzleScript;
    private Subtitles_HR subtitles;
    private Animator animator;
    public void SetActive(bool value) { isActive = value; }

    /// <summary>
    /// Inititalise variables
    /// </summary>
    private void Awake()
    {
        journal = Journal_DR.instance;
        subtitles = GameObject.Find("FirstPersonCharacter").GetComponent<Subtitles_HR>();
        animator = GetComponentInParent<Animator>();
    }

    /// <summary>
    /// Initiate puzzle script in start to ensure that the instance is initialised
    /// </summary>
    private void Start()
    {
        puzzleScript = InitiatePuzzles_CW.instance;
    }

    /// <summary>
    /// If the mass of the ball that hit the button is correct then destroy it and update the puzzleScript
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        //check if the mass of the ball is the required to push the button
        if (collision.gameObject.GetComponent<Rigidbody>().mass == massRequired)
        {
            animator.SetTrigger("Hit");
            puzzleScript.ballCounter++;
            #region CHECK_WHICH_BUTTON_FOR_JOURNAL
            if (gameObject.name == "1Button")
            {
                journal.TickOffTask("Button 1");
            }
            else if(gameObject.name == "2Button")
            {
                journal.TickOffTask("Button 2");
            }
            else if (gameObject.name == "3Button")
            {
                journal.TickOffTask("Button 3");
            }
            #endregion
            Destroy(collision.gameObject);
            //SOUND HERE for BALL hitting

            if (puzzleScript.ballCounter == ballsRequired)
            {
                subtitles.PlayAudio(Subtitles_HR.ID.P7_LINE4);
                //journal.AddJournalLog("A box? Where did this come from?");
                journal.ChangeTasks(new string[] { "Open box" });
                GameTesting_CW.instance.arePuzzlesDone[6] = true;
            }
        }
    }
}
