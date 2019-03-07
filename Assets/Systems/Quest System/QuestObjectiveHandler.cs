using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class QuestObjectiveHandler : MonoBehaviour
{
    public List<GameObject> objectives = new List<GameObject>();
    private Quest thisQuest;

    public void UpdateObjectives(Quest quest)
    {
        if (quest != null)
        {
            thisQuest = quest;
            ResetObjectives();

            switch (quest.type)
            {
                case Quest.Quest_type.Kill:
                    {
                        DisplayKill();
                        break;
                    }
                case Quest.Quest_type.Gather:
                    {
                        DisplayGather();
                        break;
                    }
                case Quest.Quest_type.FedEx:
                    {
                        DisplayFedEx();
                        break;
                    }
                case Quest.Quest_type.Escort:
                    {
                        DisplayEscort();
                        break;
                    }
                case Quest.Quest_type.Defend:
                    {
                        DisplayDefend();
                        break;
                    }
                case Quest.Quest_type.Activate:
                    {
                        DisplayActivate();
                        break;
                    }
            }
        }
    }

    private void DisplayActivate()
    {
        
    }

    private void DisplayDefend()
    {
        
    }

    private void DisplayEscort()
    {
        
    }

    private void DisplayFedEx()
    {
        Item_SO itemToDeliver = thisQuest.itemToDeliver;
        string text = String.Format("Deliver {0} to {1}", itemToDeliver.ItemTitle, this.thisQuest.npcToDeliverItemTo.name);
        int slot = FindObjective();
        objectives[slot].SetActive(true);
        objectives[slot].GetComponentInChildren<TextMeshProUGUI>().text = text;
    }

    private void DisplayGather()
    {
        for (int i = 0; i < thisQuest.itemsToGather.Count; i++)
        {
            int slot = FindObjective();

            // Fix the NPCToKill name when a database of all npcs are made
            ItemDatabase itemDatabase = FindObjectOfType<ItemDatabase>();
            Item_SO itemToGather = itemDatabase.GetItem(thisQuest.itemsToGather[i].itemToGather.ItemID);
            int amountToGather = thisQuest.itemsToGather[i].amountToGather;
            int currentGathered = thisQuest.itemsToGather[i].currentGathered;

            string text = String.Format("Gather {0} : {1} / {2}", itemToGather.ItemTitle, currentGathered, amountToGather);

            objectives[i].SetActive(true);
            objectives[i].GetComponentInChildren<TextMeshProUGUI>().text = text;
            if (thisQuest.itemsToGather[i].finished)
            {
                objectives[i].GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
            }
        }
    }

    private void DisplayKill()
    {
        for (int i = 0; i < thisQuest.NPCToKill.Count; i++)
        {
            int slot = FindObjective();

            // Fix the NPCToKill name when a database of all npcs are made
            string npcToKill = thisQuest.NPCToKill[i].npcToKill.name;
            int amountToKill = thisQuest.NPCToKill[i].amountToKill;
            int currentKill = thisQuest.NPCToKill[i].currentKill;
            string text = String.Format("Kill {0} : {1} / {2}",npcToKill ,currentKill ,amountToKill);
            objectives[i].SetActive(true);
            objectives[i].GetComponentInChildren<TextMeshProUGUI>().text = text;

            if (thisQuest.NPCToKill[i].finished)
            {
                objectives[i].GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
            }
        }
    }

    public void ResetObjectives()
    {
        for (int i = 0; i < objectives.Count; i++)
        {
            objectives[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
            objectives[i].GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
            objectives[i].SetActive(false);
        }
    }

    public void UpdateObjective()
    {

    }

    private int FindObjective()
    {
        return objectives.FindIndex(i => i.activeInHierarchy == false);
    }
}
