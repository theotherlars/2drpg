using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDatabase : MonoBehaviour
{
    public List<Quest> questDatabase = new List<Quest>();

    private void Awake()
    {
        BuildQuestDatabase();
    }

    void BuildQuestDatabase() // loads all quest scriptable objects to a complete list of all quests
    {
        questDatabase = new List<Quest>(Resources.LoadAll<Quest>("Quests"));
    }

    public Quest GetQuest(int questid) // returns quest scriptable object
    {
        return questDatabase.Find(quest => quest.id == questid);
    }
}
