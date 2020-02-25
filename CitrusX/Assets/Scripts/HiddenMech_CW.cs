/*Chase Wilding - Hidden Mechanism Puzzle 10/02/2020
 * This puzzle sets off an animation when the correct book is picked up
 * 
 * Dominique (Changes) 11/02/2020
 * Removed unused imports
 */
using UnityEngine;

public class HiddenMech_CW : MonoBehaviour
{
    Door_DR door;
    private bool isActive = false;
    public void SetActive(bool value) { isActive = value; }

    private void Awake()
    {
        door = GameObject.Find("Hidden Mech Door").GetComponent<Door_DR>(); 
    }
    private void Update()
    {
        //if interact painting
        //VOICEOVER 8-5
        
    }
    private void HiddenMechPuzzle()
    {
        if(Journal_DR.instance.AreTasksComplete())
        {
            //VOICEOVER 8-6
            //read note
            //VOICEOVER 8-7
            //close note
            //VOICEOVER 8-8
            door.Open();
            GameTesting_CW.instance.arePuzzlesDone[7] = true;
        }
    }
}