using UnityEngine;
using utils;

public class Enemy : MonoBehaviour {

    public float DeathTime;

    public int MaxHp;
    private int Hp;

    public GameObject DestroyEffect;

    bool destroyed;

    private GameManager Manager;

	// Use this for initialization
	void Start () {
        Hp = MaxHp;
        destroyed = false;
        Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y < -5) {
            Hp -= 10;
        }
	    if (Hp <= 0) {
            if (!destroyed) {
                Death();
            }
        }
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

            Hp -= damage;
            ShowDamageText(damage);
        }
    }

    void ShowDamageText(int damage) {
        GameObject prfb = Resources.Load<GameObject>("DamageText");
        GameObject t = Instantiate(prfb);
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
            Destroy(gameObject);
        }));
    }
}
