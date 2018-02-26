using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour {

	public static GamePlayController instance;

	// Tap to play button.
	[SerializeField]
	private Button instructionButton;

	// The text which shows the score.
	[SerializeField]
	private Text scoreText;

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake()
	{
		Time.timeScale = 0;
		_MakeInstance ();
	}

	/// <summary>
	/// Makes the instance.
	/// </summary>
	void _MakeInstance()
	{
		if (instance == null) 
		{
			instance = this;
		}
	}

	/// <summary>
	/// Tap to play button to play the game.
	/// </summary>
	public void _InstructionButton(){
		Time.timeScale = 1;
		instructionButton.gameObject.SetActive (false);
	}

	/// <summary>
	/// Set the score.
	/// </summary>
	/// <param name="score">Score.</param>
	public void _SetScore(int score){
		scoreText.text = "" + score;
	}

	/// <summary>
	/// Birds the died show panel.
	/// </summary>
	/// <param name="score">Score.</param>
	public void _BirdDiedShowPanel(int score){
	}

	/// <summary>
	/// Home button.
	/// </summary>
	public void _MenuButton(){
		Application.LoadLevel ("MainMenu");
	}

	/// <summary>
	/// Restart button.
	/// </summary>
	public void _RestartGameButton(){
		Application.LoadLevel ("FlappyThumb");
	}
}
