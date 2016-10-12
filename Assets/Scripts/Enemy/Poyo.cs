using UnityEngine;
using System.Collections;
using utils;

namespace enemy {
    public class Poyo : Enemy {

		public AudioClip IdolSound;

        // Use this for initialization
        public void Start() {
            base.Start();
            StartCoroutine(Utils.WaitForSeconds(3f + Random.value*5, () => {
				AudioControler.PlaySE(IdolSound);
            }));
        }

        // Update is called once per frame
        public void Update() {
            base.Update();
        }
    }
}