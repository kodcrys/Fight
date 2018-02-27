using UnityEngine;
using System.Collections;

public class ButtonShop : MonoBehaviour {
	int unlock;
	public UnityEngine.UI.Image img;
	public UnityEngine.UI.Text gold;
	public UnityEngine.UI.Text textSold;
	public Sprite[] sprite;
	public GameObject[] frameChar;
	public GameObject[] frameCharUnChoose;
	int index;
	
	// Use this for initialization
	void Start () {
		if (transform.name == "1") {
			unlock = 1;
			PlayerPrefs.SetInt ("unlock" + transform.name, unlock);
		}

		unlock = PlayerPrefs.GetInt ("unlock" + transform.name, 0);
		index = PlayerPrefs.GetInt ("indexChar", 0);

		if (unlock == 0) {
			img.sprite = sprite [0];
			textSold.text = "200";
		}
		if (unlock == 1) {
			textSold.text = "Bought";
			img.sprite = sprite [1];
			if (index + 1 == int.Parse (transform.name)) {
				for (int i = 0; i < frameChar.Length; i++) {
					if (i == index)
						frameChar [i].SetActive (true);
					else
						frameChar [i].SetActive (false);
				}
				
				for (int j = 0; j < frameCharUnChoose.Length; j++) {
					if (j == index)
						frameCharUnChoose [j].SetActive (false);
					else
						frameCharUnChoose [j].SetActive (true);
				}
				//frame.SetActive (true);
			} else {
				for(int i = 0; i<frameChar.Length; i++){
					if (i == index)
						frameChar [i].SetActive (true);
					else
						frameChar [i].SetActive (false);
				}
				for (int j = 0; j < frameCharUnChoose.Length; j++) {
					if (j == index)
						frameCharUnChoose [j].SetActive (false);
					else
						frameCharUnChoose [j].SetActive (true);
				}
				//frame.SetActive (false);
			}
		}
	}
	
	public void Click(){
		int g = PlayerPrefs.GetInt ("gold", 0);
				SoundManager.clickS.Play ();
		if (g >= 200 && unlock == 0) {
			unlock = 1;
			PlayerPrefs.SetInt ("unlock" + transform.name, unlock);
			g -= 200;
			PlayerPrefs.SetInt ("gold", g);
			img.sprite = sprite [1];
			gold.text = g.ToString ();
			//frame.SetActive (false);
		}
		if (g < 200 && unlock == 0) {
			StartCoroutine (NotEnoughGold ());
		}
		if(unlock == 1){
			textSold.text = "Bought";
			BirdMovement.indexChar = int.Parse(transform.name) - 1;
			PlayerPrefs.SetInt ("indexChar", BirdMovement.indexChar);
			//Debug.Log ("unlock " + unlock);
			//Debug.Log ((BirdMovement.indexChar + 1) + " " + transform.name);
			if (BirdMovement.indexChar + 1 == int.Parse (transform.name)) {
								
				for (int i = 0; i < frameChar.Length; i++) {
						if (i == BirdMovement.indexChar) {
												frameChar [i].SetActive (true);
												frameCharUnChoose [i].SetActive (false);
										} else {
												frameChar [i].SetActive (false);
												frameCharUnChoose [i].SetActive (true);
										}
				}
						
				/*for (int j = 0; j < frameCharUnChoose.Length; j++) {
					if (j == index)
						frameCharUnChoose [j].SetActive (false);
					else
						frameCharUnChoose [j].SetActive (true);
				}*/
								//frame.SetActive (true);
			} else {
				for(int i = 0; i<frameChar.Length; i++){
										if (i == BirdMovement.indexChar) {
												frameChar [i].SetActive (true);
												frameCharUnChoose [i].SetActive (false);
										} else {
												frameChar [i].SetActive (false);
												frameCharUnChoose [i].SetActive (true);
										}
				}
			}
		}
	}
		[SerializeField]
		GameObject goldEnough;
		IEnumerator NotEnoughGold(){
				goldEnough.transform.localScale = new Vector3 (1, 1, 1);
				yield return new WaitForSeconds (0.25f);
				goldEnough.transform.localScale = new Vector3 (1f, 1.2f, 1);
				yield return new WaitForSeconds (0.25f);
				goldEnough.transform.localScale = new Vector3 (1, 1, 1);
				yield return new WaitForSeconds (0.25f);
				goldEnough.transform.localScale = new Vector3 (1f, 1.2f, 1);
				yield return new WaitForSeconds (0.25f);
				goldEnough.transform.localScale = new Vector3 (1, 1, 1);
		}
}
