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
    private UIController uiController;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        uiController = FindObjectOfType<UIController>();
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
                switch (quest.status)
                {
                    case Quest.Quest_status.NotEligible:
                        break;
                    case Quest.Quest_status.Waiting:

                        uiController.ToggleQuestDetailsWithAnimation(quest);
                        break;
                    case Quest.Quest_status.InProgress:
                        if (quest.type == Quest.Quest_type.FedEx && FindObjectOfType<QuestInventory>().CheckActiveQuest(quest).npcToDeliverItemTo.id == quest.npcToDeliverItemTo.id)
                        {
                            quest.status = Quest.Quest_status.ReadyToDeliver;
                            uiController.ToggleQuestDetailsWithAnimation(quest);
                        }
                        break;
                    case Quest.Quest_status.ReadyToDeliver:
                        uiController.ToggleQuestDetailsWithAnimation(quest);
                        break;
                    case Quest.Quest_status.Completed:
                        break;
                    default:
                        break;
                }
            }
        }
        textHandler.LoadText(nextIndex);
    }
}
