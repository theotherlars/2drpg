using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotEnemyHealthManager : MonoBehaviour
{

    public static int maxHealth = 80;
    public static int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void HurtEnemy(int damage)
    {
        currentHealth -= damage;
    }
}
