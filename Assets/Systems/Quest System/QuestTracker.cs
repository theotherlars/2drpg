using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTracker : MonoBehaviour
{
    QuestInventory questInventory;
    Inventory inventory;

    private void Awake()
    {
        questInventory = FindObjectOfType<QuestInventory>();
        inventory = FindObjectOfType<Inventory>();
    }

    public void KillProgress(GameObject go)
    {
        EnemyController enemy = go.GetComponent<EnemyController>();
        if (enemy != null)
        {
            for (int i = 0; i < questInventory.questInventoryStorage.PlayerQuestInventory.Count; i++)
            {
                if (questInventory.questInventoryStorage.PlayerQuestInventory[i].type == Quest.Quest_type.Kill && questInventory.questInventoryStorage.PlayerQuestInventory[i].status == Quest.Quest_status.InProgress)
                {
                    for (int j = 0; j < questInventory.questInventoryStorage.PlayerQuestInventory[i].NPCToKill.Count; j++)
                    {
                        if (questInventory.questInventoryStorage.PlayerQuestInventory[i].NPCToKill[j].npcToKill.id == enemy.GetComponent<NPCInformation>().npc.id)
                        {
                            if (questInventory.questInventoryStorage.PlayerQuestInventory[i].NPCToKill[j].currentKill < questInventory.questInventoryStorage.PlayerQuestInventory[i].NPCToKill[j].amountToKill)
                            {
                                questInventory.questInventoryStorage.PlayerQuestInventory[i].NPCToKill[j].currentKill++;

                                if (questInventory.questInventoryStorage.PlayerQuestInventory[i].NPCToKill[j].currentKill >= questInventory.questInventoryStorage.PlayerQuestInventory[i].NPCToKill[j].amountToKill)
                                {
                                    questInventory.questInventoryStorage.PlayerQuestInventory[i].NPCToKill[j].currentKill = questInventory.questInventoryStorage.PlayerQuestInventory[i].NPCToKill[j].amountToKill;
                                    questInventory.questInventoryStorage.PlayerQuestInventory[i].NPCToKill[j].finished = true;

                                    for (int k = 0; k < questInventory.questInventoryStorage.PlayerQuestInventory[i].NPCToKill.Count; k++)
                                    {
                                        if (!questInventory.questInventoryStorage.PlayerQuestInventory[i].NPCToKill[k].finished)
                                        {
                                            return;
                                        }
                                    }
                                    questInventory.questInventoryStorage.PlayerQuestInventory[i].status = Quest.Quest_status.ReadyToDeliver;
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
        for (int i = 0; i < questInventory.questInventoryStorage.PlayerQuestInventory.Count; i++)
        {
            if (questInventory.questInventoryStorage.PlayerQuestInventory[i].type == Quest.Quest_type.Gather)
            {
                for (int j = 0; j < questInventory.questInventoryStorage.PlayerQuestInventory[i].itemsToGather.Count; j++)
                {
                    //List<Item_SO> itemCount = new List<Item_SO>();
                    int itemCount = 0;

                    for (int k = 0; k < inventory.characterItems.Count; k++)
                    {
                        
                        if (inventory.characterItems[k].ItemID == questInventory.questInventoryStorage.PlayerQuestInventory[i].itemsToGather[j].itemToGather.ItemID)
                        {
                            itemCount++;
                            questInventory.questInventoryStorage.PlayerQuestInventory[i].itemsToGather[j].currentGathered = itemCount;
                        }
                    }
                    
                    if (questInventory.questInventoryStorage.PlayerQuestInventory[i].itemsToGather[j].currentGathered >= questInventory.questInventoryStorage.PlayerQuestInventory[i].itemsToGather[j].amountToGather)
                    {
                        questInventory.questInventoryStorage.PlayerQuestInventory[i].itemsToGather[j].finished = true;
                    }
                }
            }
        }
    }

    public void FedExDelivered()
    {

    }
}
