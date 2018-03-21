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

	void EasyMode(){
		if (right) {
			if (fingerAIRight.enemyLeft.fingerAction == FingerBase.FingerState.Idel) {
				ranMove = Random.Range (0, 100);
				if (ranMove > 50)
					AIClick ();
				else
					AIUnClick ();
			}
		}
			
	}

	void AIClick(){
		if (left)
			FingerLeftControl.instance.ClickAtk ();
		else if (right)
			FingerRightControl.instance.ClickAtk ();
	}

	void AIUnClick(){
		if (left)
			FingerLeftControl.instance.UnClickAtk ();
		else if (right)
			FingerRightControl.instance.UnClickAtk ();
	}
}