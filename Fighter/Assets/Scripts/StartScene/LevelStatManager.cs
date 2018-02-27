
using UnityEngine;

public class LevelStatManager : MonoBehaviour {

	[SerializeField]
	Stat expBar;

	[SerializeField]
	DataLevelStat dataLevelStat;

	[SerializeField]
	UnityEngine.UI.Text levelDisplay;

	public void Start() {
		expBar.Initialze();
		expBar.bar.isChange = true;
		UpdateExp();
	}

	public void UpdateExp () {
		expBar.MaxVal = dataLevelStat.expLevelUp[dataLevelStat.level];
		expBar.CurrentVal = SaveManager.instance.state.CurExp;

		levelDisplay.text = "Lv. " + (dataLevelStat.level + 1).ToString ();
	}

	public void IncreaseExp(int expIncrease) {
		SaveManager.instance.state.CurExp += expIncrease;
		SaveManager.instance.Save ();


	}
}
