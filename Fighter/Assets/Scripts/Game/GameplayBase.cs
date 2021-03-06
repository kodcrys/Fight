﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayBase : MonoBehaviour {

	public static GameplayBase instance;

	public GameObject rightButton, leftButton;

	public bool zoomCamera;

	public bool gamePause;

	public Camera mainCamera;

	[SerializeField]
	GameObject shieldLeft, shieldRight;

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

	public static DataCharacter dataPlayer1;
	public static DataCharacter dataPlayer2;
	public static DataCharacter dataAI;

	public static DataItems hatPlayer1, hatPlayer2, amorPlayer1, amorPlayer2, wpPlayer1, wpPlayer2;
	public static DataItems hatAI, amorAI, wpAI;

	public void Start(){

		//Debug.Log (data.characterPlayer1 + " & " + data.characterPlayer2 + " & " + data.hatPlayer1 + " & " + data.hatPlayer2);

		instance = this;
		gamePause = false;
		CheckAI ();
		zoomCamera = false;
		if (SaveManager.instance.state.roundCount == 1)
			roundNumImg.sprite = numRound [0];
		else if(SaveManager.instance.state.roundCount == 2)
			roundNumImg.sprite = numRound [1];

		if (!SaveManager.instance.state.isShieldLeft) {
			shieldLeft.SetActive (false);
		} else {
			shieldLeft.SetActive (true);
		}

		if (!SaveManager.instance.state.isShieldRight) {
			shieldRight.SetActive (false);
		} else {
			shieldRight.SetActive (true);
		}

		if (!SaveManager.instance.state.player2AI) {
			FingerRightControl.instance.ChangeCharPlayer ();
			FingerRightControl.instance.ChangeItemsPlayer ();
		} else if (SaveManager.instance.state.player2AI) {
			FingerRightControl.instance.ChangeCharAI ();
			FingerRightControl.instance.ChangeItemsAI ();
		}

		FingerLeftControl.instance.ChangeCharPlayer ();
		FingerLeftControl.instance.ChangeItemsPlayer ();
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
