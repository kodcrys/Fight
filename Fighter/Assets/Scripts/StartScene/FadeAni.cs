using UnityEngine;

public class FadeAni : MonoBehaviour {

	public enum State {none, Show, Hide, Show1, Show2}
	[Header("State machine")]
	public State stateFade = State.none;

	[Header("Speed of ani")]
	[SerializeField]
	float speed;

	[Header("ObjectFade")]
	[SerializeField]
	Transform fade1;
	[SerializeField]
	Transform fade2;

	[Header("Position to move")]
	[SerializeField]
	Transform hidePosFade1;
	[SerializeField]
	Transform showPosFade1;
	[SerializeField]
	Transform showPos1Fade1;
	[SerializeField]
	Transform showPos2Fade1;
	[SerializeField]
	Transform hidePosFade2;
	[SerializeField]
	Transform showPosFade2;
	[SerializeField]
	Transform showPos1Fade2;
	[SerializeField]
	Transform showPos2Fade2;

	float timeDelay = 0;

	// Use this for initialization
	void Start () {
		timeDelay = 0;
	}
	
	// Update is called once per frame
	void Update () {
		switch (stateFade) {
			case State.none:
				break;
			case State.Show:
				fade1.position = Vector3.MoveTowards (fade1.position, showPosFade1.position, speed * Time.deltaTime);
				fade2.position = Vector3.MoveTowards (fade2.position, showPosFade2.position, speed * Time.deltaTime);
				if (fade1.position == showPosFade1.position && fade2.position == showPosFade2.position) {
					timeDelay += Time.deltaTime;
					if (timeDelay > 0.25f) {
						stateFade = State.Show1;
						timeDelay = 0;
					}
				}
				break;
			case State.Show1:
				fade1.position = Vector3.MoveTowards (fade1.position, showPos1Fade1.position, speed * Time.deltaTime);
				fade2.position = Vector3.MoveTowards (fade2.position, showPos1Fade2.position, speed * Time.deltaTime);
				if (fade1.position == showPos1Fade1.position && fade2.position == showPos1Fade2.position) {
					timeDelay += Time.deltaTime;
					if (timeDelay > 0.25f) {
						stateFade = State.Show2;
						timeDelay = 0;
					}
				}
				break;
			case State.Show2:
				fade1.position = Vector3.MoveTowards (fade1.position, showPos2Fade1.position, speed * Time.deltaTime);
				fade2.position = Vector3.MoveTowards (fade2.position, showPos2Fade2.position, speed * Time.deltaTime);
				break;
			case State.Hide:
				fade1.position = Vector3.MoveTowards (fade1.position, hidePosFade1.position, speed * Time.deltaTime);
				fade2.position = Vector3.MoveTowards (fade2.position, hidePosFade2.position, speed * Time.deltaTime);
				break;
		}
	}
}
