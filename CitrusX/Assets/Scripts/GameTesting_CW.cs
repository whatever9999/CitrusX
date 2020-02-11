/*Chase Wilding 11/2/2020
 * This puzzle calls the scripts and starts the next puzzle when one is completed
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTesting_CW : MonoBehaviour
{
    public static GameTesting_CW instance;
    internal bool isPuzzle1Done = false;
    private bool setUpPuzzle1 = false;
    private bool setUpPuzzle2 = false;
    private bool isPuzzle2Done = false;
    private bool isPuzzle3Done = false;

    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if(!isPuzzle1Done && !setUpPuzzle1)
        {
            setUpPuzzle1 = true;
            InitiatePuzzles_CW.instance.InitiateSetUpRitualPuzzle();
            
        }
        if(isPuzzle1Done && !setUpPuzzle2)
        {
            setUpPuzzle2 = true;
            InitiatePuzzles_CW.instance.InitiateFuseboxPuzzle();
        }
    }
}
