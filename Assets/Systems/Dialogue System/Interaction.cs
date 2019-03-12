using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    UIController uIController;
    [SerializeField]
    private GameObject interactionText; //Only Temp

    public Dialogue dialogue;
    private bool isCollidingWithPlayer;

    private void Start()
    {
        uIController = FindObjectOfType<UIController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isCollidingWithPlayer)
        {
            switch (dialogue.npc.type)
            {
                case NPC.NPC_Type.DialogueNPC: // NPC with dialogue
                    {
                        interactionText.SetActive(false);
                        InitiateDialogue();
                        break;
                    }
                case NPC.NPC_Type.Vendor:
                    {
                        interactionText.SetActive(false);
                        InitiateShop();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        /*if (Input.GetKeyDown(KeyCode.O))
        {
            uIController.ToggleQuestList();
        }*/
    }

    private void LateUpdate()
    {
        
    }

    private void CloseWindows()
    {
        uIController.CloseShop();
        uIController.CloseDialoguePanel();
    }

    private void Awake()
    {
        uIController = FindObjectOfType<UIController>();
    }

    public void InitiateDialogue()
    {
        var dialoguePanel = FindObjectOfType<UIController>().OpenDialoguePanel();
        var textHandler = dialoguePanel.GetComponent<TextHandler>();
        textHandler.LoadDialogue(dialogue);
    }

    public void InitiateShop()
    {
        uIController.OpenShop(this.GetComponent<VendorController>());
    }

    public void InitiateQuest()
    {
        print("Opens quest window");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            interactionText.SetActive(true);
            isCollidingWithPlayer = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            interactionText.SetActive(false);
            isCollidingWithPlayer = false;
            CloseWindows();
        }
    }
}
