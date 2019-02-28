using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC", menuName = "Own Menu/NPC/New NPC", order = 60)]
public class NPC : ScriptableObject
{
    public int id;
    public string name;
    public bool killable;
    public float maxHealth;
    public float walkingSpeed;
    public float runningSpeed;
    public NPC_Type type;
    
    public enum NPC_Type{Shop,Dialogue,QuestGiver,Enemy,Other}
}
