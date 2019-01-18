using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterUIController : MonoBehaviour
{
    private PlayerController player;

    [SerializeField]
    private TextMeshProUGUI staminaText;

    [SerializeField]
    private TextMeshProUGUI strengthText;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        staminaText.text = "Stamina: " + player.Player_Stamina.ToString();
        strengthText.text = "Strength: " + player.Player_Strength.ToString();
    }

    public void AddPlayerStamina()
    {
        player.Player_Stamina = 10;
    }
    public void SubtractPlayerStamina()
    {
        player.Player_Stamina = -10;
    }

}
