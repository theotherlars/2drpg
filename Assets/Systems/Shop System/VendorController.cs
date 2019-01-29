using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorController : MonoBehaviour
{
    public List<ShopItem> shopInventory = new List<ShopItem>();
}

[System.Serializable]
public class ShopItem
{
    public Item_SO shopItem;
    public int price;
    public int stackAmount;

    public ShopItem(Item_SO item, int itemPrice, int stackAmount)
    {
        this.shopItem = item;
        this.price = itemPrice;
        this.stackAmount = stackAmount;
    }
}
