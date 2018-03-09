using UnityEngine;

public class CharacterEquipmentManager : MonoBehaviour {

	[Header("Define typr of character")]
	[SerializeField]
	bool isCharacterObject;
	[SerializeField]
	bool isCharacterUI;

	[Header("Name of character")]
	[SerializeField]
	UnityEngine.UI.Text nameOfCharacter;

	[Header("Hat of character")]
	[SerializeField]
	UnityEngine.UI.Image hatCharImg;
	[SerializeField]
	SpriteRenderer hatCharSRChar1;
	[SerializeField]
	SpriteRenderer hatCharSRChar2;

	[Header("Data character")]
	[SerializeField]
	DataCharacter [] dataCharacter;

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
}
