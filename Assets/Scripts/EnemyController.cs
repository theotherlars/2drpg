using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    public GameObjectEvent onDeath;
    public event System.Action<float> OnHealthPercentChanged = delegate { };
    private NPCInformation npcInformation;
    private BoxCollider2D boxCollider2D;
    private NPC npc;
    private NPCMovementController npcMovementController;
    public NPCLootHandler lootHandler;
    public List<Item_SO> availableLoot = new List<Item_SO>();
    public int creditLoot;
    public float currentHealth;
    public float currentEnergy;
    public bool isDead;
    public float movementSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        npcInformation = GetComponent<NPCInformation>();
        npcMovementController = GetComponent<NPCMovementController>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        npc = npcInformation.npc;
        movementSpeed = npc.walkingSpeed;
        isDead = false;
    }
    public void Start()
    {
        currentHealth = npc.maxHealth;
        currentEnergy = npc.maxEnergy;
        
    }
    private void Update()
    {
        CheckHealth();
    }

    private void CheckHealth()
    {
        if (currentHealth > (npc.maxHealth / 2) && !isDead)
        {
            movementSpeed = npc.walkingSpeed;
        }
        else if (currentHealth <= (npc.maxHealth / 2) && !isDead)
        {
            movementSpeed = npc.runningSpeed;
        }
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth > 0 && !isDead)
        {
            currentHealth -= damage;
            float currentHealthPercentage = (float)currentHealth / (float)npc.maxHealth;
            OnHealthPercentChanged(currentHealthPercentage);
            if (!npcMovementController.isChasing)
            {
                npcMovementController.StartChase(FindObjectOfType<PlayerController>().transform);
            }
        }

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    void Die()
    {
        if (!isDead)
        {
            isDead = true;
            GetComponent<NPCMovementController>().Die();
            movementSpeed = 0;
            GenerateLoot();
            lootHandler.gameObject.SetActive(true);
            onDeath.Raise(this.gameObject);
            
            /*
            for (int i = 0; i < npc.gameObjectEvents.Count; i++)
            {
                if (npc.gameObjectEvents[i].events == NPCEvents.Events.OnDeath)
                {
                    npc.gameObjectEvents[i].gameObjectEvent.Raise(this.gameObject);
                    break;
                }
            }*/
        }
    }
    
    private void GenerateLoot()
    {
        if (UnityEngine.Random.Range(0f, 1f) > 0.5)
        {
            creditLoot = UnityEngine.Random.Range(npc.minCredit, npc.maxCredit);
        }
        else
        {
            creditLoot = 0;
        }
        
        int amountOfItemsToDrop = UnityEngine.Random.Range(0, npc.lootTable.Count); // How many items that dropped
        
        for (int i = 0; i < amountOfItemsToDrop; i++)
        {
            float roll = (float)UnityEngine.Random.Range(0f, 100f);

            if (roll <= npc.lootTable[i].ItemDropRate)
            {
                availableLoot.Add(npc.lootTable[i]);
            }
        }
    }
}
