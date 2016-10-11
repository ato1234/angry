using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonsController : MonoBehaviour {


	void Start () {
        // ボタンにハイスコアを表示
        Transform[] list = transform.GetComponentsInChildren<Transform>();

        foreach (Transform t in list) {
            if (t.tag == "StageSelectButton") {
                SetHighScore(t);
            }
        }
	}

    void SetHighScore(Transform button) {
        Text StageName = button.FindChild("StageName").GetComponent<Text>();
        Text HighScore = button.FindChild("HighScore").GetComponent<Text>();

        int score = PlayerPrefs.GetInt("HIGH_SCORE_" + StageName.text, 0);
        HighScore.text = ""+score;
    }
}
