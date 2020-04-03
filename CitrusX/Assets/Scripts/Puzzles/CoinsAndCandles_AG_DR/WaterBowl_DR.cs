/*
 * Dominique
 * 
 * The water bowl handles the baron's appearances and the coins
 * 
 * Dominique (Changes) 20/02/2020
 * Now coins are instantiated according to how many are asked for in the inspector
 * 
 * Dominique (Changes) 17/03/2020
 * The bowl no longer uses a timer to activate/deactivate the baron
 * 
 * Dominique (Changes)
 * Coins were being instantiated in the air in game
 */

/**
* \class WaterBowl_DR
* 
* \brief Handle the coins (Hold and pick up)
* 
* \author Dominique
* 
* \date Last Modified: 21/03/2020
*/

using System.Collections.Generic;
using UnityEngine;

public class WaterBowl_DR : MonoBehaviour
{
    public int numberOfCoins;
    public GameObject coinPrefab;

    internal List<GameObject> coins;

    /// <summary>
    /// Create the coins and set the current interval until the baron will appear
    /// </summary>
    private void Start()
    {
        coins = new List<GameObject>();
        for(int i = 0; i < numberOfCoins; i++)
        {
            GameObject thisCoin = Instantiate(coinPrefab, transform);
            thisCoin.transform.localPosition = Vector3.zero;
            coins.Add(thisCoin);
        }
    }

    /// <summary>
    /// If there are no coins left then the player has lost for trying to take more out or not blowing the candles out in time
    /// Otherwise a coin is removed
    /// </summary>
    /// <returns>a boolean to show if the coin was successfully removed or not</returns>
    public bool RemoveCoin()
    {
        bool coinWasRemoved = false;
        SFX_Manager_HR.instance.PlaySFX(SFX_Manager_HR.SoundEffectNames.PICK_UP_COIN, transform.position);

        if (coins.Count == 0)
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
