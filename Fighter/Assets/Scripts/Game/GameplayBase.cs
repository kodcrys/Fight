using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayBase : MonoBehaviour {

	public static GameplayBase instance;

	public bool isAtk;

	public void Start(){
		instance = this;
	}
}
