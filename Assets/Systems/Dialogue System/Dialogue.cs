using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/New Dialogue")]
public class Dialogue : ScriptableObject
{
    public int npc_ID;
    public string npc_Name;
    [SerializeField]
    private NPC_Category npc_Category;
    public NPC_Category NPCCategory { get { return npc_Category; } }
    public Sentence[] sentences;
    public enum NPC_Category
    {
        Dialogue = 0, Quest = 1, Shop = 2 // , Other = 3
    }

}

[System.Serializable]
public class Sentence
{
    [TextArea(1,2)]
    public string text;
    [Header("Max 5 responses")]
    public Response[] responses;
}

[System.Serializable]
public class Response
{
    [TextArea(1, 2)]
    public string replyOptions;
    [Tooltip("Type in the array indext of the Message the dialogue should go to if the player chooses this. Remeber -1 is for exit.")]
    public int nextSentenceIndex;
    public string prerequsite;
    public Object trigger;
}