using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public List<UIShopItem> slotsInShop = new List<UIShopItem>();
    public Transform slotsPanel;

    public void AddNewItem(ShopItem shopItem)
    {
        UpdateShop(slotsInShop.FindIndex(i => i.item == null), shopItem);
    }

    /*public void RemoveItem(Item_SO item)
    {
        UpdateShop(itemsInShop.FindIndex(i => i.item == item), null);
    }*/

    public void UpdateShop(int slot, ShopItem shopItem)
    {
        slotsInShop[slot].DeclearShopItem(shopItem);
    }

    public void CleanShop()
    {
        foreach (UIShopItem slot in slotsInShop)
        {
            slot.UpdateItem(null);
        }
        //slotsInShop.Clear();
    }

    public void OpenShop(VendorController vendorController)
    {
        for (int i = 0; i < vendorController.shopInventory.Count; i++)
        {
            AddNewItem(vendorController.shopInventory[i]);
        }
    }

    public void CloseShop()
    {
        CleanShop();
        this.gameObject.SetActive(false);
    }

}
