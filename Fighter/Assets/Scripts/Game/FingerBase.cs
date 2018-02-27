using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerBase : MonoBehaviour {

	[Header("GameObject")]
	public GameObject finger, fingerAtk;

	[Header("Finger Status")]
	public int health, defend, atk;

	[Header("Status Animation")]
	public int changeScale = 0;

	public float speedScale;

	public float scale1, scale2;

	public float rot1, rot2;

	public float pos1, pos2;

	public static bool changeAnim;

	public virtual void DoIdel(){
		
	}

	public virtual void DoAtk(){
		
	}

}
