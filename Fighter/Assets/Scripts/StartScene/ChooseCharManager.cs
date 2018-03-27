using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChooseCharManager : MonoBehaviour {

	public static ChooseCharManager instance;

	[Header("Button change type choose")]
	[SerializeField]
	UnityEngine.UI.Button[] btnsChangeType;
	[SerializeField]
	Transform [] PosBtnsChangeType;

	[SerializeField]
	GameObject typeChooseChar;
	[SerializeField]
	GameObject typeChooseEquipment;

	[Header("Data")]
	[SerializeField]
	CointainData[] dataChars;
	[SerializeField]
	CointainData[] dataItems;
	[SerializeField]
	CointainData[] dataHats;
	[SerializeField]
	CointainData[] dataAmors;
	[SerializeField]
	CointainData[] dataWeapons;

	[Header("Choose Action")]
	public GameObject chooseSymbol;

	[Header("Mask")]
	[SerializeField]
	Transform maskFrameChooseChar;
	[SerializeField]
	Transform maskFrameChooseEquip;

	[Header("PLayer2 or AI")]
	[SerializeField]
	UnityEngine.UI.Image hatSymbol;
	[SerializeField]
	UnityEngine.UI.Image hatMainR;
	[SerializeField]
	UnityEngine.UI.Image amorMainR;
	[SerializeField]
	UnityEngine.UI.Image weaponMainR;

	[Header("PLayer1")]
	[SerializeField]
	UnityEngine.UI.Image hatSymbol2;
	[SerializeField]
	UnityEngine.UI.Image hatMainL;
	[SerializeField]
	UnityEngine.UI.Image amorMainL;
	[SerializeField]
	UnityEngine.UI.Image weaponMainL;

	[Header("UIAnimations")]
	[SerializeField]
	UIAnimations topBar;
	[SerializeField]
	UIAnimations midBar;
	[SerializeField]
	UIAnimations botBar;
	[SerializeField]
	UIAnimations moveChooseFrame;
	[SerializeField]
	UIAnimations showTop;
	[SerializeField]
	UIAnimations vsImage;
	[SerializeField]
	UIAnimations nextBtn;
	[SerializeField]
	UIAnimations preBtn;

	[Header("Lock Btn")]
	[SerializeField]
	Sprite lockSpr;
	[SerializeField]
	Sprite readySpr;

	[Header("Btn Play Game")]
	[SerializeField]
	UnityEngine.UI.Text contentBtn;
	string[] playMode = { "P1 VS P2", "P1 VS CPU", "TOURNAMENT", "MINI GAME" };
	[SerializeField]
	UnityEngine.UI.Text modeAIText;
	[SerializeField]
	GameObject play2Btn;
	[SerializeField]
	GameObject aiBtn;
	string[] modeAI = {"EASY", "NORMAL", "HARD", "VERY HARD"};

	[Header("Screen Map")]
	[SerializeField]
	SpriteRenderer map;

	[Header("Fade ani")]
	[SerializeField]
	FadeAni aniFade;

	[Header("Save")]
	[SerializeField]
	SaveDataCharacter cointainSave;

	bool isTurnPlayer1 = false;

	GameObject objFollow;

	[SerializeField]
	UnityEngine.UI.Image ready1, ready2;
	[HideInInspector]
	public bool isPlayAI;

	void Awake() {
		if (instance == null)
			instance = this;
	}

	// Use this for initialization
	void Start () {
		isTurnPlayer1 = true;
		chooseSymbol.SetActive (false);
	}

	void Update() {
		if(objFollow != null)
			chooseSymbol.transform.position = objFollow.transform.position;
	}

	bool isShowTypeChar = true;

	// CHange type equipment choose or character choose
	public void ChangeTypeChar() {
		isShowTypeChar = !isShowTypeChar;
		chooseSymbol.SetActive (false);
		if (isShowTypeChar) {
			btnsChangeType [0].transform.SetParent (PosBtnsChangeType [1].transform);
			btnsChangeType [1].transform.SetParent (PosBtnsChangeType [0].transform);
			typeChooseChar.SetActive (true);
			typeChooseEquipment.SetActive (false);
		} else {
			btnsChangeType [1].transform.SetParent (PosBtnsChangeType [1].transform);
			btnsChangeType [0].transform.SetParent (PosBtnsChangeType [0].transform);
			typeChooseChar.SetActive (false);
			typeChooseEquipment.SetActive (true);
		}
	}

	// display image in each library cell
	void EnDisableLibraryCell() {
		for (int i = 0; i < dataChars.Length; i++) {
			if (dataChars [i].dataChar.isOwned == false)
				dataChars [i].GetComponent<UnityEngine.UI.Button> ().interactable = false;
			else
				dataChars [i].GetComponent<UnityEngine.UI.Button> ().interactable = true;
		}

		for (int i = 0; i < dataItems.Length; i++) {
			if (dataItems [i].dataItem.isOwned == false)
				dataItems [i].GetComponent<UnityEngine.UI.Button> ().interactable = false;
			else
				dataItems [i].GetComponent<UnityEngine.UI.Button> ().interactable = true;
		}
	}

	// run ani change scene when click play
	public void AniChangeScene() {

		// p1 zs p2
		if (contentBtn.text == playMode [0]) {
			SaveManager.instance.state.player1AI = false;
			SaveManager.instance.state.player2AI = false;
			play2Btn.SetActive (true);
			aiBtn.SetActive (false);

			isTurnPlayer1 = true;

			ready1.sprite = readySpr;
			ready2.sprite = readySpr;

			preBtn.GetComponent<UnityEngine.UI.Button> ().interactable = true;
			nextBtn.GetComponent<UnityEngine.UI.Button> ().interactable = false;

			EnDisableLibraryCell ();

			topBar.isRunMoveAni = false;
			midBar.isRunScaleAni = false;
			botBar.isRunMoveAni = false;

			showTop.isRunMoveAni = true;
			vsImage.isRunScaleAni = true;
			moveChooseFrame.isRunMoveAni = true;
			nextBtn.isRunMoveAni = true;
			preBtn.isRunMoveAni = true;
		}
		// zs AI
		if (contentBtn.text == playMode [1]) {
			SaveManager.instance.state.player1AI = false;
			SaveManager.instance.state.player2AI = true;

			play2Btn.SetActive (false);
			aiBtn.SetActive (true);

			isTurnPlayer1 = true;

			ready1.sprite = readySpr;
			preBtn.GetComponent<UnityEngine.UI.Button> ().interactable = true;
			EnDisableLibraryCell ();

			topBar.isRunMoveAni = false;
			midBar.isRunScaleAni = false;
			botBar.isRunMoveAni = false;

			showTop.isRunMoveAni = true;
			vsImage.isRunScaleAni = true;
			moveChooseFrame.isRunMoveAni = true;
			preBtn.isRunMoveAni = true;

			//Change mode ai text
			modeAIText.text = modeAI[0].ToString();

			aiBtn.GetComponent<UIAnimations> ().isRunMoveAni = true;
		}
		// tour
		if (contentBtn.text == playMode [2]) {
			
		}
		// minigame
		if (contentBtn.text == playMode [3]) {
			
		}


	}

	// run ani change scene when click back
	public void AniChangeBackScene() {
		chooseSymbol.SetActive (false);

		topBar.isRunMoveAni = true;
		midBar.isRunScaleAni = true;
		botBar.isRunMoveAni = true;

		showTop.isRunMoveAni = false;
		vsImage.isRunScaleAni = false;
		moveChooseFrame.isRunMoveAni = false;
		nextBtn.isRunMoveAni = false;
		preBtn.isRunMoveAni = false;

		aiBtn.GetComponent<UIAnimations> ().isRunMoveAni = false;
	}

	//
	public void LockWhenFinishChoose() {
		if (isTurnPlayer1) {
			nextBtn.GetComponent<UnityEngine.UI.Button> ().interactable = true;
			ready1.sprite = lockSpr;
			isTurnPlayer1 = false;

			if (contentBtn.text == playMode [1]) {
				aniFade.stateFade = FadeAni.State.Show;
				aniFade.isChangeMap = true;
				// Change mode AI afer play
				PlayModeAI ();
			}

		} else {
			if (contentBtn.text == playMode [0]) {
				ready2.sprite = lockSpr;
				aniFade.stateFade = FadeAni.State.Show;
				aniFade.isChangeMap = true;
			}
			//UnityEngine.SceneManagement.SceneManager.LoadScene ("ChooseMap");
		}
	}

	// choose character or equipment when click button in choose frame
	public void ChooseChar() {
		GameObject gob = EventSystem.current.currentSelectedGameObject;
	
		objFollow = gob;
		if (isShowTypeChar)
			chooseSymbol.transform.SetParent (maskFrameChooseChar);
		else
			chooseSymbol.transform.SetParent (maskFrameChooseEquip);
		
		CointainData ctData = gob.GetComponent<CointainData> ();
		Debug.Log(gob.name + " " + ctData.dataChar + " " + ctData.dataItem);

		if (ctData.dataChar != null && ctData.dataChar.isOwned) {
			if (isTurnPlayer1) {
				hatSymbol2.gameObject.SetActive (false);
				hatMainL.gameObject.SetActive (true);
				hatMainL.sprite = ctData.dataChar.equipmentOfChar;
				amorMainL.gameObject.SetActive (false);
				weaponMainL.gameObject.SetActive (false);

				cointainSave.characterPlayer1 = ctData.dataChar;
				//cointainSave.hatPlayer1 = null;
				//cointainSave.amorPlayer1 = null;
				//cointainSave.weaponPlayer1 = null;

			} else {
				hatSymbol.gameObject.SetActive (false);
				hatMainR.gameObject.SetActive (true);
				hatMainR.sprite = ctData.dataChar.equipmentOfChar;
				amorMainR.gameObject.SetActive (false);
				weaponMainR.gameObject.SetActive (false);

				cointainSave.characterPlayer2 = ctData.dataChar;
				//cointainSave.hatPlayer2 = null;
				//cointainSave.amorPlayer2 = null;
				//cointainSave.weaponPlayer2 = null;
			}
			chooseSymbol.SetActive (true);
		} else
			chooseSymbol.SetActive (false);

	
		if (ctData.dataItem != null && ctData.dataItem.isOwned) {
			if (isTurnPlayer1) {

				//cointainSave.characterPlayer1 = null;

				hatSymbol2.gameObject.SetActive (false);

				if (ctData.dataItem.typeItem == TypeObject.hat) {
					hatMainL.gameObject.SetActive (true);
					hatMainL.sprite = ctData.dataItem.avatar;

					cointainSave.hatPlayer1 = ctData.dataItem;
				}

				if (ctData.dataItem.typeItem == TypeObject.tshirt) {
					amorMainL.gameObject.SetActive (true);
					amorMainL.sprite = ctData.dataItem.avatar;

					cointainSave.amorPlayer1 = ctData.dataItem;
				}

				if (ctData.dataItem.typeItem == TypeObject.weapon) {
					weaponMainL.gameObject.SetActive (true);
					weaponMainL.sprite = ctData.dataItem.avatar;

					cointainSave.weaponPlayer1 = ctData.dataItem;
				}

			} else {

				//cointainSave.characterPlayer2 = null;

				hatSymbol.gameObject.SetActive (false);

				if (ctData.dataItem.typeItem == TypeObject.hat) {
					hatMainR.gameObject.SetActive (true);
					hatMainR.sprite = ctData.dataItem.avatar;

					cointainSave.hatPlayer2 = ctData.dataItem;
				}

				if (ctData.dataItem.typeItem == TypeObject.tshirt) {
					amorMainR.gameObject.SetActive (true);
					amorMainR.sprite = ctData.dataItem.avatar;

					cointainSave.amorPlayer2 = ctData.dataItem;
				}

				if (ctData.dataItem.typeItem == TypeObject.weapon) {
					weaponMainR.gameObject.SetActive (true);
					weaponMainR.sprite = ctData.dataItem.avatar;

					cointainSave.weaponPlayer2 = ctData.dataItem;
				}
			}
			chooseSymbol.SetActive (true);
		} else
			chooseSymbol.SetActive (true);

		if (ctData.dataMap != null) {
			map.sprite = ctData.dataMap;
			chooseSymbol.SetActive (true);
		}
	}

	public void RandomChar() {

		GameObject gob = EventSystem.current.currentSelectedGameObject;
		List<CointainData> dtChars = new List<CointainData> ();

		chooseSymbol.SetActive (false);

		for (int i = 0; i < dataChars.Length; i++)
			if (dataChars [i].dataChar.isOwned)
				dtChars.Add (dataChars [i]);

		if (dtChars.Count > 0) {

			int rand = Random.Range (0, dtChars.Count);
			while (dtChars [rand].dataChar.isOwned == false) {
				rand = Random.Range (0, dtChars.Count);
			}
			CointainData ctData = dtChars [rand];

			if (isTurnPlayer1) {
				hatSymbol2.gameObject.SetActive (false);
				hatMainL.gameObject.SetActive (true);
				hatMainL.sprite = ctData.dataChar.equipmentOfChar;
				amorMainL.gameObject.SetActive (false);
				weaponMainL.gameObject.SetActive (false);

				cointainSave.characterPlayer1 = ctData.dataChar;

				//cointainSave.hatPlayer1 = null;
				//cointainSave.amorPlayer1 = null;
				//cointainSave.weaponPlayer1 = null;

			} else {
				hatSymbol.gameObject.SetActive (false);
				hatMainR.gameObject.SetActive (true);
				hatMainR.sprite = ctData.dataChar.equipmentOfChar;
				amorMainR.gameObject.SetActive (false);
				weaponMainR.gameObject.SetActive (false);

				cointainSave.characterPlayer2 = ctData.dataChar;

				//cointainSave.hatPlayer2 = null;
				//cointainSave.amorPlayer2 = null;
				//cointainSave.weaponPlayer2 = null;

			}
		}
	}

	public void RandomHat() {

		GameObject gob = EventSystem.current.currentSelectedGameObject;
		List<CointainData> dtHats = new List<CointainData> ();

		chooseSymbol.SetActive (false);

		for (int i = 0; i < dataHats.Length; i++)
			if (dataHats [i].dataItem.isOwned)
				dtHats.Add (dataHats [i]);

		if (dtHats.Count > 0) {
			int rand = Random.Range (0, dtHats.Count);
			while (dtHats [rand].dataItem.isOwned == false) {
				rand = Random.Range (0, dtHats.Count);
			}
			CointainData ctData = dtHats [rand];

			if (isTurnPlayer1) {
				
				//cointainSave.characterPlayer1 = null;

				hatSymbol2.gameObject.SetActive (false);
				hatMainL.gameObject.SetActive (true);
				hatMainL.sprite = ctData.dataItem.avatar;

				cointainSave.hatPlayer1 = ctData.dataItem;
			} else {

				//cointainSave.characterPlayer2 = null;

				hatSymbol.gameObject.SetActive (false);
				hatMainR.gameObject.SetActive (true);
				hatMainR.sprite = ctData.dataItem.avatar;

				cointainSave.hatPlayer2 = ctData.dataItem;
			}
		}
	}

	public void RandomAmor() {

		GameObject gob = EventSystem.current.currentSelectedGameObject;
		List<CointainData> dtAmors = new List<CointainData> ();

		chooseSymbol.SetActive (false);

		for (int i = 0; i < dataAmors.Length; i++)
			if (dataAmors [i].dataItem.isOwned)
				dtAmors.Add (dataAmors [i]);

		if (dtAmors.Count > 0) {
			int rand = Random.Range (0, dtAmors.Count);
			while (dtAmors [rand].dataItem.isOwned == false) {
				rand = Random.Range (0, dtAmors.Count);
			}
			CointainData ctData = dtAmors [rand];

			if (isTurnPlayer1) {

				//cointainSave.characterPlayer1 = null;

				hatSymbol2.gameObject.SetActive (false);
				amorMainL.gameObject.SetActive (true);
				amorMainL.sprite = ctData.dataItem.avatar;

				cointainSave.amorPlayer1 = ctData.dataItem;
			} else {

				//cointainSave.characterPlayer2 = null;

				hatSymbol.gameObject.SetActive (false);
				amorMainR.gameObject.SetActive (true);
				amorMainR.sprite = ctData.dataItem.avatar;

				cointainSave.amorPlayer2 = ctData.dataItem;
			}
		}
	}

	public void RandomWeapon() {

		GameObject gob = EventSystem.current.currentSelectedGameObject;
		List<CointainData> dtWps = new List<CointainData> ();

		chooseSymbol.SetActive (false);

		for (int i = 0; i < dataWeapons.Length; i++)
			if (dataWeapons [i].dataItem.isOwned)
				dtWps.Add (dataWeapons [i]);

		if (dtWps.Count > 0) {
			int rand = Random.Range (0, dtWps.Count);
			while (dtWps [rand].dataItem.isOwned == false) {
				rand = Random.Range (0, dtWps.Count);
			}
			CointainData ctData = dtWps [rand];

			if (isTurnPlayer1) {

				//cointainSave.characterPlayer1 = null;

				hatSymbol2.gameObject.SetActive (false);
				weaponMainL.gameObject.SetActive (true);
				weaponMainL.sprite = ctData.dataItem.avatar;

				cointainSave.weaponPlayer1 = ctData.dataItem;
			} else {

				//cointainSave.characterPlayer2 = null;

				hatSymbol.gameObject.SetActive (false);
				weaponMainR.gameObject.SetActive (true);
				weaponMainR.sprite = ctData.dataItem.avatar;

				cointainSave.weaponPlayer2 = ctData.dataItem;
			}
		}
	}

	public void ReadSave() {
		#region character
		if (cointainSave.characterPlayer1 != null) {
			hatSymbol2.gameObject.SetActive (false);

			hatMainL.gameObject.SetActive (true);
			hatMainL.sprite = cointainSave.characterPlayer1.equipmentOfChar;

			amorMainL.gameObject.SetActive (false);
			weaponMainL.gameObject.SetActive (false);
		} else {
			hatSymbol2.gameObject.SetActive (true);
			hatMainL.gameObject.SetActive (false);
			amorMainL.gameObject.SetActive (false);
			weaponMainL.gameObject.SetActive (false);
		}

		if (cointainSave.characterPlayer2 != null) {
			hatSymbol.gameObject.SetActive (false);

			hatMainR.gameObject.SetActive (true);
			hatMainR.sprite = cointainSave.characterPlayer2.equipmentOfChar;

			amorMainR.gameObject.SetActive (false);
			weaponMainR.gameObject.SetActive (false);
		} else {
			hatSymbol.gameObject.SetActive (true);
			hatMainR.gameObject.SetActive (false);
			amorMainR.gameObject.SetActive (false);
			weaponMainR.gameObject.SetActive (false);
		}
		#endregion

		#region hatPlayer
		if (cointainSave.hatPlayer1 != null) {
			hatSymbol2.gameObject.SetActive (false);

			hatMainL.gameObject.SetActive (true);
			hatMainL.sprite = cointainSave.hatPlayer1.avatar;
		} else {
			hatSymbol2.gameObject.SetActive (true);
			hatMainL.gameObject.SetActive (false);
		}

		if (cointainSave.hatPlayer2 != null) {
			hatSymbol.gameObject.SetActive (false);

			hatMainR.gameObject.SetActive (true);
			hatMainR.sprite = cointainSave.hatPlayer2.avatar;
		} else {
			hatSymbol.gameObject.SetActive (true);
			hatMainR.gameObject.SetActive (false);
		}
		#endregion

		#region amorPlayer
		if (cointainSave.amorPlayer1 != null) {
			amorMainL.gameObject.SetActive (true);
			amorMainL.sprite = cointainSave.amorPlayer1.avatar;
		} else {
			amorMainL.gameObject.SetActive (false);
		}

		if (cointainSave.amorPlayer2 != null) {
			amorMainR.gameObject.SetActive (true);
			amorMainR.sprite = cointainSave.amorPlayer2.avatar;
		} else {
			amorMainR.gameObject.SetActive (false);
		}
		#endregion

		#region weaponPlayer
		if (cointainSave.weaponPlayer1 != null) {
			weaponMainL.gameObject.SetActive (true);
			weaponMainL.sprite = cointainSave.weaponPlayer1.avatar;
		} else {
			weaponMainL.gameObject.SetActive (false);
		}

		if (cointainSave.weaponPlayer2 != null) {
			weaponMainR.gameObject.SetActive (true);
			weaponMainR.sprite = cointainSave.weaponPlayer2.avatar;
		} else {
			weaponMainR.gameObject.SetActive (false);
		}
		#endregion
	}

	public void BackChooseChar() {
		aniFade.stateFade = FadeAni.State.Show;
		aniFade.isChangeChooseChar = true;

		// bien danh dau tu map ze choose char
		FadeAni.isRunMapToChooseChar = true;
		FadeAni.isRunMapToHome = false;
		FadeAni.isRunPlayGame = false;
	}

	public void Home() {
		aniFade.stateFade = FadeAni.State.Show;
		aniFade.isChangeChooseChar = true;
		FadeAni.isRunMapToChooseChar = true;
		FadeAni.isRunMapToHome = true;
		FadeAni.isRunPlayGame = false;
		//UnityEngine.SceneManagement.SceneManager.LoadScene ("StartScene");
	}

	public void PlayGame() {
		aniFade.stateFade = FadeAni.State.Show;
		aniFade.isChangeChooseChar = true;
		FadeAni.isRunMapToChooseChar = false;
		FadeAni.isRunMapToHome = false ;
		FadeAni.isRunPlayGame = true;
	}

	int countModeAI = 0;
	public void TopModeAI() {
		countModeAI++;
		if (countModeAI < playMode.Length)
			modeAIText.text = modeAI [countModeAI].ToString ();
		else {
			countModeAI = 0;
			modeAIText.text = modeAI [countModeAI].ToString ();
		}
		//Debug.Log (countModeAI);
	}

	public void DownModeAI() {
		countModeAI--;
		if (countModeAI >= 0)
			modeAIText.text = modeAI [countModeAI].ToString ();
		else {
			countModeAI = playMode.Length - 1;
			modeAIText.text = modeAI [countModeAI].ToString ();
		}
		Debug.Log (countModeAI);
	}

	void PlayModeAI() {
		if (modeAIText.text == modeAI [0].ToString ()) {
			// easy
			SaveManager.instance.state.levelAI = 0;
		}
		if (modeAIText.text == modeAI [1].ToString ()) {
			// normal
			SaveManager.instance.state.levelAI = 1;
		}
		if (modeAIText.text == modeAI [2].ToString ()) {
			// hard
			SaveManager.instance.state.levelAI = 2;
		}
		if (modeAIText.text == modeAI [3].ToString ()) {
			// very hard
			SaveManager.instance.state.levelAI = 3;
		}

		SaveManager.instance.Save ();
	}
}
