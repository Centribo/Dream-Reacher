using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	const float JUMP_FORCE = 200;
	const float MAX_SPEED = 1;

	RopeScript rope;
	Rigidbody2D rb;
	bool isAiming;
	Vector2 target; //Used for trig, to show where the player is aiming relative to their pos

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		isAiming = false;
		target = new Vector2(0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Jump")){
			Jump();
		}
		rb.velocity = new Vector2(Mathf.Clamp(Input.GetAxis("Horizontal"), -MAX_SPEED, MAX_SPEED), rb.velocity.y);

		if(Input.GetButtonDown("Aiming")){ isAiming = true; Debug.Log("Start aiming!"); target = Vector2.zero; }
		if(Input.GetButtonUp("Aiming")){ isAiming = false; Fire(); }
		if(isAiming){ Aim(); }
	}

	//To be called once to jump
	void Jump (){
		rb.AddForce(Vector2.up * JUMP_FORCE);
		Debug.Log("Jumped!");
	}

	//To be called once to fire a rope
	void Fire (){
		Debug.Log("Fire!");
		float angleDeg = Vector2.Angle(Vector2.zero, target);
		Debug.Log("Angle: " + angleDeg);
	}

	//To be constantly called to update where the player is firing
	void Aim (){
		Debug.Log("Aiming!");
		Debug.Log(Input.GetAxis("Aim"));
		Debug.DrawLine(Vector2.zero, target);
		target.x += Mathf.Clamp(Input.GetAxis("Aim"), -0.01f, 0.01f);
	}
}
