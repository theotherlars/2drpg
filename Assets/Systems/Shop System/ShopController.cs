using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public List<UIItem> slotsInShop = new List<UIItem>();
    public Transform slotsPanel;

    public void AddNewItem(ShopItem item)
    {
        UpdateShop(slotsInShop.FindIndex(i => i.item == null), item.shopItem);
    }

    /*public void RemoveItem(Item_SO item)
    {
        UpdateShop(itemsInShop.FindIndex(i => i.item == item), null);
    }*/

    public void UpdateShop(int slot, Item_SO shopItem)
    {
        slotsInShop[slot].UpdateItem(shopItem);
    }

    public void CleanShop()
    {
        foreach (UIItem slot in slotsInShop)
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
