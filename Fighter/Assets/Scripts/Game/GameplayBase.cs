﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayBase : MonoBehaviour {

	public static GameplayBase instance;

	public GameObject rightButton, leftButton;

	public bool zoomCamera;

	public Camera mainCamera;

	public void Start(){
		instance = this;
		zoomCamera = false;
	}

	public void Update(){
		Debug.Log (mainCamera.orthographicSize);

		if (zoomCamera) {
			if (mainCamera.orthographicSize >= 6)
				mainCamera.orthographicSize = 6;
			else
				mainCamera.orthographicSize += 2 * Time.deltaTime;
		}
	}
}
