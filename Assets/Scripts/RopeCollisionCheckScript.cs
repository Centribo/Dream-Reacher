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

	void OnCollisionEnter2D(Collision2D collider) {
		_hitCollider = true;
		// when the rope hits a player
		if (collider.gameObject.tag == "Player") {

		}
		// when the rope hits a platform
		else if (collider.gameObject.tag == "Platform") {


		}
	}
}
