using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Wareables",menuName = "Own Menu/Inventory/New Character Wearables")]
public class CharacterItemsInUse : ScriptableObject
{
    public Item_SO helmet;
    public Item_SO chest;
    public Item_SO pants;
    public Item_SO gloves;
    public Item_SO boots;
    public Item_SO melee;
    public Item_SO ranged;

}
