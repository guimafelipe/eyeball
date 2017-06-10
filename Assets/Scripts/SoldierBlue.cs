using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBlue : MonoBehaviour {

	public GameObject player, gunEdge, bulletPrefab;
	private bool isCloseToPlayer;
	private enum soldierStates : int {walking, onGround, onAir, stunned, shooting};
	private int state;

	Animator anim;
	public float shootCD, maxShootCD = 1f, speed = 1f, minDist = 1.5f, t = 0;
	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		state = (int)soldierStates.onAir;
		isCloseToPlayer = false;
		anim = GetComponent<Animator> ();
		anim.SetBool ("shooting", false);
		player = GameObject.Find ("Player");
		rb = gameObject.GetComponent<Rigidbody2D> ();
		if (gameObject.transform.position.x > player.transform.position.x) {
			gameObject.GetComponent<SpriteRenderer> ().flipX = true;
			Vector3 invertGunEdge;
			invertGunEdge = new Vector3(0, 0 ,0);
			invertGunEdge = gunEdge.transform.localPosition;
			invertGunEdge.x = -invertGunEdge.x;
			gunEdge.transform.localPosition = invertGunEdge;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < -50) {
			Die ();
		}
		switch (state) {
		case (int)soldierStates.walking:
			if (!isCloseToPlayer) {
				WalkToPlayer ();
			} else {
				state = (int)soldierStates.shooting;
			}
			break;
		case (int)soldierStates.shooting:
			ShootPlayer ();
			break;
		case (int)soldierStates.onAir:
			break;
		}
		shootCD -= Time.deltaTime;
		t += Time.deltaTime;
		if (gameObject.transform.position.x > player.transform.position.x && gunEdge.transform.localPosition.x > 0) {
			gameObject.GetComponent<SpriteRenderer> ().flipX = false;
			Vector3 invertGunEdge;
			invertGunEdge = new Vector3 (0, 0, 0);
			invertGunEdge = gunEdge.transform.localPosition;
			invertGunEdge.x = -invertGunEdge.x;
			gunEdge.transform.localPosition = invertGunEdge;
		} else if(gameObject.transform.position.x < player.transform.position.x && gunEdge.transform.localPosition.x < 0){
			gameObject.GetComponent<SpriteRenderer> ().flipX = true;
			Vector3 invertGunEdge;
			invertGunEdge = new Vector3 (0, 0, 0);
			invertGunEdge = gunEdge.transform.localPosition;
			invertGunEdge.x = -invertGunEdge.x;
			gunEdge.transform.localPosition = invertGunEdge;
		}
		if (t > 10f && state == (int)soldierStates.walking) {
			state = (int)soldierStates.shooting;
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

	void ShootPlayer(){
		anim.SetBool ("shooting", true);
		rb.velocity = new Vector3 (0, rb.velocity.y);
		if (shootCD <= 0) {
			var bullet = Instantiate (bulletPrefab, gunEdge.transform.position, Quaternion.identity);
			shootCD = maxShootCD;
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Ground") {
			state = (int)soldierStates.walking;
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
		gameObject.GetComponent<SoldierBlue> ().enabled = false;
		yield return new WaitForSeconds (0.5f);
		Destroy (gameObject);
	}

	void OnCollisionExit2D(Collision2D other){
		if (other.gameObject.tag == "Ground") {
			state = (int)soldierStates.onAir;
		}
	}
}
