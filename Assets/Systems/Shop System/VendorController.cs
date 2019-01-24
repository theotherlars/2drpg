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

    public ShopItem(Item_SO item, int itemPrice)
    {
        this.shopItem = item;
        this.price = itemPrice;
    }
}
