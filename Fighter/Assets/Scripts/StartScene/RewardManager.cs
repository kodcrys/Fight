﻿using UnityEngine;

public class RewardManager : MonoBehaviour {

	[HideInInspector]
	public bool isRewardCharacter, isRewardEquipment, isRewardGold, isRewardExp, isRewardDiamond, isRewardCharacterWeek;

	[HideInInspector]
	public bool isX1;

	[Header("UI Button Handle")]
	[SerializeField]
	GameObject btnBuyGold;
	[SerializeField]
	GameObject btnBuyDiamond;
	[SerializeField]
	GameObject btnBuyGoldX10;
	[SerializeField]
	GameObject btnBuyDiamondX10;
	[SerializeField]
	UnityEngine.UI.Button closeRewardX10;

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

	[Header("Reward Object X10")]
	[HideInInspector]
	public bool isX10Gold;
	[SerializeField]
	GameObject characterX10;
	[SerializeField]
	GameObject equipmentX10;

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

	[Header("Effect Glow")]
	[SerializeField]
	UIAnimations [] anisEffLightGlow;

	public static RewardManager instance;

	// Use this for initialization
	void Awake () {
		if (instance == null)
			instance = this;
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
		isX1 = true;
		panelOfCv_X1Reward.SetActive (true);
		panelOfCv_X10Reward.SetActive (false);

		//lightX1.SetActive (true);

		// open panel ani reward
		panel_Reward.SetActive (true);

		// gatchaX1
		charEqManager.GatchaCharacter ();

		RewardHandle (isShopGold);
	}

	public void OpenRewardX10 (bool isShopGold) {

		/*for (int i = 0; i < anisEffLightGlow.Length; i++)
			anisEffLightGlow [i].isRunEffX10Ani = true;*/

		closeRewardX10.enabled = false;

		isX1 = false;
		panelOfCv_X1Reward.SetActive (false);
		panelOfCv_X10Reward.SetActive (true);

		//lightX1.SetActive (false);

		// open panel ani reward
		panel_Reward.SetActive (true);

		charEqManager.GatchaX10Character ();

		RewardHandleX10 (isShopGold);
	}

	public void OpenRewardInRewardScene(bool isShopGold) {
		CloseReward();
		CloseRewardX10 ();
		OpenReward (isShopGold);
	}

	public void OpenRewardX10InReawardScene(bool isShopGold) {
		CloseReward();
		CloseRewardX10 ();
		OpenRewardX10 (isShopGold);
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

	void RewardHandleX10(bool isShopGold) {
		if (isShopGold) {
			
			isX10Gold = true;

			// change btn buy gold active if isShopGold = false
			btnBuyGoldX10.SetActive (false);
			btnBuyDiamondX10.SetActive (false);

			// set active light = false in shop scene
			lightBuyChar.SetActive (false);
			lightBuyEquipment.SetActive (false);

			// Set status type reward object 
			characterX10.SetActive (true);
			equipmentX10.SetActive (false);
			rewardExp.SetActive (false);
			rewardGold.SetActive (false);
			rewardDiamond.SetActive (false);
		} else {

			isX10Gold = false;

			// change btn buy diamond active if isShopGold = false
			btnBuyGoldX10.SetActive (false);
			btnBuyDiamondX10.SetActive (false);

			// set active light = false in shop scene
			lightBuyChar.SetActive (false);
			lightBuyEquipment.SetActive (false);

			// Set status type reward object 
			characterX10.SetActive (false);
			equipmentX10.SetActive (true);
			rewardExp.SetActive (false);
			rewardGold.SetActive (false);
			rewardDiamond.SetActive (false);
		}
	}

	public void ShowBtnX10EndAni() {
		if (isX10Gold) {
			btnBuyGoldX10.SetActive (true);
			btnBuyDiamondX10.SetActive (false);
		} else {
			btnBuyGoldX10.SetActive (false);
			btnBuyDiamondX10.SetActive (true);
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

		for (int i = 0; i < anisEffLightGlow.Length; i++) {
			anisEffLightGlow [i].transform.position = Vector3.zero;
			anisEffLightGlow [i].transform.localScale = new Vector3 (4, 4, 1);
			anisEffLightGlow [i].gameObject.SetActive (false);
			anisEffLightGlow [i].EffectGatchaX10ScaleOff ();
		}

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
