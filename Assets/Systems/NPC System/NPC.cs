using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC", menuName = "Own Menu/NPC/New NPC", order = 60)]
public class NPC : ScriptableObject
{
    [Header("General Information")]
    public int id;
    public string name;
    public NPC_Type type;
    public NPC_Allies allies;
    public bool killable;

    [Header("NPC Stats")]
    public float maxHealth;
    public float maxEnergy;
    public float addMoreStats;

    [Header("Movement")]
    public float attackSpeed;
    public float walkingSpeed;
    public float runningSpeed;
   
    [Header("If NPC has a dialogue:")]
    public Dialogue dialogue;
    
    [Header("If NPC is a Vendor:")]
    public List<ShopItem> shopItems = new List<ShopItem>();

    [Header("Items/Credit this NPC will drop:")]
    public List<Item_SO> lootTable = new List<Item_SO>();
    public int minCredit;
    public int maxCredit;

    [Header("NPCs abilities:")]
    public List<NPCAbilities> abilities = new List<NPCAbilities>();

    [Header("NPCs Animations:")]
    public List<NPCAnimations> animations = new List<NPCAnimations>();

    [Header("NPCs Sounds:")]
    public List<NPCSounds> sounds = new List<NPCSounds>();

    [Header("Events")]
    public List<NPCEvents> gameObjectEvents = new List<NPCEvents>();

    public enum NPC_Type{Enemy,Boss,QuestStart,QuestEnd,QuestBoth,Vendor,Dialogue,Critter,Other}
    public enum NPC_Allies { NoAllies,Allie1,Allie2,Allie3 }
}

[System.Serializable]
public class NPCAbilities
{
    public Ability ability;
    
    public NPCAbilities(Ability ability)
    {
        this.ability = ability;
    }
}

[System.Serializable]
public class NPCAnimations
{
    public AnimationType state;
    public Animation animation;

    public enum AnimationType
    { // Add more as needed
        Idle,
        WalkingLeft, WalkingRight, WalkingUp, WalkingDown,
        RunningLeft, RunningRight, RunningUp, RunningDown,
        AttackLeft, AttackRight, AttackUp, AttackDown,
        TakeDamage, Death,
        AddMoreAsNeeded
    }

    public NPCAnimations(AnimationType state, Animation animation)
    {
        this.state = state;
        this.animation = animation;
    } 
}

[System.Serializable]
public class NPCSounds
{
    public SoundTypes soundTypes;
    public AudioClip audioClip;
    
    public enum SoundTypes { Talk,Attack,TakeDamage,Death }

    public NPCSounds( SoundTypes types, AudioClip clip )
    {
        soundTypes = types;
        audioClip = clip;
    }
}

[System.Serializable]
public class NPCEvents
{
    public GameObjectEvent gameObjectEvent;
    public Events events;

    public enum Events { OnDeath, Other }

    public NPCEvents(GameObjectEvent goEvent, Events events)
    {
        gameObjectEvent = goEvent;
        this.events = events;
    }
}