using UnityEngine;
using System.Collections;

public class ParticleSystemDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
    }

    // Update is called once per frame
    void Update() {
        bool flag = true;
        // パーティクルの再生がすべて終了していれば削除
        foreach (ParticleSystem system in GetComponentsInChildren<ParticleSystem>()) {
            flag = flag && !system.isPlaying;
        }
        if (flag) {
            Destroy(gameObject);
        }
    }
}
