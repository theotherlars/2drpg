using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float speed;
    public Rigidbody2D rb;
    public float lifetime;
    public int damageToGive;

    // Start is called before the first frame update
    void Start()
    {

        //...setting shoot direction
        Vector3 shootDirection;
        shootDirection = Input.mousePosition;
        shootDirection.z = 0.0f;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = shootDirection - transform.position;

        //rb.velocity = transform.right * speed;
        rb.velocity = new Vector2(shootDirection.x * speed, shootDirection.y * speed);

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
        if (other.gameObject.CompareTag("Hostile"))
        {
            other.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(damageToGive);
            Destroy(gameObject);
        }

        //Ignores collision with player Funker ikke. FIKS!
        if (other.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(other.collider, gameObject.GetComponent<CircleCollider2D>());
        }
    }
}



 
