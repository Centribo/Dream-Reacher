using UnityEngine;
using System.Collections;

public class RopeCollisionCheckScript : MonoBehaviour {
	public bool ReachedDest { get; set;}


	private bool _hitCollider;


	// Use this for initialization
	void Start () {
		ReachedDest = false;
		_hitCollider = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (ReachedDest && !_hitCollider) {
			Debug.Log ("i didnt hit anythign");
		}
	}

	void OnCollisionEnter2D(Collision2D collider) {
		// when the rope hits a player
		if (collider.gameObject.tag == "Player") {

		}
		// when the rope hits a platform
		else if (collider.gameObject.tag == "Platform") {


		}
	}
}
