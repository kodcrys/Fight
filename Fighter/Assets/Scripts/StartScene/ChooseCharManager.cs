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

	[Header("Screen Map")]
	[SerializeField]
	SpriteRenderer map;

	bool isTurnPlayer1 = false;

	GameObject objFollow;

	[HideInInspector]
	UnityEngine.UI.Image ready1, ready2;
	public bool isPlayAI;

	void Awake() {
		if (instance == null)
			instance = this;
	}

	// Use this for initialization
	void Start () {
		if(preBtn != null)
			ready1 = preBtn.GetComponent<UnityEngine.UI.Image> ();
		if(nextBtn != null)
			ready2 = nextBtn.GetComponent<UnityEngine.UI.Image> ();

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
			
		}
		// zs AI
		if (contentBtn.text == playMode [1]) {
			
		}
		// tour
		if (contentBtn.text == playMode [2]) {
			
		}
		// minigame
		if (contentBtn.text == playMode [3]) {
			
		}

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
	}

	//
	public void LockWhenFinishChoose() {
		if (isTurnPlayer1) {
			nextBtn.GetComponent<UnityEngine.UI.Button> ().interactable = true;
			ready1.sprite = lockSpr;
			isTurnPlayer1 = false;
		} else {
			ready2.sprite = lockSpr;
			UnityEngine.SceneManagement.SceneManager.LoadScene ("ChooseMap");
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

		if (ctData.dataChar != null && ctData.dataChar.isOwned) {
			if (isTurnPlayer1) {
				hatSymbol2.gameObject.SetActive (false);
				hatMainL.gameObject.SetActive (true);
				hatMainL.sprite = ctData.dataChar.equipmentOfChar;
				amorMainL.gameObject.SetActive (false);
				weaponMainL.gameObject.SetActive (false);
			} else {
				hatSymbol.gameObject.SetActive (false);
				hatMainR.gameObject.SetActive (true);
				hatMainR.sprite = ctData.dataChar.equipmentOfChar;
				amorMainR.gameObject.SetActive (false);
				weaponMainR.gameObject.SetActive (false);
			}
			chooseSymbol.SetActive (true);
		} else
			chooseSymbol.SetActive (false);

	
		if (ctData.dataItem != null && ctData.dataItem.isOwned) {
			if (isTurnPlayer1) {
				hatSymbol2.gameObject.SetActive (false);

				if (ctData.dataItem.typeItem == TypeObject.hat) {
					hatMainL.gameObject.SetActive (true);
					hatMainL.sprite = ctData.dataItem.avatar;
				}

				if (ctData.dataItem.typeItem == TypeObject.tshirt) {
					amorMainL.gameObject.SetActive (true);
					amorMainL.sprite = ctData.dataItem.avatar;
				}

				if (ctData.dataItem.typeItem == TypeObject.weapon) {
					weaponMainL.gameObject.SetActive (true);
					weaponMainL.sprite = ctData.dataItem.avatar;
				}

			} else {
				hatSymbol.gameObject.SetActive (false);

				if (ctData.dataItem.typeItem == TypeObject.hat) {
					hatMainR.gameObject.SetActive (true);
					hatMainR.sprite = ctData.dataItem.avatar;
				}

				if (ctData.dataItem.typeItem == TypeObject.tshirt) {
					amorMainR.gameObject.SetActive (true);
					amorMainR.sprite = ctData.dataItem.avatar;
				}

				if (ctData.dataItem.typeItem == TypeObject.weapon) {
					weaponMainR.gameObject.SetActive (true);
					weaponMainR.sprite = ctData.dataItem.avatar;
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
			} else {
				hatSymbol.gameObject.SetActive (false);
				hatMainR.gameObject.SetActive (true);
				hatMainR.sprite = ctData.dataChar.equipmentOfChar;
				amorMainR.gameObject.SetActive (false);
				weaponMainR.gameObject.SetActive (false);
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
				hatSymbol2.gameObject.SetActive (false);
				hatMainL.gameObject.SetActive (true);
				hatMainL.sprite = ctData.dataItem.avatar;
			} else {
				hatSymbol.gameObject.SetActive (false);
				hatMainR.gameObject.SetActive (true);
				hatMainR.sprite = ctData.dataItem.avatar;
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
				hatSymbol2.gameObject.SetActive (false);
				amorMainL.gameObject.SetActive (true);
				amorMainL.sprite = ctData.dataItem.avatar;
			} else {
				hatSymbol.gameObject.SetActive (false);
				amorMainR.gameObject.SetActive (true);
				amorMainR.sprite = ctData.dataItem.avatar;
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
				hatSymbol2.gameObject.SetActive (false);
				weaponMainL.gameObject.SetActive (true);
				weaponMainL.sprite = ctData.dataItem.avatar;
			} else {
				hatSymbol.gameObject.SetActive (false);
				weaponMainR.gameObject.SetActive (true);
				weaponMainR.sprite = ctData.dataItem.avatar;
			}
		}
	}

	public void Home() {
		UnityEngine.SceneManagement.SceneManager.LoadScene ("StartScene");
	}
}
