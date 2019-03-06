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
