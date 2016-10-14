using UnityEngine;
using System.Collections;

namespace Cannon{
    // 複数の丸オブジェクトを配置して弾道予測線を制御するクラス
    public class BulletLine : MonoBehaviour{

        /// <summary>
        /// 弾道予測線ビットの数
        /// </summary>
        public int BulletLineSize = 20;
        /// <summary>
        /// 弾道予測線ビットの間隔
        /// </summary>
        public float BulletLineBitsInterval = 0.5f;

        public GameObject LineBitPrefab;

        private GameObject[] LineBits;
        private Cannon Cannon;
        private Vector3 Force;
        private float deltaTime;

        private GameManager Manager;

        void Start() {
            Cannon = transform.parent.gameObject.GetComponent<Cannon>();
            LineBits = new GameObject[BulletLineSize];

            Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        void Update() {
            if (!Manager.State.BulletFireFlag) {
                deltaTime = (deltaTime + Time.deltaTime/5) % BulletLineBitsInterval;
                // Linebits[i]が存在しなければ新規に作成し、存在するなら座標を変更
                for (int i = 0; i < LineBits.Length; i++) {
                    if (LineBits[i] == null) {
                        LineBits[i] = CreateLineBit(Cannon.BulletLineBitContainer.transform, Force, i);

                    } else {
                        LineBits[i].transform.position = CalcPos(Force, i);
                    }

                    LineBits[i].SetActive(LineBits[i].transform.position.y >= 0);
                }

            } else {
                foreach (GameObject b in LineBits) {
                    if (b != null) {
                        b.SetActive(false);
                    }
                }
            }
        }


        public void SetForce(Vector3 force) {
            Force = force;
        }

        GameObject CreateLineBit(Transform parent, Vector3 force, int index) {
            GameObject g = GameObject.Instantiate(
                    LineBitPrefab, 
                    CalcPos(force, index), 
                    Cannon.transform.rotation
                ) as GameObject;

            g.transform.parent = parent;

            return g;
        }

        // BulletLineBitの位置を計算する
        Vector3 CalcPos(Vector3 force, int index) {
            float t = index * BulletLineBitsInterval + deltaTime;
            float x = force.x * t;
            float y = force.y * t + (Physics.gravity.y * t * t) / 2;

            Vector3 p = new Vector3(x, y, 0) + Cannon.Muzzle.transform.position;
            return p;
        }
    }
}
