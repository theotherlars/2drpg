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
    public List<NPCLootTable> lootTable = new List<NPCLootTable>();

    [Header("NPCs abilities:")]
    public List<NPCAbility> abilities = new List<NPCAbility>();
    
    [Header("NPCs Animations:")]
    public List<NPCAnimations> animations = new List<NPCAnimations>();

    public enum NPC_Type{Enemy,Boss,QuestStart,QuestEnd,Vendor,DialogueNPC,Other}
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
public class NPCLootTable
{
    public Item_SO item;
    [Tooltip("Drop rate in percentage %")]
    public float dropRate;

    public NPCLootTable(Item_SO item, float dropRate)
    {
        this.item = item;
        this.dropRate = dropRate;
    }
}

[System.Serializable]
public class NPCAnimations
{
    public AnimationType animationType;
    public Animation animation;

    public enum AnimationType
    {
        Idle,
        WalkingLeft, WalkingRight, WalkingUp, WalkingDown,
        RunningLeft, RunningRight, RunningUp, RunningDown,
        StrikeLeft, StrikeRight, StrikeUp, StrikeDown,
        TakeDamage, Death
    }

    public NPCAnimations(AnimationType type, Animation animation)
    {
        animationType = type;
        this.animation = animation;
    }

    
}
