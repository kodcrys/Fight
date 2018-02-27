using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour {

	public int indexPipe;
	bool movePos1, movePos2;
	public Transform pos1, pos2, pos;

	public GameObject[] coin;
	// Use this for initialization
	void Start () {
		int rand = Random.Range (1, 101);
				//Debug.Log (rand);
		if (rand % 2 == 0) {
			int randCoin = Random.Range (0, coin.Length);
			for (int i = 0; i < randCoin; i++)
				coin [i].SetActive (true);
			for (int i = randCoin; i < coin.Length; i++)
				coin [i].SetActive (false);
		} else {
			coin [0].SetActive (false);
			coin [1].SetActive (false);
			coin [2].SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
				
		if (indexPipe == 0) {
			movePos2 = false; movePos1 = false;
			transform.position = pos.position;
		}
		if (indexPipe == 1) {
						
			if (transform.position == pos1.position) {
				movePos2 = true; movePos1 = false;
			}
			if (transform.position == pos2.position) {
				movePos2 = false; movePos1 = true;
			}
								
			if (movePos2) transform.position = Vector3.MoveTowards (transform.position, pos2.position, Time.deltaTime / 3);
			else transform.position = Vector3.MoveTowards (transform.position, pos1.position, Time.deltaTime / 3);
		}
	}
}
