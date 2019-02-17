using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIQuestDetailsHandler : MonoBehaviour
{
    private Quest thisQuest;
    private Animator animator;

    [Header("Quest Details:")]
    public TextMeshProUGUI questTitle;
    public TextMeshProUGUI questDescription;
    public TextMeshProUGUI questRewardCredit;
    public List<GameObject> questRewardItems = new List<GameObject>();

    [Header("Action Buttons:")]
    public Button leftButton;
    public TextMeshProUGUI leftButton_Text;
    public Button rightButton;
    public TextMeshProUGUI rightButton_Text;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Open", true);
    }

    public void CloseWindow()
    {
        animator.SetBool("Open", false);
    }

    private void DisableWindow()
    {
        gameObject.SetActive(false);
    }

    public void UpdateQuestDetails(Quest quest)
    {
        if (quest != null)
        {
            thisQuest = quest;
            ResetRewardItems();
            questTitle.text = quest.title;
            questDescription.text = quest.description;
            questRewardCredit.text = quest.creditReward.ToString();

            for (int i = 0; i < quest.itemReward.Count; i++)
            {
                UpdateItemReward(quest.itemReward[i], i);
            }
        }
    }

    public Quest CurrentlyDisplaying()
    {
        return thisQuest;
    }

    void UpdateItemReward(Item_SO item, int slot)
    {
        questRewardItems[slot].GetComponentInChildren<QuestItemReward>().UpdateReward(item);
        questRewardItems[slot].SetActive(true);
    }

    void ResetRewardItems()
    {
        for (int i = 0; i < questRewardItems.Count; i++)
        {
            questRewardItems[i].GetComponentInChildren<QuestItemReward>().ResetItem();
            questRewardItems[i].SetActive(false);
        }
    }
}
