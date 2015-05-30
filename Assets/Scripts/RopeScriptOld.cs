using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(RopeCollisionCheckScript))]
public class RopeScriptOld : MonoBehaviour {
	/// <summary>
	/// test codes	
	/// </summary>
	public Vector2 test_pos;
	public float test_angle; 
	public float test_vel;
	public float test_range;

	private bool _ropeFired;
	private Vector3 _destination;
	private Vector3 _origin;
	private float _dist;
	private float _vel;
	private float _count = 0;
	private bool _pullRopeBack = false;
	private LineRenderer _lineRenderer; 	// line Renderer Controller
	private RopeCollisionCheckScript _ropeCollisioinScript;

	
	void Start(){

	}

	void Update() {
		// render line only when the rope is fired
		if (_ropeFired && _count < _dist) {
			_count += .1f/_vel;
			float x = Mathf.Lerp(0, _dist, _count);
			Vector3 start = _origin;
			Vector3 end = _destination;
			Vector3 newEnd;
			if (_pullRopeBack) {
				newEnd = end -  x * Vector3.Normalize(end - start);
			}
			else {
				newEnd = x * Vector3.Normalize(end - start) + start;
			}
			_lineRenderer.SetPosition(1, newEnd);
			//_ropeCollisioinScript.CheckCollision(start, newEnd);
			// Sending signal to the collision check when everything is done
			if (!_pullRopeBack && ReachedDestCheck (newEnd, _destination)) {
				_ropeCollisioinScript.ReachedDest = true;
				Debug.Log  ("Send");
			} 
			else if (_pullRopeBack && ReachedDestCheck(_origin, newEnd)) {
				Destroy (gameObject);
			}
		}
	}
	/////////////////////////////////////////////////////////////////
	/// Fire Rope function
	/// Description: Entrance Function for rendering a rope on the screen
	/// Parameters:  pos: Starting position of the character
	/// 			 angle (radian): the fixed angle for shooting 
	///              vel: initial velocity 
	///				 range: the range(length) of the rope 
	/////////////////////////////////////////////////////////////////
	public void FireRope (Vector3 pos, float angle, float vel, float range){
		_lineRenderer = GetComponent<LineRenderer>();
		_ropeCollisioinScript = GetComponent<RopeCollisionCheckScript>();
		_ropeFired = false;
		_destination = Vector3.zero;
		_origin = Vector3.zero;

		//Actual fire
		_origin = new Vector3(0, 0, 0);
		_vel = vel; 
		_destination.x = Mathf.Sin(angle) * range + _origin.x;
		_destination.y = Mathf.Cos(angle) * range + _origin.y;
		_dist = Vector3.Distance(_origin, _destination); 
		// set up the starting point
		_lineRenderer.SetPosition(0, _origin);
		_ropeFired = true;
	}

	/////////////////////////////////////////////////////////////////// 
	/// Check if the smooth Damp should stop
	/// returns 1 if the smooth damp can keep going
	/// returns 0 if the smooth damp should stop
	/////////////////////////////////////////////////////////////////// 
	private bool ReachedDestCheck (Vector3 currPos, Vector3 targetPos) {
		return (Mathf.Abs(targetPos.x - currPos.x) <= 0.01)
			 	&& (Mathf.Abs(targetPos.y - currPos.y) <= 0.01);
	}
	public void PullRopeBack () {
		_count = 0;
		_ropeFired = true;
		_pullRopeBack = true;
	}

	public void SwapOriginDest (Vector3 newDest){
		Vector3 temp = _origin;
		_origin = newDest;
		_destination = temp;
	}
}
