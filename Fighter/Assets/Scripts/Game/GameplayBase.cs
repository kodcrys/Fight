using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayBase : MonoBehaviour {

	[Header("Save Data Character")]
	[SerializeField]
	SaveDataCharacter data;

	public static GameplayBase instance;

	public GameObject rightButton, leftButton;

	public bool zoomCamera;

	public bool gamePause;

	public Camera mainCamera;

	[SerializeField]
	UnityEngine.UI.Image roundNumImg;

	[SerializeField]
	List<Sprite> numRound = new List<Sprite>();

	[SerializeField]
	List<GameObject> winCheckRight = new List<GameObject>();

	[SerializeField]
	List<GameObject> winCheckLeft = new List<GameObject>();

	[SerializeField]
	GameObject player1, player2;

	public GameObject pausePanel, gameoverP1Panel, gameoverP2Panel;

	public void Start(){
		instance = this;
		gamePause = false;
		CheckAI ();
		zoomCamera = false;
		if (SaveManager.instance.state.roundCount == 1)
			roundNumImg.sprite = numRound [0];
		else if(SaveManager.instance.state.roundCount == 2)
			roundNumImg.sprite = numRound [1];

		// Skin
		if (data.characterPlayer1 != null) {
			FingerLeftControl.instance.skin.sprite = data.characterPlayer1.equipmentOfChar;
			FingerLeftControl.instance.skin.gameObject.SetActive (true);
			FingerLeftControl.instance.hat.gameObject.SetActive (false);
			FingerLeftControl.instance.amor.gameObject.SetActive (false);
			FingerLeftControl.instance.weapon.gameObject.SetActive (false);
		}
		if (data.characterPlayer2 != null) {
			FingerRightControl.instance.skin.sprite = data.characterPlayer2.equipmentOfChar;
			FingerRightControl.instance.skin.gameObject.SetActive (true);
			FingerRightControl.instance.hat.gameObject.SetActive (false);
			FingerRightControl.instance.amor.gameObject.SetActive (false);
			FingerRightControl.instance.weapon.gameObject.SetActive (false);
		}

		// Hat
		if (data.hatPlayer1 != null) {
			FingerLeftControl.instance.skin.gameObject.SetActive (false);
			FingerLeftControl.instance.hat.sprite = data.hatPlayer1.avatar;
			FingerLeftControl.instance.hat.gameObject.SetActive (true);
		}
		if (data.hatPlayer2 != null) {
			FingerRightControl.instance.skin.gameObject.SetActive (false);
			FingerRightControl.instance.hat.sprite = data.hatPlayer2.avatar;
			FingerRightControl.instance.hat.gameObject.SetActive (true);
		}

		// Amor
		if (data.amorPlayer1 != null) {
			FingerLeftControl.instance.skin.gameObject.SetActive (false);
			FingerLeftControl.instance.amor.sprite = data.amorPlayer1.avatar;
			FingerLeftControl.instance.amor.gameObject.SetActive (true);
		}
		if (data.amorPlayer2 != null) {
			FingerRightControl.instance.skin.gameObject.SetActive (false);
			FingerRightControl.instance.amor.sprite = data.amorPlayer2.avatar;
			FingerRightControl.instance.amor.gameObject.SetActive (true);
		}

		// Weapon
		if (data.weaponPlayer1 != null) {
			FingerLeftControl.instance.skin.gameObject.SetActive (false);
			FingerLeftControl.instance.weapon.sprite = data.weaponPlayer1.avatar;
			FingerLeftControl.instance.weapon.gameObject.SetActive (true);
		}
		if (data.weaponPlayer2 != null) {
			FingerRightControl.instance.skin.gameObject.SetActive (false);
			FingerRightControl.instance.weapon.sprite = data.weaponPlayer2.avatar;
			FingerRightControl.instance.weapon.gameObject.SetActive (true);
		}
	}

	public void Update(){
		if (AnimationText.canPlay) {
			if (!SaveManager.instance.state.player2AI)
				rightButton.SetActive (true);

			if (!SaveManager.instance.state.player1AI)
				leftButton.SetActive (true);
		} else {
			rightButton.SetActive (false);
			leftButton.SetActive (false);
		}
		if (zoomCamera) {
			if (mainCamera.orthographicSize >= SaveManager.instance.state.cameraSize)
				mainCamera.orthographicSize = SaveManager.instance.state.cameraSize;
			else
				mainCamera.orthographicSize += 2 * Time.deltaTime;
		}

		CheckWin ();
	}

	void CheckAI(){
		if (!SaveManager.instance.state.player1AI) {
			player1.GetComponent<AIManager> ().enabled = false;
		} else if (SaveManager.instance.state.player1AI) {
			player1.GetComponent<AIManager> ().enabled = true;
		}

		if (!SaveManager.instance.state.player2AI) {
			player2.GetComponent<AIManager> ().enabled = false;
		} else if (SaveManager.instance.state.player2AI) {
			player2.GetComponent<AIManager> ().enabled = true;
		}
	}

	void CheckWin(){
		if (SaveManager.instance.state.winCountRight > 0) {
			for (int i = 1; i <= SaveManager.instance.state.winCountRight; i++) {
				winCheckRight [i - 1].SetActive (true);
			}
		}

		if (SaveManager.instance.state.winCountLeft > 0) {
			for (int i = 1; i <= SaveManager.instance.state.winCountLeft; i++) {
				winCheckLeft [i - 1].SetActive (true);
			}
		}
	}

	public void PauseClick(){
		gamePause = true;
		pausePanel.SetActive (true);
	}

	public void CountinueClick(){
		gamePause = false;
		pausePanel.SetActive (false);
	}

	public void ReMatch(){
		UnityEngine.SceneManagement.SceneManager.LoadScene ("MainGameScene");
		SaveManager.instance.state.winCountLeft = 0;
		SaveManager.instance.state.winCountRight = 0;
		SaveManager.instance.state.roundCount = 1;
		SaveManager.instance.Save ();
	}
}
