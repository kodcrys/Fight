using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerLeftControl : FingerBase {

	public static FingerLeftControl instance;

	[SerializeField]
	bool isUIAni;

	// Use this for initialization
	void Start () {
		instance = this;
		touch = true;
		stamina = 100;
		health = maxHealth;
		changeColor = false;
		oneShotColor = false;
		stopTime = true;
		a = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (staminaImage != null)
			HanderStamina ();
		if (healthImage != null)
			HanderHealth ();
		if (redHealth != null) {
			if (takeDame)
				StartCoroutine (WaitRedBlood (0.5f));
		}

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
		} else {
			if (!enemyRight.lastAtk && !isAtk)
				fingerAction = FingerState.Idel;
		}

		if (AnimationText.canPlay) {
			if (firstAtk && enemyRight.lastAtk) {
				isAtk = true;
				takeDame = true;
			} else {
				isAtk = false;
			}
		}

		if (health <= 0 || enemyRight.health <= 0) {
			GameplayBase.instance.leftButton.SetActive (false);
			AnimationText.canPlay = false;
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
			StartCoroutine (WaitChangeColor (0.0001f));
		} else {
			finger.GetComponent<SpriteRenderer> ().color = new Color32 (255, 207, 179, 255);
			fingerAtk.GetComponent<SpriteRenderer> ().color = new Color32 (255, 207, 179, 255);
			fingerDown.GetComponent<SpriteRenderer> ().color = new Color32 (255, 207, 179, 255);
			hand.GetComponent<SpriteRenderer> ().color = new Color32 (255, 207, 179, 255);
		}

	}

	public override void HanderHealth(){
		healthImage.fillAmount = Map (health, 0, maxHealth, 0, 1);
	}

	public override void HanderRedHealth(){
		redHealth.fillAmount = Map (health, 0, maxHealth, 0, 1);
	}

	public override void HanderStamina(){
		staminaImage.fillAmount = Map (stamina, 0, 100, 0, 1);
	}

	public float Map(float value, float inMin, float inMax, float outMin, float outMax){
		return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
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
				if (stamina < 100) {
					stamina += 2;
				} else if (stamina >= 100) {
					stamina = 100;
				}
			}
		}

		if (fingerAminChanger == 0) {
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
		} else if (fingerAminChanger == 1) {
			stunFinger.enabled = true;
		}
	}

	public override void DoAtk(){
		if (!enemyRight.firstAtk && !firstAtk) {
			firstAtk = true;
			enemyRight.fingerAminChanger = 1;
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
					if (enemyRight.stamina > 0) {
						enemyRight.stamina -= 2;
					} else if (enemyRight.stamina <= 0) {
						enemyRight.stamina = 0;
					}

					if (stamina < 100) {
						stamina += 2;
					} else if (stamina >= 100) {
						stamina = 100;
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
					enemyRight.health -= atk;
				if (!enemyRight.oneShotColor) {
					CameraShake.instance.Shake ();
					enemyRight.changeColor = true;
					enemyRight.oneShotColor = true;
				}
				if (doingSomething) {
					if (stamina > 0) {
						stamina -= 8;
						if (SaveManager.instance.state.isOnRing)
							Handheld.Vibrate ();
					} else if (stamina <= 0) {
						stamina = 0;
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

		isAtk = true;
		firstAtk = false;
		lastAtk = false;

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

		isAtk = true;
		firstAtk = false;
		lastAtk = false;

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
	}

	public void UnClickAtk(){
		touch = true;
		doingSomething = false;
		enemyRight.fingerAminChanger = 0;
		if (!isAtk)
			fingerAction = FingerState.Idel;
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
		a++;
		if (a == 1) {
			SaveManager.instance.state.winCountLeft++;
			if (SaveManager.instance.state.winCountLeft < 2) {
				if (!AnimationText.endRound) {
					SaveManager.instance.state.roundCount++;
					AnimationText.endRound = true;
				}
			} else {
				if (!AnimationText.endRound) {
					AnimationText.endRound = true;
					GameplayBase.instance.gameoverPanel.SetActive (true);
				}
			}
			SaveManager.instance.Save ();
		}
	}

	IEnumerator WaitChangeColor(float time){
		yield return new WaitForSeconds (time);
		if (changeColor)
			changeColor = false;
	}

	IEnumerator WaitRedBlood(float time){
		yield return new WaitForSeconds (time);
		HanderRedHealth ();
		takeDame = false;
	}
}
