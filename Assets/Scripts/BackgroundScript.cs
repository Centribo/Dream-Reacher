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
		Debug.Log (currentBackground);
	}

}
