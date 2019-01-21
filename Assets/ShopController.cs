using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public List<Item_SO> shopInventory = new List<Item_SO>();
    public List<UIItem> slotsInShop = new List<UIItem>();
    public GameObject shopSlots;

    private void OnEnable()
    {
        
            slotsInShop.Add(shopSlots.GetComponentInChildren<UIItem>());
        
        //slotsInShop.Add(shopSlots.GetComponentInChildren<UIItem>());
        UpdateShop();
    }

    private void UpdateShop()
    {
        for (int i = 0; i < shopInventory.Count; i++)
        {
            slotsInShop[i].UpdateItem(shopInventory[i]);
        }
    }

    public void ToggleShop()
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
    }

}
