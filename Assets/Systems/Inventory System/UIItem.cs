using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UIItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item_SO item;

    public List<Item_SO> stackedItems = new List<Item_SO>();
    private List<Item_SO> stackedItemsTemp = new List<Item_SO>();

    public TextMeshProUGUI stackNumber;

    private Image spriteImage;
    private UIItem selectedItem;
    private Tooltip tooltip;

    private void Awake()
    {
        spriteImage = GetComponent<Image>();
        selectedItem = GameObject.Find("SelectedItem").GetComponent<UIItem>();
        tooltip = FindObjectOfType<Tooltip>();
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
            stackedItems.RemoveAt(stackedItems.FindLastIndex(i => i == item));
        }
        else
        {
            stackedItems.Clear();
            UpdateItem(null);
        }
        stackNumber.text = (stackedItems.Count).ToString();
    }

    public void ResetItemStack()
    {
        stackedItems.Clear();
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
        if (this.item != null)
        {
            if (Input.GetKey(KeyCode.LeftShift) && item.IsStackable)
            {
                if (selectedItem.item != null && selectedItem.item.IsStackable)
                {
                    selectedItem.AddToStack(this.item);
                    this.RemoveFromStack(this.item);
                }
                else
                {
                    selectedItem.UpdateItem(this.item);
                    selectedItem.AddToStack(this.item);
                    this.RemoveFromStack(this.item);
                }
            }
            else
            {
                if (selectedItem.item != null)
                {
                    if (this.item.ItemID == selectedItem.item.ItemID && this.item.IsStackable)
                    {
                        for (int i = 0; i < selectedItem.stackedItems.Count; i++)
                        {
                            print(selectedItem.stackedItems.Count);
                            this.AddToStack(selectedItem.stackedItems[i]);
                            selectedItem.RemoveFromStack(selectedItem.stackedItems[i]);
                        }
                    }
                    else
                    {
                        Swap();
                    }
                }
                else
                {
                    PickUp();
                }
            }
        }
        else if (selectedItem.item != null)
        {
            Place();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.gameObject.GetComponent<Image>().enabled = false;
    }


    private void Place() // Places the selected item to the selected slot
    {
        UpdateItem(selectedItem.item);
        for (int i = 0; i < selectedItem.stackedItems.Count; i++)
        {
            AddToStack(selectedItem.stackedItems[i]);
        }
        selectedItem.stackedItems.Clear();
        selectedItem.UpdateItem(null);
    }

    private void PickUp() // This will pick up selected item
    {
        selectedItem.UpdateItem(this.item);
        for (int i = 0; i < this.stackedItems.Count; i++)
        {
            selectedItem.AddToStack(this.stackedItems[i]);
        }
        stackedItems.Clear();
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
