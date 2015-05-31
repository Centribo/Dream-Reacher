using UnityEngine;
using System.Collections;

public class BackgroundScript : MonoBehaviour {

	private GameObject currentBackground;

	// Use this for initialization
	void Start () {
		currentBackground = transform.Find ("background1").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (currentBackground.transform.position.y + " " + Camera.main.transform.position.y);
		if (!isVisible (currentBackground)) {
			Debug.Log (currentBackground);
			Vector2 newPosition = new Vector2(currentBackground.transform.position.x, currentBackground.transform.position.y + 36);
			currentBackground.transform.position = newPosition;
			currentBackground = nextObject();
		}
	}

	bool isVisible(GameObject gameObject) {
		float cameraOffset = Camera.main.transform.position.y;
		float objectOffset = gameObject.transform.position.y;
		if (cameraOffset - objectOffset >= 17) {
			return false;
		}
		return true;
	}

	GameObject nextObject() {
		if (currentBackground == transform.Find ("background1").gameObject) {
			return transform.Find ("background2").gameObject;
		} else {
			return transform.Find ("background1").gameObject;
		}
	}

	public void Reset() {
		transform.Find ("background1").position = new Vector3 (0, 0, 0);
		transform.Find ("background2").position = new Vector3 (0, 18, 0);
	}

}
