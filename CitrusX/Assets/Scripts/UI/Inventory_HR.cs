
/*
 * Hugo
 * 
 * Opens and closes the Inventory on the UI using keycodes
 * From another script you can use the Inventory class to add,remove items or check if an item is in the inventory
 * 
 * Dominique (Changes) 06/04/2020
 * Added to enum, made sure item names appear with spaces instead of underscores and added functions to remove groups of items
 */

/**
* \class Inventory_HR
* 
* \brief The player can open and close the inventory and items can be placed and removed from it
* 
* Items are placed using an enum that is connected to a corresponding sprite
* Use AddItem(itemName) for an item with that name to appear in the inventory
* Use RemoveItem(slot) for the item at that slot to be reset
* Use CheckItem(itemName) to see if that item is currently in the inventory or not (returns a int that corresponds to the slot the item is at)
* 
* \author Hugo
* 
* \date Last Modified: 06/04/2020
*/
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using System; // Parse / Try Parse for future
public class Inventory_HR : MonoBehaviour
{
    // First aproach with enums
    public enum Names 
    {
        Water_Jug,
        Bowl,
        Salt,
        Bracelet,
        Necklace,
        Pendant,
        Key,
        Key_Handle,
        Key_Bit,
        Candle,
        Jewellery_Box,
        Coins,
        Pawn
    }

    public KeyCode inventoryOpenKey = KeyCode.I;
    public KeyCode[] inventoryCloseKeys = { KeyCode.I, KeyCode.Escape };

    //Can be changed to string 
    public List<Names> itemNames = new List<Names>();
    public List<Sprite> itemImages = new List<Sprite>();
    
    private Dictionary<Names, Sprite> items = new Dictionary<Names, Sprite>();    
    private const int maxItems = 12;
    private GameObject inventory;
    public GameObject[] inventoryItems = new GameObject[maxItems];

    /// <summary>
    /// Initialise variables and the items dictionary (enum to sprite)
    /// </summary>
    void Awake()
    {
        inventory = GameObject.Find("InventoryBg");

        //get all items loaded into the dicitionary
        for (int i = 0; i < itemImages.Count; i++)
        {
            items.Add(itemNames[i], itemImages[i]);
        }

        inventory.SetActive(false);
    }

    /// <summary>
    /// If the player presses I open/close the inventory UI object
    /// </summary>
    private void Update()
    {
        if (!inventory.activeInHierarchy && Input.GetKeyDown(inventoryOpenKey))
        {
            inventory.SetActive(true);
        }
        else if (inventory.activeInHierarchy)
        {
            for (int i = 0; i < inventoryCloseKeys.Length; i++)
            {
                if (Input.GetKeyDown(inventoryCloseKeys[i]))
                {
                    inventory.SetActive(false);
                }
            }
        }
    }

    /// <summary>
    /// Find the closest empty item slot and set the image and text in it to that of the itemName
    /// </summary>
    /// <param name="itemName - an enum linked to a sprite for the item"></param>
    public void AddItem(Names itemName)
    {
        bool addedKey = false;
        if(itemName == Names.Key_Bit)
        {
            int IsItemInInventory = CheckItem(Names.Key_Handle.ToString());
            if (IsItemInInventory != -1)
            {
                RemoveItem(IsItemInInventory);
                AddItem(Names.Key);
                addedKey = true;
            }
        } else if (itemName == Names.Key_Handle)
        {
            int IsItemInInventory = CheckItem(Names.Key_Bit.ToString());
            if (IsItemInInventory != -1)
            {
                RemoveItem(IsItemInInventory);
                AddItem(Names.Key);
                addedKey = true;
            }
        } 

        if(!addedKey)
        {
            for (int i = 0; i < maxItems; i++)
            {
                //If slot is empty
                if (inventoryItems[i].transform.GetChild(1).GetComponent<Text>().text == "")
                {
                    //Add image and text
                    Image inventoryImage = inventoryItems[i].transform.GetChild(0).GetComponent<Image>();
                    inventoryImage.sprite = items[itemName];
                    inventoryImage.enabled = true;
                    string name = itemName.ToString();
                    string nameInInventory = "";
                    for (int j = 0; j < name.Length; j++)
                    {
                        if (name[j] == '_')
                        {
                            nameInInventory += ' ';
                        }
                        else
                        {
                            nameInInventory += name[j];
                        }
                    }
                    inventoryItems[i].transform.GetChild(1).GetComponent<Text>().text = nameInInventory;
                    break;
                }
            }
        }
    }
    //s
    /// <summary>
    /// Set the image sprite for the slot to null an the text to ""
    /// </summary>
    /// <param name="slot - the slot where the object needs to be removed"></param>
    public void RemoveItem(int slot) 
    {
        //Remove item from slot
        Image inventoryImage = inventoryItems[slot].transform.GetChild(0).GetComponent<Image>();
        inventoryImage.sprite = null;
        inventoryImage.enabled = false;
        inventoryItems[slot].transform.GetChild(1).GetComponent<Text>().text = "";
    }

    /// <summary>
    /// Go through each slot and check if the next matches the itemName.
    /// </summary>
    /// <param name="itemName - the enum linked to the item"></param>
    /// <returns>The slot that the item is stored at. -1 if it is not there</returns>
    public int CheckItem(string itemName) 
    {
        string name = itemName.ToString();
        string nameInInventory = "";
        for (int i = 0; i < name.Length; i++)
        {
            if (name[i] == '_')
            {
                nameInInventory += ' ';
            }
            else
            {
                nameInInventory += name[i];
            }
        }

        int itemSlot = -1;

        for (int i = 0; i < maxItems; i++)
        {
            //If item is present in the inventory return the slot number
            if (inventoryItems[i].transform.GetChild(1).GetComponent<Text>().text == nameInInventory)
            {
                itemSlot = i;
                break;
            }
        }

        return itemSlot;
    }

    public void RemoveRitualItems()
    {
        int waterJugLocation = CheckItem(Names.Water_Jug.ToString());
        RemoveItem(waterJugLocation);
        int candlesLocation = CheckItem(Names.Candle.ToString());
        RemoveItem(candlesLocation);
        int coinsLocation = CheckItem(Names.Coins.ToString());
        RemoveItem(coinsLocation);
        int bowlLocation = CheckItem(Names.Bowl.ToString());
        RemoveItem(bowlLocation);
        int saltLocation = CheckItem(Names.Salt.ToString());
        RemoveItem(saltLocation);
    }

    public void RemoveJewelleryItems()
    {
        int jewelleryBoxLocation = CheckItem(Names.Jewellery_Box.ToString());
        RemoveItem(jewelleryBoxLocation);
        int braceletLocation = CheckItem(Names.Bracelet.ToString());
        RemoveItem(braceletLocation);
        int necklaceLocation = CheckItem(Names.Necklace.ToString());
        RemoveItem(necklaceLocation);
        int pendantLocation = CheckItem(Names.Pendant.ToString());
        RemoveItem(pendantLocation);
    }

    public void RemoveKey()
    {
        int keyLocation = CheckItem(Names.Key.ToString());
        RemoveItem(keyLocation);
    }

    public void RemovePawn()
    {
        int pawnLocation = CheckItem(Names.Pawn.ToString());
        RemoveItem(pawnLocation);
    }
}
