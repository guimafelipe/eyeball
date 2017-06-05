using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

	public int currentWave, currentLine;
	public string[] script;
	public Spawner[] spawners;
	private int numOfSpawnerEnded;

	// Use this for initialization
	void Start () {
		currentWave = currentLine = 0;

	}
	
	// Update is called once per frame
	void Update () {
		
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
