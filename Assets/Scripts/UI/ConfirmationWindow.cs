using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConfirmationWindow : MonoBehaviour
{
    public TextMeshProUGUI confirmationDialogue;

    private UIShopItem shopItem;
    private Item_SO item;
    private int amount;
    public enum ConfirmationType { BuyItem, SellItem, DeleteItem }
    private ConfirmationType type;

    public void ConfirmationDialogue(string input, UIShopItem shopItem = null)
    {
        confirmationDialogue.text = input;
        if (shopItem != null)
        {
            this.shopItem = shopItem;
        }
    }

    public void ConfirmationDialogue(ConfirmationType type, string input, Item_SO item = null, int stackAmount = 0)
    {
        confirmationDialogue.text = input;
        if (item != null)
        {
            this.item = item;
            this.amount = stackAmount;
            this.type = type;
        }
    }

    public void OK_Button()
    {
        if (shopItem != null)
        {
            shopItem.BuyItem();
        }
        else if (item != null)
        {
            Inventory inventory = FindObjectOfType<Inventory>();
            switch (type)
            {
                case ConfirmationType.DeleteItem:
                    {
                        inventory.RemoveItem(item.ItemID, amount);
                        break;
                    }
                case ConfirmationType.SellItem:
                    {
                        UISellItem sellItem = FindObjectOfType<UISellItem>();
                        inventory.IncreaseMoney(item.ItemSellPrice);
                        inventory.SellItem(item.ItemID, amount);
                        sellItem.ResetItemStack();
                        //sellItem.UpdateItem(null);
                        break;
                    }
                default:
                    {
                        FindObjectOfType<UIController>().LoadErrorText("An Error Occured...");
                        break;
                    }
            }
        }
        this.gameObject.SetActive(false);
    }

    public void Cancel_Button()
    {
        UIInventory uiInventory = FindObjectOfType<UIInventory>();
        if(type == ConfirmationType.SellItem && item != null)
        {
            if (amount > 0)
            {
                for (int i = 0; i < amount; i++)
                {
                    uiInventory.AddNewItem(item);
                }
            }
            else
            {
                uiInventory.AddNewItem(item);
            }

            UISellItem sellItem = FindObjectOfType<UISellItem>();
            sellItem.ResetItemStack();
            //sellItem.UpdateItem(null);
        }

        shopItem = null;
        item = null;
        this.gameObject.SetActive(false);
    }
}
