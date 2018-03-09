using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisiblePipe : MonoBehaviour {

	[SerializeField]
	private GameObject pipe3;
	void Update ()
	{
		if (pipe3.activeInHierarchy)
			transform.GetComponent <SpriteRenderer> ().enabled = true;
	}

	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.tag == "Destroy") 
		{
			transform.GetComponent <SpriteRenderer> ().enabled = false;
		}
	}
}
