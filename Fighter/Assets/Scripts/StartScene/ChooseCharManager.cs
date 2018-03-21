using System.Collections;
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

	bool isTurnPlayer1 = false;

	GameObject objFollow;

	[HideInInspector]
	public bool isPlayAI;

	void Awake() {
		if (instance == null)
			instance = this;
	}

	// Use this for initialization
	void Start () {
		isTurnPlayer1 = false;
		chooseSymbol.SetActive (false);
	}

	void Update() {
		if(objFollow != null)
			chooseSymbol.transform.position = objFollow.transform.position;
	}

	bool isShowTypeChar = true;
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

	public void ChooseChar() {
		GameObject gob = EventSystem.current.currentSelectedGameObject;
		//chooseSymbol.transform.position = gob.transform.position;
		objFollow = gob;
		if (isShowTypeChar)
			chooseSymbol.transform.SetParent (maskFrameChooseChar);
		else
			chooseSymbol.transform.SetParent (maskFrameChooseEquip);
		//chooseSymbol.transform.SetParent (gob.transform);
		chooseSymbol.SetActive (true);
		CointainData ctData = gob.GetComponent<CointainData> ();
		Debug.Log (isTurnPlayer1);
		if (ctData.dataChar != null) {
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

		if (ctData.dataItem != null) {
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
				hatMainR.gameObject.SetActive (true);
				hatMainR.sprite = ctData.dataChar.equipmentOfChar;
				amorMainR.gameObject.SetActive (false);
				weaponMainR.gameObject.SetActive (false);
			}
		}
	}
}
