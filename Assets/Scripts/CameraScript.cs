using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public float speed;
	float timeCounter;
	// Use this for initialization
	void Start () {
		timeCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
		timeCounter += Time.deltaTime;
		
		if(GameManager.isRunning){
			Camera.main.transform.position = new Vector3 (Camera.main.transform.position.x, Camera.main.transform.position.y + Mathf.Pow ((timeCounter / 400.0f), 2), Camera.main.transform.position.z);
		} else {
			if(Input.GetKey(KeyCode.N)){
				Application.LoadLevel("MainMenu");
			}
		}
		
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		float min = 0;
		float max = 0;
		if(players.Length > 0){
			min = players[0].transform.position.y;
			max = players[0].transform.position.y;
		}
		foreach (GameObject p in players) {
			if(p.transform.position.y < min){
				min = p.transform.position.y;
			}

			if(p.transform.position.y > max){
				max = p.transform.position.y;
			}
		}

		max = Mathf.Max (max, Camera.main.transform.position.y);

		float diff = max - min;
		float cameraSize = Mathf.Min (Mathf.Max (diff+2, 6), 9);
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

	public void Reset(){
		timeCounter = 0;
	}
}
