using UnityEngine;
using System.Collections;

public class SpawnerPipe : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (Spawner ());
	}
	
	IEnumerator Spawner()
	{
		int numberPipe = Mathf.RoundToInt(BirdController.instance.score / 10);

		yield return new WaitForSeconds (1.7f);

		int randomPipe = Random.Range (0, numberPipe);
	
		Vector3 temp = Vector3.zero;
		temp.y = Random.Range (-PoolManager.Intance.listRandomPos[randomPipe], PoolManager.Intance.listRandomPos[randomPipe]);
		Debug.Log (PoolManager.Intance.listRandomPos [randomPipe]);

		PoolManager.Intance.lstPool [randomPipe].getindex ();
		PoolManager.Intance.lstPool [randomPipe].GetPoolObject ().transform.position = transform.position + temp;
		PoolManager.Intance.lstPool [randomPipe].GetPoolObject ().transform.rotation = transform.rotation;
		PoolManager.Intance.lstPool [randomPipe].GetPoolObject ().SetActive (true);

		StartCoroutine (Spawner ());
	}
}
