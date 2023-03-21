using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; } // static instance of the inventory

    public List<string> keys = new List<string>(); // list of key names the player has collected
    public Text inventoryText; // UI Text component to display inventory

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Add a key to the inventory
    public void AddKey(string keyName)
    {
        if (!keys.Contains(keyName))
        {
            keys.Add(keyName);
            UpdateInventory();
        }
    }

    // Check if the inventory contains a key
    public bool HasKey(Key key)
    {
        return keys.Contains(key.keyName);
    }

    // Update the inventory display
    private void UpdateInventory()
    {
        string inventoryString = "Inventory: ";
        foreach (string keyName in keys)
        {
            inventoryString += keyName + ", ";
        }
        inventoryText.text = inventoryString;
    }
}
