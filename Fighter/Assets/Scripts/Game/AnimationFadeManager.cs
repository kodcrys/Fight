using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimationFadeManager : MonoBehaviour {

	[SerializeField]
	FadeAnimOption fadeOption;

	void Start(){
		if(fadeOption != null)
			fadeOption.starDead.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		if (fadeOption.leftControl != null) {
			if (fadeOption.leftControl.fingerAction == FingerBase.FingerState.Idel) {
				fadeOption.time += Time.deltaTime;
				if (fadeOption.time >= fadeOption.timeInter) {
					fadeOption.time = 0;
					if (fadeOption.i == 0) {
						fadeOption.i = 1;
						fadeOption.timeInter = 0.1f;
					} else {
						fadeOption.i = 0;
						fadeOption.timeInter = 3.5f;
					}
				}
				if(fadeOption.isUI == false)
					fadeOption.fadeLocation [0].sprite = fadeOption.fadeAnimOption [fadeOption.i];
				else
					fadeOption.faceList[0].sprite = fadeOption.fadeAnimOption [fadeOption.i];
			} else if (fadeOption.leftControl.fingerAction == FingerBase.FingerState.Doing) {
				if (fadeOption.leftControl.firstAtk) {
					fadeOption.fadeLocation [1].sprite = fadeOption.fadeAnimOption [2];
				} else if (fadeOption.leftControl.firstAtk && fadeOption.leftControl.enemyRight.lastAtk) {
					fadeOption.fadeLocation [1].sprite = fadeOption.fadeAnimOption [3];
				} else if (fadeOption.leftControl.lastAtk) {
					fadeOption.fadeLocation [2].sprite = fadeOption.fadeAnimOption [2];
				}
			} else if (fadeOption.leftControl.fingerAction == FingerBase.FingerState.Win) {
				fadeOption.fadeLocation [0].sprite = fadeOption.fadeAnimOption [4];
			} else if (fadeOption.leftControl.fingerAction == FingerBase.FingerState.Death) {
				fadeOption.starDead.SetActive (true);
				fadeOption.fadeLocation [1].sprite = fadeOption.fadeAnimOption [5];
			}
		}else if (fadeOption.rightControl != null) {
			if (fadeOption.rightControl.fingerAction == FingerBase.FingerState.Idel) {
				fadeOption.time += Time.deltaTime;
				if (fadeOption.time >= fadeOption.timeInter) {
					fadeOption.time = 0;
					if (fadeOption.i == 0) {
						fadeOption.i = 1;
						fadeOption.timeInter = 0.1f;
					} else {
						fadeOption.i = 0;
						fadeOption.timeInter = 3.5f;
					}
				}
				if(fadeOption.isUI == false)
					fadeOption.fadeLocation [0].sprite = fadeOption.fadeAnimOption [fadeOption.i];
				else
					fadeOption.faceList[0].sprite = fadeOption.fadeAnimOption [fadeOption.i];
			} else if (fadeOption.rightControl.fingerAction == FingerBase.FingerState.Doing) {
				if (fadeOption.rightControl.firstAtk) {
					fadeOption.fadeLocation [1].sprite = fadeOption.fadeAnimOption [2];
				} else if (fadeOption.rightControl.firstAtk && fadeOption.rightControl.enemyLeft.lastAtk) {
					fadeOption.fadeLocation [1].sprite = fadeOption.fadeAnimOption [3];
				} else if (fadeOption.rightControl.lastAtk) {
					fadeOption.fadeLocation [2].sprite = fadeOption.fadeAnimOption [2];
				}
			}else if (fadeOption.rightControl.fingerAction == FingerBase.FingerState.Win) {
				fadeOption.fadeLocation [0].sprite = fadeOption.fadeAnimOption [4];
			} else if (fadeOption.rightControl.fingerAction == FingerBase.FingerState.Death) {
				fadeOption.starDead.SetActive (true);
				fadeOption.fadeLocation [1].sprite = fadeOption.fadeAnimOption [5];
			}
		}
	}
}

[Serializable]
public class FadeAnimOption{
	public bool isUI = false;
	public List<Sprite> fadeAnimOption = new List<Sprite>();
	public List<SpriteRenderer> fadeLocation = new List<SpriteRenderer>();
	public List<UnityEngine.UI.Image> faceList = new List<UnityEngine.UI.Image>();
	public FingerLeftControl leftControl;
	public FingerRightControl rightControl;
	public float time, timeInter;
	public int i;
	public GameObject starDead;
}
