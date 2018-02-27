using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {
	[SerializeField]
	private float maxSizeCamera = 11f, minSizeCamera = 10f;
	bool zoomIn;
	float sizeCamera;
	// Use this for initialization
	void Start () {
		zoomIn = true;
		sizeCamera = transform.GetComponent<Camera> ().orthographicSize;
	}
	
	// Update is called once per frame
	void Update () {
		if (sizeCamera <= minSizeCamera)
			zoomIn = false;

		if (sizeCamera >= maxSizeCamera)
			zoomIn = true;
		
		if (zoomIn) 
		{
			sizeCamera -= 0.01f;
		} 
		else 
		{
			sizeCamera += 0.01f;
		}
			
		transform.GetComponent<Camera> ().orthographicSize = sizeCamera;
	}
}
