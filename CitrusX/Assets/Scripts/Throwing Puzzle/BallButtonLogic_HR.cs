/*
 * Hugo
 * 
 * Script to make sure the button only works with the correct ball by comparing masses 
 * 
 * Chase (changes) 17/2/2020
 * Added a setactive function and linked it to the games puzzle. Also added the counter made in initiate puzzles to see if all
 * buttons have been pressed. Also added journal.
 */
using UnityEngine;

public class BallButtonLogic_HR : MonoBehaviour
{
    public int massRequired;
    private const int ballsRequired = 3;
    private bool isActive;
    private Journal_DR journal;
    private InitiatePuzzles_CW puzzleScript;
    public void SetActive(bool value) { isActive = value; }
    private void Awake()
    {
        puzzleScript = InitiatePuzzles_CW.instance;
        journal = Journal_DR.instance;
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
                GameTesting_CW.instance.arePuzzlesDone[6] = true;
            }
        }
    }
}
