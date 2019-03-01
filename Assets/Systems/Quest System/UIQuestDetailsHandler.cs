using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIQuestDetailsHandler : MonoBehaviour
{
    public Quest thisQuest;
    private Animator animator;
    private QuestInventory questInventory;
    private QuestObjectiveHandler questObjectiveHandler;

    [Header("Quest Details:")]
    public TextMeshProUGUI questTitle;
    public TextMeshProUGUI questDescription;
    public TextMeshProUGUI questRewardCredit;
    public List<GameObject> questRewardItems = new List<GameObject>();
    public List<GameObject> questObjectives = new List<GameObject>();

    [Header("Action Buttons:")]
    public Button leftButton;
    public TextMeshProUGUI leftButton_Text;
    public Button rightButton;
    public TextMeshProUGUI rightButton_Text;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        questInventory = FindObjectOfType<QuestInventory>();
        questObjectiveHandler = FindObjectOfType<QuestObjectiveHandler>();
    }
    
    public void UpdateQuestDetails(Quest quest)
    {
        if (quest != null)
        {
            thisQuest = quest;
            ResetRewardItems();
            questTitle.text = quest.title;
            questDescription.text = quest.description;
            questRewardCredit.text = string.Format("Credits: {0}",quest.creditReward.ToString());
            questObjectiveHandler.UpdateObjectives(quest);

            for (int i = 0; i < quest.itemReward.Count; i++)
            {
                UpdateItemReward(quest.itemReward[i], i);
            }

            if (quest.status == Quest.Quest_status.Waiting)
            {
                DisplayReadyToAccept();
            }
            else if (quest.status == Quest.Quest_status.InProgress)
            {
                DisplayInProgress();
            }
            else if (quest.status == Quest.Quest_status.ReadyToDeliver)
            {
                DisplayComplete();
            }
        }
    }



    void DisplayReadyToAccept() // State 1: Player gets to choose if he wants to accept quest or not
    {
        leftButton_Text.text = "Accept";
        rightButton_Text.text = "Decline";
        leftButton.interactable = true;
        rightButton.interactable = true;
    }

    void DisplayInProgress() // State 2: Player has already has accepted and the quest is currently in progress
    {
        leftButton_Text.text = "Deliver";
        rightButton_Text.text = "Abandon Quest";
        leftButton.interactable = false;
        rightButton.interactable = true;
    }

    void DisplayComplete() // State 3: Player an active quest which has completed it's goal
    {
        leftButton_Text.text = "Deliver";
        rightButton_Text.text = "Abandon Quest";
        leftButton.interactable = true;
        rightButton.interactable = true;
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

    public void LeftButtonClick()
    {
        Quest currentQuest = CurrentlyDisplaying();
        if (currentQuest.status == Quest.Quest_status.Waiting)
        {
            questInventory.ReceiveQuest(currentQuest);
            CloseWithoutAnimation();
        }
        else if (currentQuest.status == Quest.Quest_status.ReadyToDeliver)
        {
            // TODO
            // If the quest has reward items, then make a condition to continue that the player
            // selectes a reward.
            // If reward is chosen, then give the reward(s), change status to Completed, 
            // If not, write error message "Please choose a reward" and return null.
            currentQuest.status = Quest.Quest_status.Completed;
            DisableWindow();
        }
    }

    public void RightButtonClick()
    {
        Quest currentQuest = CurrentlyDisplaying();
        if (currentQuest.status == Quest.Quest_status.Waiting)
        {
            DisableWindow();
        }
        else if (currentQuest.status == Quest.Quest_status.InProgress)
        {
            questInventory.ResetQuest(currentQuest);
            questInventory.RemoveQuest(currentQuest);
            DisableWindow();
        }
        else if (currentQuest.status == Quest.Quest_status.ReadyToDeliver)
        {
            questInventory.ResetQuest(currentQuest);
            questInventory.RemoveQuest(currentQuest);
            DisableWindow();
        }
    }

    public void OpenWithoutAnimation()
    {
        animator.Play("OpenQuestDetails", 0, 1);
        animator.SetBool("Open", true);
    }

    public void CloseWithoutAnimation()
    {
        animator.Play("CloseQuestDetails", 0, 1);
        animator.SetBool("Open", false);
    }

    public void AnimateOpenWindow()
    {
        animator.SetBool("Open", true);
    }

    public void CloseWithAnimation()
    {
        animator.SetBool("Open", false);
    }

    private void DisableWindow()
    {
        animator.SetBool("Open", false);
        gameObject.SetActive(false);
    }

    public Quest CurrentlyDisplaying()
    {
        return thisQuest;
    }
}
