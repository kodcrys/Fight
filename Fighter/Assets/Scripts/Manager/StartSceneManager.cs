using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class StartSceneManager : MonoBehaviour {

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

	List<DataQuests> lstQuest = new List<DataQuests> ();

	// part reward login;
	[Header("Daily reward")]
	[SerializeField]
	List<DailyReward> btnsClaimRewardDaily;

	// part library
	[Header("Scroll view")]
	[SerializeField]
	GameObject libraryObj;
	[SerializeField]
	GameObject scrollSkin;
	[SerializeField]
	GameObject scrollEquipment;

	//[Header("Display value gold & diamond")]
	//[SerializeField]
	//UnityEngine.UI.Text 

	void Awake() {
		DisplayClaimRewardDaily ();
	}

	// Update is called once per frame
	void Update () {

		// get list form questManager
		lstQuest = QuestManager.Intance.CalculateTimeRefreshQuest (startTime, endTime, countDownRefresh, lstQuest);

		if (dailyQuestObject.activeSelf) {
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
		dailyQuestObject.SetActive (true);
	}

	// test
	public void AutoDone() {
		QuestManager.Intance.AutoDone (lstQuest);
	}

	// login reward when click button showDaily
	public void DisplayClaimRewardDaily () {
		for (int i = 0; i < btnsClaimRewardDaily.Count; i++) {
			if (btnsClaimRewardDaily [i].dayOfweek < QuestManager.Intance.GetDate ()) {
				btnsClaimRewardDaily [i].claimBtn.interactable = false;
				btnsClaimRewardDaily [i].claimedSymbol.SetActive (true);
			}
			if (btnsClaimRewardDaily [i].dayOfweek > QuestManager.Intance.GetDate ()) {
				btnsClaimRewardDaily [i].claimBtn.interactable = false;
				btnsClaimRewardDaily [i].claimedSymbol.SetActive (false);
			}
			if (btnsClaimRewardDaily [i].dayOfweek == QuestManager.Intance.GetDate ()) {
				if (SaveManager.instance.state.isClaimedDailyReward == i) {
					btnsClaimRewardDaily [i].claimBtn.interactable = false;
					btnsClaimRewardDaily [i].claimedSymbol.SetActive (true);
				} else {
					btnsClaimRewardDaily [i].claimBtn.interactable = true;
					btnsClaimRewardDaily [i].claimedSymbol.SetActive (false);
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
}
