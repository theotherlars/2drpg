using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Dialogue", menuName = "Own Menu/Dialogue/New Dialogue", order = 20)]
public class Dialogue : ScriptableObject
{
    [Header("Conversations:")]
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
    [Header("If quest giver, gives this quest:")]
    public bool isQuest;
    public Quest quest;
    
}