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
      //  door = GameObject.Find("HiddenMechDoor").GetComponent<Door_DR>(); 
    }
 
    private void HiddenMechPuzzle()
    {
        if(Journal_DR.instance.AreTasksComplete())
        {
         //   door.ToggleOpen();
            GameTesting_CW.instance.arePuzzlesDone[7] = true;
        }
    }
}