using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public List<UIShopItem> slotsInShop = new List<UIShopItem>();
    public Transform slotsPanel;
    private int pageDisplaying = 1;
    //private VendorController vendorController;
    private NPCInformation npcInfo;
    [SerializeField]
    private Button nextButton;
    [SerializeField]
    private Button previousButton;
    [SerializeField]
    private UISellItem sellSlot;

    private void Update()
    {
        if (npcInfo.npc.shopItems.Count > slotsInShop.Count * pageDisplaying)
        {
            nextButton.interactable = true;
        }
        else
        {
            nextButton.interactable = false;
        }
        if (pageDisplaying >= 2)
        {
            previousButton.interactable = true;
        }
        else
        {
            previousButton.interactable = false;
        }
    }

    public void AddNewItem(ShopItem shopItem)
    {
        if (shopItem != null)
        {
            UpdateShop(slotsInShop.FindIndex(i => i.item == null), shopItem);
        }
        else
        {
            return;
        }
        
    }
    
    public void UpdateShop(int slot, ShopItem shopItem)
    {
        slotsInShop[slot].DeclearShopItem(shopItem);
    }

    public void CleanShop()
    {
        foreach (UIShopItem slot in slotsInShop)
        {
            slot.UpdateItem(null);
            slot.ResetItemStack();
        }
    }

    /*public void OpenShop(VendorController vendorController)
    {
        this.vendorController = vendorController;
        
        if (this.enabled)
        {
            if (vendorController.shopInventory.Count <= 8)
            {
                previousButton.interactable = false;
                nextButton.interactable = false;
                for (int i = 0; i < vendorController.shopInventory.Count; i++)
                {
                    AddNewItem(vendorController.shopInventory[i]);
                }
            }
            else if (vendorController.shopInventory.Count > 8)
            {
                pageDisplaying = 1;
                previousButton.interactable = false;
                nextButton.interactable = true;

                for (int i = 0; i < slotsInShop.Count; i++)
                {
                    AddNewItem(vendorController.shopInventory[i]);
                }
            }
        }
        else
        {
            this.enabled = true;
            OpenShop(vendorController);
        }
    }*/

    public void OpenShop(NPCInformation npcInformation)
    {
        npcInfo = npcInformation;

        if (this.enabled)
        {
            if (npcInfo.npc.shopItems.Count <= 8)
            {
                previousButton.interactable = false;
                nextButton.interactable = false;
                for (int i = 0; i < npcInfo.npc.shopItems.Count; i++)
                {
                    AddNewItem(npcInfo.npc.shopItems[i]);
                }
            }
            else if (npcInfo.npc.shopItems.Count > 8)
            {
                pageDisplaying = 1;
                previousButton.interactable = false;
                nextButton.interactable = true;

                for (int i = 0; i < slotsInShop.Count; i++)
                {
                    AddNewItem(npcInfo.npc.shopItems[i]);
                }
            }
        }
        else
        {
            this.enabled = true;
            OpenShop(npcInformation);
        }
    }

    public void NextPage()
    {
        if (npcInfo.npc.shopItems.Count > slotsInShop.Count * pageDisplaying)
        {
            pageDisplaying++;
            int itemRangeToDisplay = slotsInShop.Count * pageDisplaying;
            CleanShop();
            for (int i = 0; i < slotsInShop.Count; i++)
            {
                int itemToAdd = (itemRangeToDisplay - 8) + i;
                if (itemToAdd < npcInfo.npc.shopItems.Count)
                {
                    AddNewItem(npcInfo.npc.shopItems[itemToAdd]);
                }
                else
                { AddNewItem(null); }
            }

            previousButton.interactable = true;
        }
        else
        {
            //nextButton.interactable = false;
            return;
        }
        
    }

    public void PreviousPage()
    {
        if (pageDisplaying >= 2)
        {
            CleanShop();
            pageDisplaying--;
            int itemRangeToDisplay = slotsInShop.Count * pageDisplaying;
            for (int i = 0; i < slotsInShop.Count; i++)
            {
                AddNewItem(npcInfo.npc.shopItems[(itemRangeToDisplay - 8) + i]);
            }
        }
        else
        {
            return;
        }
    }

    public void CloseShop()
    {
        CleanShop();
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        sellSlot.Disable();
    }
}
