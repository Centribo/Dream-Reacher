using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	const float JUMP_FORCE = 300;
	const float MAX_SPEED = 3;
	const float range = 15;

	public int playerNumber;
	public GameObject ropePrefab;
	public float ropeRange;
	public float ropeSpeed;
	public float cooldown;
	public GameObject deathPrefab;

	float stageHeight;
	float stageWidth;
	float cooldownTimer;
	Animator animator;
	GameObject rope;
	Rigidbody2D rb;
	bool isAiming;
	Vector2 target; //Used for trig, to show where the player is aiming relative to their pos

	// Use this for initialization
	void Start () {
		stageWidth = Camera.main.ViewportToWorldPoint(Vector3.one).x;
		stageHeight = Camera.main.ViewportToWorldPoint(Vector3.one).y;
		animator = GetComponent<Animator>();
		animator.SetBool("Idle", true);
		animator.SetBool("Running", false);
		rb = GetComponent<Rigidbody2D>();
		isAiming = false;
		target = new Vector2(0, 0);
		cooldownTimer = cooldown;
		
	}
	
	// Update is called once per frame
	void Update () {
		//IsGrounded();
		cooldownTimer -= Time.deltaTime;
		if(Input.GetButtonDown("Jump" + playerNumber) && IsGrounded()){
			Jump();
		}
		if(Input.GetAxis("Horizontal" + playerNumber) != 0){
			rb.velocity = new Vector2(Mathf.Clamp(Input.GetAxis("Horizontal" + playerNumber)*MAX_SPEED, -MAX_SPEED, MAX_SPEED), rb.velocity.y);
			animator.SetBool("Idle", false);
			animator.SetBool("Running", true);
			animator.SetFloat("Direction", rb.velocity.x);
		} else {
			animator.SetBool("Idle", true);
			animator.SetBool("Running", false);
		}
		if(Input.GetButtonDown("Aiming" + playerNumber)){ isAiming = true; target = (Vector2)transform.position + Vector2.up; }
		if(Input.GetButtonUp("Aiming" + playerNumber) && cooldownTimer <= 0){ isAiming = false; Fire(); }
		if(isAiming){ Aim(); }
		
		Animate();
		Camera.main.transform.position = new Vector3 (Camera.main.transform.position.x, Mathf.Max(transform.position.y-2.0f, Camera.main.transform.position.y), Camera.main.transform.position.z);
		if(transform.position.x > stageWidth){
			transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z); 
		} else if (transform.position.x < -stageWidth){
			transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z); 
		}

		if(transform.position.y < Camera.main.transform.position.y - Camera.main.orthographicSize - 3){
			Die();
		}
	}

	bool IsGrounded (){ //Returns true if player is standing on ground, false otherwise
		float spriteRange = 0.6f;
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
		cooldownTimer = cooldown;
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

		target = (Vector2)transform.position + new Vector2(Input.GetAxis ("AimX" + playerNumber), -Input.GetAxis ("AimY" + playerNumber));
		//target.x += Mathf.Clamp(Input.GetAxis("Aim"), -0.01f, 0.01f);
	}

	void Die (){
		GameObject explosion = (GameObject)Instantiate(deathPrefab, new Vector3(transform.position.x, Camera.main.ViewportToWorldPoint(Vector3.zero).y + 1.7f, 0), Quaternion.identity);
		explosion.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
		Destroy(explosion, 1);
		Destroy(gameObject);
	}
}
