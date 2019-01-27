using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is required to have in all scenes since it loads the ItemBase into a list which will be refered to by other classes
// to get items as needed.

public class ItemDatabase : MonoBehaviour
{
    public List<Item_SO> items = new List<Item_SO>(); // Creates a list which will contain Item_SO's

    private void Awake()
    {
        BuildDatabase(); // Calls on scene/game start which adds all items in ItemBase to the items list
    }

    public Item_SO GetItem(int id) // Returns Item from itemDatabase using id. Example: itemDatabase.GetItem(10);
    {
        return items.Find(item => item.ItemID == id); // returns item if found in items list.
    }

    public Item_SO GetItem(string itemTitle) // Returns Item from itemDatabase using itemTitle. Example: itemDatabase.GetItem("Sword of Fire");
    {
        return items.Find(item => item.Title == itemTitle); // returns item if found in items list.
    }

    void BuildDatabase() // Loads all items which is stored under Assets/Resources/ItemBase/* and adds to items list.
    {
        items = new List<Item_SO>(Resources.LoadAll<Item_SO>("ItemBase"));
    }
}
