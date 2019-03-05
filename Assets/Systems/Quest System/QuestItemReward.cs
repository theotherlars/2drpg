using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class QuestItemReward : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Item_SO item;
    public Image image;

    //public List<Item_SO> itemStack = new List<Item_SO>();
    public TextMeshProUGUI stackText;
    public int stackCount;
    
    private Tooltip tooltip;
    private Outline outline;

    private void Awake()
    {
        tooltip = FindObjectOfType<Tooltip>();
        outline = GetComponentInParent<Outline>();
        outline.enabled = false; 
    }

    public void UpdateReward(Item_SO item, int stackAmount = 0)
    {
        this.item = item;
        
        if (item != null)
        {
            image.color = Color.white;
            image.sprite = item.ItemSprite;
            if (stackAmount != 0)
            {
                stackCount = stackAmount;
                stackText.enabled = true;
                stackText.text = stackAmount.ToString();
            }
            else
            {
                stackText.enabled = false;
                stackText.text = null;
            }
        }
        else
        {
            image.color = Color.clear;
            image.sprite = null;
            stackText.text = null;
            stackText.enabled = false;
        }
    }

    /*public void AddToStack(Item_SO item)
    {
        itemStack.Add(item);
        stackText.text = (itemStack.Count).ToString();
    }*/

    /*public void RemoveFromStack(Item_SO item)
    {
        if (itemStack.Count > 1)
        {
            itemStack.RemoveAt(itemStack.FindIndex(i => i == item));
        }
        else
        {
            ResetItem();
        }

        stackText.text = (itemStack.Count).ToString();
    }*/

    public void ResetItem()
    {
       // itemStack.Clear();
        stackCount = 0;
        UpdateReward(null);
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
