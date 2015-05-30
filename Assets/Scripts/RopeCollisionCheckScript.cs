using UnityEngine;
using System.Collections;

public class RopeCollisionCheckScript : MonoBehaviour {
	public bool ReachedDest { get; set;}


	private bool _hitCollider;
	private bool _sentSignal;

	// Use this for initialization
	void Start () {
		ReachedDest = false;
		_hitCollider = false;
		_sentSignal = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (ReachedDest && !_hitCollider && !_sentSignal) {
			GetComponent<RopeScript>().PullRopeBack();
			Debug.Log ("i didnt hit anythign");
			_sentSignal = true;
		}
	}
	public void CheckCollision (Vector3 startPos, Vector3 endPos) {
		Debug.Log("Reached here");
		RaycastHit2D raycast = Physics2D.Raycast((Vector2)startPos, (Vector2)endPos);
		Collider2D collider = raycast.collider;
		if (collider != null) {
			Collide (collider);
		}

	}


	public void Collide(Collider2D collider) {
		_hitCollider = true;
		// when the rope hits a player
		if (collider.gameObject.tag == "Player") {

		}
		// when the rope hits a platform
		else if (collider.gameObject.tag == "Ground") {
			GetComponent<RopeScript>().SwapOriginDest(collider.transform.position);
			GetComponent<RopeScript>().PullRopeBack();

		}
	}
}
