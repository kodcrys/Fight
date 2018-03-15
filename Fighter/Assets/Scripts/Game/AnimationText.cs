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
	float time, timeInter;

	public bool startAnim;

	public static bool canPlay;
	public static bool beginRound;

	void Start(){
		startAnim = false;
	}

	// Update is called once per frame
	void Update () {
		switch (textStepAnim) {
		case TextStepAnim.none:
			if (beginRound)
				canPlay = false;
			transform.GetComponent<UnityEngine.UI.Text> ().color = color3;
			if (startAnim) {
				textStepAnim = TextStepAnim.Begin;
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
			transform.localScale = new Vector3 (3, 3, 3);
			transform.GetComponent<UnityEngine.UI.Text> ().color = color1;
			t = 0;
			step = 0;
		} else if (textAnimState == TextAnim.KOText) {
		}
	}

	void DoingAnimText(){
		if (textAnimState == TextAnim.RoundText) {
			if (step == 0) {
				transform.localScale = Vector3.MoveTowards (transform.localScale, new Vector3 (1, 1, 1), Time.deltaTime * 3);
				transform.GetComponent<UnityEngine.UI.Text> ().color = Color32.Lerp (color1, color2, t);
				t += Time.deltaTime;
				if (transform.localScale == new Vector3 (1, 1, 1))
					step = 1;
			} else if (step == 1) {
				transform.eulerAngles = new Vector3 (0, 0, 10);
				time += Time.deltaTime;
				if (time >= timeInter) {
					step = 2;
					time = 0;
				}
			} else if(step == 2) {
				transform.localPosition = Vector3.MoveTowards (transform.localPosition, new Vector3 (0, -1500, 0), Time.deltaTime * 1500);
				if (transform.localPosition == new Vector3 (0, -1500, 0)) {
					textStepAnim = TextStepAnim.End;
				}
			}
		} else if (textAnimState == TextAnim.KOText) {
		}
	}

	void EndAnimText(){
		if (textAnimState == TextAnim.RoundText) {
			startAnim = false;
			textStepAnim = TextStepAnim.none;
			canPlay = true;
			beginRound = false;
		} else if (textAnimState == TextAnim.KOText) {
			
		}
	}
}
