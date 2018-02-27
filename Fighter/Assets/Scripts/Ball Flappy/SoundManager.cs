using UnityEngine;

public class SoundManager : MonoBehaviour {
	
	public AudioSource fly;
	public AudioSource score;
	public AudioSource death;
	public AudioSource click;

	public static AudioSource flyS;
	public static AudioSource scoreS;
	public static AudioSource deathS;
	public static AudioSource clickS;
	// Use this for initialization
	void Start () {
		flyS = fly;
		scoreS = score;
		deathS = death;
		clickS = click;
		DontDestroyOnLoad (gameObject);
	}
}
