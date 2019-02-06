using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonEnemyController : MonoBehaviour
{

    private Rigidbody2D myRb;
    EnemyHealthManager dragonEnemyHealthManager;
    public float moveSpeed;
    private static int currentHealth;
    private static int maxHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();

        //Get max health from health manager
        //GameObject enemyDragon = GameObject.Find("EnemyDragon");

        dragonEnemyHealthManager = GetComponent<EnemyHealthManager>();
        maxHealth = dragonEnemyHealthManager.maxHealth;
    }

    void FixedUpdate()
    {
        //Sets speed for enemy
        //myRb.velocity = (transform.forward) * moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //Makes enemy always move towards player
        //transform.position = Vector2.MoveTowards(transform.position, thePlayer.transform.position, moveSpeed * Time.deltaTime);

        //Gets current health from EnemyHealthManager
        currentHealth = dragonEnemyHealthManager.currentHealth;

        //Increases speed at half health
        /*if (currentHealth <= (maxHealth / 2))
        {
            moveSpeed = 4;
        }*/
    }
}
