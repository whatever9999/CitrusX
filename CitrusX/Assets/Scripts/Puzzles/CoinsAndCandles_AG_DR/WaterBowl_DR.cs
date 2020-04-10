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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBowl_DR : Interactable_DR
{
    public int numberOfCoins;
    public GameObject coinPrefab;

    private GameObject coinNotification;
    private const float coinNotificationLength = 0.5f;

    internal enum ReasonForLosing
    {
        None,
        Too_Few_Coins,
        Took_Without_Baron,
        Took_More_Than_There_Were,
        Baron_Got_Coin
    }

    internal ReasonForLosing reasonForLosing = ReasonForLosing.None;
    internal bool playerHasLost = false;
    internal List<GameObject> coins;

    /// <summary>
    /// Create the coins and set the current interval until the baron will appear
    /// </summary>
    private void Awake()
    {
        coinNotification = GameObject.Find("CoinCollectNotification");
        coins = new List<GameObject>();
        for(int i = 0; i < numberOfCoins; i++)
        {
            GameObject thisCoin = Instantiate(coinPrefab, transform);
            thisCoin.transform.localPosition = Vector3.zero;
            coins.Add(thisCoin);
        }
        coinNotification.SetActive(false);
    }

    /// <summary>
    /// If there are no coins left then the player has lost for trying to take more out or not blowing the candles out in time
    /// Otherwise a coin is removed
    /// </summary>
    /// <returns>a boolean to show if the coin was successfully removed or not</returns>
    public bool RemoveCoin(bool isBaron)
    {
        bool coinWasRemoved = false;
        if (isBaron)
        {
            if (reasonForLosing == WaterBowl_DR.ReasonForLosing.None) reasonForLosing = WaterBowl_DR.ReasonForLosing.Baron_Got_Coin;
            SFX_Manager_HR.instance.PlaySFX(SFX_Manager_HR.SoundEffectNames.PICK_UP_COIN, transform.position);
            coinWasRemoved = true;
            playerHasLost = true;
        } else
        {
            if (!SaveSystem_DR.instance.startingGame)
            {
                SFX_Manager_HR.instance.PlaySFX(SFX_Manager_HR.SoundEffectNames.PICK_UP_COIN, transform.position);
                StartCoroutine(CoinCollectUI());
            }

            if (coins.Count == 0)
            {
                //Trying to take coin when there are none left
                if (reasonForLosing == WaterBowl_DR.ReasonForLosing.None) reasonForLosing = WaterBowl_DR.ReasonForLosing.Took_More_Than_There_Were;
                playerHasLost = true;
            }
            else
            {
                //Remove a coin
                coins[coins.Count - 1].SetActive(false);
                coins.RemoveAt(coins.Count - 1);
                coinWasRemoved = true;
            }
        }
        return coinWasRemoved;
    }

    public IEnumerator CoinCollectUI()
    {
        coinNotification.SetActive(true);
        yield return new WaitForSeconds(coinNotificationLength);
        coinNotification.SetActive(false);
    }
}
