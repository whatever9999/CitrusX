/*Chase Wilding - Hidden Mechanism Puzzle 10/02/2020
 * This puzzle sets off an animation when the correct book is picked up
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenMech_CW : MonoBehaviour
{
    Journal_DR journal;
    Door_DR door;

    private void Awake()
    {
        journal = GameObject.Find("FPSController").GetComponent<Journal_DR>();
        door = GameObject.Find("Hidden Mech Door").GetComponent<Door_DR>();
       
    }
    private void Start()
    {
        //journal.AddJournalLog("Hmm...maybe if I find some sort of mechanism I can open this door...");
        //journal.ChangeTasks(new string[] {"book" });
    }
    private void HiddenMechPuzzle()
    {
        if(journal.AreTasksComplete())
        {
            door.Open();
        }
    }
}
