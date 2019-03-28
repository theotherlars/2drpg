using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UISellItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item_SO item;

    public List<Item_SO> stackedItems = new List<Item_SO>();
    private List<Item_SO> stackedItemsTemp = new List<Item_SO>();

    public TextMeshProUGUI stackNumber;

    private Image spriteImage;
    private UIItem selectedItem;
    private UIController uiController;
    private Tooltip tooltip;

    private void Awake()
    {
        spriteImage = GetComponent<Image>();
        selectedItem = GameObject.Find("SelectedItem").GetComponent<UIItem>();
        tooltip = FindObjectOfType<Tooltip>();
        uiController = FindObjectOfType<UIController>();
        stackNumber.text = "";
    }

    public void UpdateItem(Item_SO item)
    {
        this.item = item;

        if (item != null)
        {
            spriteImage.color = Color.white;
            spriteImage.sprite = this.item.ItemSprite;
            if (item.IsStackable)
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

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.gameObject.GetComponent<Image>().enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (selectedItem.item != null) // if slot is empty, but selected is not
        {
            string text = null;

            if (selectedItem.item.IsStackable)
            {
                PlaceItemsFromSelectedStack(); // Places all selected items in this item
                text = string.Format("Are you sure you want to sell \n {0} x {1} \n for {2} credits?", stackedItems.Count, item.ItemTitle,item.ItemSellPrice);
                uiController.LoadConfirmationDialouge(ConfirmationWindow.ConfirmationType.SellItem, text, item, stackedItems.Count);
            }
            else
            {
                Place(); // Places the single item in empty slot
                text = string.Format("Are you sure you want to sell \n {0} \n for {1} credits?", item.ItemTitle, item.ItemSellPrice);
                uiController.LoadConfirmationDialouge(ConfirmationWindow.ConfirmationType.SellItem, text, item, 0);
            }
        }
    }

    private void PlaceOneItemFromSelectedStack(int index)
    {
        if (this.stackedItems.Count < item.MaxStack)
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
        }
        else
        {
            for (int i = 0; i < itemsAdded; i++)
            {
                selectedItem.RemoveFromStack(selectedItem.stackedItems[selectedItem.stackedItems.Count - 1]);
            }

            if (selectedItem.stackedItems.Count < 1)
            {
                selectedItem.ResetItemStack();
            }
        }
    }

    private void Place() // Places the selected item to the selected slot
    {
        UpdateItem(selectedItem.item);
        selectedItem.stackedItems.Clear();
        selectedItem.UpdateItem(null);
    }

    private bool IsPointerOverUIObject()
    {
        var eventDataCurrentPosition = new PointerEventData(EventSystem.current)
        {
            position = new Vector2(Input.mousePosition.x, Input.mousePosition.y)
        };
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public void Disable()
    {
        UIInventory uiInventory = FindObjectOfType<UIInventory>();
        if (item != null)
        {
            if (stackedItems.Count >= 1)
            {
                for (int i = 0; i < stackedItems.Count; i++)
                {
                    uiInventory.AddNewItem(item);
                }
            }
            else
            {
                uiInventory.AddNewItem(item);
            }
        }

        ResetItemStack();
        stackNumber.text = "";
    }
}
