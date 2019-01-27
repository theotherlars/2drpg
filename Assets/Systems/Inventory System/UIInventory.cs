using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    public List<UIItem> uiItems = new List<UIItem>();
    //public List<Item_SO> itemStack = new List<Item_SO>();

    public GameObject slotPrefab;
    public Transform slotPanel;
    public int numberOfSlots = 16;

    private Image backgroundImage;

    private void Awake()
    {
        backgroundImage = GetComponent<Image>();
        backgroundImage.enabled = false;

        for (int i = 0; i < numberOfSlots; i++)
        {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(slotPanel);
            uiItems.Add(instance.GetComponentInChildren<UIItem>());
            uiItems[i].UpdateItem(null);
        }
        backgroundImage.enabled = true;
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
    /*
    public void AddNewItem(Item_SO item)
    {
        UpdateSlot(uiItems.FindIndex(i => i.item == null), item);
    }

    public void RemoveItem(Item_SO item)
    {
        UpdateSlot(uiItems.FindIndex(i => i.item == item), null);
    }

    */

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
                            return; // stop loop
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
}
