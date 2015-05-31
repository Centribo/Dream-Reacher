using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public float speed;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Camera.main.transform.position = new Vector3 (Camera.main.transform.position.x, Camera.main.transform.position.y + Mathf.Pow ((Time.time / 400.0f), 2), Camera.main.transform.position.z);
		GameObject p1 = GameObject.Find ("Player 1");
		GameObject p2 = GameObject.Find ("Player 2");
		GameObject p3 = GameObject.Find ("Player 3");
		GameObject p4 = GameObject.Find ("Player 4");

		float min = Mathf.Min (p1.transform.position.y, p2.transform.position.y, p3.transform.position.y, p4.transform.position.y);
		float max = Mathf.Max (p1.transform.position.y, p2.transform.position.y, p3.transform.position.y, p4.transform.position.y);

		max = Mathf.Max (max, Camera.main.transform.position.y);

		float diff = max - min;
		float cameraSize = Mathf.Min (Mathf.Max (diff+2, 6), 9);
		Debug.Log (cameraSize);
		Camera.main.orthographicSize = cameraSize;
//		if (Input.GetKey(KeyCode.RightArrow)){
//			transform.position += Vector3.right * speed * Time.deltaTime;
//		}
//		if (Input.GetKey(KeyCode.LeftArrow)){
//			transform.position += Vector3.left * speed * Time.deltaTime;
//		}
//		if (Input.GetKey(KeyCode.UpArrow)){
//			transform.position += Vector3.up * speed * Time.deltaTime;
//		}
//		if (Input.GetKey(KeyCode.DownArrow)){
//			transform.position -= Vector3.up * speed * Time.deltaTime;
//		}
	}
}
