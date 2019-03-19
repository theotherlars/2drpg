using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIInventory : MonoBehaviour
{
    public List<UIItem> uiItems = new List<UIItem>();

    private Inventory inventory;

    public GameObject slotPrefab;
    public Transform slotPanel;
    public int numberOfSlots = 16;

    public Image backgroundImage;
    private TextMeshProUGUI moneyText;
    public GameObject currencyDisplay;

    private void Awake()
    {
        backgroundImage = GetComponent<Image>();
        backgroundImage.enabled = false;
        currencyDisplay.SetActive(false);
        inventory = FindObjectOfType<Inventory>();

        for (int i = 0; i < numberOfSlots; i++)
        {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(slotPanel);
            uiItems.Add(instance.GetComponentInChildren<UIItem>());
            uiItems[i].UpdateItem(null);
        }
        //backgroundImage.enabled = true;
        //currencyDisplay.SetActive(true);

        moneyText = currencyDisplay.GetComponentInChildren<TextMeshProUGUI>();

        inventory.IncreaseMoney(50); // This is just for TESTING!!
    }

    private void LateUpdate()
    {
        moneyText.text = inventory.characterMoney.ToString();
    }

    public void UpdateSlot(int slot, Item_SO item)
    {
        uiItems[slot].UpdateItem(item);
    }

    public void AddToStack(int slot, Item_SO item)
    {
        uiItems[slot].AddToStack(item);
    }

    public void RemoveFromStack(int slot, Item_SO item)
    {
        uiItems[slot].RemoveFromStack(item);
    }

    public void AddNewItem(Item_SO item)
    {
        if (item.IsStackable)
        {
            for (int i = 0; i < uiItems.Count; i++) // For each inventory slot
            {
                if(uiItems[i].item != null) // if the slot is not null
                {
                    if (uiItems[i].item.ItemID == item.ItemID) // if that item in slot's itemID equals input item.itemID
                    {
                        if (uiItems[i].stackedItems.Count < uiItems[i].item.MaxStack)
                        {
                            AddToStack(i, item); // Add to stack instead of slot
                            return; // Exit method and loop
                        }
                    }
                }
            }
            int nextSlot = uiItems.FindIndex(i => i.item == null);
            UpdateSlot(nextSlot, item); // if the loop goes all the way through and could not find item, them add to first available slot 
            AddToStack(nextSlot, item); // adds item to itemStack
        }
        else
        {
            UpdateSlot(uiItems.FindIndex(i => i.item == null), item);
        }
    }

    public void RemoveItem(Item_SO item)
    {
        if (item.IsStackable)
        {
            RemoveFromStack(uiItems.FindIndex(i => i.item == item), item);
        }
        else
        {
            UpdateSlot(uiItems.FindIndex(i => i.item == item), null);
        }
    }

    /*public bool CheckIfInventoryIsFull()
    {
        int freeSlots = 0;

        for (int i = 0; i < uiItems.Count; i++)
        {
            if (uiItems[i].item == null)
            {
                freeSlots++;
            }
        }

        if (freeSlots > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
        
    }*/

    public bool SpaceLeftInInventory(Item_SO itemToCheck, int stackAmount = 0)
    {
        // Return true if there is space left in inventory
        // Return false if there isn't anymore space in inventory
        // 1 - Handle nonStackable items (check if there is one slot free)
        // 2 - Handle stacable items (check if there is any free slot, if there is one, put the itemstack there, if not, check through all the
        //      stackable items that matches the item ID, and see if there is stack place left.)
        //      example: if slot1 has four potions, and slot2 has one potion, the maxStack is 8 and you want to put in 10 potions. Add four to slot1 and 6 to slot2

        if (stackAmount == 0 && !itemToCheck.IsStackable)
        {
            // Handles the "nonstackable" items, returns true if there is space left, false if not.
            int freeslots = 0;
            for (int i = 0; i < uiItems.Count; i++)
            {
                if (uiItems[i].item == null) { freeslots++; }
            }

            if (freeslots > 0)
            { return true; }
            else
            { return false; }
        }
        else if (stackAmount > 0 && itemToCheck.IsStackable)
        {
            // Handles stackable items, returns true if there is a whole slot free or if inventory has the same item, but not full stacks
            int freeslots = 0;
            int stackSlotsAvailable = 0;
            for (int i = 0; i < uiItems.Count; i++)
            {
                if (uiItems[i].item == null)
                {
                    freeslots++;
                }
                else
                {
                    if (uiItems[i].item.ItemID == itemToCheck.ItemID)
                    {
                        stackSlotsAvailable += uiItems[i].item.MaxStack - uiItems[i].stackedItems.Count;
                    }
                }

                
                
            }

            if (freeslots > 0)
            { return true; }
            else
            {
                if (stackSlotsAvailable >= stackAmount)
                { return true; }
                else
                {
                    UIController uiController = FindObjectOfType<UIController>();
                    uiController.LoadErrorText("Inventory is full");
                    return false;
                }
            }
        }
        else
        {
            UIController uiController = FindObjectOfType<UIController>();
            uiController.LoadErrorText("Inventory is full");
            return false;
        }
    }
}
