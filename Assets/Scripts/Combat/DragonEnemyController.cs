using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonEnemyController : MonoBehaviour
{

    private Rigidbody2D myRb;
    public float moveSpeed;
    private static int currentHealth;
    private static int maxHealth = 100;
    public float timeBetweenShots;
    private float shotCounter;
    private bool isFiring;
    public Transform firePoint;
    public DragonBulletController bulletPrefab;

    public PlayerController thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        thePlayer = FindObjectOfType<PlayerController>(); //Finds player object

        //Get max health from health manager
        GameObject enemyRobot = GameObject.Find("EnemyRobot");
        EnemyHealthManager robotEnemyHealthManager = enemyRobot.GetComponent<EnemyHealthManager>();
        maxHealth = robotEnemyHealthManager.maxHealth;
        shotCounter = 0;

    }

    void FixedUpdate()
    {
        //Sets speed for enemy
        myRb.velocity = (transform.forward) * moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //Makes enemy always move towards player
        transform.position = Vector2.MoveTowards(transform.position, thePlayer.transform.position, moveSpeed * Time.deltaTime);

        //Gets current health from EnemyHealthManager
        currentHealth = EnemyHealthManager.currentHealth;

        //Increases speed at half health
        if (currentHealth <= (maxHealth / 2))
        {
            moveSpeed = 4;
        }

        //Shooting
        if (isFiring)
        {
            shotCounter -= Time.deltaTime;

            if (shotCounter <= 0)
            {
                shotCounter = timeBetweenShots;
                Shoot();
                isFiring = false;
            }
        }
        else
        {
            shotCounter = 0;
        }
    }

    void Shoot()
    {

        //...instantiating the bullet from the firepoint
        DragonBulletController newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation) as DragonBulletController;
    }
}
