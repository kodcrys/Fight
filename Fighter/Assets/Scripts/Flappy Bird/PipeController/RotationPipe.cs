using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPipe : MonoBehaviour {

	// Use this for initialization
	void OnEnable () 
	{
		int rotation;
		rotation = Random.Range (-25, 0);
		transform.rotation = Quaternion.Euler(new Vector3 (0, 0, rotation));
	}

	void OnTriggerEnter2D (Collider2D target)
	{
		Debug.Log (target.gameObject.tag);
		if (target.gameObject.tag == "Destroy") 
		{
			transform.gameObject.SetActive (false);
		}
	}

}
