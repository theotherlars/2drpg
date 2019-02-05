using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class CharacterSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item_SO item;
    private Image spriteImage;
    private UIItem selectedItem;
    private Tooltip tooltip;
    private PlayerController player;
   

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        spriteImage = GetComponent<Image>();
        //selectedItem = GameObject.Find("SelectedItem").GetComponent<UIItem>();
        tooltip = GameObject.Find("Tooltip").GetComponent<Tooltip>();

    }
    private void Start()
    {
        selectedItem = GameObject.Find("SelectedItem").GetComponent<UIItem>();
    }

    public void UpdateItem(Item_SO item)
    {
        this.item = item;

        if (item != null)
        {
            spriteImage.color = Color.white;
            spriteImage.sprite = this.item.ItemSprite;
            AddStatsToPlayer(item);
        }
        else
        {
            spriteImage.color = Color.clear;
            spriteImage.sprite = null;
        }
    }

    private void AddStatsToPlayer(Item_SO item_SO)
    {
        for (int i = 0; i < item_SO.Attributes.Count; i++)
        {
            switch(item_SO.Attributes[i].attribute.attributeName)
            {
                case "Stamina":
                    {
                        player.UpdatePlayerAttributes(item_SO.Attributes[i].attribute.attributeName, item_SO.Attributes[i].amount);
                        break;
                    }
                case "Strength":
                    {
                        player.UpdatePlayerAttributes(item_SO.Attributes[i].attribute.attributeName, item_SO.Attributes[i].amount);
                        break;
                    }
                case "Spirit":
                    {
                        player.UpdatePlayerAttributes(item_SO.Attributes[i].attribute.attributeName, item_SO.Attributes[i].amount);
                        break;
                    }
                case "Agility":
                    {
                        player.UpdatePlayerAttributes(item_SO.Attributes[i].attribute.attributeName, item_SO.Attributes[i].amount);
                        break;
                    }
                default:
                {
                    break;
                }
            }
        }
        
    }

    private void RemoveStatsFromPlayer(Item_SO item_SO)
    {
        for (int i = 0; i < item_SO.Attributes.Count; i++)
        {
            switch (item_SO.Attributes[i].attribute.attributeName)
            {
                case "Stamina":
                    {
                        player.UpdatePlayerAttributes(item_SO.Attributes[i].attribute.attributeName, -item_SO.Attributes[i].amount);
                        break;
                    }
                case "Strength":
                    {
                        player.UpdatePlayerAttributes(item_SO.Attributes[i].attribute.attributeName, -item_SO.Attributes[i].amount);
                        break;
                    }
                case "Spirit":
                    {
                        player.UpdatePlayerAttributes(item_SO.Attributes[i].attribute.attributeName, -item_SO.Attributes[i].amount);
                        break;
                    }
                case "Agility":
                    {
                        player.UpdatePlayerAttributes(item_SO.Attributes[i].attribute.attributeName, -item_SO.Attributes[i].amount);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {   
        if (this.item != null)
        {
            if (selectedItem.item != null && this.tag.Replace("Slot", "") == selectedItem.item.ItemWeaponArmor.ToString())
            {
                Item_SO clone = Item_SO.CreateInstance(selectedItem.item);
                RemoveStatsFromPlayer(this.item);

                selectedItem.UpdateItem(this.item);
                UpdateItem(clone);
            }
            else
            {
                RemoveStatsFromPlayer(this.item);

                selectedItem.UpdateItem(this.item);
                UpdateItem(null);
            }
        }
        else if (selectedItem.item != null && this.tag.Replace("Slot", "") == selectedItem.item.ItemWeaponArmor.ToString()) // Checks if the item selected is same category as itemslot
        {
            
            UpdateItem(selectedItem.item);
            selectedItem.UpdateItem(null);
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.item != null)
        {
            //tooltip.gameObject.SetActive(true);
            tooltip.gameObject.GetComponent<Image>().enabled = true;
            tooltip.GenerateTooltip(this.item);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //tooltip.gameObject.SetActive(false);
        tooltip.gameObject.GetComponent<Image>().enabled = false;
    }
}
