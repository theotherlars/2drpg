using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the script which creates a new Item.
// That is done by right clicking the asset window > Create > New Item
// Every item is its own file with variables and reference to a sprite icon
// Here we need to add more field if we want more details per item

[CreateAssetMenu(fileName = "New Item",menuName = "Own Menu/Items/New Game Item",order = 40)]
public class Item_SO : ScriptableObject
{
    [Header("Required:")]
    [SerializeField]
    private string title = "";
    public string ItemTitle {get { return title;} } // Item_SO().Title;

    [SerializeField]
    private int itemID = 0;
    public int ItemID { get { return itemID; } } // Item_SO().ItemID;

    [SerializeField]
    private Sprite itemSprite = null;
    public Sprite ItemSprite { get { return itemSprite; } } // Item_SO().ItemSprite;

    [SerializeField]
    private int itemLevel = 0;
    public int ItemLevel { get { return itemLevel; } } // Item_SO().ItemLevel;

    [SerializeField][TextArea(1,5)]
    public string description = "";
    public string Description { get { return description; } } // Item_SO().Description;

    [Header("Item Specifics:")]
    [SerializeField]
    private Category category = 0;
    public Category ItemCategory { get { return category; } } // Item_SO().ItemCategory;

    [SerializeField]
    private Rarity rarity = 0;
    public Rarity ItemRarity { get { return rarity; } } // Item_SO().ItemRarity;

    [Header("If Item is Consumable:")]
    [SerializeField]
    private bool isStackable = false;
    public bool IsStackable { get { return isStackable; } } // Item_SO().IsStackable;

    [SerializeField]
    private int maxStack = 0;
    public int MaxStack { get { return maxStack; } } // Item_SO().IsStackable;

    [Header("If Item is Weapon/Gear:")]
    [SerializeField]
    private WeaponArmor weaponArmor = 0;
    public WeaponArmor ItemWeaponArmor { get { return weaponArmor; } } // Item_SO().ItemWeaponArmor;

    [Header("Attributes:")]
    [SerializeField]
    private List<ItemAttribute> attributes = null;
    public List<ItemAttribute> Attributes { get { return attributes; } } // Item_SO().Attributes.x;

    private Item_SO item;
    
    internal static Item_SO CreateInstance(Item_SO item) // Input an item and get the same item as Output, used for cloning when selecting items
    {
        return item;
    }

    public Item_SO(Item_SO item) // This is the constructor for this class
    {
        this.item = item;
    }

    public enum Rarity // Shows the rarity of the item, add or remove as needed. The text on tooltip reflects on what rarity the item has
    {
        Normal = 0, Rare = 1, Epic = 2, Mythic = 3, Legendary = 4
    }
    public enum Category // Not sure if this is needed...
    {
        Consumable = 0, Weapon = 1, Armor = 2, Other = 3
    }
    public enum WeaponArmor // Enums for what type of Weapon or Armor the item is, add or remove as needed
    {
        None = 0, Chest = 1, Pants = 2, Gloves = 3, Boots = 4, Ring = 5, Neckless = 6, Ranged = 7, Melee = 8, Shield = 9, Helmet = 10
    }


}