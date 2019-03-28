using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This will be these aspects of the enemy:
// - Health
// - Movement
// - DamageTaken


public class EnemyController : MonoBehaviour
{
    //public NPC npc;
    //public GameObjectEvent onDeath;
    public event System.Action<float> OnHealthPercentChanged = delegate { };
    private NPCInformation npcInformation;
    private NPC npc;
    public NPCLootHandler lootHandler;
    public List<Item_SO> availableLoot = new List<Item_SO>();
    [HideInInspector]
    public int creditLoot;

    public float currentHealth;
    public float currentEnergy;
    public bool isDead;

    private float movementSpeed;

    private Rigidbody2D rigidbody;
    [HideInInspector]
    public bool chase;
    Transform chaseObject;
    [HideInInspector]
    public bool idle;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        npcInformation = GetComponent<NPCInformation>();
        npc = npcInformation.npc;

        isDead = false;
        chase = false;
        idle = true;
    }
    public void Start()
    {
        currentHealth = npc.maxHealth;
        currentEnergy = npc.maxEnergy;
        movementSpeed = npc.walkingSpeed;
    }
    private void Update()
    {
        Chase();

    }

    public void TakeDamage(float damage)
    {
        if (currentHealth > 0 && !isDead)
        {
            currentHealth -= damage;
            float currentHealthPercentage = (float)currentHealth / (float)npc.maxHealth;
            OnHealthPercentChanged(currentHealthPercentage);
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
            chase = false;
            movementSpeed = 0;
            rigidbody.velocity = Vector2.zero;

            lootHandler.gameObject.SetActive(true);
            GenerateLoot();
            
            for (int i = 0; i < npc.gameObjectEvents.Count; i++)
            {
                if (npc.gameObjectEvents[i].events == NPCEvents.Events.OnDeath)
                {
                    npc.gameObjectEvents[i].gameObjectEvent.Raise(this.gameObject);
                    break;
                }
            }
        }
    }

    public void InitiateChase(GameObject go)
    {
        if (!chase)
        {
            chase = true;
            idle = false;
            chaseObject = go.transform;
        }
    }

    private void Chase()
    {
        if (currentHealth > (npc.maxHealth / 2) && chase && !isDead)
        {
            movementSpeed = npc.walkingSpeed;
            transform.position = Vector2.MoveTowards(transform.position, chaseObject.position, movementSpeed * Time.deltaTime);
        }
        else if (currentHealth <= (npc.maxHealth / 2) && chase && !isDead)
        {
            movementSpeed = npc.runningSpeed;
            transform.position = Vector2.MoveTowards(transform.position, chaseObject.position, movementSpeed * Time.deltaTime);
        }
    }

    private void GenerateLoot()
    {
        creditLoot = Random.Range(npc.minCredit, npc.maxCredit);
        
        int amountOfItemsToDrop = Random.Range(0, npc.lootTable.Count); // How many items that dropped
        
        for (int i = 0; i < amountOfItemsToDrop; i++)
        {
            float roll = (float)Random.Range(0f, 100f);

            if (roll <= npc.lootTable[i].ItemDropRate)
            {
                availableLoot.Add(npc.lootTable[i]);
            }
        }
    }
}
