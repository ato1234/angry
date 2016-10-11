using UnityEngine;
using System.Collections;
using utils;

namespace enemy {
    public class Enemy : MonoBehaviour {

        public float DeathTime;

        public int MaxHp;
        private int Hp;
        public float CheckPoyo;

        public GameObject DestroyEffect;

        bool destroyed;

        private GameManager Manager;
        GameState State;
        private GameObject DamageTextPref;
        private AudioManager AudioControler;

        // Use this for initialization
        void Start() {
            Hp = MaxHp;
            destroyed = false;

            Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
            AudioControler = GameObject.Find("AudioManager").GetComponent<AudioManager>();
            DamageTextPref = Resources.Load<GameObject>("GameObject/DamageText");

            StartCoroutine(CryPoyo(3f + Random.value));
        }

        // Update is called once per frame
        void Update() {
            if (transform.position.y < -5) {
                Hp -= 10;
            }
            if (Hp <= 0) {
                if (!destroyed) {
                    Death();
                }
            }
        }

        IEnumerator CryPoyo(float Waittime) {
            yield return new WaitForSeconds(Waittime);
            AudioControler.PlaySE(AudioManager.SE_POYOYO);
        }




        void OnCollisionEnter(Collision who) {
            GameObject obj = who.gameObject;

            if (obj.tag == "Enemy") {
                int damage = CalcDamage(obj, 100);
                AddDamage(damage);

            } else if (obj.tag == "Bullet") {
                int damage = CalcDamage(obj, 10);
                AddDamage(damage);
            }
        }

        int CalcDamage(GameObject enemy, float coef) {
            Vector3 v = enemy.GetComponent<Rigidbody>().velocity;
            float mass = enemy.GetComponent<Rigidbody>().mass;

            float dmg = v.magnitude * mass * coef;

            return dmg > 10 ? (int)dmg : 0;
        }

        public void AddDamage(int damage) {
            if (damage > 0) {
                Manager.AddScore(damage);
                AudioControler.PlaySE(AudioManager.SE_NPOYO);

                Hp -= damage;
                ShowDamageText(damage);
            }
        }

        void ShowDamageText(int damage) {
            GameObject t = Instantiate(DamageTextPref);
            Vector3 p = t.transform.position;
            p.x = transform.position.x;
            p.y = transform.position.y;
            t.transform.position = p;
            t.GetComponent<TextMesh>().text = damage.ToString();
        }

        void Death() {
            destroyed = true;
            StartCoroutine(Utils.WaitForSeconds(DeathTime, () => {
                GameObject t = Instantiate(DestroyEffect);
                t.transform.position = transform.position;
                AudioControler.PlaySE(AudioManager.SE_POYOYO);
                Destroy(gameObject);
            }));
        }
    }
}