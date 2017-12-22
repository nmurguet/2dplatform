using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {


	public KeyCode left; 
	public KeyCode right; 
	public KeyCode jump; 

	private Rigidbody2D rb; 

	public float moveSpeed;
	public float jumpForce; 

	public Animator anim; 

	public Transform groundCheckPoint; 
	public bool isGrounded; 
	public float groundCheckRadius; 
	public LayerMask whatIsGround; 

	public Transform wallSensor; 
	public bool isTouchingWall;
	public float wallSensorRadius; 


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> (); 
		anim = GetComponent<Animator> (); 

		
	}
	
	// Update is called once per frame
	void Update () {
		Movement (); 
	}

	void Movement(){
		anim.SetFloat ("Speed", Mathf.Abs (rb.velocity.x));
		anim.SetBool ("Grounded", isGrounded);
		anim.SetFloat ("YSpeed", rb.velocity.y);

		isTouchingWall = Physics2D.OverlapCircle (wallSensor.position, wallSensorRadius, whatIsGround);
		isGrounded = Physics2D.OverlapCircle (groundCheckPoint.position, groundCheckRadius, whatIsGround);

		if (Input.GetKey (left)) {
			if (isTouchingWall && transform.localScale.x == -1) {
				rb.velocity = new Vector2 (0, rb.velocity.y);
			} else {
				rb.velocity = new Vector2 (-moveSpeed, rb.velocity.y);
			}

		} else if (Input.GetKey (right)) {
			if (isTouchingWall && transform.localScale.x == 1) {
				rb.velocity = new Vector2 (0, rb.velocity.y);
			} else
				rb.velocity = new Vector2 (moveSpeed, rb.velocity.y);

		} else {
			rb.velocity = new Vector2 (0, rb.velocity.y);

		}

		if (Input.GetKeyDown (jump) && isGrounded) {
			rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
		}
		if (rb.velocity.x < 0) {
			transform.localScale = new Vector3 (-1, 1, 1);
		} else if (rb.velocity.x > 0) {
			transform.localScale = new Vector3 (1, 1, 1);
		}

	}

}
