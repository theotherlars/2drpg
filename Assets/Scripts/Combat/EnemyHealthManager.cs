using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{

    public int maxHealth;

    [HideInInspector]
    public int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Kills enemy
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    //Deals damage
    public void HurtEnemy(int damage)
    {
        currentHealth -= damage;
    }
}
