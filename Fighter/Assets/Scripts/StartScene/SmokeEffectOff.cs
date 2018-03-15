using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeEffectOff : MonoBehaviour {

	ParticleSystem partical;

	// Use this for initialization
	void Start () {
		partical = GetComponent<ParticleSystem> ();
		StartCoroutine (OffEffect ());
	}
	
	IEnumerator OffEffect() {

		Debug.Log (gameObject.name + " " + partical.IsAlive ());
		while (true) {
			if (partical.IsAlive ())
				gameObject.SetActive (false);
			yield return new WaitForSeconds (0.02f);
		}
	}
}
