using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{

    [SerializeField]
    private GameObject interactionText; //Only Temp

    //private TextHandler textHandler;
    public Dialogue dialogue;
    UIController uIController;
    //public GameObject dialoguePanel;
    //public ShopController shopController;

    private bool isCollidingWithPlayer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isCollidingWithPlayer)
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
        GameObject shopController = uIController.OpenShop();
        var shop = shopController.GetComponent<ShopController>();

        if (!shopController.activeSelf)
        {
            shop.OpenShop(gameObject.GetComponent<VendorController>());
        }
        else
        {
            shop.CloseShop();
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
