using UnityEngine;
using System.Collections;

public class DestroyScript : MonoBehaviour {
	
	private float destoryRadius = 20;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float cameraY = Camera.main.transform.position.y;
		float selfY = transform.position.y;
		if (selfY < cameraY - destoryRadius) {
			Destroy(gameObject);
		}
	}
}
