using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Own Menu/Quest/ Create New Quest", order = 10)]
public class Quest : ScriptableObject
{
    [Header("Quest Information:")]
    public int id;
    public string title;
    [TextArea(3, 7)]
    public string description;
    public Quest_type type;
    public Quest_pattern pattern;
    public Quest_status status;
    public List<ItemsToGather> itemsToGather = new List<ItemsToGather>();
    public List<NPCsToKill> NPCToKill = new List<NPCsToKill>();

    [Header("Rewards (no more than 5 items):")]
    public int creditReward;
    [Tooltip("'Get All Rewards' = Player gets all items, 'ChooseOne' = Player gets to choose one item")]
    public Quest_Reward_Decision itemRewardOptions;
    public List<ItemReward> itemReward = new List<ItemReward>();

    // Enums
    public enum Quest_type { Kill, Gather, FedEx, Defend, Activate, Escort }
    public enum Quest_pattern { Single, Chain, SideQuest, Dilemma, ChooseOne }
    public enum Quest_status { NotEligible, Waiting, InProgress, ReadyToDeliver, Completed }
    public enum Quest_Reward_Decision { GetAllRewards, ChooseOne }
}
[System.Serializable]
public class ItemsToGather
{
    public Item_SO itemToGather;
    public int amountToGather;
    public int currentGathered;
    public bool finished;

    public ItemsToGather(Item_SO item, int amount, int current, bool finished)
    {
        itemToGather = item;
        amountToGather = amount;
        currentGathered = current;
        this.finished = finished;
    }
}

[System.Serializable]
public class NPCsToKill
{
    public NPC npcToKill;
    public int amountToKill;
    public int currentKill;
    public bool finished;

    public NPCsToKill(NPC npc, int amount, int currentAmount, bool finished)
    {
        npcToKill = npc;
        amountToKill = amount;
        currentKill = currentAmount;
        this.finished = finished;
    }
}

[System.Serializable]
public class ItemReward
{
    public Item_SO itemReward;
    public int itemStack;

    public ItemReward(Item_SO item, int stackAmount)
    {
        itemReward = item;
        itemStack = stackAmount;
    }
}