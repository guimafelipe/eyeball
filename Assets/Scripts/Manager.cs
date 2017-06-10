using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Manager : MonoBehaviour {

	public int currentWave, currentLine;
	private enum levelstate : int {waitingStartWave, startGame, inGame, gameOver};
	private int state;

	public GameObject UIsystem;
	public Text WaveNumber;

	private bool gameEnded;
	public GameObject gameOverTexture;
	public GameObject player;

	//public string[] script;
	public Spawner[] spawners;
	private int numOfSpawnerEnded;

	// Use this for initialization
	void Start () {
		currentWave = currentLine = 1;
		gameEnded = false;
		spawners = GetComponentsInChildren<Spawner> ();
		player = GameObject.Find ("Player");
		gameOverTexture.SetActive (false);
		state = (int)levelstate.waitingStartWave;
	}
	
	// Update is called once per frame
	void Update () {
		if (player.GetComponent<Player> ().hp <= 0) {
			StartCoroutine (GameOver ());
		}
		if (gameEnded) {
			if (Input.GetKey (KeyCode.Space)) {
				SceneManager.LoadScene (0);
			}
		}
		switch (state) {
		case (int)levelstate.waitingStartWave:
			
			UIsystem.SetActive (true);
			WaveNumber.text = currentWave + "";
			if (Input.GetKey (KeyCode.Space)) {
				state = (int)levelstate.startGame;
			}
			break;
		case (int)levelstate.startGame:
			
			Debug.Log (currentWave);
			UIsystem.SetActive (false);
			foreach (Spawner _spawner in spawners) {
				_spawner.SpawnWave (1 << (currentWave - 1)); 
			}
			state = (int)levelstate.inGame;
			currentWave++;
			break;
		case (int)levelstate.inGame:
			int count = 0;
			foreach (Spawner _spawner in spawners) {
				if (_spawner.AllDied ()) {
					count++;
				}
			}
			if (count == spawners.Length) {
				state = (int)levelstate.waitingStartWave;
			}
			break;
		default:
			break;
		}
		//Debug.Log (state);
	}

	IEnumerator GameOver(){
		player.GetComponent<Player> ().enabled = false;
		player.GetComponent<SpriteRenderer>().enabled = false;
		yield return new WaitForSeconds (1f);
		gameOverTexture.SetActive (true);
		gameEnded = true;
		//player.SetActive (false);
	}

	void ThrowWave(int numOfEnemies){
		numOfSpawnerEnded = 0;
		foreach(Spawner _spawner in spawners){
			_spawner.SpawnWave (numOfEnemies);
		}
	}

	public void SpawnerEnded(){
		numOfSpawnerEnded++;
	}
}
