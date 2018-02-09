using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (dailyQuestObject.activeSelf) {
			lstQuest = QuestManager.Intance.CalculateTimeRefreshQuest (startTime, endTime, countDownRefresh, lstQuest);
			if(lstQuest.Count > 0)
				QuestManager.Intance.LoadData (quests, lstQuest);
		}
	}
}
