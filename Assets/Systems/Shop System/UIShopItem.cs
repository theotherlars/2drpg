using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UIShopItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item_SO item;
    private ShopItem shopItem;
    private UIController uIController;
    private Inventory inventory;
    private UIInventory uIInventory;

    public List<Item_SO> stackedItems = new List<Item_SO>();
    private List<Item_SO> stackedItemsTemp = new List<Item_SO>();

    public TextMeshProUGUI stackNumber;
    public TextMeshProUGUI text_itemPrice;
    public TextMeshProUGUI text_itemName;

    private Image spriteImage;
    private UIItem selectedItem;
    private Tooltip tooltip;

    // Item details
    private int itemPrice;

    private void Awake()
    {
        spriteImage = GetComponent<Image>();
        selectedItem = GameObject.Find("SelectedItem").GetComponent<UIItem>();
        tooltip = FindObjectOfType<Tooltip>();
        uIController = FindObjectOfType<UIController>();
        inventory = FindObjectOfType<Inventory>();
        uIInventory = FindObjectOfType<UIInventory>();
        stackNumber.enabled = false;
    }

    public void DeclearShopItem(ShopItem itemInput)
    {
        this.shopItem = itemInput;
        UpdateItem(itemInput.shopItem);
        for (int i = 0; i < itemInput.stackAmount; i++)
        {
            AddToStack(itemInput.shopItem);
        }
    }

    public void UpdateItem(Item_SO item)
    {
        this.item = item;

        if (item != null)
        {
            // Sprite related
            spriteImage.color = Color.white;
            spriteImage.sprite = this.item.ItemSprite;
            
            // Item name related
            text_itemName.GetComponent<Transform>().gameObject.SetActive(true);
            text_itemName.text = this.item.ItemTitle;

            // Item price related
            itemPrice = this.shopItem.price;
            text_itemPrice.GetComponent<Transform>().gameObject.SetActive(true);
            text_itemPrice.text = itemPrice.ToString();

            if (this.item.IsStackable)
            {    
                stackNumber.enabled = true;
            }
            else
            {
                stackNumber.enabled = false;
            }
        }
        else
        {
            spriteImage.color = Color.clear;
            spriteImage.sprite = null;
            text_itemName.GetComponent<Transform>().gameObject.SetActive(false);
            text_itemPrice.GetComponent<Transform>().gameObject.SetActive(false);
            stackNumber.enabled = false;
        }
    }


    public void AddToStack(Item_SO item)
    {
        stackedItems.Add(item);
        stackNumber.text = (stackedItems.Count).ToString();
    }

    public void RemoveFromStack(Item_SO item)
    {
        if (stackedItems.Count > 1)
        {
            stackedItems.RemoveAt(stackedItems.FindIndex(i => i == item));
        }
        else
        {
            ResetItemStack();
        }

        stackNumber.text = (stackedItems.Count).ToString();
    }

    public void ResetItemStack()
    {
        stackedItems.Clear();
        UpdateItem(null);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.item != null)
        {
            tooltip.gameObject.GetComponent<Image>().enabled = true;
            tooltip.GenerateTooltip(this.item);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        /* METHODS:
            - PickUp(); - Picks up a nonstackable item and updates both this item and selected item.
            - PickUpOneItem(); - Picks up one item in stack list, adds to selected item and removes from this item stack
            - PickUpAllItems(); - Picks up all items in stack list, adds them to selected items stack and removes from this item stack. Updates this and selected item with item
            - Swap(); - Swaps if item.itemID is equal to selected.item.itemID
            - Place(); - Places item and items in stack, updates this item and selected item
            - AddStackFromSelected(); - Adds items from selected stack to this item stack, but stops if this item stack is full
            - AddOneItemToStackFromSelected(); - Adds one item from selected list to this list
         */

        if (this.item != null) // if slot is empty or not 
        {
            if (selectedItem.item != null) // if selected slot is empty or not
            {
                if (this.item.IsStackable && selectedItem.item.IsStackable) // if both the selected item and item in slot is stackable
                {
                    if (this.item.ItemID == selectedItem.item.ItemID)
                    {
                        if (Input.GetKey(KeyCode.LeftShift))
                        {
                            if(selectedItem.stackedItems.Count < this.item.MaxStack)
                            {
                                PickUpOneItem(); // picks up one item until the stack is full
                            }
                        }
                        else
                        {
                            PlaceItemsFromSelectedStack(); // Adds items to this item stack until its full
                        }
                    }
                    else
                    {
                        Swap(); // Swaps items with selected items if doesn't match itemID
                    }
                }
                else
                {
                    Swap(); // Swaps item with selected item, either if it stackable or not
                }
            }
            else 
            {
                if (this.item.IsStackable) // if the item you are picking up is stackable
                {
                    string dialogue = string.Format("Do you want to buy: {0} x{1} for {2} credits ?", item.ItemTitle,shopItem.stackAmount,itemPrice);
                    uIController.LoadConfirmationDialouge(dialogue, this);
                    //PickUpAllItems(); // picks up all stacked items and clears this item stack
                }
                else
                {
                    string dialogue = string.Format("Do you want to buy: {0} for {1} credits ?", item.ItemTitle,itemPrice);
                    uIController.LoadConfirmationDialouge(dialogue, this);

                    //PickUp(); // picks up non stackable item
                }
            }
        }
        else if (selectedItem.item != null) // if slot is empty, but selected is not
        {
            if (selectedItem.item.IsStackable)
            {
                PlaceItemsFromSelectedStack(); // Places all selected items in this item
            }
            else
            {
                Place(); // Places the single item in empty slot
            }
        }
    }

    private void PlaceOneItemFromSelectedStack(int index)
    {
        if(this.stackedItems.Count < item.MaxStack)
        {
            this.AddToStack(selectedItem.stackedItems[index]);
            selectedItem.RemoveFromStack(selectedItem.stackedItems[index]);
        }
    }

    private void PlaceItemsFromSelectedStack()
    {
        UpdateItem(selectedItem.item);
        int itemsAdded = 0;

        for (int i = 0; i < selectedItem.stackedItems.Count; i++)
        {
            if (this.stackedItems.Count >= this.item.MaxStack)
            {
                break;
            }
            else
            {
                itemsAdded++;
                this.AddToStack(selectedItem.stackedItems[i]);
            }    
        }
        UpdateItem(selectedItem.item);

        if (selectedItem.stackedItems.Count < 1)
        {
            selectedItem.ResetItemStack();
            selectedItem.UpdateItem(null);
        }
        else
        {
            for (int i = 0; i < itemsAdded; i++)
            {
                selectedItem.RemoveFromStack(selectedItem.stackedItems[selectedItem.stackedItems.Count -1]);
            }

            if (selectedItem.stackedItems.Count <= 0)
            {
                selectedItem.ResetItemStack();
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.gameObject.GetComponent<Image>().enabled = false;
    }

    public void BuyItem()
    {
        if (inventory.CheckMoney() >= shopItem.price)
        {
            bool successfull = false;

            if (item.IsStackable)
            {
                if (uIInventory.CheckIfFreeSpaceForStack(shopItem.shopItem, shopItem.stackAmount))
                {
                    for (int i = 0; i < shopItem.stackAmount; i++)
                    {
                            inventory.GiveItem(item.ItemID);
                            successfull = true;
                    }
                }
                else
                {
                    uIController.LoadErrorText("Inventory is full");
                    successfull = false;
                }
            }
            else
            {
                if (!uIInventory.CheckIfInventoryIsFull())
                {
                    inventory.GiveItem(item.ItemID);
                    successfull = true;
                }
                else
                {
                    uIController.LoadErrorText("Inventory is full");
                    successfull = false;
                }
            }

            if (successfull)
            {
                inventory.DecreaseMoney(shopItem.price);
            }
        }
        else
        {
            string text = "You don't have enough credit...";
            uIController.LoadErrorText(text);
        }
    }

    private void PickUpAllItems()
    {
        // Picks up all items in a stack, adds them to the selected item, removed from item stack.
        
        for (int i = 0; i < this.stackedItems.Count; i++)
        {
            selectedItem.AddToStack(this.stackedItems[i]);
        }
        selectedItem.UpdateItem(this.item);
        this.stackedItems.Clear();
        UpdateItem(null);
    }

    private void PickUpOneItem()
    {
        // picks up one item and adds to selected stack, removes one from this item stack.
   
        if (selectedItem.stackedItems.Count < 1)
        {
            selectedItem.UpdateItem(this.item);
        }
        selectedItem.AddToStack(this.item);
        this.RemoveFromStack(this.item);

    }

    private void Place() // Places the selected item to the selected slot
    {
        UpdateItem(selectedItem.item);
        selectedItem.stackedItems.Clear();
        selectedItem.UpdateItem(null);
    }

    private void PickUp() // This will pick up selected item
    {
        selectedItem.UpdateItem(this.item);
        UpdateItem(null);
    }

    private void Swap() // This will replace the object you are holding with the one in slot
    {
        Item_SO clone = Item_SO.CreateInstance(selectedItem.item);

        for (int i = 0; i < selectedItem.stackedItems.Count; i++) // Adds selected.stackeditems to temp list
        {
            stackedItemsTemp.Add(selectedItem.stackedItems[i]);
        }

        selectedItem.ResetItemStack(); // Empties selected item stack

        for (int i = 0; i < this.stackedItems.Count; i++) // Adds this.stackeditems to selected
        {
            selectedItem.AddToStack(this.stackedItems[i]);
        }
        selectedItem.UpdateItem(this.item);

        this.ResetItemStack(); // Empties this item stack

        for (int i = 0; i < this.stackedItemsTemp.Count; i++) // Adds tempItems to stacked items list
        {
            this.AddToStack(this.stackedItemsTemp[i]);
        }

        UpdateItem(clone);

        this.stackedItemsTemp.Clear(); // Empties the temp item stack
    }


}
