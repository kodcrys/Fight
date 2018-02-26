using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class StartSceneManager : MonoBehaviour {

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

	[Header("Daily reward")]
	[SerializeField]
	List<DailyReward> btnsClaimRewardDaily;

	void Awake() {
		DisplayClaimRewardDaily ();
	}

	// Update is called once per frame
	void Update () {
		
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

	public void ShowDailyQuest() {
		dailyQuestObject.SetActive (true);
	}

	public void AutoDone() {
		QuestManager.Intance.AutoDone (lstQuest);
	}

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
}
