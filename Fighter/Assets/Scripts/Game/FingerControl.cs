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
	float time, timeInter = 0;

	[SerializeField]
	GameObject finger;

	[SerializeField]
	float speedScale;


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
			if (changeScale == 0)
				changeScale = 1;
			else
				changeScale = 0;
		}

		if (changeScale == 0) {
			finger.transform.localScale = Vector3.MoveTowards (finger.transform.localScale, new Vector3 (1, 1, 1), Time.deltaTime * speedScale);
		} else if (changeScale == 1) {
			finger.transform.localScale = Vector3.MoveTowards (finger.transform.localScale, new Vector3 (1f, 0.9f, 1), Time.deltaTime * speedScale);
		}
	}

	void Do Atk(){
		
	}
		
	public void ClickAtk(){
		fingerAction = FingerState.Atk;
	}

	public void UnClickAtk(){
		fingerAction = FingerState.Idel;
	}
}

