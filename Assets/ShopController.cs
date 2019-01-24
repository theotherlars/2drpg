using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public List<Item_SO> shopInventory = new List<Item_SO>();
    public List<UIItem> slotsInShop = new List<UIItem>();
    private GameObject shopSlots;


    private void OnEnable()
    {
        shopSlots = GetComponentInChildren<Transform>().gameObject;
        print(shopSlots.name);


        foreach (GameObject slot in shopSlots.GetComponentInChildren<Transform>())
        {
            print(slot.name);
        }
        //
        UpdateShop();
    }

    private void UpdateShop()
    {
        for (int i = 0; i < shopInventory.Count; i++)
        {
            slotsInShop[i].UpdateItem(shopInventory[i]);
        }
    }

    public void OpenShop()
    {
        this.gameObject.SetActive(true);
    }

}
