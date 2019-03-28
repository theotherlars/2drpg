using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    UIController uIController;
    [SerializeField]
    private GameObject interactionText; //Only Temp

    //public Dialogue dialogue;
    private NPCInformation npcInformation;
    private bool isCollidingWithPlayer;

    private void Start()
    {
        uIController = FindObjectOfType<UIController>();
        npcInformation = GetComponent<NPCInformation>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isCollidingWithPlayer)
        {
            switch(npcInformation.npc.type)
            //switch (dialogue.npc.type)
            {
                case NPC.NPC_Type.Dialogue: // NPC with dialogue
                    {
                        uIController.ToggleInteractionTextOn(false);
                        InitiateDialogue();
                        break;
                    }
                case NPC.NPC_Type.QuestStart:
                    {
                        uIController.ToggleInteractionTextOn(false);
                        InitiateDialogue();
                        break;
                    }
                case NPC.NPC_Type.QuestEnd:
                    {
                        uIController.ToggleInteractionTextOn(false);
                        InitiateDialogue();
                        break;
                    }
                case NPC.NPC_Type.QuestBoth:
                    {
                        uIController.ToggleInteractionTextOn(false);
                        InitiateDialogue();
                        break;
                    }
                case NPC.NPC_Type.Vendor:
                    {
                        uIController.ToggleInteractionTextOn(false);
                        InitiateShop();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }

    private void CloseWindows()
    {
        uIController.CloseShop();
        uIController.CloseDialoguePanel();
        uIController.CloseConfirmationDialogue();
    }

    private void Awake()
    {
        uIController = FindObjectOfType<UIController>();
    }

    public void InitiateDialogue()
    {
        uIController.OpenDialoguePanel(npcInformation.npc.dialogue, this.gameObject);

        //var dialoguePanel = FindObjectOfType<UIController>().OpenDialoguePanel();
        //var textHandler = dialoguePanel.GetComponent<TextHandler>();

        // Load the NPC's dialogue into the UI
        //textHandler.LoadDialogue(npcInformation.npc.dialogue,this.gameObject);
    }

    public void InitiateShop()
    {
        uIController.OpenShop(npcInformation);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            uIController.ToggleInteractionTextOn();
            isCollidingWithPlayer = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            uIController.ToggleInteractionTextOn(false);
            
            isCollidingWithPlayer = false;
            CloseWindows();
        }
    }
}
