using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

	bool zoomIn;
	float sizeCamera;
	// Use this for initialization
	void Start () {
		zoomIn = true;
		sizeCamera = transform.GetComponent<Camera> ().orthographicSize;
	}
	
	// Update is called once per frame
	void Update () {
		if (sizeCamera <= 10)
			zoomIn = false;

		if (sizeCamera >= 11)
			zoomIn = true;
		
		if (zoomIn) 
		{
			Debug.Log ("ZoomIN");
			sizeCamera -= Time.deltaTime * 10f;
		} 
		else 
		{
			Debug.Log ("ZoomOUT");
			sizeCamera += Time.deltaTime * 10f;
		}

		Debug.Log (sizeCamera);
		transform.GetComponent<Camera> ().orthographicSize = sizeCamera;
	}
}
