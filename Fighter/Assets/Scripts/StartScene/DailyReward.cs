using UnityEngine;
using System;

public class DailyReward : MonoBehaviour {

	[SerializeField]
	int idDaily;

	[SerializeField]
	DataRewardsDaily data;

	[SerializeField]
	UnityEngine.UI.Image icon;

	public UnityEngine.UI.Button claimBtn;

	public DayOfWeek dayOfweek;

	public GameObject claimedSymbol;

	// Use this for initialization
	void Start () {
		icon.sprite = data.iconReward;
	}

	public void ClaimReward() {
		switch (data.typeReward) {
		case DataRewardsDaily.TypeReward.gold:
			SaveManager.instance.state.TotalGold += data.reward;
			SaveManager.instance.Save ();
			break;
		case DataRewardsDaily.TypeReward.exp:
			SaveManager.instance.state.CurExp += data.reward;
			SaveManager.instance.Save ();
			break;
		case DataRewardsDaily.TypeReward.diamond:
			SaveManager.instance.state.TotalDiamond += data.reward;
			SaveManager.instance.Save ();
			break;
		case DataRewardsDaily.TypeReward.cardRandomCharacter:
			SaveManager.instance.state.TotalCardChar += data.reward;
			SaveManager.instance.Save ();
			break;
		}

		SaveManager.instance.state.isClaimedDailyReward = idDaily;
		SaveManager.instance.Save ();

		transform.GetChild (1).gameObject.SetActive (true);

		claimBtn.interactable = false;
	}
}
