using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPipe : MonoBehaviour {

	// Use this for initialization
	void OnEnable () 
	{
 		// Random the rotation of the pipe.
		transform.rotation = Quaternion.Euler(new Vector3 (0, 0, Random.Range (-25, 0)));
	}
}
