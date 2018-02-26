using UnityEngine;

[CreateAssetMenu(fileName = "New Reward Daily", menuName = "Data/DailyReward")]
public class DataRewardsDaily : ScriptableObject {
	public enum TypeReward {none, diamond, gold, exp, cardRandomCharacter};
	public TypeReward typeReward;
	public Sprite iconReward;
	public int reward;
}
