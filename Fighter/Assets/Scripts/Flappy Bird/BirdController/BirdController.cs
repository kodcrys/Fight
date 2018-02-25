	using UnityEngine;
using System.Collections;

public class BirdController : MonoBehaviour {

	public static BirdController instance;

	public float bounceForce;

	private Rigidbody2D myBody;
	private Animator anim;

	[SerializeField]
	private AudioSource audioSource;

	[SerializeField]
	private AudioClip flyClip,pingClip,diedClip;

	private bool isAlive;
	private bool didFlap;
	private float time;
	private GameObject spawner;

	public float flag = 0;
	public int score = 0;

	// Use this for initialization
	void Awake () 
	{
		isAlive = true;
		myBody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		_MakeInstance ();
		spawner = GameObject.Find ("Spawner Pipe");
	}

	void _MakeInstance()
	{
		if (instance == null) 
		{
			instance = this;
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		_BirdMoveMent ();
	}

	float tempx = 125, tempy = 250;
	void _BirdMoveMent(){

		if (isAlive) 
		{
			if (didFlap) 
			{
				if (time <= 0.5f) 
				{
					tempx -= 25f;
					tempy -= 50f;
				}
				else 
				{
					tempx = 125;
					tempy = 250;
				}

				if (tempx <= 75) 
				{
					tempx = 75;
					tempy = 150;
				}

				time = 0f;
				Debug.Log (tempx + " " + tempy);
				myBody.AddForce (new Vector2 (tempx, tempy));

			//	myBody.velocity = new Vector2 (20 ,40);
				audioSource.PlayOneShot (flyClip);
				didFlap = false;
			} 
			else 
			{ 
				time += Time.deltaTime;
				myBody.AddForce (new Vector2 (-5f, -10f));
				//myBody.velocity = new Vector2 (-1.5f - Time.deltaTime * 3 , -3f - Time.deltaTime * 3);
			}
		}
		if (myBody.velocity.y > 0) 
		{
			float angel = 0;
			angel = Mathf.Lerp (0, 90, myBody.velocity.y / 7);
			transform.rotation = Quaternion.Euler (0, 0, angel);
		}
		else 
			if (myBody.velocity.y == 0) 	
			{
				transform.rotation = Quaternion.Euler (0, 0, 0);
			}
			else 
				if (myBody.velocity.y < 0) 
				{
				float angel = 0;
				angel = Mathf.Lerp (0, -90, -myBody.velocity.y / 7);
				transform.rotation = Quaternion.Euler (0, 0, angel);
				}
	}

	public void FlapButton(){
		didFlap = true;
	}

	void OnTriggerEnter2D(Collider2D target){
		if (target.tag == "PipeHolder") {
			score++;
			if (GamePlayController.instance != null) {
				GamePlayController.instance._SetScore (score);
			}
			audioSource.PlayOneShot (pingClip);
		}
	}

	void OnCollisionEnter2D(Collision2D target){
		if (target.gameObject.tag == "Pipe" || target.gameObject.tag == "Ground") {
			flag = 1;
			if (isAlive) {
				isAlive = false;
				Destroy (spawner);
				audioSource.PlayOneShot (diedClip);
				anim.SetTrigger ("Died");
				myBody.velocity = Vector2.zero;
			}
			if (GamePlayController.instance != null) {
				GamePlayController.instance._BirdDiedShowPanel (score);
			}
		}
	}
}
