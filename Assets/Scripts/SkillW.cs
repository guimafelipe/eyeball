using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillW : MonoBehaviour {

	[SerializeField]
	private float cooldown;
	private float maxCooldown = 5f;

	public GameObject iceball;

	GameObject player;

	void Start(){
		player = GameObject.Find ("Player");
		cooldown = 0;
	}

	void Update(){
		cooldown -= Time.deltaTime;
	}

	public void Shot(){
		Instantiate (iceball, player.transform.position, Quaternion.identity);
	}

	public bool IsOnCD(){
		if (cooldown > 0) {
			return true;
		}
		cooldown = -1;
		return false;
	}

	public void Aim(){

		if (true) { //Isso aqui ta muito errrado
			Shot ();
			cooldown = maxCooldown;
		}
	}
}
