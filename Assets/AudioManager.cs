using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

	GameManager Manager;
	GameState State;

	AudioSource SE1;
	public int PlayCountCheck;


	void Start () {
		PlayCountCheck = 0;
		State = transform.parent.FindChild("GameState").GetComponent<GameState>();
		SE1 = GameObject.Find ("GameManager/AudioManager/SE").GetComponent<AudioSource> ();
	
	}

	
	void Update () {
		if (!State.BulletFireFlag) {
			PlayCountCheck = 0;
		}
	}

	public void PlaySE(AudioClip Clip){			
		if (PlayCountCheck < 4) {
			SE1.PlayOneShot (Clip);
			PlayCountCheck = PlayCountCheck + 1;
		} else {
			PlayCountCheck = PlayCountCheck - 2;
		}
	}


}




