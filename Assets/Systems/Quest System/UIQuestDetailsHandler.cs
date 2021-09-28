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
    public TextMeshProUGUI questRewardOption;
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
            thisQuest = quest; // store the quest to a variable so it's easier for to use the quest object in other functions in this class

            // Inserts quest information in the details window
            ResetRewardItems(); // resets potential old information
            questTitle.text = quest.title;
            questDescription.text = quest.description;

            string questRewardOptionText = "";
            if (quest.itemRewardOptions == Quest.Quest_Reward_Decision.ChooseOne) // Dynamically changes the reward text if the player has to choose or gets it all
            {
                questRewardOptionText = string.Format("Choose one option:"); // if the player has to choose its reward/item
            }
            else
            {
                questRewardOptionText = string.Format("You'll receive:"); // if the player gets all rewards
            }
            questRewardOption.text = questRewardOptionText;


            questRewardCredit.text = string.Format("Credits: {0}",quest.creditReward.ToString());
            questObjectiveHandler.UpdateObjectives(quest);

            for (int i = 0; i < quest.itemReward.Count; i++)
            {
                UpdateItemReward(quest.itemReward[i].itemReward, quest.itemReward[i].itemStack, i); // for each itemreward in this quest, update the quest slot
            }

            if (quest.status == Quest.Quest_status.Waiting) // how to display the action buttons depending on the quest status
            {
                DisplayReadyToAccept(); // if the quest is in waiting (waiting to be accepted)
            }
            else if (quest.status == Quest.Quest_status.InProgress)
            {                 
                DisplayInProgress(); // if the quest is InProgress (already picked up and in players questinventory
            }
            else if (quest.status == Quest.Quest_status.ReadyToDeliver)
            {
                DisplayReadToDeliver(); // players has finished all the objectives and is ready to turn in quest
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

    void DisplayReadToDeliver() // State 3: Player an active quest which has completed it's goal
    {
        leftButton_Text.text = "Deliver";
        rightButton_Text.text = "Abandon Quest";
        leftButton.interactable = true;
        rightButton.interactable = true;
    }

    void UpdateItemReward(Item_SO item, int stack, int slot)
    {

        questRewardItems[slot].GetComponentInChildren<QuestItemReward>().UpdateReward(item, stack); // update the itemreward slot, stack is default 0 but changes if stack is > 0
        questRewardItems[slot].GetComponent<QuestItemRewardSelection>().ResetSelection(); // Resets the item reward selection so that it dosn't show from last time window was open
        questRewardItems[slot].SetActive(true); // sets the "slot" active in the correct spot
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
            switch (currentQuest.type)
            {
                case Quest.Quest_type.Kill:
                    {
                        questInventory.ReceiveQuest(currentQuest);
                        CloseWithoutAnimation();
                        break;
                    }
                case Quest.Quest_type.Gather:
                    {
                        questInventory.ReceiveQuest(currentQuest);
                        CloseWithoutAnimation();
                        break;
                    }
                case Quest.Quest_type.FedEx:
                    {
                        if (FindObjectOfType<UIInventory>().SpaceLeftInInventory(currentQuest.itemToDeliver))
                        {
                            Inventory inventory = FindObjectOfType<Inventory>();
                            inventory.GiveItem(currentQuest.itemToDeliver.ItemID);

                            if (inventory.CheckForItem(currentQuest.itemToDeliver.ItemID) != null)
                            {
                                questInventory.ReceiveQuest(currentQuest);
                                currentQuest.status = Quest.Quest_status.InProgress;
                                CloseWithoutAnimation();
                                break;
                            }
                            else
                            {
                                FindObjectOfType<UIController>().LoadErrorText("Something went wrong, you didn't receive the item to deliver...");
                            }
                        }
                        else
                        {
                            FindObjectOfType<UIController>().LoadErrorText("Inventory is full");
                        }

                        break;
                    }
                case Quest.Quest_type.Defend:
                    { break; }
                case Quest.Quest_type.Activate:
                    { break; }
                case Quest.Quest_type.Escort:
                    { break; }
                default:
                    { break; }
            }
        }
        else if (currentQuest.status == Quest.Quest_status.ReadyToDeliver)
        {
            Inventory inventory = FindObjectOfType<Inventory>(); // Finds and stores the inventory to a variable
            if (inventory.CheckForItem(currentQuest.itemToDeliver.ItemID))
            {
                inventory.RemoveItem(currentQuest.itemToDeliver.ItemID);
            }

            if (currentQuest.itemRewardOptions == Quest.Quest_Reward_Decision.ChooseOne && currentQuest.itemReward.Count != 0) // if the quest_reward_decision is correct and quest has item rewards
            {
                int selectedItem = 0;
                int selectedItemStack = 0;
                for (int i = 0; i < currentQuest.itemReward.Count; i++)
                {
                    if (questRewardItems[i].GetComponent<Outline>().enabled) // looks through all the quest reward items to see if the Outline is enabled, if it is then...
                    {
                        selectedItem = questRewardItems[i].GetComponentInChildren<QuestItemReward>().item.ItemID; // store itemReward ID
                        selectedItemStack = questRewardItems[i].GetComponentInChildren<QuestItemReward>().stackCount; // store potential stack (if the reward is a stack)
                        break; // breaks loop
                    }
                }

                if (selectedItem != 0) // if item is not null
                {
                    if (selectedItemStack > 0) // if itemstack i not null
                    {
                        for (int i = 0; i < selectedItemStack; i++)
                        {
                            inventory.GiveItem(selectedItem); // for each stacked item, give item
                        }
                    }
                    else
                    {
                        FindObjectOfType<Inventory>().GiveItem(selectedItem); // if only one item (no stack), then give that item
                    }

                    questInventory.CompleteQuest(thisQuest); //run method which clears quest button, sets correct status and cleans up
                    DisableWindow(); // disable the details window
                }
                else
                {
                    string text = string.Format("Please select a reward");

                    FindObjectOfType<UIController>().LoadErrorText(text); // Write error message if there is not chosen a reward
                }
            }
            else if (currentQuest.itemRewardOptions == Quest.Quest_Reward_Decision.GetAllRewards && currentQuest.itemReward.Count != 0) // if the item reward decision is correct and item reward is not null
            {

                for (int i = 0; i < currentQuest.itemReward.Count; i++)
                {
                    // for each item, check if inventory is not full, if it returns false, then prompt error message do not give the player any items...
                    // Also stop the "complete quest method"
                    // first check, then give, then complete

                    int item = questRewardItems[i].GetComponentInChildren<QuestItemReward>().item.ItemID; // for each reward item store itemID
                    int itemStack = questRewardItems[i].GetComponentInChildren<QuestItemReward>().stackCount; // for each reward item store StackCount

                    if (item != 0) // if item is not null
                    {
                        if (itemStack != 0) // if itemstack is not null
                        {
                            for (int j = 0; j < itemStack; j++)
                            {
                                inventory.GiveItem(item); // for each itemStack give item
                            }
                        }
                        else
                        {
                            inventory.GiveItem(item); // if there is only one item, then give that item
                        }
                    }
                }

                questInventory.CompleteQuest(thisQuest); //run method which clears quest button, sets correct status and cleans up
                DisableWindow(); // disable the details window
            }
            else
            {
                questInventory.CompleteQuest(thisQuest); //run method which clears quest button, sets correct status and cleans up
                DisableWindow(); // disable the details window
            }
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
            //questInventory.ResetQuest(currentQuest);
            questInventory.RemoveQuest(currentQuest);
            DisableWindow();
        }
        else if (currentQuest.status == Quest.Quest_status.ReadyToDeliver)
        {
            //questInventory.ResetQuest(currentQuest);
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
