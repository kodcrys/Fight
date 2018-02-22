using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class QuestManager : MonoBehaviour {

	public static QuestManager Intance;

	[Header("Amount shuffle")]
	[SerializeField]
	int amountShuffle;

	[Header("List daily quest random")]
	List<int> dailyIndexLst = new List<int> ();

	[Header("Data of quest")]
	[SerializeField]
	private List<DataQuests> questList = new List<DataQuests>();

	private List<DataQuests> questTempList = new List<DataQuests>();

	[Header("")]
	[SerializeField]
	Button[] BtnsClaimReward;

	void Awake() {
		if (Intance == null)
			Intance = this;
	}

	// Calculate countDown refresh quest
	public List<DataQuests> CalculateTimeRefreshQuest(string startTime, string endTime, Text timeTxt, List<DataQuests> lstStoreQuest){
		TimeSpan start = TimeSpan.Parse(startTime);
		TimeSpan end = TimeSpan.Parse(endTime);

		TimeSpan curr = TimeSpan.Parse (System.DateTime.Now.ToString ("HH:mm:ss"));
		TimeSpan result = new TimeSpan ();

		if (curr >= start && curr <= end) {
			result = end.Subtract(curr);
			timeTxt.text = "Restart in: " + GetRemainingTime (result.TotalMilliseconds);
		}

		if (SaveManager.instance.state.isFirstPlay || isSameDay () == false) {
			
			lstStoreQuest = RefeshQuestNewDay (lstStoreQuest);

			SaveQuest (lstStoreQuest);

			SetDay (GetDay ());

			if (SaveManager.instance.state.isFirstPlay) {
				SaveManager.instance.state.isFirstPlay = false;
				SaveManager.instance.Save ();
			}

		} else {
			lstStoreQuest = LoadQuest ();
		}

		return lstStoreQuest;
	}

	// Refresh quest when time change time of new day
	private List<DataQuests> RefeshQuestNewDay(List<DataQuests> lstStoreQuest) {
		lstStoreQuest = ShuffleQuest (questList.Count, amountShuffle);
		return lstStoreQuest;
	}

	// 
	private List<DataQuests> ShuffleQuest(int questMax, int amountShuffle) {

		// Create list store daily quest random
		List<DataQuests> questShuffle = new List<DataQuests> ();

		// Convert questList to questTempList 
		for (var i = 0; i < questList.Count; i++)
			questTempList.Add (questList [i]);

		// Generate value of list index
		for (var i = 0; i < questList.Count; i++)
			dailyIndexLst.Add(i);

		// Generate value daily quest of questShuffle
		for (var i = 0; i < amountShuffle; i++)
			questShuffle.Add (questList [PickNumber (dailyIndexLst)]);

		return questShuffle;
	}

	// Get value not repeat
	private int PickNumber(List<int> lst) {
		if (lst.Count <= 0)
			return -1; // No numbers left;
		var index = UnityEngine.Random.Range(0, lst.Count);
		var value = lst[index];
		lst.RemoveAt(index);
		return value;
	}

	// Conver time to string 
	private string GetRemainingTime(double x) {
		TimeSpan tempB = TimeSpan.FromMilliseconds(x);
		string Timeformat = string.Format("{0:D2}:{1:D2}:{2:D2}", tempB.Hours, tempB.Minutes, tempB.Seconds);
		return Timeformat;
	}

	// Check day in save is same current day
	public bool isSameDay() {
		int curDay = GetDay ();
		if (SaveManager.instance.state.oldDay != curDay)
			return false;
		else
			return true;
	}

	// Get current day
	private int GetDay() {
		return DateTime.Now.Day;
	}

	// Set day with identifine day
	private void SetDay(int day) {
		SaveManager.instance.state.oldDay = day;
		SaveManager.instance.Save ();
	}

	// Read data in ScriptableObject
	private void ReadData(Image iconQuest, Text contentQuest, Text doing, Text rewardGold, Text rewardExp, DataQuests quest) {
		iconQuest.sprite = quest.icon;
		contentQuest.text = quest.content;
		doing.text = quest.doing + " / " + quest.requirement;
		rewardExp.text = quest.rewardExp.ToString ();
		rewardGold.text = quest.rewardGold.ToString ();
	}

	// Load data in ScriptableObject
	public void LoadData(Transform lstTransform, List<DataQuests> lstStoreQuest) {

		int indexQuest = 0;

		foreach (Transform t in lstTransform) {
			Image iconQuest = t.GetChild (0).GetComponent<Image> ();
			Text contentQuest = t.GetChild (1).GetComponent<Text> ();
			Text doing = t.GetChild (2).GetComponent<Text> ();
			Text rewardGold = t.GetChild (3).GetComponent<Text> ();
			Text rewardExp = t.GetChild (4).GetComponent<Text> ();

			ReadData (iconQuest, contentQuest, doing, rewardGold, rewardExp, lstStoreQuest [indexQuest]);

			indexQuest++;
		}
	}

	private void SaveQuest(List<DataQuests> lstQuest){
		for (int i = 0; i < lstQuest.Count; i++)
			PlayerPrefs.SetInt ("quest" + i, lstQuest [i].idQuest);
	}

	private List<DataQuests> LoadQuest() {
		List<DataQuests> lstQuests = new List<DataQuests> ();
		int[] idQuest = new int[3];
		for (int i = 0; i < 3; i++)
			idQuest [i] = PlayerPrefs.GetInt ("quest" + i, 0);

		for (int i = 0; i < 3; i++)
			lstQuests.Add (questList [idQuest [i]]);

		return lstQuests;
	}

	public void DoneQuest(List<DataQuests> lstStoreQuest) {
		for (int i = 0; i < lstStoreQuest.Count; i++) {
			if (lstStoreQuest [i].doing == lstStoreQuest [i].requirement) {
				BtnsClaimReward [i].enabled = true;
				BtnsClaimReward [i].onClick.AddListener (delegate {
					ClaimReward (lstStoreQuest [i]);
				});
			}
			else
				BtnsClaimReward [i].enabled = false;
		}
	}

	void ClaimReward(DataQuests quest) {
		SaveManager.instance.state.TotalGold += quest.rewardGold;
		SaveManager.instance.state.CurExp += quest.rewardExp;
	}

	public void UpdateProgressDaily(List<DataQuests> lstStoreQuest) {
		
	}
}
