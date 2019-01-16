using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    public List<UIItem> uiItems = new List<UIItem>();
    public GameObject slotPrefab;
    public Transform slotPanel;
    public int numberOfSlots = 16;

    private Image backgroundImage;

    private void Awake()
    {
        backgroundImage = GetComponent<Image>();
        backgroundImage.enabled = false;

        for (int i = 0; i < numberOfSlots; i++)
        {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(slotPanel);
            uiItems.Add(instance.GetComponentInChildren<UIItem>());
        }
        backgroundImage.enabled = true;
    }


    public void UpdateSlot(int slot, Item_SO item)
    {
        uiItems[slot].UpdateItem(item);
    }

    public void AddNewItem(Item_SO item)
    {
        UpdateSlot(uiItems.FindIndex(i => i.item == null), item);
    }

    public void RemoveItem(Item_SO item)
    {
        UpdateSlot(uiItems.FindIndex(i => i.item == item), null);
    }
}
