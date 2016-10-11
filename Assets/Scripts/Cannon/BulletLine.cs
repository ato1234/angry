using UnityEngine;
using System.Collections;

namespace Cannon{
    // 複数の丸オブジェクトを配置して弾道予測線を制御するクラス
    public class BulletLine : MonoBehaviour{

        /// <summary>
        /// 弾道予測線表示フラグ
        /// </summary>
        public bool BulletLineFlag = true;
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
        

        void Start() {
            Cannon = transform.parent.gameObject.GetComponent<Cannon>();
            LineBits = new GameObject[BulletLineSize];
        }

        public void ShowBulletLine(Vector3 force) {
            if (BulletLineFlag) {
                // Linebits[i]が存在しなければ新規に作成し、存在するなら座標を変更
                for (int i = 0; i < LineBits.Length; i++) {
                    if (LineBits[i] == null) {
                        LineBits[i] = CreateLineBit(Cannon.BulletLineBitContainer.transform, force, i);

                    } else {
                        LineBits[i].transform.position = CalcPos(force, i);
                    }

                    LineBits[i].SetActive(LineBits[i].transform.position.y >= 0);
                }
            }
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
            float t = index * BulletLineBitsInterval;
            float x = force.x * t;
            float y = force.y * t + (Physics.gravity.y * t * t) / 2;

            Vector3 p = new Vector3(x, y, 0) + Cannon.Muzzle.transform.position;
            return p;
        }
    }
}
