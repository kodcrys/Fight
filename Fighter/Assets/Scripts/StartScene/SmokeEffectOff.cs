using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeEffectOff : MonoBehaviour {

	float time = 0;
	public bool isRunFinish;
	// Use this for initialization
	void OnEnable () {
		time = 0;
		isRunFinish = false;
	}
	
	void Update() {
		time += Time.deltaTime;
		if (time >= 1.8f) {
			gameObject.SetActive (false);
			isRunFinish = true;
			time = 0;
		}
	}
}
