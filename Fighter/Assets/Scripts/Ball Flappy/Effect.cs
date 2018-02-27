using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour {
		float time;
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
				time += Time.deltaTime;
				if (time > 0.5f) {
						gameObject.SetActive (false);
						time = 0;
				}
	}
}
