using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Ability", menuName = "Own Menu/Abilities/Create New Ability")]
public class Ability : ScriptableObject
{
    public int id;
    [Tooltip("This is the name of the abilitie")]
    public string title;

    [Tooltip("This icon will be shown in actionbar and abilities book")]
    public Sprite icon;

    public Ability_Type type;

    [TextArea(3, 3)]
    public string description;

    [Tooltip("How fast the object moves from dealer to receiver, in seconds")]
    public float movementSpeed;

    [Tooltip("Is this ability a part of the global cooldown?")]
    public bool globalCooldown;

    [Tooltip("How long it takes to cast this ability, in seconds")]
    public float castTime;

    [Tooltip("How long it takes before this ability can be cast again, in seconds")]
    public float cooldown;

    [Tooltip("The minimal damage this abililty takes, without any added attributes")]
    public float minDamage;

    [Tooltip("The maximum damage this ability takes, without and added attributes")]
    public float maxDamage;

    public List<AbilityDebuff> debuff = new List<AbilityDebuff>();


    // Just Temporary
    public enum Ability_Type { Physical, Frost, Fire, Arcane, Nature, Shadow, Healing };
}

[System.Serializable]
public class AbilityDebuff
{
    public Debuff debuff;
    [Tooltip("How long the debuff lasts on the target, In seconds")]
    public float timeactive;

    public AbilityDebuff(Debuff debuff, float timeactive)
    {
        this.debuff = debuff;
        this.timeactive = timeactive;
    }
}
