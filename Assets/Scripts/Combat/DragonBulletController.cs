using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBulletController : MonoBehaviour
{

    public float speed;
    public Rigidbody2D rb;
    public float lifetime;
    public int damageToGive;

    public PlayerController thePlayer;
    public DragonEnemyController theDragon;

    // Start is called before the first frame update
    void Start()
    {

        thePlayer = FindObjectOfType<PlayerController>();
        theDragon = FindObjectOfType<DragonEnemyController>();

        //...setting shoot direction
        Vector2 shootDirection;
        shootDirection = thePlayer.transform.position;

        //rb.velocity = transform.right * speed;
        rb.velocity = new Vector2(shootDirection.x * speed, shootDirection.y * speed);

        Physics2D.IgnoreCollision(theDragon.gameObject.GetComponent<PolygonCollider2D>(), gameObject.GetComponent<CircleCollider2D>());
    }

    void Update()
    {
        //Destroys bullet after set time
        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //Deals damage to enemy and destroys bullet
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().incomingDamage(damageToGive);
            Destroy(gameObject);
        }
    }
}




