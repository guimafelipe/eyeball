using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed = 15f, lifetime = 20f;
	int damage = 5;
	GameObject player;
	Vector3 destination;
	Rigidbody2D rb;
	Vector3 dir;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		destination = player.transform.position;
		dir = destination - this.transform.position;
		dir = dir.normalized;
		//rb = gameObject.GetComponent<Rigidbody2D> ();
		//rb.velocity = new Vector2(dir.x * speed, dir.y*speed);
		//Debug.Log (rb.velocity);
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (rb.velocity);
		lifetime -= Time.deltaTime;
		if (lifetime < 0) {
			Destroy (gameObject);
		}
		gameObject.transform.position = new Vector3 (gameObject.transform.position.x + dir.x * speed * Time.deltaTime, gameObject.transform.position.y + dir.y * speed * Time.deltaTime,
			gameObject.transform.position.z);
		Vector3 dist = new Vector3();
		dist = gameObject.transform.position - player.transform.position;
		if(dist.magnitude < 1.2f){
			player.GetComponent<Player> ().TakeDamage (damage);
			Destroy (gameObject);
		}
	}



	/*void OnTriggerEnter2D (Collider2D other){
		Debug.Log ("Colidiu");
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<Player> ().TakeDamage (damage);
			Destroy (gameObject);
		} else if (other.gameObject.tag == "Ground"){
			Destroy (gameObject);
		}
	}*/
}
