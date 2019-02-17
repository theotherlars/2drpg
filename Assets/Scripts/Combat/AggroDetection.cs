using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroDetection : MonoBehaviour
{
    RobotEnemyController parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<RobotEnemyController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            parent.chasePlayer = true;
        }
    }
}
