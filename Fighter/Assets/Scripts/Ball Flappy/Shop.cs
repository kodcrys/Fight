
using UnityEngine;

public class Shop : MonoBehaviour {

	[SerializeField]
	UnityEngine.UI.Text gold;
	// Use this for initialization
	void Start () {
				//PlayerPrefs.SetInt ("gold", 10000);
		gold.text = PlayerPrefs.GetInt ("gold", 0).ToString ();
	}
	
	public void Back(){
		SoundManager.clickS.Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("StartGame");
	}
}
