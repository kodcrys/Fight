﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIAnimations : MonoBehaviour {

	[Header("Object run animation")]
	[SerializeField]
	Transform target;

	[SerializeField]
	float speed;

	[Header("Scale")]
	// Var define object run ani
	public bool isRunScaleAni;
	// var bool define Object run ANi scale when var isRunScaleAni = false
	[SerializeField]
	bool isScaleAni;
	[SerializeField]
	// var define is botBarObject contain this script
	bool isUIBotBar;
	[SerializeField]
	Vector3 maxScale;
	[SerializeField]
	Vector3 originScale;
	[SerializeField]
	Vector3 minScale;
	bool changeScale1, changeScale2;

	[Header("Button of botBar")]
	[SerializeField]
	bool isShop;
	[SerializeField]
	bool isRate;
	[SerializeField]
	bool isShare;
	[SerializeField]
	bool isLeaderboard;
	[SerializeField]
	string linkRate;
	[SerializeField]
	string linkShare;

	[Header("Change sprite")]
	[SerializeField]
	Sprite sprChangeFrame1;
	[SerializeField]
	Sprite sprChangeFrame2;

	[Header("Change color")]
	[SerializeField]
	bool isRunChangeColorAni;
	[SerializeField]
	Color32 color1;
	[SerializeField]
	Color32 color2;
	Color32 lerpedColor;
	private float t = 0;
	private bool flag;
	Image imgEff;

	[Header("Move Object")]
	// Var define object run ani
	public bool isRunMoveAni;
	[SerializeField]
	// var bool define Object run ANi move move when var isRunMoveAni = false
	bool isMoveAni;
	[SerializeField]
	Transform pos1;
	[SerializeField]
	Transform pos2;
	[SerializeField]
	Transform pos3;
	bool changeDes1, changeDes2, changeDes3;

	[Header("Button Play")]
	[SerializeField]
	bool isRunBtnPlayAni;
	[SerializeField]
	string[] btnContent = { "P1 VS P2", "P1 VS CPU", "TOURNAMENT", "MINI GAME" };
	[SerializeField]
	Button playBtn;
	[SerializeField]
	Text contentTxt;
	[SerializeField]
	Button nextBtn;
	[SerializeField]
	Button preBtn;
	public static int indexMode = 0;
	bool isLeft, isRight, changeLeft1, changeLeft2, changeRight1, changeRight2;

	[Header("Ani Sequence Move")]
	// Var define object run ani
	public bool isRunSeqAni;
	[SerializeField]
	// var bool define Object run ANi sequence move when var isRunSeqAni = false
	bool isSequence;
	[SerializeField]
	Transform obj1;
	[SerializeField]
	Transform obj2;
	[SerializeField]
	Transform obj3;
	[SerializeField]
	Transform pos1Seq;
	[SerializeField]
	Transform pos2Seq;
	[SerializeField]
	Transform pos3Seq;
	[SerializeField]
	GameObject progressBar;

	[Header("Ani Shake")]
	public bool isRunShakeAni;
	[SerializeField]
	bool isShakeAni;
	[SerializeField]
	float timeInterShake;
	float timeShake;

	void OnEnable() {
		if (contentTxt != null)
			contentTxt.text = btnContent [indexMode];

		timeShake = 0;
		isLeft = isRight = changeLeft1 = changeLeft2 = changeRight1 = changeRight2 = false;
		changeDes1 = changeDes2 = changeDes3 = false;
		changeScale1 = changeScale2 = false;
		imgEff=GetComponent<Image>();
		StartCoroutine (RunAni ());
	}

	public void OnOffSound() {
		Image imgTarget = target.gameObject.GetComponent<Image> ();
		if (SaveManager.instance.state.isOnSound) {
			SaveManager.instance.state.isOnSound = false;
			imgTarget.sprite = sprChangeFrame2;
		} else {
			SaveManager.instance.state.isOnSound = true;
			imgTarget.sprite = sprChangeFrame1;
		}
		SaveManager.instance.Save ();
	}

	public void OnOffMusic() {
		Image imgTarget = target.gameObject.GetComponent<Image> ();
		if (SaveManager.instance.state.isOnMusic) {
			SaveManager.instance.state.isOnMusic = false;
			imgTarget.sprite = sprChangeFrame2;
		} else {
			SaveManager.instance.state.isOnMusic = true;
			imgTarget.sprite = sprChangeFrame1;
		}
		SaveManager.instance.Save ();
	}

	public void OnOffVoice() {
		Image imgTarget = target.gameObject.GetComponent<Image> ();
		if (SaveManager.instance.state.isOnVoice) {
			SaveManager.instance.state.isOnVoice = false;
			imgTarget.sprite = sprChangeFrame2;
		} else {
			SaveManager.instance.state.isOnVoice = true;
			imgTarget.sprite = sprChangeFrame1;
		}
		SaveManager.instance.Save ();
	}

	public void OnOffRing() {
		Image imgTarget = target.gameObject.GetComponent<Image> ();
		if (SaveManager.instance.state.isOnRing) {
			SaveManager.instance.state.isOnRing = false;
			imgTarget.sprite = sprChangeFrame2;
		} else {
			SaveManager.instance.state.isOnRing = true;
			imgTarget.sprite = sprChangeFrame1;
		}
		SaveManager.instance.Save ();
	}

	public void CheckSound() {
		Image imgTarget = target.gameObject.GetComponent<Image> ();
		if (SaveManager.instance.state.isOnSound)
			imgTarget.sprite = sprChangeFrame1;
		else
			imgTarget.sprite = sprChangeFrame2;
	}

	public void CheckMusic() {
		Image imgTarget = target.gameObject.GetComponent<Image> ();
		if (SaveManager.instance.state.isOnMusic)
			imgTarget.sprite = sprChangeFrame1;
		else
			imgTarget.sprite = sprChangeFrame2;
	}

	public void CheckVoice() {
		Image imgTarget = target.gameObject.GetComponent<Image> ();
		if (SaveManager.instance.state.isOnVoice)
			imgTarget.sprite = sprChangeFrame1;
		else
			imgTarget.sprite = sprChangeFrame2;
	}

	public void CheckRing() {
		Image imgTarget = target.gameObject.GetComponent<Image> ();
		if (SaveManager.instance.state.isOnRing)
			imgTarget.sprite = sprChangeFrame1;
		else
			imgTarget.sprite = sprChangeFrame2;
	}

	void ChangeColor() {
		lerpedColor = Color32.Lerp(color1, color2,  t);
		imgEff.color = lerpedColor;

		if (flag == true) {
			t -= Time.deltaTime * 2f;
			if (t < 0.01f)
				flag = false;
		} else {
			t += Time.deltaTime * 2f;
			if (t > 0.99f)
				flag = true;
		}
	}

	void Move3DesGoDes1ToDes3() {
		if (changeDes1 == false) {
			target.position = Vector3.MoveTowards (target.position, pos1.position, speed * Time.deltaTime);
			if (target.position == pos1.position)
				changeDes1 = true;
		}
		if (changeDes1 && changeDes2 == false) {
			target.position = Vector3.MoveTowards (target.position, pos2.position, speed * Time.deltaTime);
			if (target.position == pos2.position)
				changeDes2 = true;
		}
		if (changeDes1 && changeDes2 && changeDes3 == false) {
			target.position = Vector3.MoveTowards (target.position, pos3.position, speed * Time.deltaTime);
			if (target.position == pos3.position)
				changeDes3 = true;
		}
	}

	void Move3DesBackDes3ToDes1() {
		target.position =  Vector3.MoveTowards (target.position, new Vector3(target.position.x, -6f, target.position.z), speed * Time.deltaTime);
		if(target.position == new Vector3(target.position.x, -6f, target.position.z))
			changeDes1 = changeDes2 = changeDes3 = false;
	}
		
	public void AllowScaleAni() {
		isRunScaleAni = true;
	}

	void ScaleToScale() {
		if (changeScale1 == false) {
			target.localScale = Vector3.MoveTowards (target.localScale, maxScale, speed * Time.deltaTime);
			if (target.localScale == maxScale)
				changeScale1 = true;
		}
		if (changeScale1 && changeScale2 == false) {
			target.localScale = Vector3.MoveTowards (target.localScale, originScale, speed * Time.deltaTime);
			if (target.localScale == originScale) {
				changeScale2 = true;
				if (isUIBotBar) {
					isRunScaleAni = false;
					changeScale1 = changeScale2 = false;
					HandleBotBar ();
				}
			}
		}
	}

	void HandleBotBar() {
		if (isShop) {
		}
		if (isRate)
			Application.OpenURL (linkRate);
		if (isShare)
			Application.OpenURL (linkShare);
		if (isLeaderboard) {
		}
	}

	void ScaleToMin() {
		target.localScale =  Vector3.MoveTowards (target.localScale, minScale, speed * Time.deltaTime);
		if (target.localScale == minScale)
			changeScale1 = changeScale2 = false;
	}

	public void NextButtonModePlay() {

		if (indexMode >= btnContent.Length - 1)
			indexMode = -1;
		
		indexMode++;
		contentTxt.text = btnContent [indexMode];
	}

	public void NextBtnRunAni(){
		isRunBtnPlayAni = true;
		changeScale1 = false;
		changeScale2 = false;
		isRight = true;
		isLeft = false;
	}

	public void PreButtonModePlay() {
		indexMode--;
		if (indexMode < 0)
			indexMode = btnContent.Length - 1;
		
		contentTxt.text = btnContent [indexMode];
	}

	public void PreBtnRunAni() {
		isRunBtnPlayAni = true;
		changeScale1 = false;
		changeScale2 = false;
		isLeft = true;
		isRight = false;
	}

	void AniBtnNextOrPre() {
		if (isRight) {
			if (changeRight1 == false) {
				nextBtn.transform.localScale = Vector3.MoveTowards (nextBtn.transform.localScale, maxScale, speed * Time.deltaTime);
				if (nextBtn.transform.localScale == maxScale)
					changeRight1 = true;
			}
			if (changeRight1 && changeRight2 == false) {
				nextBtn.transform.localScale = Vector3.MoveTowards (nextBtn.transform.localScale, originScale, speed * Time.deltaTime);
				if (nextBtn.transform.localScale == originScale) {
					changeRight2 = true;
					isRunBtnPlayAni = false;
					changeRight1 = changeRight2 = false;
				}
			}
		} else {
			if (changeLeft1 == false) {
				preBtn.transform.localScale = Vector3.MoveTowards (preBtn.transform.localScale, new Vector3 (-maxScale.x, maxScale.y, 1), speed * Time.deltaTime);
				if (preBtn.transform.localScale == new Vector3 (-maxScale.x, maxScale.y, 1))
					changeLeft1 = true;
			}
			if (changeLeft1 && changeLeft2 == false) {
				preBtn.transform.localScale = Vector3.MoveTowards (preBtn.transform.localScale, new Vector3 (-originScale.x, originScale.y, 1), speed * Time.deltaTime);
				if (preBtn.transform.localScale == new Vector3 (-originScale.x, originScale.y, 1)) {
					changeLeft2 = true;
					isRunBtnPlayAni = false;
					changeLeft1 = changeLeft2 = false;
				}
			}
		}
	}

	void AniSequenceMoveGo() {
		obj1.position = Vector3.MoveTowards (obj1.position, pos1Seq.position, speed * Time.deltaTime);
		if (obj1.position.x < 5f)
			obj2.position = Vector3.MoveTowards (obj2.position, pos2Seq.position, speed * Time.deltaTime);
		if (obj2.position.x < 5f)
			obj3.position = Vector3.MoveTowards (obj3.position, pos3Seq.position, speed * Time.deltaTime);
		if (obj3.position == pos3Seq.position)
			progressBar.SetActive (true);
	}
		
	void AniSequenceMoveBack() {
		progressBar.SetActive (false);

		obj3.position = Vector3.MoveTowards (obj3.position, new Vector3 (20f, obj3.position.y, obj3.position.z), speed * Time.deltaTime);
		if (obj3.position.x > 15f)
			obj2.position = Vector3.MoveTowards (obj2.position, new Vector3 (20f, obj2.position.y, obj2.position.z), speed * Time.deltaTime);
		if (obj2.position.x > 15f)
			obj1.position = Vector3.MoveTowards (obj1.position, new Vector3 (20f, obj1.position.y, obj1.position.z), speed * Time.deltaTime);
	}

	public void ReturnPosHideQuest() {
		obj1.position = new Vector3 (20f, obj1.position.y, obj1.position.z);
		obj2.position = new Vector3 (20f, obj2.position.y, obj1.position.z);
		obj3.position = new Vector3 (20f, obj3.position.y, obj1.position.z);

		progressBar.SetActive (false);
	}

	void AniShake() {
		if (timeShake >= timeInterShake) {
			timeInterShake = 0.2f;
			timeShake = 0;
		} else
			timeShake += Time.deltaTime;

		target.Rotate (0, 0, speed);
		if (timeShake >= timeInterShake) {
			speed *= -1;
		}
	}

	IEnumerator RunAni(){
		while (true) {
			if (isRunChangeColorAni)
				ChangeColor ();
			if (isRunMoveAni)
				Move3DesGoDes1ToDes3 ();
			else if (isMoveAni)
				Move3DesBackDes3ToDes1 ();
			if (isRunScaleAni)
				ScaleToScale ();
			else if (isScaleAni)
				ScaleToMin ();
			if (isRunBtnPlayAni) {
				AniBtnNextOrPre ();
				ScaleToScale ();
			}
			if (isRunSeqAni)
				AniSequenceMoveGo ();
			else if(isSequence)
				AniSequenceMoveBack ();
			if (isRunShakeAni)
				AniShake ();
			else if (isShakeAni)
				target.eulerAngles = new Vector3 (0, 0, 0);
			yield return new WaitForSeconds (0.02f);
		}
	}
}
