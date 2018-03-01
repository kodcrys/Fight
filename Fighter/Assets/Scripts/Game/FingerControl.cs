using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerControl : FingerBase {

	enum FingerState {none, Idel, Atk}
	[SerializeField]
	FingerState fingerAction = FingerState.none;


	void Start(){
		atk = 100;
	}

	// Update is called once per frame
	void FixedUpdate () {
		switch (fingerAction) {
		case FingerState.none:
			fingerAction = FingerState.Idel;
			break;
		case FingerState.Idel:
			DoIdel ();
			break;
		case FingerState.Atk:
			DoAtk ();
			break;
		}

		if (!doingAtk) {
			finger.SetActive (true);
			fingerDown.SetActive (false);
			fingerAtk.SetActive (false);
		}
	}

	public override void DoIdel(){
		doingAtk = false;
		if (firstAtk)
			firstAtk = false;
		if (lastAtk)
			lastAtk = false;
		if (atk < 100 && !enemy.doingAtk) {
			atk++;
		} else if(atk >= 100) {
			atk = 100;
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

	public override void DoAtk(){
		if (!enemy.doingAtk) {
			if (enemy.atk > 2)
				enemy.atk--;
			else if (enemy.atk == 2)
				enemy.atk = 2;
		} else {
			atk--;
		}
		if (atk > 0) {
			doingAtk = true;
		} else {
			if (lastAtk) {
				GameplayBase.instance.isAtk = false;
				UnClickAtk ();
			}
			if (firstAtk) {
				if (!GameplayBase.instance.isAtk) {
					UnClickAtk ();
				}
			}
		}

		finger.SetActive (false);
		if (!enemy.doingAtk) {
			if (!enemy.firstAtk) {
				fingerDown.SetActive (true);
				fingerAtk.SetActive (false);
				firstAtk = true;
				lastAtk = false;
			}
		} else {
			if (enemy.firstAtk) {
				GameplayBase.instance.isAtk = true;
				fingerDown.SetActive (false);
				fingerAtk.SetActive (true);
				lastAtk = true;
				firstAtk = false;
			}
		}
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

