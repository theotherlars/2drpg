using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// This will be these aspects of the enemy:
// - Health
// - Movement
// - DamageTaken


public class EnemyController : MonoBehaviour
{
    public NPC npc;
    public GameObjectEvent onDeath;
    public event Action<float> OnHealthPercentChanged = delegate { };

    public float currentHealth;
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

        currentHealth = npc.maxHealth;
        isDead = false;

        movementSpeed = npc.walkingSpeed;

        chase = false;
        idle = true;
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
            onDeath.Raise(this.gameObject);
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
}
