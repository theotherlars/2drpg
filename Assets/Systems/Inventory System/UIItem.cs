using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UIItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item_SO item;

    public List<Item_SO> stackedItems = new List<Item_SO>(); // Testing

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
        //if (this.item.ItemID == item.ItemID && stackedItems.Count < this.item.MaxStack)
        stackedItems.Add(item);
        stackNumber.text = (stackedItems.Count).ToString();
    }

    public void ResetItemStack()
    {
        stackedItems.Clear();
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (this.item != null)
        {
            if (selectedItem.item != null)
            {
                Item_SO clone = Item_SO.CreateInstance(selectedItem.item);
                selectedItem.UpdateItem(this.item);
                UpdateItem(clone);
            }
            else
            {
                selectedItem.UpdateItem(this.item);
                for (int i = 0; i < this.stackedItems.Count; i++)
                {
                    selectedItem.AddToStack(this.stackedItems[i]);
                }
                stackedItems.Clear();
                UpdateItem(null);
            }
        }
        else if (selectedItem.item != null)
        {
            UpdateItem(selectedItem.item);
            for (int i = 0; i < selectedItem.stackedItems.Count; i++)
            {
                AddToStack(selectedItem.stackedItems[i]);
                //stackedItems.Add(selectedItem.stackedItems[i]);
            }
            selectedItem.stackedItems.Clear();
            selectedItem.UpdateItem(null);
            
        }
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
}
