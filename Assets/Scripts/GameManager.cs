using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public int maxScore;
	public int[] scores;
	public GameObject playerPrefab;
	public int playersNum;
	public Color[] colours;

	Vector3 originalPos;

	// Use this for initialization
	void Start () {
		originalPos = transform.position;
		scores = new int[playersNum];
		for (int i = 1; i <= playersNum; i++) {
			scores[i-1] = 0;
		}
		SpawnPlayers();
	}
	
	// Update is called once per frame
	void Update () {
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		int playersLeft = players.Length;
		if(playersLeft == 0){
			//Debug.Log("Tie!");
		} else if (playersLeft == 1){
			scores[players[0].GetComponent<PlayerScript>().playerNumber - 1] ++;
			Debug.Log("Winner: " + players[0].GetComponent<PlayerScript>().playerNumber);
			EndRound();
			StartRound();
		}
	}

	void EndRound(){
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject p in players) {
			Destroy(p);
		}
	}

	void SpawnPlayers(){
		for (int i = 1; i <= playersNum; i++) {
			GameObject spawn = (GameObject) Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
			spawn.GetComponent<PlayerScript>().playerNumber = i;
			spawn.name = "Player " + i;
			spawn.transform.localScale = new Vector3(2, 2, 1);
			spawn.GetComponent<Renderer>().material.color = colours[i-1];
		}
	}

	void StartRound(){
		transform.position = originalPos;
		SpawnPlayers();
	}
}
