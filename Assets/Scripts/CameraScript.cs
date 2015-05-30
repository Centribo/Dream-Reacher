using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public float speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Camera.main.transform.position = new Vector3 (Camera.main.transform.position.x, Camera.main.transform.position.y + Mathf.Pow ((Time.time / 600.0f), 2), Camera.main.transform.position.z);
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
