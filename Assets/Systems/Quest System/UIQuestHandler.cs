using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIQuestHandler : MonoBehaviour
{
    [Header("Active Quests List:")]
    public List<Button> uiQuestButtons = new List<Button>();
    QuestInventory questInventory;


    private void Start()
    {
        questInventory = FindObjectOfType<QuestInventory>();
        ResetUIQuestButtons();
        UpdateActiveQuestList();
    }
    
    void UpdateActiveQuestList()
    {
        for (int i = 0; i < questInventory.activeQuests.Count; i++)
        {
            UpdateQuestButton(questInventory.activeQuests[i]);
        }
    }

    public void UpdateQuestButton(Quest quest)
    {
        if (quest != null)
        {
            int slot = FindAvailableButton();
            print(slot);
            uiQuestButtons[slot].GetComponentInChildren<UIQuestButton>().UpdateUIQuestButton(quest);
        }
    }

    public void ResetQuestButton(Quest quest)
    {
        if (quest != null)
        {
            int slot = FindQuestButton(quest);
            uiQuestButtons[slot].GetComponent<UIQuestButton>().ResetUIQuestButton();
        }
    }

    void ResetUIQuestButtons()
    {
        for (int i = 0; i < uiQuestButtons.Count; i++)
        {
            uiQuestButtons[i].gameObject.SetActive(false);
        }
    }

    int FindQuestButton(Quest quest)
    {
        return uiQuestButtons.FindIndex(i => i.GetComponent<UIQuestButton>().quest.id == quest.id);
    }

    int FindAvailableButton()
    {
        return uiQuestButtons.FindIndex(i => i.GetComponent<UIQuestButton>().quest == null);
    }
}
