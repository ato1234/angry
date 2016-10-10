using UnityEngine;
using System.Collections;
using utils;
using UnityEngine.UI;

using animation;

public class GameManager : MonoBehaviour {

    /// <summary>
    /// 敵グループ
    /// </summary>
    GameObject Enemys;
    /// <summary>
    /// 大砲
    /// </summary>
    GameObject Cannon;
    /// <summary>
    /// 砲身
    /// </summary>
    GameObject Barrel;

    /// <summary>
    /// マウス座標が更新されたフラグ
    /// </summary>
    [HideInInspector]
    public bool MouseUpdated;
    /// <summary>
    /// ワールド座標に変換されたマウスの座標
    /// </summary>
    [HideInInspector]
    public Vector3 MousePos;

    /// <summary>
    /// パラメータまとめクラス
    /// </summary>
    [HideInInspector]
    public GameState State;

    /// <summary>
    /// 動いている弾
    /// </summary>
    [HideInInspector]
    public GameObject NowMovingBullet = null;

    /// <summary>
    /// 撃った後の弾の親
    /// </summary>
    [HideInInspector]
    public GameObject Bullets;

    /// <summary>
    /// スコア表示用キャンバス
    /// </summary>
    GameObject ScoreBorad;

    /// <summary>
    /// スコア表示用テキスト
    /// </summary>
    UnityEngine.UI.Text ScoreText;

    // Use this for initialization
    void Start () {
        State = transform.FindChild("GameState").GetComponent<GameState>();
        Enemys = GameObject.Find("Enemys");
        Cannon = GameObject.Find("Cannon");
        Bullets = GameObject.Find("Bullets");
        Barrel = Cannon.transform.FindChild("Barrel").gameObject;

        ScoreBorad = GameObject.Find("ScoreBoard");
        ScoreText = ScoreBorad.transform.FindChild("ScoreText").GetComponent<UnityEngine.UI.Text>();
        MousePos = new Vector3();

        gameObject.AddComponent<StageView>();
    }

    // Update is called once per frame
    void Update() {
        switch (State.Stat) {
            case GameState.STAT_START:
                break;
            case GameState.STAT_MAIN:
                UpdateMousePos(Input.GetMouseButton(0));
                UpdateNowMovingBullet();
                StageClearCheck();

                ScoreText.text = "Score : " + State.Score;
                break;
            case GameState.STAT_CLEAR:
                break;
        }
    }

    private void UpdateMousePos(bool mouseClicFlag) {
        if (mouseClicFlag) {
            Vector3 mouse = Input.mousePosition;
            // マウスのスクリーン座標にz軸情報を加え、ワールド座標へ変換
            mouse.z = 0f;
            mouse = Camera.main.ScreenToWorldPoint(mouse);
            // ワールド座標ではz軸情報は要らないのでカット
            mouse.z = 0.0f;
            // マウスの座標を砲身の右上に制限
            mouse.x = (mouse.x - Barrel.transform.position.x) < 0 ? Barrel.transform.position.x : mouse.x;
            mouse.y = (mouse.y - Barrel.transform.position.y) < 0 ? Barrel.transform.position.y : mouse.y;

            MousePos = mouse;
        }
        MouseUpdated = mouseClicFlag;
    }

    private void UpdateNowMovingBullet() {
        if (!State.BulletFireFlag) {
            return;
        }
        Bullet b = NowMovingBullet.GetComponent<Bullet>();

        if (!b.IsAlive) {
            NowMovingBullet = null;
        }
    }

    private void StageClearCheck() {
        if (Enemys.transform.childCount == 0) {
            NextGameState();
        }
    }

    /// <summary>
    /// クリア時の処理
    /// </summary>
    private void StageClear() {
        GameObject text = Camera.main.gameObject.transform.FindChild("Canvas/ClearText").gameObject;
        text.SetActive(true);
        StartCoroutine(Utils.WaitForSeconds(3, () => {
            text.SetActive(false);
            GameObject.Find("SceneLoader").GetComponent<SceneLoader>().LoadSceneSingle("StageSelect");
        }));
    }

    /// <summary>
    /// 次のステータスへ
    /// </summary>
    public void NextGameState() {
        switch (State.Stat) {
            case GameState.STAT_START:
                State.Stat = GameState.STAT_MAIN;
                ScoreText.enabled = true;
                break;

            case GameState.STAT_MAIN:
                State.Stat = GameState.STAT_CLEAR;
                StageClear();
                break;

            case GameState.STAT_CLEAR:
                break;
        }
    }

    /// <summary>
    /// 得点を加算する
    /// </summary>
    /// <param name="score"></param>
    public void AddScore(int score) {
        State.Score += score;
    }
}
