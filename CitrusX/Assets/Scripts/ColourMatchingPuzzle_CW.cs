/*Chase Wilding
 * Colour matching script
 * This scripts main function 'Start colour Matching puzzle' can be called during game play to add it to the journal and start the process
 * when this puzzle begins

 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourMatchingPuzzle_CW : MonoBehaviour
{
    private Journal_DR journal;
    private bool isActive = false;
    internal void SetActive(bool value) { isActive = false; }
    public void Awake()
    {
        journal = Journal_DR.instance;
    }
    private void Update()
    {
        if(journal.AreTasksComplete())
        {
            GameTesting_CW.instance.arePuzzlesDone[2] = true;
        }
    }
}
