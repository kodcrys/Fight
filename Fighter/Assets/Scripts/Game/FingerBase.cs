using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerBase : MonoBehaviour {

	public enum FingerState {none, Idel, Atk, Doing, Death}

	public FingerState fingerAction = FingerState.none;

	[Header("GameObject")]
	public GameObject finger, fingerAtk, fingerDown;

	[Header("Enemy Left")]
	public FingerLeftControl enemyLeft;

	[Header("Enemy Right")]
	public FingerRightControl enemyRight;

	[Header("Finger Status")]
	public int health, defend, atk;

	[Header("Status Animation")]
	public bool fingerRight, fingerLeft;

	public int changeScale = 0;

	public float speedScale;

	public float scale1, scale2;

	public float rot1, rot2;

	public float pos1, pos2;

	public static bool changeAnim;

	public bool firstAtk, lastAtk, doingSomething, isAtk;

	public bool touch;

	public float time, timeInter;

	public UnityEngine.UI.Text atkText;


	public virtual void DoIdel(){
		
	}

	public virtual void DoAtk(){

	}

	public virtual void DoingAtk(){

	}

	public virtual void Dead(){

	}
}
