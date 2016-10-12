using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextBoard : MonoBehaviour {

    [HideInInspector]
    public GameObject ScoreText;

    [HideInInspector]
    public GameObject ClearText;

    [HideInInspector]
    public GameObject TweetButton;

    [HideInInspector]
    public GameObject NextButton;

    private GameManager Manager;

    void Start() {
        ScoreText = transform.FindChild("ScoreText").gameObject;
        ClearText = transform.FindChild("ClearText").gameObject;
        TweetButton = transform.FindChild("TweetButton").gameObject;
        NextButton = transform.FindChild("NextButton").gameObject;

        Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update() {
        switch (Manager.State.Stat) {
            case GameState.STAT_MAIN:
                ScoreText.GetComponent<Text>().text = "Score : " + Manager.State.Score;
                break;
        }
    }

    IEnumerator ClearMovie() {
        yield return new WaitForSeconds(1);
        iTween.MoveTo(ClearText,iTween.Hash(
                "y", Screen.height*3/4,
                "time", 1
            ));
        yield return new WaitForSeconds(1);
        TweetButton.SetActive(true);
        NextButton.SetActive(true);
    }

    /// <summary>
    /// ゲームステータスが変更されたときに呼ばれるイベントハンドラ的な？
    /// </summary>
    public void OnGameStateChange(int newState) {
        switch (newState) {
            case GameState.STAT_MAIN:
                ScoreText.GetComponent<Text>().enabled = true;
                break;
            case GameState.STAT_CLEAR:
                StartCoroutine(ClearMovie());
                ClearText.GetComponent<Text>().enabled = true;
                break;
        }
    }
}
