using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	const float JUMP_FORCE = 200;
	const float MAX_SPEED = 1;

	private RopeScript rope;
	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Jump")){
			Jump();
		}
		Debug.Log(Mathf.Clamp(Input.GetAxis("Horizontal"), -MAX_SPEED, MAX_SPEED));
		rb.velocity = new Vector2(Mathf.Clamp(Input.GetAxis("Horizontal"), -MAX_SPEED, MAX_SPEED), rb.velocity.y);
	}

	void Jump (){
		rb.AddForce(Vector2.up * JUMP_FORCE);
		Debug.Log("Jumped!");
	}
}
