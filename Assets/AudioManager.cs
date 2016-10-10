using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

	GameManager Manager;
	GameState State;

	AudioSource SE1;
	public int PlayCountCheck;
	AudioClip[] SE_Clips = new AudioClip[5];
	public const int SE_POYO1 = 0;
	public const int SE_POYO2 = 1;
	public const int SE_POYOYO = 2;
	public const int SE_NPOYO = 3;
	public const int SE_NZZ = 4;

	void Start () {
		PlayCountCheck = 0;
		State = transform.parent.FindChild("GameState").GetComponent<GameState>();
		SE1 = GameObject.Find ("GameManager/AudioManager/SE").GetComponent<AudioSource> ();
		SE_Clips [SE_POYO1] = Resources.Load<AudioClip> ("Audio/SE/Poyo1_poyo");
		SE_Clips [SE_POYO2] = Resources.Load<AudioClip> ("Audio/SE/Poyo2_poyo~");
		SE_Clips [SE_POYOYO] = Resources.Load<AudioClip> ("Audio/SE/Poyo4_poyoyo");
		SE_Clips [SE_NPOYO] = Resources.Load<AudioClip> ("Audio/SE/Poyo3_npoyo");
		SE_Clips [SE_NZZ] = Resources.Load<AudioClip> ("Audio/SE/nzz1");
	
	}

	
	void Update () {
		if (!State.BulletFireFlag) {
			PlayCountCheck = 0;
		}
	}


	public void PlaySE(int index){			

		if (PlayCountCheck < 4) {
			SE1.clip = SE_Clips [index];
			SE1.PlayOneShot (SE1.clip);
			PlayCountCheck = PlayCountCheck + 1;
		} else {
			PlayCountCheck = PlayCountCheck - 2;
		}
	}


}




