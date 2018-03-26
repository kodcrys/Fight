using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {
	private float maxSizeCamera = 12.5f, minSizeCamera = 11.5f;
	bool zoomIn;
	float sizeCamera;
	void Awake ()
	{
		
	}

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
			sizeCamera -= 0.008f;
		} 
		else 
		{
			sizeCamera += 0.008f;
		}
			
		transform.GetComponent<Camera> ().orthographicSize = sizeCamera;
	}
}
