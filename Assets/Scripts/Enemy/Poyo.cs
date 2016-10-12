using UnityEngine;
using System.Collections;
using utils;

namespace enemy {
    public class Poyo : Enemy {

        // Use this for initialization
        public void Start() {
            base.Start();
            StartCoroutine(Utils.WaitForSeconds(3f + Random.value*5, () => {
                AudioControler.PlaySE(AudioManager.SE_POYOYO);
            }));
        }

        // Update is called once per frame
        public void Update() {
            base.Update();
        }
    }
}