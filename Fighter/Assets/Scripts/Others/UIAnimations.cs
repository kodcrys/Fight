using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIAnimations : MonoBehaviour {

	[Header("Object run animation")]
	[SerializeField]
	Transform target;

	[SerializeField]
	float speed;

	[Header("Scale")]
	[SerializeField]
	bool isRunScaleAni;
	[SerializeField]
	Vector3 maxScale;
	[SerializeField]
	Vector3 originScale;
	[SerializeField]
	Vector3 minScale;
	bool changeScale1, changeScale2;

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
	[SerializeField]
	bool isRunMoveAni;
	[SerializeField]
	Transform pos1;
	[SerializeField]
	Transform pos2;
	[SerializeField]
	Transform pos3;
	bool changeDes1, changeDes2, changeDes3;

	[Header("Button Play")]
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

	void OnEnable() {
		if (contentTxt != null)
			contentTxt.text = btnContent [indexMode];
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

	void Move3Des() {
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

	void ScaleToScale() {
		if (changeScale1 == false) {
			target.localScale = Vector3.MoveTowards (target.localScale, maxScale, speed * Time.deltaTime);
			if (target.localScale == maxScale)
				changeScale1 = true;
		}
		if (changeScale1 && changeScale2 == false) {
			target.localScale = Vector3.MoveTowards (target.localScale, originScale, speed * Time.deltaTime);
			if (target.localScale == originScale)
				changeScale2 = true;
		}
	}

	public void NextButtonModePlay() {

		if (indexMode >= btnContent.Length - 1)
			indexMode = -1;
		
		indexMode++;
		contentTxt.text = btnContent [indexMode];
	}

	public void PreButtonModePlay() {
		indexMode--;
		if (indexMode < 0)
			indexMode = btnContent.Length - 1;
		
		contentTxt.text = btnContent [indexMode];
	}

	IEnumerator RunAni(){
		while (true) {
			if (isRunChangeColorAni)
				ChangeColor ();
			if (isRunMoveAni)
				Move3Des ();
			if (isRunScaleAni)
				ScaleToScale ();
			yield return new WaitForSeconds (0.02f);
		}
	}
}
