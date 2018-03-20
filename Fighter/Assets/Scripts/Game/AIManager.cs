using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour {

	enum DifficultLevel {none, Easy, Normal, Hard}
	[SerializeField]
	DifficultLevel difLevel = DifficultLevel.none;

	public bool useAI;

	[SerializeField]
	bool left, right;

	[SerializeField]
	GameObject fingerAI;

	[SerializeField]
	GameObject clickControl;

	void Awake(){
		if (!useAI) {
			gameObject.GetComponent<AIManager> ().enabled = false;
			clickControl.SetActive (true);
		} else {
			gameObject.GetComponent<AIManager> ().enabled = true;
			clickControl.SetActive (false);
		}
	}

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
}