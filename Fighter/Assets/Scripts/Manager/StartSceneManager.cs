﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class StartSceneManager : MonoBehaviour {

	public static StartSceneManager instance;

	// part daily Quest
	[Header("Daily Quest UI")]
	[SerializeField]
	string startTime;
	[SerializeField]
	string endTime;
	[SerializeField]
	Text countDownRefresh;
	[SerializeField]
	Transform quests;
	[SerializeField]
	GameObject dailyQuestObject;
	[SerializeField]
	UIAnimations aniOfDailyQuest;
	[SerializeField]
	BarScript progress;
	[SerializeField]
	UIAnimations aniOfRewardBonus;
	[SerializeField]
	GameObject effectRewardBonus;
	bool isShowDailyQuest;

	List<DataQuests> lstQuest = new List<DataQuests> ();

	// part reward login;
	[Header("Daily reward")]
	[SerializeField]
	List<DailyReward> btnsClaimRewardDaily;
	[SerializeField]
	GameObject dailyRewardObj;
	[SerializeField]
	GameObject EffDailyReward;
	[SerializeField]
	Transform rewardSymbol;

	// part library
	[Header("Scroll view")]
	[SerializeField]
	GameObject libraryObj;
	[SerializeField]
	GameObject scrollSkin;
	[SerializeField]
	GameObject scrollEquipment;

	[Header("Display value gold & diamond")]
	[SerializeField]
	UnityEngine.UI.Text totalGoldTxt;
	[SerializeField]
	UnityEngine.UI.Text totalDiamondTxt;

	[Header("UI Animations")]
	[SerializeField]
	UIAnimations soundUI;
	[SerializeField]
	UIAnimations musicUI;
	[SerializeField]
	UIAnimations voiceUI;
	[SerializeField]
	UIAnimations ringUI;
	[SerializeField]
	UIAnimations topBar;
	[SerializeField]
	UIAnimations botBar;
	[SerializeField]
	UIAnimations midBar;

	[Header("Fade ani")]
	[SerializeField]
	FadeAni fadeAni; 

	void Awake() {
		if (instance != null)
			instance = this;

		DisplayGold ();
		DisplayDiamond ();
		CheckSetting ();

		ChooseCharManager.instance.ReadSave ();

		// Chua lam tat app thi tra ze
		/*if (SaveManager.instance.state.isFirstOpenApp) {
			dailyRewardObj.SetActive (true);
			SaveManager.instance.state.isFirstOpenApp = false;
			SaveManager.instance.Save ();
		} else
			dailyRewardObj.SetActive (false);*/

		DisplayClaimRewardDaily ();

		if (FadeAni.isRunMapToChooseChar) {
			if(FadeAni.isRunMapToHome == false)
				ChooseCharManager.instance.AniChangeScene ();
			fadeAni.OffFade ();
			fadeAni.stateFade = FadeAni.State.Hide;
			FadeAni.isRunMapToChooseChar = false;
		}
	}

	// Update is called once per frame
	void Update () {
		// get list form questManager
		lstQuest = QuestManager.Intance.CalculateTimeRefreshQuest (startTime, endTime, countDownRefresh, lstQuest);

		if (/*dailyQuestObject.activeSelf*/aniOfDailyQuest.isRunSeqAni) {
			if (lstQuest.Count > 0 && QuestManager.Intance.isCanLoadData) {
				QuestManager.Intance.LoadData (quests, lstQuest);
				QuestManager.Intance.isCanLoadData = false;
			}

			QuestManager.Intance.DoneQuest (lstQuest);
		} else
			QuestManager.Intance.isCanLoadData = true;
	}
		
	// daily quest
	public void ShowDailyQuest() {
		if (progress.fillAmount == 1 && SaveManager.instance.state.isRewardBonus == false) {
			aniOfRewardBonus.isRunShakeAni = true;
			effectRewardBonus.SetActive (true);
		} else {
			aniOfRewardBonus.isRunShakeAni = false;
			effectRewardBonus.SetActive (false);

		}
		if (SaveManager.instance.state.isRewardBonus)
			QuestManager.Intance.ChangeSpriteClaimedRewardBonus ();
		else
			QuestManager.Intance.ChangeSpriteRewardBonus ();
		if (isShowDailyQuest) {
			botBar.isRunMoveAni = true;
			midBar.isRunScaleAni = true;
			isShowDailyQuest = !isShowDailyQuest;
			aniOfDailyQuest.isRunSeqAni = false;
			rewardSymbol.eulerAngles = new Vector3 (0, 0, 0);
		} else {
			botBar.isRunMoveAni = false;
			midBar.isRunScaleAni = false;
			isShowDailyQuest = true;
			aniOfDailyQuest.isRunSeqAni = true;
		}
	}

	// test
	public void AutoDone() {
		QuestManager.Intance.AutoDone (lstQuest);
	}

	// login reward when click button showDaily
	public void DisplayClaimRewardDaily () {
		for (int i = 0; i < btnsClaimRewardDaily.Count; i++) {
			//Button truoc ngay nhan reward
			if (btnsClaimRewardDaily [i].dayOfweek < QuestManager.Intance.GetDate ()) {
				btnsClaimRewardDaily [i].claimBtn.interactable = false;
				btnsClaimRewardDaily [i].claimedSymbol.SetActive (true);
				btnsClaimRewardDaily [i].GetComponent<Image> ().sprite = btnsClaimRewardDaily [i].GetComponent<DailyReward> ().data.iconRewardClaim;
				//EffDailyReward.SetActive (false);
			}
			// Button sau ngay nhan reward
			if (btnsClaimRewardDaily [i].dayOfweek > QuestManager.Intance.GetDate ()) {
				btnsClaimRewardDaily [i].claimBtn.interactable = false;
				btnsClaimRewardDaily [i].claimedSymbol.SetActive (false);
				btnsClaimRewardDaily [i].GetComponent<Image> ().sprite = btnsClaimRewardDaily [i].GetComponent<DailyReward> ().data.iconReward;
				//EffDailyReward.SetActive (false);
			}
			// Button ngay nhan reward
			if (btnsClaimRewardDaily [i].dayOfweek == QuestManager.Intance.GetDate ()) {
				if (SaveManager.instance.state.isClaimedDailyReward == i) {
					btnsClaimRewardDaily [i].claimBtn.interactable = false;
					btnsClaimRewardDaily [i].claimedSymbol.SetActive (true);
					btnsClaimRewardDaily [i].GetComponent<Image> ().sprite = btnsClaimRewardDaily [i].GetComponent<DailyReward> ().data.iconRewardClaim;
					//EffDailyReward.SetActive (false);
				} else {
					btnsClaimRewardDaily [i].claimBtn.interactable = true;
					btnsClaimRewardDaily [i].claimedSymbol.SetActive (false);
					btnsClaimRewardDaily [i].GetComponent<Image> ().sprite = btnsClaimRewardDaily [i].GetComponent<DailyReward> ().data.iconReward;
					EffDailyReward.transform.position = btnsClaimRewardDaily [i].transform.position;
					EffDailyReward.SetActive (true);
				}
			}
		}
	}
	#region Library
	public void OpenLibrary() {
		if (libraryObj != null)
			libraryObj.SetActive (true);
	}

	public void CloseLibrary() {
		if (libraryObj != null)
			libraryObj.SetActive (false);
	}

	public void OpenScrollSkin() {
		scrollSkin.SetActive (true);
		scrollEquipment.SetActive (false);
	}

	public void OpenScrollEquipment() {
		scrollSkin.SetActive (false);
		scrollEquipment.SetActive (true);
	}
	#endregion

	public void DisplayGold() {
		totalGoldTxt.text = SaveManager.instance.state.TotalGold.ToString ();
	}

	public void DisplayDiamond() {
		totalDiamondTxt.text = SaveManager.instance.state.TotalDiamond.ToString ();
	}

	void CheckSetting() {
		soundUI.CheckSound ();
		musicUI.CheckMusic ();
		voiceUI.CheckVoice ();
		ringUI.CheckRing ();
	}


}
