using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    void UpdateQuestButton(Quest quest)
    {
        int slot = FindAvailableButton();
        uiQuestButtons[slot].GetComponentInChildren<UIQuestButton>().UpdateUIQuestButton(quest);
    }

    void ResetUIQuestButtons()
    {
        for (int i = 0; i < uiQuestButtons.Count; i++)
        {
            uiQuestButtons[i].gameObject.SetActive(false);
        }
    }

    int FindAvailableButton()
    {
        return uiQuestButtons.FindIndex(i => i.IsActive() == false);
    }
}
