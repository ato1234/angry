using UnityEngine;
using System.Collections;

namespace Cannon {
    /// <summary>
    /// 大砲を制御するクラス
    /// </summary>
    public class Cannon : MonoBehaviour {

        /// <summary>
        /// 弾丸のプレハブ
        /// </summary>
        public GameObject BulletPrefab;
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
        /// <summary>
        /// 力の最大値
        /// </summary>
        public float MaxForce;

        /// <summary>
        /// 弾丸を発射する威力ベクトル
        /// </summary>
        Vector3 Force;

        GameManager Manager;
        private BulletLine BulletLine;

        /// <summary>
        /// 弾道予測線ビット格納用gameObject
        /// </summary>
        public GameObject BulletLineBitContainer { get; private set; }
        /// <summary>
        /// 砲身
        /// </summary>
        public GameObject Barrel { get; private set; }
        /// <summary>
        /// 銃口.弾丸はここから発生する
        /// </summary>
        public GameObject Muzzle { get; private set; }
        
        void Start() {
            Force = new Vector3(1, 1, 0);
            BulletLine = new BulletLine(this, BulletLineSize, BulletLineBitsInterval);

            Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
            BulletLineBitContainer = transform.FindChild("BulletLine").gameObject;
            Barrel = transform.FindChild("Barrel").gameObject;
            Muzzle = Barrel.transform.FindChild("Muzzle").gameObject;
        }

        void Update() {
            if (Manager.State.Stat == GameState.STAT_MAIN) {
                if (Manager.State.BulletFireFlag) {

                } else {
                    UpdateBeforeFire();
                }
            }
        }

        void UpdateBeforeFire() {
            if (Manager.MouseUpdated) {

                SetBarrelAngle(Manager.MousePos);
                SetForce(Manager.MousePos);
            }

            if (Input.GetKeyDown("space")) {
                Fire();
            }
        }

        private void SetForce(Vector3 mouse) {
            Force = mouse - transform.position;
            // 軸ずれを防ぐためz軸方向の力を0に
            Force.z = 0;

            if (MaxForce * MaxForce < Force.sqrMagnitude) {
                Force = Force.normalized * MaxForce;
            }
            if (BulletLineFlag) {
                BulletLine.ShowBulletLine(Force);
            }
        }

        private void SetBarrelAngle(Vector3 mouse) {
            Barrel.transform.LookAt(mouse);
            // 砲身の角度制限
            Vector3 ang = Barrel.transform.eulerAngles;
            ang.y = 90;
            Barrel.transform.rotation = Quaternion.Euler(ang);
        }

        private void Fire() {
            GameObject b = (GameObject)Instantiate(
                    BulletPrefab,
                    Muzzle.transform.position,
                    transform.rotation
                );

            b.transform.parent = Manager.Bullets.transform;
            b.GetComponent<Rigidbody>().AddForce(Force, ForceMode.VelocityChange);
            Manager.NowMovingBullet = b;
        }
    }
}
