using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ResponsButton : MonoBehaviour
{
    [SerializeField]
    private TextHandler textHandler;
    public int nextIndex;
    public bool isQuest;
    public int questToGive;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Button button = GetComponent<Button>();
        button.onClick.AddListener(HandleClick);
        
    }

    public void ResetButton()
    {
        nextIndex = -1;
        isQuest = false;
        questToGive = -1;
    }

    public void HandleClick()
    {
        if (isQuest)
        {
            Quest quest = FindObjectOfType<QuestDatabase>().GetQuest(questToGive);
            if (quest != null)
            {
                if (quest.status == Quest.Quest_status.Waiting)
                {
                    FindObjectOfType<UIController>().ToggleQuestDetailsWithAnimation(quest);
                }
            }
        }
        textHandler.LoadText(nextIndex);
    }
}
