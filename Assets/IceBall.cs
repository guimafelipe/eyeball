using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBall : MonoBehaviour {
	public GameObject explosion;

	GameObject player;
	Vector3 dir;

	float speed = 1.5f, lifetime = 25f;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		Vector3 mousePos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10f);
		mousePos = Camera.main.ScreenToWorldPoint (mousePos);
		dir = new Vector3 ();

		dir = mousePos - gameObject.transform.position;
		dir = dir.normalized;
	}

	void Update(){
		lifetime -= Time.deltaTime;
		if (lifetime < 0) {
			Destroy (gameObject);
		}
		gameObject.transform.position = new Vector3 (gameObject.transform.position.x + dir.x * speed * Time.deltaTime, gameObject.transform.position.y + dir.y * speed * Time.deltaTime,
			gameObject.transform.position.z);
	}

}
