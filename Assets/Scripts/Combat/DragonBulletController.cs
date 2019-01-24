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

    // Start is called before the first frame update
    void Start()
    {

        thePlayer = FindObjectOfType<PlayerController>();

        //...setting shoot direction
        Vector3 shootDirection;
        shootDirection = thePlayer.transform.position;

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
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().incomingDamage(damageToGive);
            Destroy(gameObject);
        }
    }
}




