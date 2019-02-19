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
            int progress = 0;

            string text = String.Format("Gather {0} : {1} / {2}", itemToGather.ItemTitle, progress, amountToGather);

            objectives[i].SetActive(true);
            objectives[i].GetComponentInChildren<TextMeshProUGUI>().text = text;
        }
    }

    private void DisplayKill()
    {
        for (int i = 0; i < thisQuest.NPCToKill.Count; i++)
        {
            int slot = FindObjective();

            // Fix the NPCToKill name when a database of all npcs are made
            string npcToKill = thisQuest.NPCToKill[i].npcToKill.ToString();
            int amountToKill = thisQuest.NPCToKill[i].amountToKill;
            string text = String.Format("Kill {0} : {1} / {2}",npcToKill ,"0" ,amountToKill);
            objectives[i].SetActive(true);
            objectives[i].GetComponentInChildren<TextMeshProUGUI>().text = text;
        }
    }

    public void ResetObjectives()
    {
        for (int i = 0; i < objectives.Count; i++)
        {
            objectives[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
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
