using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

	AudioSource SE;
	AudioClip[] SE_Clips = new AudioClip[5];
	public const int SE_NPOYO = 1;
	public const int SE_NZZ = 2;

	void Start () {
		SE = GameObject.Find ("GameManager/AudioManager/SE").GetComponent<AudioSource> ();
		SE_Clips [SE_NPOYO] = Resources.Load<AudioClip> ("Audio/SE/Poyo3_npoyo");
		SE_Clips [SE_NZZ] = Resources.Load<AudioClip> ("Audio/SE/nzz1");
		
	}

	
	void Update () {

	}


	public void PlaySE(int index){			
		SE.clip = SE_Clips[index];
		SE.Play();

	}


}



