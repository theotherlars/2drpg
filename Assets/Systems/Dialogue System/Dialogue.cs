using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/New Dialogue")]
public class Dialogue : ScriptableObject
{
    public int npc_ID;
    public string npc_Name;
    public Sentence[] sentences;
    
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