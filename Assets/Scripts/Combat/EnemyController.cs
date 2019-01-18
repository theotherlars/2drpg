using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private Rigidbody2D myRb;
    public float moveSpeed;

    public PlayerController thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        thePlayer = FindObjectOfType<PlayerController>(); //Finds player object
        
    }

    void FixedUpdate()
    {
        myRb.velocity = (transform.forward) * moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(thePlayer.transform.position);
    }
}
