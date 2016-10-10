using UnityEngine;
using System.Collections;

/// <summary>
/// ゲームの状態を保持するクラス
/// </summary>
public class GameState : MonoBehaviour{

    /// <summary>
    /// ステージ名称
    /// </summary>
    public string StageName;

    /// <summary>
    /// スコア
    /// </summary>
    [HideInInspector]
    public int Score;

    /// <summary>
    /// 現在のステータス
    /// </summary>
    [HideInInspector]
    public int Stat = STAT_START;

    /// <summary>
    /// シーン起動直後の状態
    /// </summary>
    [HideInInspector]
    public const int STAT_START = 0;

    /// <summary>
    /// メイン処理実行中の状態
    /// </summary>
    [HideInInspector]
    public const int STAT_MAIN = 1;

    /// <summary>
    /// ステージクリア後
    /// </summary>
    [HideInInspector]
    public const int STAT_CLEAR = 2;

    /// <summary>
    /// 一発撃った後、この時間がたてば次の弾を撃つ
    /// </summary>
    [Range(0, 60)]
    public float NextBulletTime;

    /// <summary>
    /// 弾が停止しているとみなす速度の下限値
    /// </summary>
    [Range(0, 5)]
    public float BulletSpeedLowerLimit;

    /// <summary>
    /// 弾の速度をチェックする間隔。二回連続で規定速度を下回れば停止しているとみなす
    /// </summary>
    [Range(0, 5)]
    public float BulletSpeedCheckInterval;

    /// <summary>
    /// 砲弾が発射されているフラグ
    /// </summary>
    [HideInInspector]
    public bool BulletFireFlag {
        get { return (Manager != null) && (Manager.NowMovingBullet != null); }
        private set { }
    }


    GameManager Manager;

    void Start() {
        Stat = STAT_START;
        Manager = transform.parent.GetComponent<GameManager>();
    }
}
