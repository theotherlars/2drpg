using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    Animator animator;
    private Rigidbody2D rigidbody2D;
    Vector2 velocity;
    Vector2 positionLastFrame;
    Vector3 currentPos;
    Vector3 lastPos;
    public ParticleSystem lootParticles;
    public ParticleSystem damageTakenParticles;
    EnemyController enemyController;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        lootParticles.gameObject.SetActive(false);
        enemyController = GetComponent<EnemyController>();
    }
    
    // Update is called once per frame
    private void FixedUpdate()
    {
        AnimateMovement();
    }

    private void AnimateMovement()
    {
        currentPos = transform.position; 
        var direction = currentPos - lastPos;
        lastPos = transform.position;

        //velocity = rigidbody2D.velocity;
        //if (velocity != Vector2.zero && !enemyController.isDead)
        if (!enemyController.isDead)
        {
            animator.SetBool("IsWalking", true);
            animator.SetFloat("WalkingHorizontal", direction.x);
            animator.SetFloat("WalkingVertical", direction.y);
        }
        else if (enemyController.isDead)
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("Dead", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }

    public void DamageTaken()
    {
        damageTakenParticles.Emit(3);
    }

    public void LootParticles(bool active)
    {
        lootParticles.gameObject.SetActive(active);
    }
}
