using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TextHandler : MonoBehaviour
{
    private Dialogue cachedDialogue;
    
    [SerializeField]
    private TextMeshProUGUI npc_NameText;
    [SerializeField]
    private TextMeshProUGUI npc_DialogueText;
    [SerializeField]
    private GameObject responsPanel;
    [SerializeField]
    private List<GameObject> responsButtons;

    public void LoadDialogue(Dialogue dialogue)
    {
        cachedDialogue = dialogue;
        LoadText(0);
    }

    public void LoadText(int textIndex)
    {
        ResetDialogueBox(); // Resets all the text and buttons to default so there won't be any from last sentence/dialogue

        if (textIndex >= 0)
        {
            npc_NameText.text = cachedDialogue.npc_Name; // Name of NPC
            npc_DialogueText.text = cachedDialogue.sentences[textIndex].text; // NPC's dialogue text

            if (cachedDialogue.sentences[textIndex].responses.Length > 0) // If there are more than 0 responses available the it will start adding buttons for responses
            {
                for (int i = 0; i < cachedDialogue.sentences[textIndex].responses.Length; i++)
                {
                    responsButtons[i].SetActive(true); // Activates the necessary amount of buttons (max 5)

                    TextMeshProUGUI buttonText = responsButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                    buttonText.SetText(cachedDialogue.sentences[textIndex].responses[i].replyOptions);

                    ResponsButton button = responsButtons[i].GetComponent<ResponsButton>();
                    button.nextIndex = cachedDialogue.sentences[textIndex].responses[i].nextSentenceIndex;
                }
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    private void ResetDialogueBox()
    {
        npc_NameText.text = "";
        npc_DialogueText.text = "";
        for (int i = 0; i < responsButtons.Count; i++)
        {
            responsButtons[i].SetActive(false);
        }
    }
}
