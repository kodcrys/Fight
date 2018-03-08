using UnityEngine;

public class RewardManager : MonoBehaviour {

	[HideInInspector]
	public bool isRewardCharacter, isRewardEquipment, isRewardGold, isRewardExp, isRewardDiamond, isRewardCharacterWeek;

	[HideInInspector]
	public bool isX1, isX10;

	[Header("UI Button Handle")]
	[SerializeField]
	GameObject btnBuyGold;
	[SerializeField]
	GameObject btnBuyDiamond;

	[Header("Reward Object")]
	[SerializeField]
	GameObject character;
	[SerializeField]
	GameObject equipment;
	[SerializeField]
	GameObject rewardGold;
	[SerializeField]
	GameObject rewardDiamond;
	[SerializeField]
	GameObject rewardExp;

	[Header("Reward Scene")]
	[SerializeField]
	GameObject panel_Reward;
	[SerializeField]
	GameObject canvas_Reward;
	[SerializeField]
	GameObject lightBuyChar;
	[SerializeField]
	GameObject lightBuyEquipment;

	// Use this for initialization
	void OnEnable () {
		
	}

	public void ShowBtn() {
		if (isRewardCharacter) {
			btnBuyGold.SetActive (true);
			btnBuyDiamond.SetActive (false);
		} else if (isRewardEquipment) {
			btnBuyGold.SetActive (false);
			btnBuyDiamond.SetActive (true);
		} else {
			btnBuyGold.SetActive (false);
			btnBuyDiamond.SetActive (false);
		}
	}

	void TypeOfReward () {
		
	}

	public void OpenReward(bool isShopGold) {
		panel_Reward.SetActive (true);
		canvas_Reward.SetActive (true);
		if (isShopGold) {
			btnBuyGold.SetActive (true);
			btnBuyDiamond.SetActive (false);
			lightBuyChar.SetActive (false);
			lightBuyEquipment.SetActive (false);
		} else {
			btnBuyGold.SetActive (false);
			btnBuyDiamond.SetActive (true);
			lightBuyChar.SetActive (false);
			lightBuyEquipment.SetActive (false);
		}
	}

	public void CloseReward() {
		panel_Reward.SetActive (false);
		canvas_Reward.SetActive (false);
		lightBuyChar.SetActive (true);
		lightBuyEquipment.SetActive (true);
	}
}
