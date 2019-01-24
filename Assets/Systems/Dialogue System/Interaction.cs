using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{

    [SerializeField]
    private GameObject interactionText; //Only Temp

    private TextHandler textHandler;
    public Dialogue dialogue;
    public GameObject dialoguePanel;

    private bool isCollidingWithPlayer;

    private void Update()
    {
        if (Input.GetKey(KeyCode.E) && isCollidingWithPlayer)
        {
            switch (dialogue.NPCCategory)
            {
                case Dialogue.NPC_Category.Dialogue: // NPC with dialogue
                    {
                        interactionText.SetActive(false);
                        InitiateDialogue();
                        break;
                    }
                case Dialogue.NPC_Category.Quest: // NPC with quest
                    {
                        interactionText.SetActive(false);
                        InitiateQuest();
                        break;
                    }
                case Dialogue.NPC_Category.Shop:
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
    }

    private void Awake()
    {
        dialoguePanel.SetActive(false);
        textHandler = dialoguePanel.GetComponent<TextHandler>();
    }

    public void InitiateDialogue()
    {
        dialoguePanel.SetActive(true);
        textHandler.LoadDialogue(dialogue);
    }

    public void InitiateShop()
    {
        print("test");
        ShopController shopController = FindObjectOfType<ShopController>();
        print("this is the shopController: " + shopController.gameObject.name);
        if (shopController.gameObject.activeInHierarchy)
        {
            shopController.OpenShop();
        }
        
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
        }
    }
}
