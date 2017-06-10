using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour {

	public GameObject player, gunEdge, bulletPrefab;
	private bool isCloseToPlayer;
	private enum soldierStates : int {onGround, onAir, stunned, shooting};
	private int state;

	private float shootCD, maxShootCD = 0.25f, speed = 2f, minDist = 6.5f, t = 0f, floatSpeed = 0.5f, omega = 1.2f;
	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		state = (int)soldierStates.onAir;
		isCloseToPlayer = false;
		player = GameObject.Find ("Player");
		rb = gameObject.GetComponent<Rigidbody2D> ();
		if (gameObject.transform.position.x > player.transform.position.x) {
			gameObject.GetComponent<SpriteRenderer> ().flipX = true;
			Collider2D col = gameObject.GetComponent<Collider2D> ();
			col.offset = new Vector2 (-col.offset.x, col.offset.y);
			Vector3 invertGunEdge;
			invertGunEdge = new Vector3(0, 0 ,0);
			invertGunEdge = gunEdge.transform.localPosition;
			invertGunEdge.x = -invertGunEdge.x;
			gunEdge.transform.localPosition = invertGunEdge;
		}
	}

	// Update is called once per frame
	void Update () {
		if (transform.position.y < -50 || transform.position.y > 8 || Mathf.Abs(transform.position.x) > 50) {
			Die ();
		}
		switch (state) {
		case (int)soldierStates.onAir:
			DoFloatMov ();
			if (!isCloseToPlayer) {
				WalkToPlayer ();
			} else {
				state = (int)soldierStates.shooting;
			}
			break;
		case (int)soldierStates.shooting:
			DoFloatMov ();
			ShootPlayer ();
			break;
		case (int)soldierStates.stunned:
			break;
		}
		shootCD -= Time.deltaTime;
		if (gameObject.transform.position.x > player.transform.position.x && gunEdge.transform.localPosition.x > 0) {
			gameObject.GetComponent<SpriteRenderer> ().flipX = true;
			Collider2D col = gameObject.GetComponent<Collider2D> ();
			col.offset = new Vector2 (-col.offset.x, col.offset.y);
			Vector3 invertGunEdge;
			invertGunEdge = new Vector3 (0, 0, 0);
			invertGunEdge = gunEdge.transform.localPosition;
			invertGunEdge.x = -invertGunEdge.x;
			gunEdge.transform.localPosition = invertGunEdge;
		} else if(gameObject.transform.position.x < player.transform.position.x && gunEdge.transform.localPosition.x < 0){
			gameObject.GetComponent<SpriteRenderer> ().flipX = false;
			Collider2D col = gameObject.GetComponent<Collider2D> ();
			col.offset = new Vector2 (-col.offset.x, col.offset.y);
			Vector3 invertGunEdge;
			invertGunEdge = new Vector3 (0, 0, 0);
			invertGunEdge = gunEdge.transform.localPosition;
			invertGunEdge.x = -invertGunEdge.x;
			gunEdge.transform.localPosition = invertGunEdge;
		}
	}

	void WalkToPlayer(){
		float dir = (player.transform.position.x - this.transform.position.x) / Mathf.Abs(player.transform.position.x - this.transform.position.x);
		//rb.AddForce (new Vector2(speed * dir, 0));
		if(Mathf.Abs(rb.velocity.x) < speed){
			rb.velocity = new Vector3(rb.velocity.x + speed*dir, rb.velocity.y, 0);
			//Debug.Log ("Force added");
		}
		if (Mathf.Abs (gameObject.transform.position.x - player.transform.position.x) < minDist) {
			state = (int)soldierStates.shooting;
		}
	}

	void DoFloatMov(){
		rb.velocity = new Vector3 (rb.velocity.x, floatSpeed * Mathf.Sin (t * omega));
		t += Time.deltaTime;
	}

	void ShootPlayer(){
		rb.velocity = new Vector3 (0, rb.velocity.y);
		if (shootCD <= 0) {
			Instantiate (bulletPrefab, gunEdge.transform.position, Quaternion.identity);
			shootCD = maxShootCD;
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Ground") {
			state = (int)soldierStates.onGround;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Damage") {
			Die ();
		}
	}

	public void Die(){
		StartCoroutine (PlsDie ());
	}

	IEnumerator PlsDie(){
		//gameObject.GetComponent<Collider2D> ().enabled = false;
		gameObject.GetComponent<Helicopter> ().enabled = false;
		gameObject.GetComponent<Rigidbody2D> ().gravityScale = 1;
		yield return new WaitForSeconds (0.5f);
		Destroy (gameObject);
	}

	void OnCollisionExit2D(Collision2D other){
		if (other.gameObject.tag == "Ground") {
			state = (int)soldierStates.onAir;
		}
	}
}
