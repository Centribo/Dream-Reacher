using UnityEngine;
using System.Collections;

public class GenerateTile : MonoBehaviour {

	public GameObject tile;

	private float lastDrawnHeight;
	private float lastDrawnX;
	// Use this for initialization
	void Start () {
		float height = Camera.main.pixelHeight;
		float width = Camera.main.pixelWidth;
		lastDrawnHeight = height / 3.0f;
		lastDrawnX = Random.Range (0, width);

		Generate ();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("LastHeight: " + lastDrawnHeight + " Camera: " + GetCameraHeight ());
		if (lastDrawnHeight < GetCameraHeight ()) {
			Generate();
		}
	}

	//Generate a tile
	void GenerateGameTile(float x, float y) {
		Vector2 position = Camera.main.ScreenToWorldPoint (new Vector2 (x, y));
		GameObject obj = Instantiate(tile, position, Quaternion.identity) as GameObject;
		obj.transform.localScale = new Vector2 (10.0f, 0.5f);
	}

	//Higher chance of picking a number furthur from x0
	float ScaledRandom(float x0, float width) {
		float leftRange = -(x0) * (x0 + 1) / 2.0f;
		float rightRange = (width-x0) * ((width-x0) + 1) / 2.0f;
		//Debug.Log ("left: "+leftRange+" Right: "+rightRange);
		float random = Random.Range (leftRange, rightRange);
		float n = (-1 + (Mathf.Sqrt (1 + 8 * Mathf.Abs (random)))) / 2.0f;
		if (random > 0) {
			n = n + x0;
		} else {
			n = x0 - n;
		}
		//Debug.Log ("x: " + n + "Random: " + random);
		return n;
	}
	// Generate starting tiles
	void Generate () {
		float width = Camera.main.pixelWidth;
		while (lastDrawnHeight < GetCameraHeight()+100) {
			GenerateGameTile(lastDrawnX, lastDrawnHeight);
			lastDrawnHeight = lastDrawnHeight + Random.Range(50,75);
			lastDrawnX = ScaledRandom(lastDrawnX, width);
		}
	}

	float GetCameraHeight() {
		return (Camera.main.ViewportToScreenPoint(Camera.main.transform.position).y + Camera.main.pixelHeight);
	}
}
