using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    // Common properties for all items
    public int id;
    public string title;
    public string description;
    public Sprite icon;

    // Unique properties (stats) for items
    public Dictionary<string, int> stats = new Dictionary<string, int>();


    public Item(int id, string title, string description, Dictionary<string, int> stats)
    {
        this.id = id;
        this.title = title;
        this.description = description;
        this.icon = Resources.Load<Sprite>("Assets/Resources/Sprites/UI/Inventory/" + title);
        this.stats = stats;
    }

    public Item(Item item)
    {
        this.id = item.id;
        this.title = item.title;
        this.description = item.description;
        this.icon = Resources.Load<Sprite>("Assets/Resources/Sprites/UI/Inventory/" + item.title);
        this.stats = item.stats;
    }
}
