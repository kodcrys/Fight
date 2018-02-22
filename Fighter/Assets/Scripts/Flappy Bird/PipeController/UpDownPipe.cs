using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownPipe : MonoBehaviour {
	
	[SerializeField]
	private float maxSpeed, minSpeed, MaxRange, minRange;

	float randSpeed, randPos;
	Vector3 startPos;
	bool MoveUp;

	// Use this for initialization
	void OnEnable ()
	{
		// Random the speed for the pipe move up and down.
		randSpeed = Random.Range (minSpeed, maxSpeed);
		// Random range for the position which the pipe will move to.
		randPos = Random.Range (minRange, MaxRange);
		// Save the start position of the pipe.
		startPos = transform.position;
		// At first, the pipe will move up.
		MoveUp = true;
	}
	
	// Update is called once per frame
	void Update () {
		// If position > the range, to check the bool variable and vice versa.
		if (transform.position.y >= (startPos.y + randPos))
			MoveUp = false;
		else
			if (transform.position.y <= (startPos.y - randPos))
				MoveUp = true;

		// Move up and down the pipe.
		Vector3 temp = transform.position;
		if (MoveUp)
			temp.y += Time.deltaTime * 0.5f;
		else
			temp.y -= Time.deltaTime * 0.5f;
		transform.position = temp;
	}
}
