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
    private bool areLit;
    // Start is called before the first frame update
    void Start()
    {
        areLit = false;
    }

    public void LightCandles()
    {
        if(!areLit)
        {
            areLit = true;
        }
    }
    public void BlowOut()
    {
        if(areLit)
        {
            areLit = false;
        }
    }

    public bool AreLit() { return areLit; }
}
