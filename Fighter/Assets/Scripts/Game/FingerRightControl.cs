using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerRightControl : FingerBase {

	[SerializeField]
	bool isUIAni;

	// Use this for initialization
	void Start () {
		touch = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(atkText != null)
			atkText.text = atk.ToString();

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
		case FingerState.Doing:
			DoingAtk ();
			break;
		case FingerState.Death:

			break;
		}

		if (doingSomething) {
			if (!enemyLeft.firstAtk && lastAtk) {
				lastAtk = false;
				fingerAction = FingerState.Atk;
			}
		}
	}

	public override void DoIdel(){
		if(finger != null)
			finger.SetActive (true);
		if (fingerDown != null)
			fingerDown.SetActive (false);
		if (fingerAtk != null)
			fingerAtk.SetActive (false);

		touch = true;
		isAtk = false;
		firstAtk = false;
		lastAtk = false;

		if (isUIAni == false) {

			if (!enemyLeft.firstAtk) {
				if (atk < 100 && atk >= 0) {
					atk++;
				} else {
					atk = 100;
				}
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

	public override void DoAtk(){
		if (!firstAtk && !enemyLeft.firstAtk) {
			firstAtk = true;

			if(finger != null)
				finger.SetActive (false);
			if (fingerDown != null)
				fingerDown.SetActive (true);
			if (fingerAtk != null)
				fingerAtk.SetActive (false);
			
			fingerAction = FingerState.Doing;
		} else if(enemyLeft.firstAtk){
			lastAtk = true;
			if(finger != null)
				finger.SetActive (false);
			if (fingerDown != null)
				fingerDown.SetActive (false);
			if (fingerAtk != null)
				fingerAtk.SetActive (true);
			fingerAction = FingerState.Doing;
		}
	}

	public override void DoingAtk(){
		
		if (firstAtk) {
			if (!enemyLeft.lastAtk) {
				if (enemyLeft.atk > 0) {
					enemyLeft.atk--;
				}

				if (atk < 100 && atk >= 0) {
					atk++;
				} else {
					atk = 100;
				}
			}

			if (isAtk) {
				if (!enemyLeft.doingSomething) {
					isAtk = false;
					touch = false;
					fingerAction = FingerState.Idel;
				}
			}
		} else if (lastAtk) {
			enemyLeft.isAtk = true;
			if (doingSomething) {
				if (atk > 0) {
					atk--;
				} else if (atk == 0) {
					enemyLeft.isAtk = false;
					enemyLeft.touch = false;
					fingerAction = FingerState.Atk;
					isAtk = false;
					enemyLeft.fingerAction = FingerState.Idel;
				}
			} 
		}
	}

	public void ClickAtk(){
		doingSomething = true;
		if (touch) {
			if (!isAtk)
				fingerAction = FingerState.Atk;
		}


		FingerBase.changeAnim = true;
	}

	public void UnClickAtk(){
		touch = true;
		doingSomething = false;
		if (!isAtk)
			fingerAction = FingerState.Idel;
		FingerBase.changeAnim = false;
	}
}
