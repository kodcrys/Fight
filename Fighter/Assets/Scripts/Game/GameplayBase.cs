﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayBase : MonoBehaviour {

	public static GameplayBase instance;

	public GameObject rightButton, leftButton;

	public bool zoomCamera;

	public Camera mainCamera;

	[SerializeField]
	UnityEngine.UI.Image roundNumImg;

	[SerializeField]
	List<Sprite> numRound = new List<Sprite>();

	[SerializeField]
	List<GameObject> winCheckRight = new List<GameObject>();

	[SerializeField]
	List<GameObject> winCheckLeft = new List<GameObject>();


	public void Start(){
		instance = this;
		zoomCamera = false;
		if (SaveManager.instance.state.roundCount == 1)
			roundNumImg.sprite = numRound [0];
		else if(SaveManager.instance.state.roundCount == 2)
			roundNumImg.sprite = numRound [1];
	}

	public void Update(){
		if (AnimationText.canPlay) {
			rightButton.SetActive (true);
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
}
