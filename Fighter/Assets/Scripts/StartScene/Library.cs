using UnityEngine;
using UnityEngine.EventSystems;

public class Library : MonoBehaviour {
	
	public void ShowInfo(Transform frameInfo) {
		var go = EventSystem.current.currentSelectedGameObject;
		Debug.Log (go);
		/*frameInfo.gameObject.SetActive (true);

		foreach (Transform frame in frameInfo) {
			frame.GetChild (0).GetComponent<UnityEngine.UI.Text> ().text = "Name: ";	
		}*/
	}
}
