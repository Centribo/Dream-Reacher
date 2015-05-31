using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateTile : MonoBehaviour {

	public GameObject tile;
	public float mapHeight;
	public float mapWidth;
	public float tileHeightSpacing;
	public float tileLength;

	private float lastDrawnHeight;
	private float lastDrawnX;

	// Use this for initialization
	void Start () {
		lastDrawnHeight = mapHeight / 3.0f;
		lastDrawnX = Random.Range (0, mapWidth);

		Generate ();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("LastHeight: " + lastDrawnHeight + " Camera: " + GetCameraHeight ());
		if (lastDrawnHeight < GetCameraHeight ()) {
			Generate();
		}
	}

	//Generate a tile
	private void GenerateGameTile(float x, float y) {
		Vector2 position = new Vector2 (x-(mapWidth/2.0f), y-(mapHeight/2.0f));
		//Debug.Log (position);
		GameObject obj = Instantiate(tile, position, Quaternion.identity) as GameObject;
		obj.transform.parent = transform;
		float tileWidth = Random.Range (tileLength / 2.0f, tileLength * 1.5f);
		obj.transform.localScale = new Vector2 (tileWidth, tileLength);
		obj.AddComponent <DestroyScript>();
	}

	//Higher chance of picking a number furthur from x0
	private float ScaledRandom(float x0, float width) {
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
	private void Generate () {
		while (lastDrawnHeight < GetCameraHeight()+2) {
			GenerateGameTile(lastDrawnX, lastDrawnHeight);
			lastDrawnHeight = lastDrawnHeight + Random.Range(tileHeightSpacing/2.0f, tileHeightSpacing*1.5f);
			lastDrawnX = ScaledRandom(lastDrawnX*100, mapWidth*100)/100;
		}
	}

	private float GetCameraHeight() {
		return Camera.main.transform.position.y + mapHeight;
	}

	public void Reset(){
		var childList = new List<GameObject>();
		foreach (Transform child in transform){
			childList.Add(child.gameObject);
		}
		childList.ForEach(child => Destroy(child));
		Start ();
	}
}
