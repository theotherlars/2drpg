using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> characterItems = new List<Item>();
    public ItemDatabase itemDatabase;
    public UIInventory uiInventory;

    private void Start()
    {
        
        GiveItem(1);
        GiveItem(0);
        GiveItem(0);
        GiveItem(1);        
    }

    public void GiveItem(int id) // Adds item from ItemDatabase to characterItems list by id
    {
        Item itemToAdd = itemDatabase.GetItem(id);
        characterItems.Add(itemToAdd);
        uiInventory.AddNewItem(itemToAdd);
    }

    public void GiveItem(string itemTitle) // Adds item from ItemDatabase to characterItems list by title
    {
        Item itemToAdd = itemDatabase.GetItem(itemTitle);
        characterItems.Add(itemToAdd);
        uiInventory.AddNewItem(itemToAdd);
    }

    public Item CheckForItem(int id) // Checks if characterItems list contains item by id
    {
        return characterItems.Find(item => item.id == id);
    }

    //
    // TODO Check if the characterList contains 2x of same item, how to handle that...
    //
    public void RemoveItem(int id) // If characterItem list contains item, removes it
    {
        Item itemToRemove = CheckForItem(id);
        if (itemToRemove != null)
        {
            characterItems.Remove(itemToRemove);
            uiInventory.RemoveItem(itemToRemove);
        }
    }
}
