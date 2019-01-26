using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonWeaponController : MonoBehaviour
{

    public Transform firePoint;
    public DragonBulletController bulletPrefab;
    public bool isFiring;
    public float timeBetweenShots;
    private float shotCounter;
    public PlayerController thePlayer;
    private float distanceToPlayer;
    public float aggroRange;

    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>(); //Finds player object
        distanceToPlayer = Vector2.Distance(gameObject.transform.position, thePlayer.transform.position);
    }

    // Update is called once per frame
    void Update()
    {

        if (distanceToPlayer < aggroRange)
        {
            isFiring = true;
        }

        //Delay between shots
        if (isFiring)
        {
            shotCounter -= Time.deltaTime;

            if (shotCounter <= 0)
            {
                shotCounter = timeBetweenShots;
                Shoot();
                isFiring = false;
            }
            isFiring = false;
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
