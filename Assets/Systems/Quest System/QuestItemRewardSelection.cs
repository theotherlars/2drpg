using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuestItemRewardSelection : MonoBehaviour, IPointerClickHandler
{
    public List<GameObject> rewardSlots = new List<GameObject>();
    public int rewardSelected;
    public int rewardSelectedStack;

    private Outline outline;

    private void OnEnable()
    {
        outline = GetComponent<Outline>();
    }

    public void ResetSelection()
    {
        outline.enabled = false;
        rewardSelected = 0;
        rewardSelectedStack = 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UIQuestDetailsHandler uIQuestDetailsHandler = GetComponentInParent<UIQuestDetailsHandler>();
        if(uIQuestDetailsHandler.thisQuest.status == Quest.Quest_status.ReadyToDeliver && uIQuestDetailsHandler.thisQuest.itemRewardOptions == Quest.Quest_Reward_Decision.ChooseOne)
        {
            outline.enabled = true;
            rewardSelected = GetComponentInChildren<QuestItemReward>().item.ItemID;
            rewardSelectedStack = GetComponentInChildren<QuestItemReward>().stackCount;


            for (int i = 0; i < rewardSlots.Count; i++)
            {
                if (rewardSlots[i].gameObject != this.gameObject)
                {
                    rewardSlots[i].GetComponent<QuestItemRewardSelection>().ResetSelection();
                }
            }
        }
    }
}
