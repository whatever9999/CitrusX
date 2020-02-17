
/*
 * Hugo
 * 
 * Opens and closes the Inventory on the UI using keycodes
 * From another script you can use the Inventory class to add,remove items or check if an item is in the inventory
 * 
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
      WaterJug
    }

    public KeyCode inventoryOpenKey = KeyCode.I;
    public KeyCode[] inventoryCloseKeys = { KeyCode.I, KeyCode.Escape };

    //Can be changed to string 
    public List<Names> itemNames = new List<Names>();
    public List<Sprite> itemImages = new List<Sprite>();
    
    private Dictionary<Names, Sprite> items = new Dictionary<Names, Sprite>();    
    private const int maxItems = 12;
    private GameObject inventory;
    private GameObject[] inventoryItems = new GameObject[maxItems];

    void Awake()
    {
        inventory = GameObject.Find("InventoryBg");
        int i = 0;
        //Get all slots from inventory
        foreach (GameObject inventoryPanels in GameObject.FindGameObjectsWithTag("InventoryPanel"))
        {
            inventoryItems[i] = inventoryPanels;
            i++;
        }

        //get all items loaded into the dicitionary
        for (i = 0; i < itemImages.Count; i++)
        {
            items.Add(itemNames[i], itemImages[i]);
        }

        inventory.SetActive(false);
    }

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

    public void AddItem(Names itemName)
    {
        for (int i = 0; i < maxItems; i++)
        {
            //If slot is empty
            if (inventoryItems[i].transform.GetChild(1).GetComponent<Text>().text == "")
            {
                //Add image and text
                inventoryItems[i].transform.GetChild(0).GetComponent<Image>().sprite = items[itemName];
                inventoryItems[i].transform.GetChild(1).GetComponent<Text>().text = itemName.ToString();
                break;
            }
        }
    }

    public void RemoveItem(int slot) 
    {
        //Remove item from slot
        inventoryItems[slot].transform.GetChild(0).GetComponent<Image>().sprite = null; 
        inventoryItems[slot].transform.GetChild(1).GetComponent<Text>().text = "";
    }

    public int CheckItem(string itemName) 
    {
        int itemSlot = -1;

        for (int i = 0; i < maxItems; i++)
        {
            //If item is present in the inventory return the slot number
            if (inventoryItems[i].transform.GetChild(1).GetComponent<Text>().text == itemName)
            {
                itemSlot = i;
                break;
            }
        }

        return itemSlot;
    
    }
}
