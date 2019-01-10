using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public bool isDead;
    public int player_HealthPoints = 100;



    // Start is called before the first frame update
    void Start()
    {
        player_HealthPoints = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Unfriendly")
        {
            player_HealthPoints--; 
        }
    }
}
