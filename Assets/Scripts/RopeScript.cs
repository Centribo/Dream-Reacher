using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(RopeCollisionCheckScript))]
public class RopeScript : MonoBehaviour {
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
	private LineRenderer _lineRenderer; 	// line Renderer Controller
	private RopeCollisionCheckScript _ropeCollisioinScript;
	
	void Start(){
		_lineRenderer = GetComponent<LineRenderer>();
		_ropeCollisioinScript = GetComponent<RopeCollisionCheckScript>();
		_ropeFired = false;
		_destination = Vector3.zero;
		_origin = Vector3.zero;
		FireRope(test_pos, test_angle, test_vel, test_range);
	}

	void Update() {
		// render line only when the rope is fired
		if (_ropeFired && _count < _dist) {
			_count += .1f/_vel;
			float x = Mathf.Lerp(0, _dist, _count);
			Vector3 start = _origin;
			Vector3 end = _destination;

			Vector3 line = x * Vector3.Normalize(end - start) + start;

			_lineRenderer.SetPosition(1, line);

			// Sending signal to the collision check when everything is done
			if (ReachedDestCheck (line, _destination)) {
				_ropeFired = false;
				_ropeCollisioinScript.ReachedDest = true;
				Debug.Log  ("Send");
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
	public void FireRope (Vector2 pos, float angle, float vel, float range){
		_origin = pos;
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
}
