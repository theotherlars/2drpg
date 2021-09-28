using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Own Menu/Quest/Create New Quest", order = 10)]
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
    [Header("If Quest is: Gather")]
    public List<ItemsToGather> itemsToGather = new List<ItemsToGather>();
    [Header("If Quest is: Kill")]
    public List<NPCsToKill> NPCToKill = new List<NPCsToKill>();
    [Header("If Quest is: FedEx")]
    public Item_SO itemToDeliver;
    public NPC npcToDeliverItemTo;

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


    [ContextMenu("ResetQuest")]
    public void ResetQuest(){
        
        // RESET GATHERING QUESTS
        for (int i = 0; i < itemsToGather.Count; i++)
        {
            itemsToGather[i].currentGathered = 0;
            itemsToGather[i].finished = false;
        }

        // RESET KILL QUESTS
        for (int i = 0; i < NPCToKill.Count; i++)
        {
            NPCToKill[i].currentKill = 0;
            NPCToKill[i].finished = false;
        }

        // RESETTING STATUS
        status = Quest_status.Waiting;
        
    }
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