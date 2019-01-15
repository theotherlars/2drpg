using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    private void Awake()
    {
        BuildDatabase();
    }

    public Item GetItem(int id) // Returns Item from itemDatabase using id
    {
        return items.Find(item => item.id == id);
    }

    public Item GetItem(string itemTitle) // Returns Item from itemDatabase using itemTitle
    {
        return items.Find(item => item.title == itemTitle);
    }

    void BuildDatabase()
    {
        items = new List<Item>()
        {
            new Item(0,"Steel Axe","This axe has seen alot of battle, but is still shiny as new.",
            new Dictionary<string, int>
            {
                {"Attack Power",10},
                {"Attack Speed",2}
            }),

            new Item(1,"Steel Tower Shield","A little worn, but is strong with its steel bars.",
            new Dictionary<string, int>
            {
                {"Block", 25},
                {"Defence", 10}
            }),

            new Item(2,"Arrow of Death","Little arrow bring a great amount of damage to your enemy. With this in your quiver you'll bring fear to all your enemies.",
            new Dictionary<string, int>
            {
                {"Attack Power", 100},
                {"Attack Speed", 25}
            })
        };
    }
}
