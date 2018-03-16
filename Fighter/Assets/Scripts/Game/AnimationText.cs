using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationText : MonoBehaviour {

	public enum TextAnim {none, RoundText, KOText}

	public TextAnim textAnimState = TextAnim.none;

	public enum TextStepAnim {none, Begin, Doing, End}

	public TextStepAnim textStepAnim = TextStepAnim.none;

	int colorA;

	[SerializeField]
	Color32 color1, color2, color3;

	[SerializeField]
	float t;

	[SerializeField]
	int step;

	[SerializeField]
	float time, timeInter, timeEnd;

	[SerializeField]
	Transform K, O, kDes, oDes;

	public bool startAnim;

	public static bool canPlay;
	public static bool beginRound;
	public static bool endRound;

	void Start(){
		startAnim = false;
		beginRound = true;
		startAnim = true;
		endRound = false;
	}

	// Update is called once per frame
	void Update () {
		switch (textStepAnim) {
		case TextStepAnim.none:
			if (textAnimState == TextAnim.RoundText) {
				if (beginRound)
					canPlay = false;
				transform.GetChild (0).gameObject.SetActive (false);
				transform.GetChild (1).gameObject.SetActive (false);
				transform.GetChild (2).gameObject.SetActive (false);
				if (startAnim) {
					textStepAnim = TextStepAnim.Begin;
				}
			} else if (textAnimState == TextAnim.KOText) {
				transform.GetChild (0).gameObject.SetActive (false);
				transform.GetChild (1).gameObject.SetActive (false);
				transform.GetChild (2).gameObject.SetActive (false);
				transform.GetChild (3).gameObject.SetActive (false);
				if (endRound) {
					canPlay = false;
					textStepAnim = TextStepAnim.Begin;
				}
			}
			break;
		case TextStepAnim.Begin:
			BeginAnimText ();
			textStepAnim = TextStepAnim.Doing;
			break;
		case TextStepAnim.Doing:
			DoingAnimText ();
			break;
		case TextStepAnim.End:
			EndAnimText ();
			break;
		}
	}

	void BeginAnimText(){
		if (textAnimState == TextAnim.RoundText) {
			if (SaveManager.instance.state.roundCount <= 2) {
				transform.GetChild (0).gameObject.SetActive (true);
				transform.GetChild (1).gameObject.SetActive (true);
				transform.GetChild (2).gameObject.SetActive (false);
			} else {
				transform.GetChild (0).gameObject.SetActive (false);
				transform.GetChild (1).gameObject.SetActive (true);
				transform.GetChild (2).gameObject.SetActive (true);
			}
			transform.localScale = new Vector3 (3, 3, 3);
			step = 0;
		} else if (textAnimState == TextAnim.KOText) {
			transform.GetChild (0).gameObject.SetActive (true);
			transform.GetChild (1).gameObject.SetActive (true);
			transform.GetChild (2).gameObject.SetActive (true);
			transform.GetChild (3).gameObject.SetActive (true);
			K.localPosition = new Vector3 (-1500, 800, 0);
			O.localPosition = new Vector3 (1500, -800, 0);
			step = 0;
		}
	}

	void DoingAnimText(){
		if (textAnimState == TextAnim.RoundText) {
			if (step == 0) {
				transform.localScale = Vector3.MoveTowards (transform.localScale, new Vector3 (1, 1, 1), Time.deltaTime * 5);
				if (transform.localScale == new Vector3 (1, 1, 1))
					step = 1;
			} else if (step == 1) {
				transform.eulerAngles = new Vector3 (0, 0, -10);
				time += Time.deltaTime;
				if (time >= timeInter) {
					step = 2;
					time = 0;
				}
			} else if(step == 2) {
				transform.localPosition = Vector3.MoveTowards (transform.localPosition, new Vector3 (0, -1500, 0), Time.deltaTime * 5000);
				if (transform.localPosition == new Vector3 (0, -1500, 0)) {
					textStepAnim = TextStepAnim.End;
				}
			}
		} else if (textAnimState == TextAnim.KOText) {
			if (step == 0) {
				K.localPosition = Vector3.MoveTowards (K.localPosition, kDes.localPosition * 10, Time.deltaTime * 5000);
				O.localPosition = Vector3.MoveTowards (O.localPosition, oDes.localPosition * 10, Time.deltaTime * 5000);
				if (K.localPosition == kDes.localPosition * 10) {
					step = 1;
				}
			} else if (step == 1) {
				K.localPosition = Vector3.MoveTowards (K.localPosition, kDes.localPosition, Time.deltaTime * 5000);
				O.localPosition = Vector3.MoveTowards (O.localPosition, oDes.localPosition, Time.deltaTime * 5000);
				if (K.localPosition == kDes.localPosition) {
					textStepAnim = TextStepAnim.End;
				}
			}
		}
	}

	void EndAnimText(){
		if (textAnimState == TextAnim.RoundText) {
			startAnim = false;
			textStepAnim = TextStepAnim.none;
			canPlay = true;
			beginRound = false;
			startAnim = false;
		} else if (textAnimState == TextAnim.KOText) {
			time += Time.deltaTime;
			if (time >= timeEnd) {
				UnityEngine.SceneManagement.SceneManager.LoadScene ("MainGameScene");
				textStepAnim = TextStepAnim.none;
				time = 0;
			}
		}
	}
}
