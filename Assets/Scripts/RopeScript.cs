using UnityEngine;
using System.Collections;

public class RopeScript : MonoBehaviour {

	GameObject player;
	Vector3 end;
	Vector3 target;
	Vector3 initial;
	LineRenderer lineRenderer;
	float percent;
	float vel;
	int state; //0 Static, 1 being fired, 2 Retracting, 3 hit ground

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		switch(state){
			case 0:
			break;
			case 1:
				Extend();
				CheckCollision();
			break;
			case 2:
				Shrink();
			break;
			case 3:
				initial = player.transform.position + (Vector3)(Vector2.up * 0.3f);
				
				if(Vector2.Distance(initial, end) <= 1){
					Destroy(gameObject);
				}
			break;
		}
		UpdateLine();
		Debug.DrawLine(end, target);
	}

	void UpdateLine(){
		float dist = Vector2.Distance(initial, end);
		int segments = 1+(int)(dist/0.15f);
		lineRenderer.SetVertexCount(segments);
		lineRenderer.SetPosition(0, initial);
		float deltaX = (end.x - initial.x)/segments;
		float deltaY = (end.y - initial.y)/segments;
		for(int i = 1; i < segments; i++){
			lineRenderer.SetPosition(i, initial + new Vector3(deltaX * i, deltaY * i, 0)); 
		}
		lineRenderer.SetPosition(segments-1, end);
	}

	void Shrink(){
		percent -= vel;
		float x = Mathf.Lerp(initial.x, target.x, percent);
		float y = Mathf.Lerp(initial.y, target.y, percent);
		end = new Vector3(x, y, 0);
		if(percent <= 0){
			Destroy(gameObject);
		}
	}

	void Extend(){
		initial = player.transform.position + (Vector3)(Vector2.up * 0.3f);
		percent += vel;
		if(percent >= 1){
			state = 2;
		}
		float x = Mathf.Lerp(initial.x, target.x, percent);
		float y = Mathf.Lerp(initial.y, target.y, percent);
		end = new Vector3(x, y, 0);
	}
	
	void CheckCollision(){
		RaycastHit2D hit = Physics2D.Linecast(initial, end);
		if(hit.collider == null){
		} else {
			if(hit.collider.tag == "Ground"){
				state = 3;
				float magnitude = 500 * 1/Vector2.Distance(end, initial);
				Debug.Log("Mag: " + magnitude);
				player.GetComponent<Rigidbody2D>().AddForce((Vector2)(end-initial) * magnitude);
				Destroy(gameObject);
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
	public void FireRope (float angle, float vel, float range){
		lineRenderer = GetComponent<LineRenderer>();
		player = transform.parent.gameObject;
		state = 1;
		percent = 0;
		this.vel = vel;
		float x = Mathf.Sin(angle) * range + player.transform.position.x;
		float y = Mathf.Cos(angle) * range + player.transform.position.y;
		target = new Vector3(x, y, 0);
		end = player.transform.position;
		initial = player.transform.position;
	}
}
