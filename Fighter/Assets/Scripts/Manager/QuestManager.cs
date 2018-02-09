using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

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

		int curDay = GetDay ();

		if (SaveManager.instance.state.isFirstPlay || SaveManager.instance.state.oldDay != curDay) {
			lstStoreQuest = ShuffleQuest (questList.Count, amountShuffle);

			SetDay (curDay);

			if (SaveManager.instance.state.isFirstPlay) {
				SaveManager.instance.state.isFirstPlay = false;
				SaveManager.instance.Save ();
			}
		}

		return lstStoreQuest;
	}
		
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
			questShuffle.Add (questList[PickNumber (dailyIndexLst)]);

		for (int i = 0; i < questShuffle.Count; i++)
			Debug.Log (questShuffle [i].idQuest);

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

	private string GetRemainingTime(double x) {
		TimeSpan tempB = TimeSpan.FromMilliseconds(x);
		string Timeformat = string.Format("{0:D2}:{1:D2}:{2:D2}", tempB.Hours, tempB.Minutes, tempB.Seconds);
		return Timeformat;
	}

	private int GetDay() {
		return DateTime.Now.Day;
	}

	private void SetDay(int day) {
		SaveManager.instance.state.oldDay = day;
		SaveManager.instance.Save ();
	}

	private void ReadData(Image iconQuest, Text contentQuest, Text doing, Text rewardGold, Text rewardExp, DataQuests quest) {
		iconQuest.sprite = quest.icon;
		contentQuest.text = quest.content;
		doing.text = quest.doing + " / " + quest.requirement;
		rewardExp.text = quest.rewardExp.ToString ();
		rewardGold.text = quest.rewardGold.ToString ();
	}

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
}
