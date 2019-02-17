using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIQuestButton : MonoBehaviour
{
    public Quest quest;
    public TextMeshProUGUI buttonText;
    UIController uiController;

    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(HandleClick);
        uiController = FindObjectOfType<UIController>();
    }

    public void UpdateUIQuestButton(Quest quest)
    {
        this.quest = quest;

        if (quest != null)
        {
            buttonText.text = quest.title;
            gameObject.SetActive(true);
        }
        else
        {
            buttonText.text = null;
            gameObject.SetActive(false);
        }
    }

    void HandleClick()
    {
        uiController.ToggleQuestDetails(quest);
    }
}
