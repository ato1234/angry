using UnityEngine;
using System.Collections;
using utils;


public class Text : MonoBehaviour {

    public float time;
    public float speed;

    // Use this for initialization
    void Start() {
        StartCoroutine(Utils.WaitForSeconds(time, ()=> {
            Destroy(gameObject);
        }));
    }

    // Update is called once per frame
    void Update() {
        Vector3 p = transform.position;
        p.y += speed;
        transform.position = p;
    }
}
