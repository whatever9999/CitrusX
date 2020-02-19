/*
 * Script: CoinBowlScript
 * Created By: Adam Gordon
 * Created On: 18/02/2020
 * 
 * Summary: Used by the container in which the player stores retrieved coins. Tracks the number of coins in the game, how many the player has saved.
 *          Used to determine if the player has won the game when blowing out the candles.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBowlScript_AG : MonoBehaviour
{
    private int startingCoinCount;
    private int coinsInContainer;
    private int coinsPlayerRemoved;
    private List<GameObject> containedCoins;

    bool candlesLit = false;
    bool gameStarted = false;

    void Start()
    {
        startingCoinCount = 0;
        coinsInContainer = 0;
        coinsPlayerRemoved = 0;
        containedCoins = new List<GameObject>();
    }


    /// <summary>
    /// Determines whether the player retrieved all coins from the water bowl.
    /// </summary>
    /// <returns></returns>
    public bool PlayerSavedAllCoins()
    {
        bool allCoins = false;

        if(startingCoinCount > coinsInContainer + coinsPlayerRemoved)
        {
            allCoins = true;
        }

        return allCoins;
    }

    /// <summary>
    /// Puts a coin into the container and tracks amount of coins
    /// </summary>
    public void AddCoin(/*GameObject coin*/)
    {
        //containedCoins.Add(coin); //TODO - Needs fully implementing

        coinsInContainer = containedCoins.ToArray().Length;
        Debug.Log("Coin Added");
    }

    /// <summary>
    /// Removes a coins from the container, if any are present, and track
    /// </summary>
    public void RemoveCoin()
    {
        if (coinsInContainer > 0)
        {
            containedCoins.RemoveAt(0);
            coinsInContainer = containedCoins.ToArray().Length;
            Debug.Log("Coin Removed");
        }
        else
        {
            Debug.Log("There are no coins left in the container...");
        }
    }
    /// <summary>
    /// Used to set an initial amount of coins for comparison at end and begin the game
    /// </summary>
    /// <param name="startingCoins"></param>
    public void BeginTracking(int startingCoins)
    {
        startingCoinCount = startingCoins;
        gameStarted = true;
    }

    /// <summary>
    /// Set state of "candlesLit"
    /// </summary>
    /// <param name="areTheyLit"></param>
    public void CandlesLit(bool areTheyLit) { candlesLit = areTheyLit; }

    /// <summary>
    /// Get the value of the bool "candlesLit"
    /// </summary>
    /// <returns></returns>
    public bool CandlesLit() { return candlesLit; }

    /// <summary>
    /// Return state of "gameStarted"
    /// </summary>
    /// <returns></returns>
    public bool GameStarted() { return gameStarted; }
}
