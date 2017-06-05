using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {


	public int quantity = 8;
	public float cooldown = 1f;
	public GameObject prefab;
	private bool endedSpawn;
	//private GameManager gameManager;

	// Use this for initialization
	void Start () {
		endedSpawn = false;
		//SpawnWave (quantity);
	}
	
	// Update is called once per frame
	void Update () {
		if (endedSpawn) {
			if (gameObject.transform.childCount == 0) {
				AllDied ();
				endedSpawn = false;
			}
		}
	}

	void AllDied(){
		//gameManager.OnNotify ("AllDied");
		return;
	}

	public void SpawnWave(int numOfEnemy){
		StartCoroutine (SpawnSingle (numOfEnemy));

	}

	IEnumerator SpawnSingle(int remaining){
		yield return new WaitForSeconds (cooldown);
		var npc = Instantiate (prefab, gameObject.transform.position, Quaternion.identity);
		npc.gameObject.transform.SetParent(this.gameObject.transform);
		if (remaining > 0) {
			StartCoroutine (SpawnSingle (remaining - 1));
		} else {
			endedSpawn = true;
			Debug.Log ("Ended spawn wave");
		}
	}
}
