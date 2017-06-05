using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillQ : MonoBehaviour {

	[SerializeField]
	private float cooldown;
	private float maxCooldown = 5f;

	public GameObject fireball;

	GameObject player;

	void Start(){
		player = GameObject.Find ("Player");
		cooldown = 0;
	}

	void Update(){
		cooldown -= Time.deltaTime;
	}

	public void Shot(){
		Instantiate (fireball, player.transform.position, Quaternion.identity);
	}

	public bool IsOnCD(){
		if (cooldown > 0) {
			return true;
		}
		cooldown = -1;
		return false;
	}

	public void Aim(){

		if (Input.GetMouseButtonDown (0)) {
			Shot ();
			cooldown = maxCooldown;
		}
	}
}
