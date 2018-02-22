using UnityEngine;
using System.Collections;

public class SpawnerPipe : MonoBehaviour {

	[SerializeField]
	private GameObject pipeHolder;

	// Use this for initialization
	void Start () {
		StartCoroutine (Spawner ());
	}
	
	IEnumerator Spawner()
	{
		yield return new WaitForSeconds (1.5f);
		Vector3 temp = pipeHolder.transform.position;
		temp.y = Random.Range (-1.5f, 1.5f);
		Instantiate (pipeHolder, temp, pipeHolder.transform.rotation);
		Debug.Log (temp);
		//Instantiate (pipeHolder, temp, Quaternion.identity);
		StartCoroutine (Spawner ());
	}
}
