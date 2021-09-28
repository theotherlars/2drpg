﻿using System.Collections;
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
    public GameObject interactionText;
    public GameObject shopController;
    public GameObject inventoryPanel;
    public GameObject inventorySlots;
    public GameObject characterPanel;
    public GameObject confirmationDialogue;
    public GameObject dialoguePanel;
    public GameObject questActiveList;
    public GameObject uiQuestDetails;
    public GameObject lootWindow;
    public GameObject pauseMenu;
    public bool isLootWindowOpen { get { return lootWindow.activeInHierarchy; } }
    public Text playerHP;

    public GameObject deathMenu;
    private float lastToggle;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        deathMenu.SetActive(false);
        interactionText.SetActive(false);
        //inventoryPanel.SetActive(false);
        inventorySlots.SetActive(false);
        characterPanel.SetActive(false);
        confirmationDialogue.SetActive(false);
        dialoguePanel.SetActive(false);
        shopController.SetActive(false);
        questActiveList.SetActive(false);
        uiQuestDetails.SetActive(false);
        lootWindow.SetActive(false);
        pauseMenu.SetActive(false);
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

        if (Input.GetKeyDown(KeyCode.Escape)){
            this.TogglePauseMenu();
        }

        playerHP.text = "HP: " + playerController.player_HealthPoints.ToString();
    }

    private void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
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
        UIInventory uiInventory = FindObjectOfType<UIInventory>();
        uiInventory.backgroundImage.enabled = !uiInventory.backgroundImage.enabled;
        uiInventory.currencyDisplay.SetActive(!uiInventory.currencyDisplay.activeSelf);
        //inventoryPanel.SetActive(!inventoryPanel.gameObject.activeSelf);
        inventorySlots.SetActive(!inventorySlots.activeSelf);
    }

    public void ToggleInteractionTextOn(bool i = true)
    {
        interactionText.SetActive(i);
    }

    public GameObject OpenDialoguePanel()
    {
        dialoguePanel.SetActive(true);
        return dialoguePanel;
    }
    public void OpenDialoguePanel(Dialogue dialogue, GameObject go)
    {
        dialoguePanel.SetActive(true);
        dialoguePanel.GetComponent<TextHandler>().LoadDialogue(dialogue, go);
    }

    public void CloseDialoguePanel()
    {
        dialoguePanel.SetActive(false);
    }

    public void OpenLootWindow(GameObject go)
    {
        lootWindow.SetActive(true);
        ToggleInteractionTextOn(false);
        lootWindow.GetComponent<UILootWindowHandler>().UpdateLootTable(go);
    }

    public void CloseLootWindow()
    {
        lootWindow.SetActive(false);
    }

    public void OpenShop(NPCInformation npcInfo)
    {
        if (!shopController.activeSelf)
        {
            shopController.SetActive(true);
            shopController.GetComponent<ShopController>().OpenShop(npcInfo);
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
            else if (uiQuestDetails.activeInHierarchy && quest.id != uIQuestHandler.thisQuest.id)
            {
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

    public void LoadConfirmationDialouge(ConfirmationWindow.ConfirmationType type, string dialougeInput, Item_SO item, int amount)
    {   
        confirmationDialogue.SetActive(true);
        confirmationDialogue.GetComponent<ConfirmationWindow>().ConfirmationDialogue(type, dialougeInput, item, amount);
    }

    public void CloseConfirmationDialogue()
    {
        confirmationDialogue.SetActive(false);
    }

    public void LoadErrorText(string textInput)
    {
        errorText.GetComponent<ErrorText>().DisplayText(textInput);
    }


}
