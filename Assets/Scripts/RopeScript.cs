using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class RopeScript : MonoBehaviour {
	/// <summary>
	/// test codes	
	/// </summary>
	public Vector2 test_pos;
	public float test_angle; 
	public float test_vel;
	public float test_range;

	// line Renderer Controller
	private LineRenderer _lineRenderer;
	private Transform _destination;
	private Transform _origin;

	public void Start(){
		_lineRenderer = GetComponent<LineRenderer>();
		FireRope(test_pos, test_angle, test_vel, test_range);
	}

	/////////////////////////////////////////////////////////////////
	/// Fire Rope function
	/// Description: Entrance Function for rendering a rope on the screen
	/// Parameters:  pos: Starting position of the character
	/// 			 angle (radian): the fixed angle for shooting 
	///              vel: initial velocity 
	///				 range: the range(length) of the rope 
	/////////////////////////////////////////////////////////////////
	public int FireRope (Vector2 pos, float angle, float vel, float range){

		//_lineRenderer.SetPosition
		return 1;
	}
	private void SetValues (Vector2 pos, float angle, float range) {
		_origin.position = pos;



	}
}
