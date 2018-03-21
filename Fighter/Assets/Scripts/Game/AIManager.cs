using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour {

	enum DifficultLevel {none, Easy, Normal, Hard}
	[SerializeField]
	DifficultLevel difLevel = DifficultLevel.none;

	[SerializeField]
	bool left, right;

	[SerializeField]
	GameObject fingerAI;

	// Use this for initialization
	void Start () {
		// chon do kho code o day
		if (left)
			fingerAI.GetComponent<FingerLeftControl> ();
		else if (right)
			fingerAI.GetComponent<FingerRightControl>();
	}
	
	// Update is called once per frame
	void Update () {
		switch (difLevel) {
		case DifficultLevel.Easy:
			break;
		case DifficultLevel.Normal:
			break;
		case DifficultLevel.Hard:
			break;
		}
	}

	void EasyMode(){
		
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