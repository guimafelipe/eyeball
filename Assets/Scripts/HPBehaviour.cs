using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBehaviour : MonoBehaviour {

	Player player;
	[SerializeField]
	RectTransform HP;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player").GetComponent<Player> ();
		if (HP == null) {
			Debug.Log ("there is no health bar!");
		}
	}
	
	// Update is called once per frame
	void Update () {
		HP.localScale = new Vector3 ( Mathf.Clamp01( ((float) player.hp)/player.maxHp), HP.localScale.y, HP.localScale.z);
	}
}
