using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTest : MonoBehaviour {

[SerializeField]private float movementSpeed = 100.0f;
	private Rigidbody2D rigidbody2D;
	private float horizontalMove = 0f;
	private float verticalMove = 0f;
	
	//private Animator animator;



	// Use this for initialization
	void Start ()
	{
		rigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	// This is where we should get user input
	void Update () 
	{
		horizontalMove 	= Input.GetAxisRaw("Horizontal"); // A or D button
		verticalMove 	= Input.GetAxisRaw("Vertical"); // W or S button
	}

	private void FixedUpdate() 
	{
		rigidbody2D.velocity = new Vector2(0,0); // reset velocity each frame

		if(horizontalMove > 0) // D button, move right
		{
			rigidbody2D.velocity = new Vector2(movementSpeed * Time.deltaTime,rigidbody2D.velocity.y);
		}
		else if(horizontalMove < 0) // A button, move left
		{
			rigidbody2D.velocity = new Vector2(-movementSpeed * Time.deltaTime,rigidbody2D.velocity.y);
		}

		if(verticalMove > 0) // W button, move up
		{
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x,movementSpeed * Time.deltaTime);
		}
		else if(verticalMove < 0) // S button, move down
		{
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x,-movementSpeed * Time.deltaTime);
		}
	}
}
