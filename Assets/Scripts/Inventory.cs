using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<string> keys = new List<string>(); // list of key names the player has collected
    public Text inventoryText; // UI Text component to display inventory

    // Add a key to the inventory
    public void AddKey(string keyName)
    {
        if (!keys.Contains(keyName))
        {
            keys.Add(keyName);
            UpdateInventory();
        }
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
