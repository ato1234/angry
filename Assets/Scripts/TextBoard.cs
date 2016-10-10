using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextBoard : MonoBehaviour {

    [HideInInspector]
    public Text ScoreText;

    [HideInInspector]
    public Text ClearText;

    private GameManager Manager;

    void Start() {
        ScoreText = transform.FindChild("ScoreText").GetComponent<Text>();
        ClearText = transform.FindChild("ClearText").GetComponent<Text>();

        Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update() {
        switch (Manager.State.Stat) {
            case GameState.STAT_MAIN:
                ScoreText.text = "Score : " + Manager.State.Score;
                break;
        }
    }

    /// <summary>
    /// ゲームステータスが変更されたときに呼ばれるイベントハンドラ的な？
    /// </summary>
    public void OnGameStateChange(int newState) {
        switch (newState) {
            case GameState.STAT_START:
                ScoreText.enabled = false;
                ClearText.enabled = false;
                break;
            case GameState.STAT_MAIN:
                ScoreText.enabled = true;
                break;
            case GameState.STAT_CLEAR:
                ClearText.enabled = true;
                break;
        }
    }
}
