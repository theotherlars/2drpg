using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item_SO> items = new List<Item_SO>();

    private void Awake()
    {
        BuildDatabase();
    }

    public Item_SO GetItem(int id) // Returns Item from itemDatabase using id
    {
        return items.Find(item => item.ItemID == id);
    }

    public Item_SO GetItem(string itemTitle) // Returns Item from itemDatabase using itemTitle
    {
        return items.Find(item => item.Title == itemTitle);
    }

    void BuildDatabase()
    {
        items = new List<Item_SO>(Resources.LoadAll<Item_SO>("ItemBase"));

        /*items = new List<Item_SO>()
        {
            new Item_SO(0,"Steel Axe","This axe has seen alot of battle, but is still shiny as new.",
            new Dictionary<string, int>
            {
                {"Attack Power",10},
                {"Attack Speed",2}
            }),

            new Item_SO(1,"Steel Tower Shield","A little worn, but is strong with its steel bars.",
            new Dictionary<string, int>
            {
                {"Block", 25},
                {"Defence", 10}
            }),

            new Item_SO(2,"Arrow of Death","Little arrow bring a great amount of damage to your enemy. With this in your quiver you'll bring fear to all your enemies.",
            new Dictionary<string, int>
            {
                {"Attack Power", 100},
                {"Attack Speed", 25}
            })
        };*/
    }
}
