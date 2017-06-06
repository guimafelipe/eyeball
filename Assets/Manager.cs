using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

	public int currentWave, currentLine;
	private enum levelstate : int {waitingStartWave, startGame, inGame, gameOver};
	private int state;

	public GameObject UIsystem;
	public Text WaveNumber;

	public GameObject player;

	//public string[] script;
	public Spawner[] spawners;
	private int numOfSpawnerEnded;

	// Use this for initialization
	void Start () {
		currentWave = currentLine = 1;
		spawners = GetComponentsInChildren<Spawner> ();
		player = GameObject.Find ("Player");
		state = (int)levelstate.waitingStartWave;
	}
	
	// Update is called once per frame
	void Update () {
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
