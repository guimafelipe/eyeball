﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	float timelife = 0.25f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (timelife < 0) {
			Destroy (gameObject);
		}
		timelife -= Time.deltaTime;
	}
}
