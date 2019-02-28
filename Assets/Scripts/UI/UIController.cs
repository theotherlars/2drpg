using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class UIController : MonoBehaviour
{
    PlayerController playerController;
    public GameObject errorText;
    public GameObject shopController;
    public GameObject inventoryPanel;
    public GameObject inventorySlots;
    public GameObject characterPanel;
    public GameObject testButtons;
    public GameObject confirmationDialogue;
    public GameObject dialoguePanel;
    public GameObject questActiveList;
    public GameObject uiQuestDetails;
    public Text playerHP;

    public GameObject deathMenu;
    private float lastToggle;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        deathMenu.SetActive(false);
        inventoryPanel.SetActive(false);
        inventorySlots.SetActive(false);
        characterPanel.SetActive(false);
        testButtons.SetActive(false);
        confirmationDialogue.SetActive(false);
        dialoguePanel.SetActive(false);
        shopController.SetActive(false);
        questActiveList.SetActive(false);
        uiQuestDetails.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventoryUI();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCharacterPanel();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            this.ToggleQuestList();
        }

        playerHP.text = "HP: " + playerController.player_HealthPoints.ToString();
    }

    private void FixedUpdate()
    {
        if (playerController.isDead)
        {
            deathMenu.SetActive(true);
        }
    }

    private void ToggleCharacterPanel()
    {
        characterPanel.SetActive(!characterPanel.activeSelf);
    }

    public void ToggleInventoryUI()
    {
        inventoryPanel.SetActive(!inventoryPanel.gameObject.activeSelf);
        inventorySlots.SetActive(!inventorySlots.activeSelf);
        testButtons.SetActive(!testButtons.activeSelf);
    }

    public GameObject OpenDialoguePanel()
    {
        dialoguePanel.SetActive(true);
        return dialoguePanel;
    }

    public void CloseDialoguePanel()
    {
        dialoguePanel.SetActive(false);
    }

    public void OpenShop(VendorController vendorController)
    {
        if (!shopController.activeSelf)
        {
            shopController.SetActive(true);
            shopController.GetComponent<ShopController>().OpenShop(vendorController);
        }
    }

    public void CloseShop()
    {
        if (shopController.activeSelf)
        {
            shopController.GetComponent<ShopController>().CloseShop();
            shopController.SetActive(false);
        }
    }

    public void ToggleQuestList()
    {
        if (lastToggle != Time.time)
        {
            lastToggle = Time.time;
            if (questActiveList.activeInHierarchy)
            {
                questActiveList.SetActive(false);
                uiQuestDetails.SetActive(false);
            }
            else
            {
                questActiveList.SetActive(true);
            }
        }
    }
    public void ToggleQuestDetailsWithoutAnimation(Quest quest)
    {
        UIQuestDetailsHandler uIQuestHandler = uiQuestDetails.GetComponent<UIQuestDetailsHandler>();
        if (lastToggle != Time.time)
        {
            lastToggle = Time.time;

            if (!uiQuestDetails.activeInHierarchy)
            {
                uiQuestDetails.SetActive(true);
                uIQuestHandler.OpenWithoutAnimation();
                uIQuestHandler.UpdateQuestDetails(quest);
            }
            else
            {
                uIQuestHandler.CloseWithoutAnimation();
                //uiQuestDetails.SetActive(false);
            }
        }
    }

    public void ToggleQuestDetailsWithAnimation(Quest quest)
    {
        UIQuestDetailsHandler uIQuestHandler = uiQuestDetails.GetComponent<UIQuestDetailsHandler>();
        if (lastToggle != Time.time)
        {
            lastToggle = Time.time;

            if (!uiQuestDetails.activeInHierarchy)
            {
                uiQuestDetails.SetActive(true);
                uIQuestHandler.AnimateOpenWindow();
                uIQuestHandler.UpdateQuestDetails(quest);
            }
            else
            {
                uIQuestHandler.CloseWithAnimation();
                //uiQuestDetails.SetActive(false);
            }
        }
    }
    
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadConfirmationDialouge(string dialougeInput, UIShopItem shopItem)
    {
        confirmationDialogue.SetActive(true);
        confirmationDialogue.GetComponent<ConfirmationWindow>().ConfirmationDialogue(dialougeInput, shopItem);
    }

    public void LoadErrorText(string textInput)
    {
        errorText.GetComponent<ErrorText>().DisplayText(textInput);
    }
}
