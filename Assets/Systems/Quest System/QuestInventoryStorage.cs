using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New QuestInventoryStorage",menuName = "Own Menu/Quest/Create New QuestInventoryStorage",order = 11)]
public class QuestInventoryStorage : ScriptableObject
{
    public List<Quest> PlayerQuestInventory = new List<Quest>();
}
