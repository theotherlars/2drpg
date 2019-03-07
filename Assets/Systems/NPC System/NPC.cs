using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC", menuName = "Own Menu/NPC/New NPC", order = 60)]
public class NPC : ScriptableObject
{
    public int id;
    public string name;
    public NPC_Type type;
    public bool killable;
    public float maxHealth;
    public float walkingSpeed;
    public float runningSpeed;
    
    [Header("Items this NPC will drop:")]
    public List<NPCInventory> inventory = new List<NPCInventory>();

    [Header("NPCs abilities:")]
    public List<NPCAbility> abilities = new List<NPCAbility>();


    public enum NPC_Type{Enemy,Dialogue,QuestGiver,Shop,Other}
}

[System.Serializable]
public class NPCAbility
{
    public Ability ability;
    public Animation animation;

    public NPCAbility(Ability ability, Animation animation)
    {
        this.ability = ability;
        this.animation = animation;
    }
}

[System.Serializable]
public class NPCInventory
{
    public Item_SO item;
    [Tooltip("Drop rate in percentage %")]
    public float dropRate;

    public NPCInventory(Item_SO item, float dropRate)
    {
        this.item = item;
        this.dropRate = dropRate;
    }
}
