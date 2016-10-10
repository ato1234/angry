using UnityEngine;
using System.Collections;
using utils;

public class Bullet : MonoBehaviour {

    [HideInInspector]
    public bool IsAlive { get; private set; }

    GameManager Manager;
    GameState State;

    void Awake() {
        IsAlive = true;
    }

    void Start () {
        Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        State = Manager.State;

        StartCoroutine(BulletStopCheck());
        // 一定時間が過ぎれば強制的に次の弾を撃てるようにする
        StartCoroutine(Utils.WaitForSeconds(State.NextBulletTime, () => {
            IsAlive = false;
        }));
    }
	
	void Update () {
        if (transform.position.y < -5) {
            IsAlive = false;
        }
	}
    /*
    void OnCollisionEnter(Collision who) {
        GameObject obj = who.gameObject;
        
        if (obj.tag == "Enemy") {
            int damage = CalcDamage(obj);
            obj.GetComponent<Enemy>().AddDamage(damage);
        }
    }

    int CalcDamage(GameObject enemy) {
        Vector3 v = GetComponent<Rigidbody>().velocity;
        return (int)(v.magnitude * 10);
    }
    */

    /// <summary>
    /// 一定時間移動がなければ弾が停止したとみなす
    /// </summary>
    IEnumerator BulletStopCheck() {
        int count = 0;
        float limit = State.BulletSpeedLowerLimit * State.BulletSpeedLowerLimit;

        while (true) {
            Vector3 v = GetComponent<Rigidbody>().velocity;
            count = v.sqrMagnitude < limit ? count + 1 : 0;

            if (1 < count || !IsAlive) {
                break;
            }
            yield return new WaitForSeconds(State.BulletSpeedCheckInterval);
        }
        IsAlive = false;
    }
}
