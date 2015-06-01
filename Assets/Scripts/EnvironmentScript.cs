using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnvironmentScript : MonoBehaviour {

	public GameObject buildingLeftSmall;
	public GameObject buildingLeftBig;
	public GameObject buildingRightSmall;
	public GameObject buildingRightBig;

	private float lastBuildingHeight;

	enum Building {LeftSmall=0, LeftBig, RightSmall, RightBig};
	enum Side {Left=0, Right};

	// Use this for initialization
	void Start () {
		lastBuildingHeight = 0;

		for (int i=0; i<4; i+=2) {
			lastBuildingHeight = Random.Range(2, 5);
			GameObject building = createBuilding(lastBuildingHeight, Random.Range(0+i,2+i), Random.Range(0,2));
			building.transform.parent = transform;
			building.GetComponent<SpriteRenderer>().sortingOrder = -5;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (lastBuildingHeight < GetCameraHeight()) {
			float random = Random.Range(lastBuildingHeight + 1.5f, lastBuildingHeight + 3.0f);
			lastBuildingHeight = random;
			GameObject building = createBuilding(random, Random.Range(0,4), Random.Range(0,2));
			building.transform.parent = transform;
			building.GetComponent<SpriteRenderer>().sortingOrder = -5;
		}
	}

	GameObject createBuilding(float y, int type, int side){
		float x;
		GameObject obj = buildingRightBig;
		switch (type) {
			case (int)Building.LeftBig:
				x = -8.07f;
				obj = buildingLeftBig;
				break;
			case (int)Building.LeftSmall:
				x = -8.31f;
				obj = buildingLeftSmall;
				break;
			case (int)Building.RightBig:
				x = 7.71f;
				obj = buildingRightBig;
				break;
			case (int)Building.RightSmall:
				x = 7.71f;
				obj = buildingRightSmall;
				break;
			default:
				x = 0.0f;
				break;
		}
		Quaternion rotation = Quaternion.identity;
		if ((side == (int)Side.Left && (type == (int)Building.RightBig || type == (int)Building.RightSmall)) ||
			(side == (int)Side.Right && (type == (int)Building.LeftBig || type == (int)Building.LeftSmall))) {
			rotation = Quaternion.Euler(0, 180, 0);
			x = x*-1;
		}
		GameObject building = Instantiate (obj, new Vector2 (x, y), rotation) as GameObject;
		building.AddComponent <DestroyScript>();
		return building;
	}

	private float GetCameraHeight() {
		return Camera.main.transform.position.y + 20;
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
