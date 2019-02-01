using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConfirmationWindow : MonoBehaviour
{
    public TextMeshProUGUI confirmationDialogue;

    private UIShopItem shopItem;

    public void ConfirmationDialogue(string input, UIShopItem shopItem = null)
    {
        confirmationDialogue.text = input;
        if (shopItem != null)
        {
            this.shopItem = shopItem;
        }
    }

    public void OK_Button()
    {
        if (shopItem != null)
        {
            shopItem.BuyItem();
        }
        this.gameObject.SetActive(false);
    }

    public void Cancel_Button()
    {
        shopItem = null;
        this.gameObject.SetActive(false);
    }
}
