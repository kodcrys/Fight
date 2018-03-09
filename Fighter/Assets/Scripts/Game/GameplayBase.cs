using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayBase : MonoBehaviour {

	public static GameplayBase instance;

	public bool isAtk;

	public bool firstAtk;

	public bool lastAtk;

	public bool atkDone;

	public void Start(){
		instance = this;
	}
}
