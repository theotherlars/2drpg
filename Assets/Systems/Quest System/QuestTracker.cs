using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTracker : MonoBehaviour
{
    QuestInventory questInventory;
    Inventory inventory;

    private void Start()
    {
        questInventory = FindObjectOfType<QuestInventory>();
        inventory = FindObjectOfType<Inventory>();
    }

    public void KillProgress(GameObject go)
    {
        EnemyController enemy = go.GetComponent<EnemyController>();
        if (enemy != null)
        {
            for (int i = 0; i < questInventory.activeQuests.Count; i++)
            {
                if (questInventory.activeQuests[i].type == Quest.Quest_type.Kill && questInventory.activeQuests[i].status == Quest.Quest_status.InProgress)
                {
                    for (int j = 0; j < questInventory.activeQuests[i].NPCToKill.Count; j++)
                    {
                        if (questInventory.activeQuests[i].NPCToKill[j].npcToKill.id == enemy.npc.id)
                        {
                            if (questInventory.activeQuests[i].NPCToKill[j].currentKill < questInventory.activeQuests[i].NPCToKill[j].amountToKill)
                            {
                                questInventory.activeQuests[i].NPCToKill[j].currentKill++;

                                if (questInventory.activeQuests[i].NPCToKill[j].currentKill >= questInventory.activeQuests[i].NPCToKill[j].amountToKill)
                                {
                                    questInventory.activeQuests[i].NPCToKill[j].currentKill = questInventory.activeQuests[i].NPCToKill[j].amountToKill;
                                    questInventory.activeQuests[i].NPCToKill[j].finished = true;

                                    for (int k = 0; k < questInventory.activeQuests[i].NPCToKill.Count; k++)
                                    {
                                        if (!questInventory.activeQuests[i].NPCToKill[k].finished)
                                        {
                                            return;
                                        }
                                    }
                                    questInventory.activeQuests[i].status = Quest.Quest_status.ReadyToDeliver;
                                    break;
                                }
                                
                            }
                        }
                    }
                }
            }

        }
    }

    public void GatherProgress()
    {
        for (int i = 0; i < questInventory.activeQuests.Count; i++)
        {
            if (questInventory.activeQuests[i].type == Quest.Quest_type.Gather)
            {
                for (int j = 0; j < questInventory.activeQuests[i].itemsToGather.Count; j++)
                {
                    //List<Item_SO> itemCount = new List<Item_SO>();
                    int itemCount = 0;

                    for (int k = 0; k < inventory.characterItems.Count; k++)
                    {
                        
                        if (inventory.characterItems[k].ItemID == questInventory.activeQuests[i].itemsToGather[j].itemToGather.ItemID)
                        {
                            itemCount++;
                            questInventory.activeQuests[i].itemsToGather[j].currentGathered = itemCount;
                        }
                    }
                    
                    if (questInventory.activeQuests[i].itemsToGather[j].currentGathered >= questInventory.activeQuests[i].itemsToGather[j].amountToGather)
                    {
                        questInventory.activeQuests[i].itemsToGather[j].finished = true;
                    }
                }
            }
        }
    }

    public void FedExDelivered()
    {

    }
}
