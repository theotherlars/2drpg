using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{

    public int maxHealth;

    [HideInInspector]
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
        //Kills enemy
        if (currentHealth <= 0)
        {
            isDead = true;
            //Destroy(this.gameObject);
        }
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
            currentHealth -= damage;
        }
        
    }
}
