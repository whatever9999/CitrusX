/*
 * Script: CandleScript
 * Created By: Adam Gordon
 * Created On: 18/02/2020
 * 
 * Summary: Used by the candles to light/blow out
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleScript_AG : MonoBehaviour
{
    private CoinBowlScript_AG CoinBowlScript;
    private bool areLit;
    // Start is called before the first frame update
    void Start()
    {
        areLit = false;
        CoinBowlScript = GameObject.FindGameObjectWithTag("DryBowl").GetComponent<CoinBowlScript_AG>();
    }

    /// <summary>
    /// Set "areLit" to true
    /// </summary>
    public void LightCandles()
    {
        if(!areLit)
        {
            areLit = true;
            CoinBowlScript.CandlesLit(true);
            Debug.Log("Candles have been lit");
        }
    }

    /// <summary>
    /// set "areLit" to false
    /// </summary>
    public void BlowOut()
    {
        if(areLit)
        {
            areLit = false;
            CoinBowlScript.CandlesLit(false);
            Debug.Log("Candles have been extinguished.");
        }
    }

    /// <summary>
    /// Return the value of "areLit"
    /// </summary>
    /// <returns></returns>
    public bool AreLit() { return areLit; }
}
