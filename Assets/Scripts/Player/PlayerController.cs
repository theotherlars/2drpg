using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private PlayerController player;

    public bool isDead;
    public int player_HealthPoints = 100;

    public float playerStaminaImpactOnHealth = 1f;

    public int player_Stamina = 10;
    public int Player_Stamina { get { return player_Stamina; } set { player_Stamina = value;} }

    public int player_Strength = 5;
    public int Player_Strength { get { return player_Strength; } set { player_Strength = value; } }

    public int player_Spirit = 7;
    public int Player_Spirit { get { return player_Spirit; } set { player_Spirit = value; } }

    public int player_Agility = 12;
    public int Player_Agility { get { return player_Agility; } set { player_Agility = value; } }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player_HealthPoints <= 0)
        {
            playerDied();
        }
    }

    public void UpdatePlayerAttributes(string attributeName, int attributeValue)
    {
        switch (attributeName)
        {
            case "Stamina":
                {
                    if (attributeValue > 0)
                    {
                        player_Stamina += attributeValue;
                        player_HealthPoints += Mathf.RoundToInt(attributeValue * 1);
                    }
                    else if (attributeValue < 0)
                    {
                        player_Stamina += attributeValue;
                        player_HealthPoints += Mathf.RoundToInt(attributeValue * 1);
                    }
                    break;
                }
            case "Strength":
                {
                    if (attributeValue > 0)
                    {
                        player_Strength += attributeValue;
                    }
                    else if (attributeValue < 0)
                    {
                        player_Strength += attributeValue;
                    }
                    break;
                }
            case "Spirit":
                {
                    if (attributeValue > 0)
                    {
                        player_Spirit += attributeValue;
                    }
                    else if (attributeValue < 0)
                    {
                        player_Spirit += attributeValue;
                    }
                    break;
                }
            case "Agility":
                {
                    if (attributeValue > 0)
                    {
                        player_Agility += attributeValue;
                    }
                    else if (attributeValue < 0)
                    {
                        player_Agility += attributeValue;
                    }
                    break;
                }
            default: { break; }
        }
    }

    private void playerDied()
    {
        isDead = true;
    }

    private void OnCollisionStay2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Unfriendly")
        {
            if(player_HealthPoints > 0)
            {
                player_HealthPoints--;
            }
        }
    }

    public void incomingDamage(int damage)
    {
        player_HealthPoints -= damage;
    }
}
