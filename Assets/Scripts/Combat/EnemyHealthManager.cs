using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    GameObjectEvent onDeath;
    public int maxHealth;
    public int currentHealth;

    private bool isDead;
    public bool IsDead { get { return isDead; } }

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
    }

    //Deals damage
    public void HurtEnemy(int damage)
    {
        if (!isDead)
        {
            if (GetComponent<EnemyAnimationController>() != null)
            {
                GetComponent<EnemyAnimationController>().DamageTaken();
            }

            if (currentHealth > 0)
            {
                currentHealth -= damage;
            }

            if (currentHealth <= 0)
            {
                Died();
            }
            
        }
        
    }

    public void Died()
    {
        isDead = true;
        onDeath.Raise(this.gameObject);
    }
}
