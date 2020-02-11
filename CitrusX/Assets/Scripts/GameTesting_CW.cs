/*Chase Wilding 11/2/2020
 * This puzzle calls the scripts and starts the next puzzle when one is completed
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTesting_CW : MonoBehaviour
{
    public static GameTesting_CW instance;
    private bool[] setUpPuzzle = { false, false, false, false, false, false, false, false, false, false };
    internal bool[] arePuzzlesDone = { false, false, false, false, false, false, false, false, false, false};

    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if(!arePuzzlesDone[0] && !setUpPuzzle[0])
        {
            setUpPuzzle[0] = true;
            InitiatePuzzles_CW.instance.InitiateSetUpRitualPuzzle();
            
        }
        if(arePuzzlesDone[0] && !setUpPuzzle[1])
        {
            setUpPuzzle[1] = true;
            InitiatePuzzles_CW.instance.InitiateFuseboxPuzzle();
        }
        if(arePuzzlesDone[1] && !setUpPuzzle[2])
        {

        }
        if(arePuzzlesDone[6] && !setUpPuzzle[7])
        {
            setUpPuzzle[7] = true;
            InitiatePuzzles_CW.instance.InitiateHiddenMechanismPuzzle();
        }
    }
}
