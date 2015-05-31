using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject winnerTextPrefab;
	public GameObject gameOverPrefab;
	public int maxScore;
	public int[] scores;
	public GameObject playerPrefab;
	public int playersNum;
	public Color[] colours;

	Vector3 originalPos;
	static public bool isRunning;

	// Use this for initialization
	void Start () {
		originalPos = Camera.main.transform.position;
		scores = new int[playersNum];
		for (int i = 1; i <= playersNum; i++) {
			scores[i-1] = 0;
		}
		SpawnPlayers();
		isRunning = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(isRunning){
			GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
			int playersLeft = players.Length;
			if(playersLeft == 0){
				EndRound();
				StartRound();
			} else if (playersLeft == 1){
				scores[players[0].GetComponent<PlayerScript>().playerNumber - 1] ++;
				bool over = false;
				for(int i = 0; i < scores.Length; i++){
					if(scores[i] >= maxScore){
						over = true;
						isRunning = false;
						EndGame(i+1);
					}
				}
				if(!over){
					EndRound();
					StartRound();
				}
			}
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
			GameObject spawn = (GameObject) Instantiate(playerPrefab, new Vector3(0, 0, 1), Quaternion.identity);
			spawn.GetComponent<PlayerScript>().playerNumber = i;
			spawn.name = "Player " + i;
			spawn.transform.localScale = new Vector3(2, 2, 1);
			spawn.GetComponent<Renderer>().material.color = colours[i-1];
		}
	}

	void StartRound(){
		Camera.main.transform.position = originalPos;
		Camera.main.GetComponent<CameraScript>().Reset();
		SpawnPlayers();
	}

	void EndGame(int winner){
		foreach (GameObject o in Object.FindObjectsOfType<GameObject>()) {
			if(o.name != "Main Camera"){
				Destroy(o, 1);
			}
		}
		GameObject winnerText = (GameObject)Instantiate(winnerTextPrefab, transform.position + new Vector3(0, 0, 0), Quaternion.identity);
		winnerText.GetComponent<TextMesh>().text = "Winner: " + winner;
        //Instantiate(gameOverPrefab, new Vector3(0, 1, 0), Quaternion.identity);
	}
}
