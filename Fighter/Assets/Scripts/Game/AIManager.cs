using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour {

	public enum DifficultLevel {none, Easy, Normal, Hard}

	public DifficultLevel difLevel = DifficultLevel.none;

	[SerializeField]
	bool left, right;

	[SerializeField]
	GameObject finger;

	[SerializeField]
	FingerLeftControl fingerAILeft;

	[SerializeField]
	FingerRightControl fingerAIRight;

	[SerializeField]
	int ranMove;

	[SerializeField]
	float time, timeInter;

	// Use this for initialization
	void Start () {
		// chon do kho code o day
		if (left)
			fingerAILeft = finger.GetComponent<FingerLeftControl> ();
		else if (right)
			fingerAIRight = finger.GetComponent<FingerRightControl> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (AnimationText.canPlay) {
			time += Time.deltaTime;
			switch (difLevel) {
			case DifficultLevel.Easy:
				EasyMode ();
				break;
			case DifficultLevel.Normal:
				break;
			case DifficultLevel.Hard:
				break;
			}
		}
	}

	void EasyMode(){
		if (right) {
			if (fingerAIRight.enemyLeft.fingerAction == FingerBase.FingerState.Idel) {
				if (time >= timeInter) {
					ranMove = Random.Range (0, 100);
					time = 0;
				}
				if (ranMove > 50) {
					if (!fingerAIRight.isAtk)
						AIClick ();
				} else {
					if (!fingerAIRight.isAtk)
						AIUnClick ();
				}
			} else {
				if (time >= timeInter) {
					ranMove = Random.Range (0, 100);
					time = 0;
				}
				if (ranMove > 70) {
					if (!fingerAIRight.isAtk)
						AIClick ();
				} else {
					if (!fingerAIRight.isAtk)
						AIUnClick ();
				}
			}
		}
			
	}

	void AIClick(){
		if (left) {
			fingerAILeft.doingSomething = true;
			if (fingerAILeft.touch) {
				if (!fingerAILeft.isAtk)
					fingerAILeft.DoAtk ();
			}
		} else if (right) {
			fingerAIRight.doingSomething = true;
			if (fingerAIRight.touch) {
				if (!fingerAIRight.isAtk)
					fingerAIRight.DoAtk ();
			}
		}
	}

	void AIUnClick(){
		if (left)
			FingerLeftControl.instance.UnClickAtk ();
		else if (right)
			FingerRightControl.instance.UnClickAtk ();
	}
}