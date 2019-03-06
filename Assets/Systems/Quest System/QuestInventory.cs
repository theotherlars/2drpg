using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInventory : MonoBehaviour
{
    public List<Quest> activeQuests = new List<Quest>();
    public QuestInventoryStorage questInventoryStorage;
    QuestDatabase questDatabase;
    UIQuestHandler uiQuestHandler;

    private void Start()
    {
        questDatabase = FindObjectOfType<QuestDatabase>();
        uiQuestHandler = FindObjectOfType<UIQuestHandler>();

        for (int i = 0; i < questInventoryStorage.PlayerQuestInventory.Count; i++)
        {
            activeQuests.Add(questInventoryStorage.PlayerQuestInventory[i]);
        }
    }

    public void ReceiveQuest(Quest questToAdd)
    {
        //uiQuestHandler = FindObjectOfType<UIQuestHandler>();
        Quest questObject = questDatabase.GetQuest(questToAdd.id);

        bool alreadyHave = CheckActiveQuest(questObject);

        if (questObject != null && questObject.status == Quest.Quest_status.Waiting && CheckActiveQuest(questObject) == false)
        {
            activeQuests.Add(questObject);
            questInventoryStorage.PlayerQuestInventory.Add(questObject);
            uiQuestHandler.UpdateQuestButton(questToAdd);
            questObject.status = Quest.Quest_status.InProgress;
        }
    }

    public void ReceiveQuest(int questIdToAdd)
    {
        //uiQuestHandler = FindObjectOfType<UIQuestHandler>();
        Quest questToAdd = questDatabase.GetQuest(questIdToAdd);
        if (questToAdd != null && questToAdd.status == Quest.Quest_status.Waiting && CheckActiveQuest(questToAdd) == false)
        {
            activeQuests.Add(questToAdd);
            questInventoryStorage.PlayerQuestInventory.Add(questToAdd);
            uiQuestHandler.UpdateQuestButton(questToAdd);
            questToAdd.status = Quest.Quest_status.InProgress;
        }
    }

    public void RemoveQuest(Quest questToRemove)
    {
        //uiQuestHandler = FindObjectOfType<UIQuestHandler>();

        if (questToRemove != null && CheckActiveQuest(questToRemove) == true)
        {

            uiQuestHandler.ResetQuestButton(questToRemove);

            int slot = FindIndexOfQuest(questToRemove);
            activeQuests.RemoveAt(slot);
            questInventoryStorage.PlayerQuestInventory.RemoveAt(slot);
            if (questToRemove.status == Quest.Quest_status.InProgress || questToRemove.status == Quest.Quest_status.ReadyToDeliver)
            {
                questToRemove.status = Quest.Quest_status.Waiting;
            }
           
        }
    }

    public void RemoveQuest(int questIdToRemove)
    {
        //uiQuestHandler = FindObjectOfType<UIQuestHandler>();
        Quest questToRemove = questDatabase.GetQuest(questIdToRemove);

        if (questToRemove != null && CheckActiveQuest(questIdToRemove) == true)
        {
            int slot = FindIndexOfQuest(questToRemove);
            activeQuests.RemoveAt(slot);
            questInventoryStorage.PlayerQuestInventory.RemoveAt(slot);
            uiQuestHandler.ResetQuestButton(questToRemove);
            if (questToRemove.status == Quest.Quest_status.InProgress || questToRemove.status == Quest.Quest_status.ReadyToDeliver)
            {
                questToRemove.status = Quest.Quest_status.Waiting;
            }
        }
    }

    public void ResetQuest(Quest quest)
    {
        int slot = FindIndexOfQuest(quest);
        if (slot >= 0)
        {
            for (int i = 0; i < quest.itemsToGather.Count; i++)
            {
                quest.itemsToGather[i].currentGathered = 0;
            }

            for (int i = 0; i < quest.NPCToKill.Count; i++)
            {
                quest.NPCToKill[i].finished = false;
                quest.NPCToKill[i].currentKill = 0;
            }

            quest.status = Quest.Quest_status.Waiting;
        }
    }

    public void CompleteQuest(Quest quest)
    {
        int slot = FindIndexOfQuest(quest);
        if (slot >= 0)
        {
            activeQuests.RemoveAt(slot);
            questInventoryStorage.PlayerQuestInventory.RemoveAt(slot);
            uiQuestHandler.ResetQuestButton(quest);
            quest.status = Quest.Quest_status.Completed;
        }
    }

    int FindIndexOfQuest(Quest quest)
    {
        return questInventoryStorage.PlayerQuestInventory.FindIndex(item => item.id == quest.id);
        //return activeQuests.FindIndex(item => item.id == quest.id);
    }

    public bool CheckActiveQuest(Quest questToCheck)
    {
        try
        {
            Quest quest = questInventoryStorage.PlayerQuestInventory.Find(questObject => questObject.id == questToCheck.id);
            //Quest quest = activeQuests.Find(questObject => questObject.id == questToCheck.id);
            if (quest != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }

    public bool CheckActiveQuest(int questIdToCheck)
    {
        try
        {
            Quest quest = activeQuests.Find(questObject => questObject.id == questIdToCheck);
            //Quest quest = activeQuests.Find(questObject => questObject.id == questIdToCheck);
            if (quest != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch { return false; }
        
    }
}
