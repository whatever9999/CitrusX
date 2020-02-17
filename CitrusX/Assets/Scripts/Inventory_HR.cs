using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using System;

public class Inventory_HR : MonoBehaviour
{
    public enum Names 
    {
      WaterJug
    }

    public KeyCode inventoryOpenKey = KeyCode.I;
    public KeyCode[] inventoryCloseKeys = { KeyCode.I, KeyCode.Escape };

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
        foreach (GameObject inventoryPanels in GameObject.FindGameObjectsWithTag("InventoryPanel"))
        {
            inventoryItems[i] = inventoryPanels;
            i++;
        }

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
            if (inventoryItems[i].transform.GetChild(1).GetComponent<Text>().text == "")
            {
                inventoryItems[i].transform.GetChild(0).GetComponent<Image>().sprite = items[itemName];
                inventoryItems[i].transform.GetChild(1).GetComponent<Text>().text = itemName.ToString();
                break;
            }
        }
    }

    public void RemoveItem(int slot) 
    {
        inventoryItems[slot].transform.GetChild(0).GetComponent<Image>().sprite = null; 
        inventoryItems[slot].transform.GetChild(1).GetComponent<Text>().text = "";
    }

    public int CheckItem(string itemName) 
    {
        int itemSlot = -1;

        for (int i = 0; i < maxItems; i++)
        {
            if (inventoryItems[i].transform.GetChild(1).GetComponent<Text>().text == itemName)
            {
                itemSlot = i;
                break;
            }
        }

        return itemSlot;
    
    }
}
