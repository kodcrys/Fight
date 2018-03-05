using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerControl : FingerBase {

	enum FingerState {none, Idel, First, Atk, Down, Death}
	[SerializeField]
	FingerState fingerAction = FingerState.none;


	void Start(){
		atk = 100;
	}

	// Update is called once per frame
	void FixedUpdate () {
		atkText.text = atk.ToString ();

		switch (fingerAction) {
		case FingerState.none:
			fingerAction = FingerState.Idel;
			break;
		case FingerState.Idel:
			DoIdel ();
			break;
		case FingerState.First:
			DoFirstAtk ();
			break;
		case FingerState.Atk:
			DoLastAtk ();
			break;
		case FingerState.Down:
			DoDown ();
			break;
		case FingerState.Death:

			break;
		}

		if (!doingSomething) {
			if (!isAtk) {
				touch = true;
				fingerAction = FingerState.Idel;
			}
		} else {
			if (touch) {
				if (!firstAtk && !enemy.firstAtk || !enemy.firstAtk && lastAtk) {
					fingerAction = FingerState.First;
				} else if (enemy.firstAtk && !lastAtk) {
					fingerAction = FingerState.Atk;
				} else if (firstAtk && enemy.lastAtk || enemy.firstAtk && lastAtk) {
					fingerAction = FingerState.Down;
				}
			} else {
				if (!enemy.firstAtk) {
					fingerAction = FingerState.First;
				}
			}
		}
	}

	public override void DoIdel(){
		finger.SetActive (true);
		fingerDown.SetActive (false);
		fingerAtk.SetActive (false);

		firstAtk = false;
		lastAtk = false;
		isAtk = false;

		if (!enemy.firstAtk) {
			if (atk < 100 && atk >= 0) {
				atk++;
			} else {
				atk = 100;
			}
		}

		if (!FingerBase.changeAnim) {
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
		}
	}

	public override void DoFirstAtk(){
		isAtk = false;
		if (enemy.atk > 1) {
			enemy.atk--;
		}

		if (atk < 100 && atk >= 0) {
			atk++;
		} else {
			atk = 100;
		}
		firstAtk = true;
		lastAtk = false;
		finger.SetActive (false);
		fingerDown.SetActive (true);
		fingerAtk.SetActive (false);
		if (enemy.lastAtk)
			fingerAction = FingerState.Down;
	}

	public override void DoLastAtk(){
		isAtk = false;
		lastAtk = true;
		firstAtk = false;
		finger.SetActive (false);
		fingerDown.SetActive (false);
		fingerAtk.SetActive (true);
		if (enemy.firstAtk)
			fingerAction = FingerState.Down;
	}

	public override void DoDown(){
		Debug.Log (1);
		isAtk = true;
		if (lastAtk) {
			Debug.Log (2);
			if (atk > 0)
				atk--;
		}

		if (atk == 0) {
			Debug.Log (3);
			isAtk = false;
			touch = false;
			enemy.fingerAction = FingerState.Idel;
		}
	}

	public override void Dead(){
		
	}
		
	public void ClickAtk(){
		doingSomething = true;

		FingerBase.changeAnim = true;
	}

	public void UnClickAtk(){
		doingSomething = false;

		FingerBase.changeAnim = false;
	}
}

