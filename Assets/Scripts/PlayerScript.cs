using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	const float JUMP_FORCE = 300;
	const float MAX_SPEED = 3;

	public GameObject ropePrefab;
	public float ropeRange;
	public float ropeSpeed;

	Animator animator;
	GameObject rope;
	Rigidbody2D rb;
	bool isAiming;
	Vector2 target; //Used for trig, to show where the player is aiming relative to their pos

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		animator.SetBool("Idle", true);
		animator.SetBool("Running", false);
		rb = GetComponent<Rigidbody2D>();
		isAiming = false;
		target = new Vector2(0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		IsGrounded();
		if(Input.GetButtonDown("Jump") && IsGrounded()){
			Jump();
		}
		if(Input.GetAxis("Horizontal") != 0){
			rb.velocity = new Vector2(Mathf.Clamp(Input.GetAxis("Horizontal")*MAX_SPEED, -MAX_SPEED, MAX_SPEED), rb.velocity.y);
			animator.SetBool("Idle", false);
			animator.SetBool("Running", true);
			animator.SetFloat("Direction", rb.velocity.x);
		} else {
			animator.SetBool("Idle", true);
			animator.SetBool("Running", false);
		}
		if(Input.GetButtonDown("Aiming")){ isAiming = true; Debug.Log("Start aiming!"); target = (Vector2)transform.position + Vector2.up; }
		if(Input.GetButtonUp("Aiming")){ isAiming = false; Fire(); }
		if(isAiming){ Aim(); }
		
		Animate();
		Debug.Log(Input.GetAxis("Horizontal"));
	}

	bool IsGrounded (){ //Returns true if player is standing on ground, false otherwise
		float spriteRange = 0.3f;
		float raycastRange = spriteRange + 0.05f;
		RaycastHit2D hit = Physics2D.Linecast(transform.position - new Vector3(0, spriteRange, 0), transform.position - new Vector3(0, raycastRange, 0));
		Debug.DrawLine(transform.position - new Vector3(0, spriteRange, 0), transform.position - new Vector3(0, raycastRange, 0));

		if(hit.collider == null){ //If the raycast didn't hit anything
			return false;
		} else if(hit.collider.tag == "Ground"){ //If it hit a ground's collider
			return true;
		} else { //If it hit anything else
			return false;
		}		
	}

	void Animate (){
		if(rb.velocity.x != 0){
			animator.SetFloat("Direction", rb.velocity.x);
		}
		if(rb.velocity == Vector2.zero){
			animator.SetBool("Idle", true);
			animator.SetBool("Running", false);
		}
	}

	//To be called once to jump
	void Jump (){
		rb.AddForce(Vector2.up * JUMP_FORCE);
	}

	//To be called once to fire a rope
	void Fire (){
		float angleDeg = Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x) * Mathf.Rad2Deg;
		angleDeg -= 90;
		angleDeg *= -1;
		float angleRad = angleDeg * Mathf.Deg2Rad;
		rope = (GameObject) Instantiate(ropePrefab, transform.position, Quaternion.identity);
		rope.transform.parent = transform;
		rope.GetComponent<RopeScript>().FireRope(angleRad, ropeSpeed, ropeRange);
	}

	//To be constantly called to update where the player is firing
	void Aim (){
		Debug.DrawLine(transform.position, target);
		target.x += Mathf.Clamp(Input.GetAxis("Aim"), -0.01f, 0.01f);
	}
}
