using UnityEngine;
using System.Collections;
using utils;

using camera;

public class StageClear : MonoBehaviour {

    GameManager Manager;

	void Start () {
        Manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        float time = 5;
        StartCoroutine(StageClearCoroutine());

    }
	
	void Update () {
	    
	}

    IEnumerator StageClearCoroutine() {
        CameraAnimation ca = Camera.main.GetComponent<CameraAnimation>();
        CameraControl cc = Camera.main.GetComponent<CameraControl>();

        float waittime = 3;
        float fotime = 0.7f;

        yield return new WaitForSeconds(waittime);
        ca.FadeOut(fotime);
        yield return new WaitForSeconds(fotime);
        
    }
}
