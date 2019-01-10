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
        if (player_HealthPoints <= 0)
        {
            playerDied();
        }
    }

    private void playerDied()
    {
        isDead = true;
        //Time.timeScale = 0;
    }

    private void OnCollisionStay2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Unfriendly")
        {
            if(player_HealthPoints > 0)
            {
                player_HealthPoints--;
            }
        }
    }
}
