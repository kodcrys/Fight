using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class QuestManager : MonoBehaviour {

	[Header("Amount shuffle")]
	[SerializeField]
	int amountShuffle;

	[Header("List daily quest random")]
	List<int> dailyIndexLst = new List<int> ();

	[Header("Data of quest")]
	[SerializeField]
	private List<DataQuests> questList = new List<DataQuests>();

	private List<DataQuests> questTempList = new List<DataQuests>();

	//void Start(){
	//	ShuffleQuest (questList.Count, amountShuffle);
	//}

	public List<DataQuests> ShuffleQuest(int questMax, int amountShuffle) {

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

	// Calculate countDown resfresh quest
	public void CalculateTimeCountDown(TimeSpan curr, string startTime, string endTime, Text timeTxt){
		TimeSpan start = TimeSpan.Parse(startTime);
		TimeSpan end = TimeSpan.Parse(endTime);

		TimeSpan result = new TimeSpan ();

		if (curr >= start && curr <= end) {
			result = end.Subtract(curr);
			timeTxt.text = "Restart in: " + GetRemainingTime (result.TotalMilliseconds);
		}
	}

	public string GetRemainingTime(double x) {
		TimeSpan tempB = TimeSpan.FromMilliseconds(x);
		string Timeformat = string.Format("{0:D2}:{1:D2}:{2:D2}", tempB.Hours, tempB.Minutes, tempB.Seconds);
		return Timeformat;
	}

	public void ReadData() {
		
	}
}
