using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayBase : MonoBehaviour {

	public static GameplayBase instance;

	public GameObject rightButton, leftButton;

	public bool zoomCamera;

	public Camera mainCamera;

	[SerializeField]
	UnityEngine.UI.Text roundText;


	public void Start(){
		instance = this;
		zoomCamera = false;
		if (SaveManager.instance.state.roundCount <= 2)
			roundText.text = "Round " + SaveManager.instance.state.roundCount;
		else
			roundText.text = "Final\nRound";
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
			if (mainCamera.orthographicSize >= 6)
				mainCamera.orthographicSize = 6;
			else
				mainCamera.orthographicSize += 2 * Time.deltaTime;
		}
	}
}
