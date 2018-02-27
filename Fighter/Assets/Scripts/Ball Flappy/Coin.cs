using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour 
{	
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate (new Vector3 (0, 100 * Time.deltaTime, 0));
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.name == "PlayerBird") 
		{
			gameObject.SetActive (false);
		}
	}
}
