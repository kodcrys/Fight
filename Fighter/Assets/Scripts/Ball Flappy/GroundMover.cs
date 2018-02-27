using UnityEngine;
using System.Collections;

public class GroundMover : MonoBehaviour {
	void FixedUpdate() {
		transform.position = transform.position + Vector3.right  * Time.deltaTime * 0.00001f;
	}
}
