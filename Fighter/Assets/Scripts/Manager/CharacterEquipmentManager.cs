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

	[Header("Origin Position")]
	[SerializeField]
	Transform originPos;

	// Use this for initialization
	void Start () {
		
	}

	public void GatchaCharacter() {
		int indexChar = Random.Range (0, dataCharacter.Length);
		DataCharacter data = dataCharacter [indexChar];

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
			hatX10CharImg [i].sprite = dataCharacter [indexChar [i]].equipmentOfChar;
			nameOfX10Character [i].text = dataCharacter [indexChar [i]].name;
		}
	}

	public void OriginGatchaX10() {
		for (int i = 0; i < 10; i++) {
			Transform gOB = hatX10CharImg [i].transform.parent.parent.transform;
			gOB.position = originPos.position;
			gOB.localScale = new Vector3 (1, 1, 1);
			gOB.gameObject.SetActive (false);
		}
		gatchaX10.indexGatcha = 0;
	}
}
