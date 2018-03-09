using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnim : MonoBehaviour {

	enum AnimationState {none, twoHand, head}
	[SerializeField]
	AnimationState whoAnim;

	[SerializeField]
	GameObject Twohand;

	[SerializeField]
	float time, timeInter;

	[SerializeField]
	float speedScale, speedRot, speedPot;

	[SerializeField]
	int changeAnim;

	[SerializeField]
	bool changeRot, changePot;

	bool hitThePhone;

	[SerializeField]
	private Transform shakeHandPos;

	// max scale and min scale.
	[SerializeField]
	float maxScale, minScale;

	void Start(){
		transform.localScale = new Vector3 (minScale, minScale, 1f);
		changeRot = false;
		changePot = false;
		hitThePhone = false;
	}

	void Update(){
		if (time >= timeInter) {
			time = 0;
		} else {
			time += Time.deltaTime;
		}

		switch (whoAnim) 
		{
		case AnimationState.twoHand:
			HandMoveAnim ();
			break;
		case AnimationState.head:
			break;
		}

		if (hitThePhone) 
		{
			transform.position = Vector3.MoveTowards (transform.position, shakeHandPos.position, speedRot * 5);
			if (transform.position == shakeHandPos.position) 
			{
				transform.position = Vector3.MoveTowards (shakeHandPos.position, transform.position, speedRot * 5);
			}
		}
	}

	void ShakeHand ()
	{
		hitThePhone = true;
	}

	void HandMoveAnim(){
		if (time >= timeInter) 
		{
			if (changeAnim == 0) 
			{
				changeAnim = 1;
				if (!changePot)
					changePot = true;
				else
					changePot = false;
			} 
			else 
			{
				changeAnim = 0;
				if (!changeRot)
					changeRot = true;
				else
					changeRot = false;
			}
		}
		if (changeAnim == 0) {
			Twohand.transform.localScale = Vector3.MoveTowards (Twohand.transform.localScale, new Vector3 (minScale, minScale, Twohand.transform.localScale.z), Time.deltaTime * speedScale);
			if (!changeRot)
				Twohand.transform.Rotate (Twohand.transform.localRotation.x, Twohand.transform.localRotation.y, -speedRot * Time.deltaTime);
			else
				Twohand.transform.Rotate (Twohand.transform.localRotation.x, Twohand.transform.localRotation.y, speedRot * Time.deltaTime);

			if (!changePot)
				Twohand.transform.localPosition = Vector3.MoveTowards (Twohand.transform.localPosition, new Vector3 (Twohand.transform.localPosition.x + 1.5f, 0, Twohand.transform.localPosition.z), Time.deltaTime * speedPot);
			else
				Twohand.transform.localPosition = Vector3.MoveTowards (Twohand.transform.localPosition, new Vector3 (Twohand.transform.localPosition.x - 1.5f, 0, Twohand.transform.localPosition.z), Time.deltaTime * speedPot);
		} else if (changeAnim == 1) {
			Twohand.transform.localScale = Vector3.MoveTowards (Twohand.transform.localScale, new Vector3 (maxScale, maxScale, Twohand.transform.localScale.z), Time.deltaTime * speedScale);
			if (!changeRot)
				Twohand.transform.Rotate (Twohand.transform.localRotation.x, Twohand.transform.localRotation.y, speedRot * Time.deltaTime);
			else
				Twohand.transform.Rotate (Twohand.transform.localRotation.x, Twohand.transform.localRotation.y, -speedRot * Time.deltaTime);
			if (!changePot)
				Twohand.transform.localPosition = Vector3.MoveTowards (Twohand.transform.localPosition, new Vector3 (Twohand.transform.localPosition.x + 1.5f, 0, Twohand.transform.localPosition.z), Time.deltaTime * speedPot);
			else
				Twohand.transform.localPosition = Vector3.MoveTowards (Twohand.transform.localPosition, new Vector3 (Twohand.transform.localPosition.x - 1.5f, 0, Twohand.transform.localPosition.z), Time.deltaTime * speedPot);
		}
	}
}
