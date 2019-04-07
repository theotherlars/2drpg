using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILootWindowHandler : MonoBehaviour
{
    public GameObject lootParent;
    public GameObject lootButton;
    public List<Button> itemButtons = new List<Button>();

    private void Update()
    {
        if (itemButtons.Count <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void UpdateLootTable(GameObject go)
    {
        itemButtons.Clear(); // empty list first

        EnemyController enemy = go.GetComponent<EnemyController>();
        
        for (int i = 0; i < enemy.availableLoot.Count; i++)
        {
            var button = Instantiate(lootButton, lootParent.transform);
            Button UIButton = button.GetComponent<Button>();
            itemButtons.Add(UIButton);
            UIButton.GetComponent<LootItemButton>().UpdateLootItem(enemy, false, i); // enemy and which slot 
        }

        if (enemy.creditLoot > 0)
        {
            var button = Instantiate(lootButton, lootParent.transform);
            Button UIButton = button.GetComponent<Button>();
            itemButtons.Add(UIButton);
            UIButton.GetComponent<LootItemButton>().UpdateLootItem(enemy,true,0); // enemy, function gets credit amount
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < itemButtons.Count; i++)
        {
            if (itemButtons[i] != null)
            {
                Destroy(itemButtons[i].gameObject);
            }
            else
            {
                itemButtons.RemoveAt(i);
            }
        }
        itemButtons.Clear();
    }
}
