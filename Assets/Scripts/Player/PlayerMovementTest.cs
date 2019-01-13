using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTest : MonoBehaviour {

	[SerializeField]private float movementSpeed = 100.0f;
	private new Rigidbody2D rigidbody2D;
	private float horizontalMove = 0f;
	private float verticalMove = 0f;
    [SerializeField] private float animationSpeed = 0.35f;

    PlayerController playerController;

	private Animator animator;

	// Use this for initialization
	void Start ()
	{
		rigidbody2D = GetComponent<Rigidbody2D>();
        playerController = FindObjectOfType<PlayerController>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	// This is where we should get user input
	void Update () 
	{
        if (playerController.isDead == false)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal"); // A or D button
            verticalMove = Input.GetAxisRaw("Vertical"); // W or S button
        }
	}

	private void FixedUpdate() 
	{
        if ((verticalMove == 0.0f) && (horizontalMove == 0.0f))
        {
            animator.speed = 0.0f;
        }

        rigidbody2D.velocity = new Vector2(0,0); // reset velocity each frame

		if(horizontalMove > 0) // D button, move right
		{
			rigidbody2D.velocity = new Vector2(movementSpeed * Time.deltaTime,rigidbody2D.velocity.y);
            animator.SetInteger("Direction", 1);
            animator.speed = animationSpeed;
		}
		else if(horizontalMove < 0) // A button, move left
		{
			rigidbody2D.velocity = new Vector2(-movementSpeed * Time.deltaTime,rigidbody2D.velocity.y);
            animator.SetInteger("Direction", 3);
            animator.speed = animationSpeed;
        }

		if(verticalMove > 0) // W button, move up
		{
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x,movementSpeed * Time.deltaTime);
            animator.SetInteger("Direction", 0);
            animator.speed = animationSpeed;
        }
		else if(verticalMove < 0) // S button, move down
		{
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x,-movementSpeed * Time.deltaTime);
            animator.SetInteger("Direction", 2);
            animator.speed = animationSpeed;
        }
	}
}
