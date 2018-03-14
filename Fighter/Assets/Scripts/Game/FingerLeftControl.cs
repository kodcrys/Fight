﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerLeftControl : FingerBase {

	[SerializeField]
	bool isUIAni;

	// Use this for initialization
	void Start () {
		touch = true;
		atk = 100;
		health = 100;
		changeColor = false;
		oneShotColor = false;
		stopTime = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (atkText != null)
			atkText.text = atk.ToString ();
		if (healthText != null)
			healthText.text = health.ToString ();

		switch (fingerAction) {
		case FingerState.none:
			fingerAction = FingerState.Idel;
			break;
		case FingerState.Idel:
			DoIdel ();
			break;
		case FingerState.Atk:
			DoAtk ();
			break;
		case FingerState.Doing:
			DoingAtk ();
			break;
		case FingerState.Win:
			Win ();
			break;
		case FingerState.Death:
			Dead ();
			break;
		}


		if (doingSomething) {
			if (!enemyRight.firstAtk && lastAtk) {
				lastAtk = false;
				fingerAction = FingerState.Atk;
			}
		}

		if (health <= 0 || enemyRight.health <= 0) {
			GameplayBase.instance.leftButton.SetActive (false);
			if (stopTime) {
				GameplayBase.instance.mainCamera.orthographicSize = 4;
				fingerAction = FingerState.Doing;
				StartCoroutine (WhoDeadWhoWin (1f));
			}
		}


		if (changeColor) {
			finger.GetComponent<SpriteRenderer> ().color = new Color32 (255, 255, 255, 255);
			fingerAtk.GetComponent<SpriteRenderer> ().color = new Color32 (255, 255, 255, 255);
			fingerDown.GetComponent<SpriteRenderer> ().color = new Color32 (255, 255, 255, 255);
			hand.GetComponent<SpriteRenderer> ().color = new Color32 (255, 255, 255, 255);
			StartCoroutine (WaitChangeColor (0.005f));
		} else {
			finger.GetComponent<SpriteRenderer> ().color = new Color32 (255, 207, 179, 255);
			fingerAtk.GetComponent<SpriteRenderer> ().color = new Color32 (255, 207, 179, 255);
			fingerDown.GetComponent<SpriteRenderer> ().color = new Color32 (255, 207, 179, 255);
			hand.GetComponent<SpriteRenderer> ().color = new Color32 (255, 207, 179, 255);
		}

	}

	public override void DoIdel(){
		if (finger != null)
			finger.SetActive (true);
		if (fingerDown != null)
			fingerDown.SetActive (false);
		if (fingerAtk != null)
			fingerAtk.SetActive (false);

		touch = true;
		isAtk = false;
		firstAtk = false;
		lastAtk = false;

		if (isUIAni == false) {
			if (!enemyRight.firstAtk) {
				if (atk < 100) {
					atk += 2;
				} else if (atk >= 100) {
					atk = 100;
				}
			}
		}

		if (!FingerBase.changeAnim) {
			if (time >= timeInter) {
				time = 0;
			} else {
				time += Time.deltaTime;
			}

			if (time >= timeInter) {
				if (changeScale == 0)
					changeScale = 1;
				else
					changeScale = 0;
			}

			if (changeScale == 0) {
				finger.transform.localScale = Vector3.MoveTowards (finger.transform.localScale, new Vector3 (finger.transform.localScale.x, scale1, finger.transform.localScale.z), Time.deltaTime * speedScale);
				finger.transform.Rotate (finger.transform.localRotation.x, finger.transform.localRotation.y, rot1);
				finger.transform.localPosition = Vector3.MoveTowards (finger.transform.localPosition, new Vector3 (finger.transform.localPosition.x + pos1, finger.transform.localPosition.y, finger.transform.localPosition.z), Time.deltaTime * speedScale);
			} else {
				finger.transform.localScale = Vector3.MoveTowards (finger.transform.localScale, new Vector3 (finger.transform.localScale.x, scale2, finger.transform.localScale.z), Time.deltaTime * speedScale);
				finger.transform.Rotate (finger.transform.localRotation.x, finger.transform.localRotation.y, rot2);
				finger.transform.localPosition = Vector3.MoveTowards (finger.transform.localPosition, new Vector3 (finger.transform.localPosition.x - pos2, finger.transform.localPosition.y, finger.transform.localPosition.z), Time.deltaTime * speedScale);
			}
		}
	}

	public override void DoAtk(){
		if (!enemyRight.firstAtk && !firstAtk) {
			firstAtk = true;
			finger.SetActive (false);
			fingerDown.SetActive (true);
			fingerAtk.SetActive (false);
			fingerAction = FingerState.Doing;
		} else if(enemyRight.firstAtk) {
			lastAtk = true;
			finger.SetActive (false);
			fingerDown.SetActive (false);
			fingerAtk.SetActive (true);
			fingerAction = FingerState.Doing;
		}
	}

	public override void DoingAtk(){
		if (health > 0) {
			if (firstAtk) {
				if (!enemyRight.lastAtk) {
					if (enemyRight.atk > 0) {
						enemyRight.atk -= 5;
					} else if (enemyRight.atk <= 0) {
						enemyRight.atk = 0;
					}

					if (atk < 100) {
						atk += 2;
					} else if (atk >= 100) {
						atk = 100;
					}
				}

				if (isAtk) {
					if (!enemyRight.doingSomething) {
						isAtk = false;
						touch = false;
						fingerAction = FingerState.Idel;
					}
				}
			} else if (lastAtk) {
				enemyRight.isAtk = true;
				if (enemyRight.health > 0)
					enemyRight.health -= 2;
				if (!enemyRight.oneShotColor) {
					enemyRight.changeColor = true;
					enemyRight.oneShotColor = true;
				}
				if (doingSomething) {
					if (atk > 0) {
						atk -= 6;
					} else if (atk <= 0) {
						atk = 0;
						enemyRight.touch = false;
						enemyRight.isAtk = false;
						fingerAction = FingerState.Atk;
						isAtk = false;
						if (enemyRight.health > 1)
							enemyRight.fingerAction = FingerState.Idel;
					}
				}
			}
		} else if (health <= 0) {
			finger.SetActive (false);
			fingerDown.SetActive (true);
			fingerAtk.SetActive (false);
		} else if (enemyRight.health <= 0) {
			finger.SetActive (false);
			fingerDown.SetActive (false);
			fingerAtk.SetActive (true);
		}
	}

	public override void Win(){
		finger.SetActive (true);
		fingerDown.SetActive (false);
		fingerAtk.SetActive (false);

		StartCoroutine (WaitForNextRound (1.5f));
		
		if (time >= timeInter) {
			time = 0;
		} else {
			time += Time.deltaTime;
		}

		if (time >= timeInter) {
			if (changeScale == 0)
				changeScale = 1;
			else
				changeScale = 0;
		}

		if (changeScale == 0) {
			finger.transform.localScale = Vector3.MoveTowards (finger.transform.localScale, new Vector3 (finger.transform.localScale.x, scale1, finger.transform.localScale.z), Time.deltaTime * speedScale);
			finger.transform.Rotate (finger.transform.localRotation.x, finger.transform.localRotation.y, rot1);
			finger.transform.localPosition = Vector3.MoveTowards (finger.transform.localPosition, new Vector3 (finger.transform.localPosition.x + pos1, finger.transform.localPosition.y, finger.transform.localPosition.z), Time.deltaTime * speedScale);
		} else {
			finger.transform.localScale = Vector3.MoveTowards (finger.transform.localScale, new Vector3 (finger.transform.localScale.x, scale2, finger.transform.localScale.z), Time.deltaTime * speedScale);
			finger.transform.Rotate (finger.transform.localRotation.x, finger.transform.localRotation.y, rot2);
			finger.transform.localPosition = Vector3.MoveTowards (finger.transform.localPosition, new Vector3 (finger.transform.localPosition.x - pos2, finger.transform.localPosition.y, finger.transform.localPosition.z), Time.deltaTime * speedScale);
		}
	}

	public override void Dead(){
		finger.SetActive (false);
		fingerDown.SetActive (true);
		fingerAtk.SetActive (false);

		if (time >= timeInter) {
			time = 0;
		} else {
			time += Time.deltaTime;
		}

		if (time >= timeInter) {
			if (changeScale == 0)
				changeScale = 1;
			else
				changeScale = 0;
		}

		if (changeScale == 0) {
			fingerDown.transform.localScale = Vector3.MoveTowards (fingerDown.transform.localScale, new Vector3 (fingerDown.transform.localScale.x, scale1, fingerDown.transform.localScale.z), Time.deltaTime * speedScale);
			fingerDown.transform.Rotate (fingerDown.transform.localRotation.x, fingerDown.transform.localRotation.y, rot1);
			fingerDown.transform.localPosition = Vector3.MoveTowards (fingerDown.transform.localPosition, new Vector3 (fingerDown.transform.localPosition.x + pos1, fingerDown.transform.localPosition.y, fingerDown.transform.localPosition.z), Time.deltaTime * speedScale);
		} else {
			fingerDown.transform.localScale = Vector3.MoveTowards (fingerDown.transform.localScale, new Vector3 (fingerDown.transform.localScale.x, scale2, fingerDown.transform.localScale.z), Time.deltaTime * speedScale);
			fingerDown.transform.Rotate (fingerDown.transform.localRotation.x, fingerDown.transform.localRotation.y, rot2);
			fingerDown.transform.localPosition = Vector3.MoveTowards (fingerDown.transform.localPosition, new Vector3 (fingerDown.transform.localPosition.x - pos2, fingerDown.transform.localPosition.y, fingerDown.transform.localPosition.z), Time.deltaTime * speedScale);
		}
	}

	public void ClickAtk(){
		doingSomething = true;
		if (touch) {
			if (!isAtk)
				fingerAction = FingerState.Atk;
		}

		FingerBase.changeAnim = true;
	}

	public void UnClickAtk(){
		touch = true;
		doingSomething = false;
		if (!isAtk)
			fingerAction = FingerState.Idel;
		FingerBase.changeAnim = false;
		enemyRight.oneShotColor = false;
	}

	IEnumerator WhoDeadWhoWin(float time){
		yield return new WaitForSeconds (time);
		stopTime = false;
		GameplayBase.instance.zoomCamera = true;
		if (health <= 0) {
			health = 0;
			fingerAction = FingerState.Death;
		} else if (enemyRight.health <= 0) {
			fingerAction = FingerState.Win;
		}
	}

	IEnumerator WaitForNextRound(float time){
		yield return new WaitForSeconds (time);
		SaveManager.instance.state.winCountLeft++;
		SaveManager.instance.Save ();
		if (SaveManager.instance.state.winCountLeft < 2)
			UnityEngine.SceneManagement.SceneManager.LoadScene ("MainGameScene");

	}

	IEnumerator WaitChangeColor(float time){
		yield return new WaitForSeconds (time);
		if (changeColor)
			changeColor = false;
	}
}
