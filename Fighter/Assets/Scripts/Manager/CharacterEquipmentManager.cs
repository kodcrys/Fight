using UnityEngine;

public class CharacterEquipmentManager : MonoBehaviour {

	[Header("Define typr of character")]
	[SerializeField]
	bool isCharacterObject;
	[SerializeField]
	bool isCharacterUI;

	[Header("Name of X1 character")]
	[SerializeField]
	UnityEngine.UI.Text nameOfCharacter;

	[Header("Name of X10 character")]
	[SerializeField]
	UnityEngine.UI.Text[] nameOfX10Character;

	[Header("Hat of X1 character")]
	[SerializeField]
	UnityEngine.UI.Image hatCharImg;
	[SerializeField]
	SpriteRenderer hatCharSRChar1;
	[SerializeField]
	SpriteRenderer hatCharSRChar2;

	[Header("Hat of X10 character")]
	[SerializeField]
	UnityEngine.UI.Image[] hatX10CharImg;
	[SerializeField]
	UIAnimations gatchaX10;

	[Header("Data character")]
	[SerializeField]
	DataCharacter [] dataCharacter;

	[Header("Name of X1 equipment")]
	[SerializeField]
	UnityEngine.UI.Text nameOfEquipment;

	[Header("Name of X10 equipment")]
	[SerializeField]
	UnityEngine.UI.Text[] nameOfX10Equipment;

	[Header("X1 equipment")]
	[SerializeField]
	UnityEngine.UI.Image equipmentHatImg;
	[SerializeField]
	UnityEngine.UI.Image equipmentWpImg;
	[SerializeField]
	UnityEngine.UI.Image equipmentAmorImg;

	[Header("X10 equipment")]
	[SerializeField]
	UnityEngine.UI.Image[] X10EquipmentHatImg;
	[SerializeField]
	UnityEngine.UI.Image[] X10EquipmentWpImg;
	[SerializeField]
	UnityEngine.UI.Image[] X10EquipmentAmorImg;
	[SerializeField]
	UIAnimations gatchaX10Equip;


	[Header("Data Equipment")]
	[SerializeField]
	DataItems [] dataEquipment;

	[Header("Origin Position")]
	[SerializeField]
	Transform originPos;

	// Use this for initialization
	void Start () {
		
	}

	public void GatchaCharacter() {
		int indexChar = Random.Range (0, dataCharacter.Length);
		DataCharacter data = dataCharacter [indexChar];

		hatCharImg.transform.parent.parent.GetComponent<CointainData> ().dataChar = data;
		if (isCharacterUI) {
			nameOfCharacter.text = data.name;
			hatCharImg.sprite = data.equipmentOfChar;
		}
	}

	public void GatchaX10Character() {
		int[] indexChar = new int[10];
		for (int i = 0; i < indexChar.Length; i++)
			indexChar [i] = Random.Range (0, dataCharacter.Length);

		for (int i = 0; i < indexChar.Length; i++) {
			hatX10CharImg [i].transform.parent.parent.GetComponent<CointainData> ().dataChar = dataCharacter [indexChar [i]];

			hatX10CharImg [i].sprite = dataCharacter [indexChar [i]].equipmentOfChar;
			nameOfX10Character [i].text = dataCharacter [indexChar [i]].name;
		}
	}

	public void GatchaEquipment() {
		int indexEquipment = Random.Range (0, dataEquipment.Length);
		DataItems data = dataEquipment [indexEquipment];

		equipmentHatImg.transform.parent.GetComponent<CointainData> ().dataItem = data;

		nameOfEquipment.text = data.name;

		if (data.typeItem == TypeObject.hat) {
			equipmentHatImg.sprite = data.avatar;

			equipmentHatImg.gameObject.SetActive (true);
			equipmentAmorImg.gameObject.SetActive (false);
			equipmentWpImg.gameObject.SetActive (false);
		}
		else if (data.typeItem == TypeObject.tshirt) {
			equipmentAmorImg.sprite = data.avatar;
			equipmentHatImg.gameObject.SetActive (false);
			equipmentAmorImg.gameObject.SetActive (true);
			equipmentWpImg.gameObject.SetActive (false);
		}
		else if (data.typeItem == TypeObject.weapon) {
			equipmentWpImg.sprite = data.avatar;
			equipmentHatImg.gameObject.SetActive (false);
			equipmentAmorImg.gameObject.SetActive (false);
			equipmentWpImg.gameObject.SetActive (true);
		}
	}

	public void GatchaX10Equipment() {
		int[] indexEquip = new int[10];
		for (int i = 0; i < indexEquip.Length; i++)
			indexEquip [i] = Random.Range (0, dataEquipment.Length);

		for (int i = 0; i < 10; i++) {

			DataItems data = dataEquipment [indexEquip [i]];
		
			X10EquipmentHatImg[i].transform.parent.GetComponent<CointainData> ().dataItem = data;

			nameOfX10Equipment [i].text = data.name;

			if (data.typeItem == TypeObject.hat) {
				X10EquipmentHatImg [i].sprite = dataEquipment [indexEquip [i]].avatar;

				X10EquipmentHatImg [i].gameObject.SetActive (true);
				X10EquipmentAmorImg [i].gameObject.SetActive (false);
				X10EquipmentWpImg [i].gameObject.SetActive (false);
			}
			else if (data.typeItem == TypeObject.tshirt) {
				X10EquipmentAmorImg [i].sprite = dataEquipment [indexEquip [i]].avatar;

				X10EquipmentHatImg [i].gameObject.SetActive (false);
				X10EquipmentAmorImg [i].gameObject.SetActive (true);
				X10EquipmentWpImg [i].gameObject.SetActive (false);
			}
			else if (data.typeItem == TypeObject.weapon) {
				X10EquipmentWpImg [i].sprite = dataEquipment [indexEquip [i]].avatar;

				X10EquipmentHatImg [i].gameObject.SetActive (false);
				X10EquipmentAmorImg [i].gameObject.SetActive (false);
				X10EquipmentWpImg [i].gameObject.SetActive (true);
			}
		}
	}

	public void OriginGatchaX10Char() {
		for (int i = 0; i < 10; i++) {
			Transform gOB = hatX10CharImg [i].transform.parent.parent.transform;
			gOB.position = originPos.position;
			gOB.localScale = new Vector3 (1, 1, 1);
			gOB.gameObject.SetActive (false);
		}
		gatchaX10.indexGatcha = 0;
		gatchaX10Equip.indexGatcha = 0;
	}

	public void OriginGatchaX10Equipment() {
		for (int i = 0; i < 10; i++) {
			Transform goB = X10EquipmentHatImg [i].transform.parent.transform;
			goB.position = originPos.position;
			goB.localScale = new Vector3 (1, 1, 1);
			goB.gameObject.SetActive (false);
		}
		gatchaX10.indexGatcha = 0;
		gatchaX10Equip.indexGatcha = 0;
	}
}
