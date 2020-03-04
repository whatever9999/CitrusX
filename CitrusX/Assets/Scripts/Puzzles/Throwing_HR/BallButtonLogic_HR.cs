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
using UnityEngine;

public class BallButtonLogic_HR : MonoBehaviour
{
    public int massRequired;
    private const int ballsRequired = 3;
    private bool isActive;
    private Journal_DR journal;
    private InitiatePuzzles_CW puzzleScript;
    private Subtiles_HR subtitles;
    public void SetActive(bool value) { isActive = value; }
    private void Awake()
    {
        journal = Journal_DR.instance;
        subtitles = GameObject.Find("FirstPersonCharacter").GetComponent<Subtiles_HR>();
    }

    //Initiate puzzle script in start to ensure that the instance is initialised
    private void Start()
    {
        puzzleScript = InitiatePuzzles_CW.instance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //check if the mass of the ball is the required to push the button
        if (collision.gameObject.GetComponent<Rigidbody>().mass == massRequired)
        {
            puzzleScript.ballCounter++;
            Destroy(collision.gameObject);
            if(puzzleScript.ballCounter == ballsRequired)
            {
                journal.TickOffTask("press all buttons");
                subtitles.PlayAudio(Subtiles_HR.ID.P7_LINE4);
                //Open box
                //VOICEOVER 7-5
                //box closes
                //VOICEOVER 7-6
                GameTesting_CW.instance.arePuzzlesDone[6] = true;
            }
        }
    }
}
