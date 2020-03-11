/*
 * Dominique
 * 
 * The water bowl handles the baron's appearances and the coins
 * 
 * Dominique (Changes) 20/02/2020
 * Now coins are instantiated according to how many are asked for in the inspector
 */

/**
* \class WaterBowl_DR
* 
* \brief Make the baron appear on a timer and hold the coins
* 
* \author Dominique
* 
* \date Last Modified: 20/02/2020
*/

using System.Collections.Generic;
using UnityEngine;

public class WaterBowl_DR : MonoBehaviour
{
    public Vector2 baronAppearanceIntervalRange;
    public int numberOfCoins;
    public GameObject coinPrefab;

    private float currentBaronAppearanceInterval;
    private float baronAppearanceInterval;
    private GameObject baron;
    private List<GameObject> coins;

    public bool GetBaronActive() { return baron.activeInHierarchy; }

    /// <summary>
    /// Create the coins and set the current interval until the baron will appear
    /// </summary>
    private void Start()
    {
        baron = GameObject.Find("Baron");
        baron.SetActive(false);

        currentBaronAppearanceInterval = 0;
        baronAppearanceInterval = Random.Range(baronAppearanceIntervalRange[0], baronAppearanceIntervalRange[1]);

        coins = new List<GameObject>();
        for(int i = 0; i < numberOfCoins; i++)
        {
            GameObject thisCoin = Instantiate(coinPrefab, transform);
            coins.Add(thisCoin);
        }
    }

    /// <summary>
    /// Run the timer for the baron's appearance (if he is not currently present) or make him appear
    /// </summary>
    private void Update()
    {
        if(currentBaronAppearanceInterval >= baronAppearanceInterval)
        {
            baron.SetActive(true);
        } else if(!baron.activeInHierarchy)
        {
            currentBaronAppearanceInterval += Time.deltaTime;
        }
    }

    /// <summary>
    /// Make the baron disappear and reset the time until he will next be visible
    /// </summary>
    public void ResetBaron()
    {
        baron.SetActive(false);
        currentBaronAppearanceInterval = 0;
        baronAppearanceInterval = Random.Range(baronAppearanceIntervalRange[0], baronAppearanceIntervalRange[1]);
    }

    /// <summary>
    /// If there are no coins left then the player has lost for trying to take more out or not blowing the candles out in time
    /// Otherwise a coin is removed
    /// </summary>
    /// <returns>a boolean to show if the coin was successfully removed or not</returns>
    public bool RemoveCoin()
    {
        bool coinWasRemoved = false;

        if(coins.Count == 0)
        {
            Debug.Log("Coins have run out but someone is trying to take one");
            //Player hasn't blown out candles even though coins have run out and baron has tried to take a coin
            //OR player has lost count of coins and tried to take one when they shouldn't
            //TODO: Game ends
        } else
        {
            //Remove a coin and give it to the player if they are the one who got it
            //If the baron is the one taking a coin then it disappears but the player won't be able to get the right amount of coins so they've lost the game
            coins[coins.Count - 1].SetActive(false);
            coins.RemoveAt(coins.Count - 1);
            coinWasRemoved = true;
        }

        return coinWasRemoved;
    }
}
