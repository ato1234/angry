using UnityEngine;
using System.Collections;

using camera;

namespace animation {
    /// <summary>
    /// ゲーム開始時にステージを一望するアニメーション
    /// フェードインしながら移動し、フェードアウト、元の位置にカメラを戻してフェードイン
    /// </summary>
    public class StageView : MonoBehaviour {

        GameManager Manager;

        /// <summary>
        /// アニメーションを開始する
        /// </summary>
        public void Start() {
            Manager = gameObject.GetComponent<GameManager>();
            StartCoroutine(StageViewCoroutine());
        }

        public IEnumerator StageViewCoroutine() {
            CameraAnimation ca = Camera.main.GetComponent<CameraAnimation>();
            CameraControl cc = Camera.main.GetComponent<CameraControl>();
            float x = cc.MaxCameraPosX;
            float y = cc.MaxCameraPosY;
            float size = cc.MaxCameraSize;

            float fitime = 0.5f;
            float fotime = 0.5f;
            float mvtime = 3f;

            ca.FadeIn(fitime);
            ca.MoveTo(x, y, mvtime);
            ca.OrthographicSizeTo(size, mvtime);
            yield return new WaitForSeconds(mvtime - fitime);
            ca.FadeOut(fotime);
            yield return new WaitForSeconds(fotime);
            cc.SetDefault();
            ca.FadeIn(fitime, gameObject, "OnComplete");
        }

        void OnComplete() {
            Manager.SetGameState(GameState.STAT_MAIN);
            Destroy(this);
        }
    }
}
