using UnityEngine;
using System.Collections;

public class SrollRect : MonoBehaviour {
	public RectTransform rectTransform;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//741
		rectTransform.anchoredPosition = new Vector3 (-8.507324f , Mathf.Clamp (rectTransform.anchoredPosition.y, -2364f, 1000f), 0);
	}
}
