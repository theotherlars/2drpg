using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInventory : MonoBehaviour
{
    public List<Quest> activeQuests = new List<Quest>();
    QuestDatabase questDatabase;

    private void Start()
    {
        questDatabase = FindObjectOfType<QuestDatabase>();
    }

    public void ReceiveQuest(Quest questToAdd)
    {
        Quest questObject = questDatabase.GetQuest(questToAdd.id);
        if (questObject != null && questObject.status == Quest.Quest_status.Waiting && CheckActiveQuest(questObject) != null)
        {
            activeQuests.Add(questObject);
            questObject.status = Quest.Quest_status.InProgress;
        }
    }

    public void ReceiveQuest(int questIdToAdd)
    {
        Quest questObject = questDatabase.GetQuest(questIdToAdd);
        if (questObject != null && questObject.status == Quest.Quest_status.Waiting && CheckActiveQuest(questObject) != null)
        {
            activeQuests.Add(questObject);
            questObject.status = Quest.Quest_status.InProgress;
        }
    }

    public void RemoveQuest(Quest questToRemove)
    {
        if (questToRemove != null && CheckActiveQuest(questToRemove) != null)
        {
            activeQuests.Remove(questToRemove);
            if (questToRemove.status == Quest.Quest_status.InProgress || questToRemove.status == Quest.Quest_status.ReadyToDeliver)
            {
                questToRemove.status = Quest.Quest_status.Waiting;
            }
        }
    }

    public void RemoveQuest(int questIdToRemove)
    {
        Quest questToRemove = CheckActiveQuest(questIdToRemove);

        if (questToRemove != null)
        {
            activeQuests.Remove(questToRemove);
            if (questToRemove.status == Quest.Quest_status.InProgress || questToRemove.status == Quest.Quest_status.ReadyToDeliver)
            {
                questToRemove.status = Quest.Quest_status.Waiting;
            }
        }
    }

    public Quest CheckActiveQuest(Quest questToCheck)
    {
        return activeQuests.Find(questObject => questObject.id == questToCheck.id);
    }

    public Quest CheckActiveQuest(int questIdToCheck)
    {
        return activeQuests.Find(questObject => questObject.id == questIdToCheck);
    }
}
