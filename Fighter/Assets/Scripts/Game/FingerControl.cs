using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerControl : MonoBehaviour {

	enum FingerState {none, Idel, Atk}
	[SerializeField]
	FingerState fingerAction = FingerState.none;

	[SerializeField]
	int changeScale = 0;

	[SerializeField]
	float time, timeInter;

	[SerializeField]
	GameObject fingerRight, fingerLeft;


	void Start(){
		
	}
	// Update is called once per frame
	void Update () {
		if (time >= timeInter) {
			time = 0;
		} else {
			time += Time.deltaTime;
		}

		switch (fingerAction) {
		case FingerState.none:
			fingerAction = FingerState.Idel;
			break;
		case FingerState.Idel:
			DoIdel ();
			break;
		case FingerState.Atk:
			break;
		}
	}

	void DoIdel(){
		if (time >= timeInter) {
			if (changeScale == 0) {
				fingerRight.transform.localScale = new Vector3 (1, 1, 1);
				fingerRight.transform.position = new Vector3 (0.27f, 3.77f, 0);
				changeScale = 1;
			} else if (changeScale == 1) {
				fingerRight.transform.localScale = new Vector3 (1, 1.1f, 1);
				changeScale = 2;
			} else {
				fingerRight.transform.localScale = new Vector3 (1, 0.9f, 1);
				changeScale = 1;
			}
		}
	}
}
