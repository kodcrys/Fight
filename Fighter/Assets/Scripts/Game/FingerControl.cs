using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerControl : FingerBase {

	enum FingerState {none, Idel, Atk}
	[SerializeField]
	FingerState fingerAction = FingerState.none;

	[SerializeField]
	float time, timeInter;


	// Update is called once per frame
	void Update () {
		switch (fingerAction) {
		case FingerState.none:
			fingerAction = FingerState.Idel;
			break;
		case FingerState.Idel:
			if (!FingerBase.changeAnim)
				DoIdel ();
			break;
		case FingerState.Atk:
			DoAtk ();
			break;
		}
	}

	public override void DoIdel(){
		if (time >= timeInter) {
			time = 0;
		} else {
			time += Time.deltaTime;
		}

		if (time >= timeInter) {
			if (changeScale == 0)
				changeScale = 1;
			else
				changeScale = 0;
		}

		if (changeScale == 0) {
			finger.transform.localScale = Vector3.MoveTowards (finger.transform.localScale, new Vector3 (finger.transform.localScale.x, scale1, finger.transform.localScale.z), Time.deltaTime * speedScale);
			finger.transform.Rotate (finger.transform.localRotation.x, finger.transform.localRotation.y, rot1);
			finger.transform.localPosition = Vector3.MoveTowards (finger.transform.localPosition, new Vector3 (finger.transform.localPosition.x + pos1, finger.transform.localPosition.y, finger.transform.localPosition.z), Time.deltaTime * speedScale);
		} else {
			finger.transform.localScale = Vector3.MoveTowards (finger.transform.localScale, new Vector3 (finger.transform.localScale.x, scale2, finger.transform.localScale.z), Time.deltaTime * speedScale);
			finger.transform.Rotate (finger.transform.localRotation.x, finger.transform.localRotation.y, rot2);
			finger.transform.localPosition = Vector3.MoveTowards (finger.transform.localPosition, new Vector3 (finger.transform.localPosition.x - pos2, finger.transform.localPosition.y, finger.transform.localPosition.z), Time.deltaTime * speedScale);
		}

		finger.SetActive (true);
		fingerAtk.SetActive (false);
	}

	public override void DoAtk(){
		finger.SetActive (false);
		fingerAtk.SetActive (true);
	}
		
	public void ClickAtk(){
		fingerAction = FingerState.Atk;
		FingerBase.changeAnim = true;
	}

	public void UnClickAtk(){
		fingerAction = FingerState.Idel;
		FingerBase.changeAnim = false;
	}
}

