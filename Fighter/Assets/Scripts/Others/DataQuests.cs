using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]
public class DataQuests : ScriptableObject {
	new public int idQuest = 0;
	public string content = "";
	public int requirement = 0;
	public int doing = 0;
}
