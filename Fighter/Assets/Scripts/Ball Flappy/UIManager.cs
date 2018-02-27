using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour 
{
	public static int score;

	[SerializeField]
	UnityEngine.UI.Text scoreTxt;

	// Use this for initialization
	void Awake () 
	{
		score = 0;
		ShowScore ();
	}

	public void ShowScore()
	{
		scoreTxt.text = score.ToString ();
	}
}
