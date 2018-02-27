﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Time.timeScale = 0;
		gameObject.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) 
		{
			Time.timeScale = 1;
			SoundManager.flyS.Play ();
			gameObject.SetActive (false);
		}
	}
}
