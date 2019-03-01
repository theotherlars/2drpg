using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the class that controlls the backend of the players inventory, this is storing if the player has the item or not.

    // TODO:
    // - Save and Load items that player has.
    // - Implement money

public class Inventory : MonoBehaviour
{
    public List<Item_SO> characterItems = new List<Item_SO>(); // Creates a list to store the players items
    public ItemDatabase itemDatabase;
    public UIInventory uiInventory; // This is controlling / displaying the items in Inventory

    public int characterMoney;

    public GameEvent OnItemAdd;
    public GameEvent OnItemRemove;

    private void Start()
    {
        
        GiveItem(1); // FOR TESTING
        GiveItem(2); // FOR TESTING
        GiveItem(3); // FOR TESTING
        GiveItem(4); // FOR TESTING
        GiveItem(4); // FOR TESTING
        GiveItem(4); // FOR TESTING
        GiveItem(4); // FOR TESTING
    }

    public void GiveItem(int id) // Adds item from ItemDatabase to characterItems list by id
    {
        Item_SO itemToAdd = itemDatabase.GetItem(id);
        if (itemToAdd != null && !uiInventory.CheckIfInventoryIsFull())
        {
            characterItems.Add(itemToAdd);
            uiInventory.AddNewItem(itemToAdd);
            OnItemAdd.Raised();
        }
        else if (uiInventory.CheckIfInventoryIsFull())
        {
            UIController uiController = FindObjectOfType<UIController>();
            uiController.LoadErrorText("Inventory is full");
        }
    }

    public void GiveItem(string itemTitle) // Adds item from ItemDatabase to characterItems list by title
    {
        Item_SO itemToAdd = itemDatabase.GetItem(itemTitle);
        if (itemToAdd != null && !uiInventory.CheckIfInventoryIsFull())
        {
            characterItems.Add(itemToAdd);
            uiInventory.AddNewItem(itemToAdd);
            OnItemAdd.Raised();
        }
        else if (uiInventory.CheckIfInventoryIsFull())
        {
            UIController uiController = FindObjectOfType<UIController>();
            uiController.LoadErrorText("Inventory is full");
        }
    }

    public Item_SO CheckForItem(int id) // Checks if characterItems list contains item by id
    {
        return characterItems.Find(item => item.ItemID == id);
    }
    
    public void RemoveItem(int id) // If characterItem list contains item, removes it
    {
        Item_SO itemToRemove = CheckForItem(id);
        if (itemToRemove != null)
        {
            characterItems.Remove(itemToRemove);
            uiInventory.RemoveItem(itemToRemove);
            OnItemRemove.Raised();
        }
    }

    public void IncreaseMoney(int amount)
    {
        characterMoney += amount;
    }

    public void DecreaseMoney(int amount)
    {
        if(characterMoney >= amount)
        {
            characterMoney -= amount;
        }
    }

    public int CheckMoney()
    {
        return characterMoney;
    }
}
