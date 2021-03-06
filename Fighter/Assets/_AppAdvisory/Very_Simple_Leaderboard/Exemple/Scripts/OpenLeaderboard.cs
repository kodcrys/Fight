﻿
/***********************************************************************************************************
 * Produced by App Advisory	- http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/


using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using AppAdvisory.social;

public class OpenLeaderboard : MonoBehaviour 
{
	void Awake()
	{
		LeaderboardManager.Getname ();
		GetComponent<Button>().onClick.AddListener(OnClicked);
	}

	void OnClicked()
	{
		LeaderboardManager.ShowLeaderboardUI();
	}
}
