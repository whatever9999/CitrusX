/*
 * Dominique
 * 
 * The water bowl handles the baron's appearances and the coins
 */

using System.Collections.Generic;
using UnityEngine;

public class WaterBowl_DR : MonoBehaviour
{
    public Vector2 baronAppearanceIntervalRange;

    private float currentBaronAppearanceInterval;
    private float baronAppearanceInterval;
    private GameObject baron;
    private List<GameObject> coins;

    public bool GetBaronActive() { return baron.activeInHierarchy; }

    //Get the coins (that are children to the bowl) and set the current interval until the baron will appear
    private void Start()
    {
        baron = GameObject.Find("Baron");
        baron.SetActive(false);

        currentBaronAppearanceInterval = 0;
        baronAppearanceInterval = Random.Range(baronAppearanceIntervalRange[0], baronAppearanceIntervalRange[1]);

        coins = new List<GameObject>();
        for(int i = 0; i < transform.childCount; i++)
        {
            coins.Add(transform.GetChild(i).gameObject);
        }
    }

    //Run the timer for the baron's appearance (if he is not currently present) or make him appear
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

    //Make the baron disappear and reset the time until he will next be visible
    public void ResetBaron()
    {
        baron.SetActive(false);
        currentBaronAppearanceInterval = 0;
        baronAppearanceInterval = Random.Range(baronAppearanceIntervalRange[0], baronAppearanceIntervalRange[1]);
    }

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
