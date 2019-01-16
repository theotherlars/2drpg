using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item",menuName = "New Game Item",order =51)]
public class Item_SO : ScriptableObject
{
    [Header("Required:")]
    [SerializeField]
    private string title;
    public string Title {get { return title;}}

    [SerializeField]
    private int itemID;
    public int ItemID { get { return itemID; } }

    [SerializeField]
    private Sprite itemSprite;
    public Sprite ItemSprite { get { return itemSprite; } }

    [SerializeField]
    private int itemLevel;
    public int ItemLevel { get { return itemLevel; } }

    [SerializeField][TextArea(1,5)]
    public string description;
    public string Description { get { return description; } }

    [Header("Item Specifics:")]
    [SerializeField]
    private Category category;
    public Category ItemCategory { get { return category; } }

    [SerializeField]
    private Rarity rarity;
    public Rarity ItemRarity { get { return rarity; } }

    [Header("If Item is Consumable:")]
    [SerializeField]
    private bool isStackable;
    public bool IsStackable { get { return isStackable; } }

    [Header("If Item is Weapon/Gear:")]
    [SerializeField]
    private WeaponArmor weaponArmor;
    public WeaponArmor ItemWeaponArmor { get { return weaponArmor; } }

    [Header("Attributes:")]
    [SerializeField]
    private List<ItemAttribute> attributes;

    private Item_SO item;
    
    public List<ItemAttribute> Attributes { get { return attributes; } }

    public Item_SO(Item_SO item)
    {
        this.item = item;
    }

    public enum Rarity
    {
        Normal = 0, Rare = 1, Epic = 2, Mythic = 3, Legendary = 4
    }
    public enum Category
    {
        Consumable = 0, Weapon = 1, Armor = 2, Other = 3
    }
    public enum WeaponArmor
    {
        None = 0, Chest = 1, Pants = 2, Gloves = 3, Boots = 4, Ring = 5, Neckless = 6, Gun = 7, Melee = 8, Shield = 9, Helmet = 10
    }
}