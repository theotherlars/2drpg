using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotEnemyController : MonoBehaviour
{
    private PlayerController thePlayer;
    public NPC npc;

    private Rigidbody2D myRb;
    private EnemyHealthManager enemyHealthManager;

    private static float currentHealth;
    private static float maxHealth = 100;

    public float moveSpeed;
    
    public int damageToGive;
    public float timeBetweenHits;
    private float timeBetweenHitsCounter;
    public bool chasePlayer;

    
    

    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        thePlayer = FindObjectOfType<PlayerController>(); //Finds player object
        chasePlayer = false;

        // Get max health from health manager
        enemyHealthManager = GetComponent<EnemyHealthManager>();
        //maxHealth = enemyHealthManager.maxHealth;
        maxHealth = npc.maxHealth;
        
        moveSpeed = npc.walkingSpeed;
        timeBetweenHitsCounter = 0;

    }

    void FixedUpdate()
    {
        //Sets speed for enemy
        myRb.velocity = (transform.forward) * moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        ChasePlayer();
        //Gets current health from EnemyHealthManager
        currentHealth = enemyHealthManager.currentHealth;
       
        //Increases speed at half health
        if (currentHealth <= (maxHealth/2))
        {
            moveSpeed = npc.runningSpeed;
        }
    }


    void ChasePlayer()
    {
        if (chasePlayer)
        {
            if (!enemyHealthManager.IsDead)
            {
                //Makes enemy always move towards player
                transform.position = Vector2.MoveTowards(transform.position, thePlayer.transform.position, moveSpeed * Time.deltaTime);
            }
            else
            {
                myRb.isKinematic = true;
                moveSpeed = 0;
            }
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        //Deals damage to player
        if (other.gameObject.CompareTag("Player") && (timeBetweenHitsCounter <= 0) && !enemyHealthManager.IsDead)
        {
            other.gameObject.GetComponent<PlayerController>().incomingDamage(damageToGive);
            timeBetweenHitsCounter = timeBetweenHits;
        }
        timeBetweenHitsCounter -= Time.deltaTime;
    }
}
