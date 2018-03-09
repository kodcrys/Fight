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
	GameObject panelOfCv_X1Reward;
	[SerializeField]
	GameObject panelOfCv_X10Reward;
	[SerializeField]
	GameObject canvas_Reward;
	[SerializeField]
	GameObject lightBuyChar;
	[SerializeField]
	GameObject lightBuyEquipment;
	[SerializeField]
	GameObject lightReward;
	[SerializeField]
	GameObject rewardObj;

	[Header("Character Equipment Manager")]
	[SerializeField]
	CharacterEquipmentManager charEqManager;

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

	public void OpenReward(bool isShopGold) {
		panelOfCv_X1Reward.SetActive (true);
		panelOfCv_X10Reward.SetActive (false);

		// open panel ani reward
		panel_Reward.SetActive (true);

		// gatchaX1
		charEqManager.GatchaCharacter ();

		RewardHandle (isShopGold);
	}

	public void OpenRewardX10 (bool isShopGold) {
		panelOfCv_X1Reward.SetActive (false);
		panelOfCv_X10Reward.SetActive (true);

		// open panel ani reward
		panel_Reward.SetActive (true);

		charEqManager.GatchaX10Character ();

		RewardHandle (isShopGold);
	}

	public void OpenRewardInRewardScene(bool isShopGold) {
		CloseReward();

		OpenReward (isShopGold);
	}

	void RewardHandle(bool isShopGold) {
		if (isShopGold) {
			// change btn buy gold active if isShopGold = false
			btnBuyGold.SetActive (true);
			btnBuyDiamond.SetActive (false);

			// set active light = false in shop scene
			lightBuyChar.SetActive (false);
			lightBuyEquipment.SetActive (false);

			// Set status type reward object 
			character.SetActive (true);
			equipment.SetActive (false);
			rewardExp.SetActive (false);
			rewardGold.SetActive (false);
			rewardDiamond.SetActive (false);
		} else {
			// change btn buy diamond active if isShopGold = false
			btnBuyGold.SetActive (false);
			btnBuyDiamond.SetActive (true);

			// set active light = false in shop scene
			lightBuyChar.SetActive (false);
			lightBuyEquipment.SetActive (false);

			// Set status type reward object 
			character.SetActive (false);
			equipment.SetActive (true);
			rewardExp.SetActive (false);
			rewardGold.SetActive (false);
			rewardDiamond.SetActive (false);
		}
	}

	public void CloseReward() {
		// close 2 panel ani reward and reward
		panel_Reward.SetActive (false);
		canvas_Reward.SetActive (false);

		// open 2 light in shop scene
		lightBuyChar.SetActive (true);
		lightBuyEquipment.SetActive (true);

		// close light in scene reward and set origin scale
		lightReward.transform.localScale = new Vector3 (2, 2, 1);
		lightReward.SetActive (false);

		// reopen reward object
		rewardObj.SetActive (true);
	}

	public void CloseRewardX10() {

		charEqManager.OriginGatchaX10 ();

		// close 2 panel ani reward and reward
		panel_Reward.SetActive (false);
		canvas_Reward.SetActive (false);

		// open 2 light in shop scene
		lightBuyChar.SetActive (true);
		lightBuyEquipment.SetActive (true);

		// close light in scene reward and set origin scale
		lightReward.transform.localScale = new Vector3 (2, 2, 1);
		lightReward.SetActive (false);

		// reopen reward object
		rewardObj.SetActive (true);
	}
}
