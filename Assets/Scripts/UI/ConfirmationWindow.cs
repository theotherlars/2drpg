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

    public void ConfirmationDialogue(string input, UIShopItem shopItem = null)
    {
        confirmationDialogue.text = input;
        if (shopItem != null)
        {
            this.shopItem = shopItem;
        }
    }

    public void ConfirmationDialogue(string input, Item_SO item = null, int stackAmount = 0)
    {
        confirmationDialogue.text = input;
        if (item != null)
        {
            this.item = item;
            this.amount = stackAmount;
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
            FindObjectOfType<Inventory>().RemoveItem(item.ItemID, amount);
        }
        this.gameObject.SetActive(false);
    }

    public void Cancel_Button()
    {
        shopItem = null;
        item = null;
        this.gameObject.SetActive(false);
    }
}
