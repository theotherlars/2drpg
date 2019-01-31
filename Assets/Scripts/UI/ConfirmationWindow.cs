using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConfirmationWindow : MonoBehaviour
{
    public TextMeshProUGUI confirmationDialogue;
    
    public void ConfirmationDialogue(string input)
    {
        confirmationDialogue.text = input;
    }

    public void OK_Button()
    {
        print("you bought the item");
        this.gameObject.SetActive(false);
    }

    public void Cancel_Button()
    {
        print("you canceled");
        this.gameObject.SetActive(false);
    }
}
