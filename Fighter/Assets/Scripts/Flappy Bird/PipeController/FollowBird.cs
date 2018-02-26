using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBird : MonoBehaviour {
	private GameObject Player;
	[SerializeField]
	float maxTime, minTime, speed;

	float randomTime;
	bool checkIdle;

	// Use this for initialization
	void OnEnable () {
		Player = GameObject.FindGameObjectWithTag ("Player");
		randomTime = Random.Range (maxTime, minTime);
		checkIdle = false;
	}
		
	// Update is called once per frame
	void Update () 
	{
		Debug.Log (transform.position.x + " " + Player.transform.position.x + " ");
		if (transform.position.x <= Player.transform.position.x) 
		{
			
			checkIdle = true;

		}

		if (checkIdle)
			randomTime -= Time.deltaTime;

		if (randomTime >= 0 && checkIdle) 
		{
			Vector3 temp = transform.position;
			temp.x += speed * Time.deltaTime;
			temp.y -= speed * Time.deltaTime * 0.3f;
			transform.position = temp;
		} 
		else
			checkIdle = false;
				
	}
}
