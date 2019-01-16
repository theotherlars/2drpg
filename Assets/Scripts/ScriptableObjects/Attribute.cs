using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attribute",menuName = "New Attribute",order = 52)]

public class Attribute: ScriptableObject
{
    [Header("Required:")]
    public string attributeName;

    [Header("Choose:")]
    public int healthPoint;
    public int manaPoint;
    public float healthGeneration;
    public float manaGeneration;
    public int damagePoints;
    public float critPoints;
    public float blockingPoints;
    public float attackSpeed;
}


[System.Serializable]
public class ItemAttribute
{
    public Attribute attribute;
    public int amount; // Example 15 Strenght and 12 Agility

    public ItemAttribute(Attribute thisAttribute, int thisAmount)
    {
        attribute = thisAttribute;
        amount = thisAmount;
    }
}