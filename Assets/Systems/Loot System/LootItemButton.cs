using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LootItemButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    public Image image;
    public TextMeshProUGUI itemName;
    public Item_SO item;
    private Tooltip tooltip;
    private EnemyController enemy;
    private int itemSlotInEnemy;
    private bool isCredits;
    private int creditsDisplaying;

    private void OnEnable()
    {
        tooltip = FindObjectOfType<Tooltip>();
    }


    public void UpdateLootItem(EnemyController enemyIn, bool credit, int slot)
    {
        if (!credit)
        {

            if (enemyIn != null)
            {
                item = enemyIn.availableLoot[slot];
                itemSlotInEnemy = slot;
                this.enemy = enemyIn;

                image.sprite = item.ItemSprite;
                image.color = Color.white;
                itemName.text = item.ItemTitle;
                //item = loot;
                isCredits = false;
            }
            else
            {
                image.sprite = null;
                image.color = Color.clear;
                itemName.text = "";
                item = null;
                isCredits = false;
                Destroy(this.gameObject);
            }
        }
        else
        {
            this.enemy = enemyIn;
            if (enemy.creditLoot > 0)
            {
                creditsDisplaying = enemy.creditLoot;
                image.sprite = Resources.Load<Sprite>("Sprites/UI/goldCoin");
                image.color = Color.white;
                itemName.text = string.Format("{0} Credits", enemy.creditLoot);
                item = null;
                isCredits = true;
            }
        }
    }

    public void OnClick()
    {
        if (!isCredits)
        {
            FindObjectOfType<Inventory>().GiveItem(item.ItemID);
            enemy.availableLoot.RemoveAt(enemy.availableLoot.FindIndex(i => i == item));
            //enemy.availableLoot.RemoveAt(itemSlotInEnemy);
        }
        else
        {
            FindObjectOfType<Inventory>().IncreaseMoney(creditsDisplaying);
            enemy.creditLoot = 0;
        }

        UpdateLootItem(null,false,0);
        Destroy(this.gameObject);
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
        tooltip.gameObject.GetComponent<Image>().enabled = false;
    }
}
