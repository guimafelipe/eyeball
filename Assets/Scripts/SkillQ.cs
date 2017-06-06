using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillQ : MonoBehaviour {

	[SerializeField]
	private float cooldown;
	private float maxCooldown = 2f;

	public GameObject fireball;

	GameObject player;

	private Animator animator;
	public RectTransform skillUI;
	public Text text;

	void Start(){
		player = GameObject.Find ("Player");
		animator = skillUI.GetComponent<Animator> ();
		cooldown = 0;
	}

	void Update(){
		cooldown -= Time.deltaTime;
		if (cooldown > 0) {
			animator.SetBool ("IsOn", false);
			text.text = Mathf.CeilToInt (cooldown) + "";
		} else {
			animator.SetBool ("IsOn", true);
		}
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

		if (true) { //Isso aqui ta muito errrado
			Shot ();
			cooldown = maxCooldown;
		}
	}
}
