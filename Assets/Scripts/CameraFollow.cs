using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {


	GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = new Vector3 (player.transform.position.x, gameObject.transform.position.y,
			gameObject.transform.position.z);
	}
}
