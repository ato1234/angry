using UnityEngine;
using System.Collections;

namespace camera {
    public class CameraControl : MonoBehaviour {

        public float DefaultCameraPosX;
        public float DefaultCameraPosY;
        public float DefaultCameraSize;

        public float Margin = 0;

        [Range(0.0f, 0.5f)]
        public float CameraMoveSideRateX = 0;
        [Range(0.0f, 0.5f)]
        public float CameraMoveSideRateY = 0;

        public float CameraMoveSpeed = 0;

        public float MinCameraSize = 5;
        public float MaxCameraSize = 25;

        public float MinCameraPosY = 0;
        public float MaxCameraPosY = 0;

        public float MinCameraPosX = 0;
        public float MaxCameraPosX = 0;

        GameManager Manager;
        GameObject Cannon;
        Camera _camera;
        /// <summary>
        /// カメラのアスペクト比
        /// </summary>
        float ScreenAspect = 0;

        /// <summary>
        /// カメラサイズを変更するフラグ
        /// </summary>
        bool ZoomFlag = true;

        // Use this for initialization
        void Start() {
            ScreenAspect = (float)Screen.height / Screen.width;

            Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
            Cannon = GameObject.Find("Cannon");
            _camera = GetComponent<Camera>();
        }

        // Update is called once per frame
        void Update() {
            if (Manager.MouseUpdated) {
                if (Manager.State.BulletFireFlag) {

                } else {
                    UpdateBeforeFire();
                }
            }
        }

        /// <summary>
        /// Cannonが砲弾を発射する前のカメラ位置調整
        /// </summary>
        void UpdateBeforeFire() {
            // マウススクリーン座標
            Vector3 mousePosS = Input.mousePosition;

            // カメラ移動を行うかの判定用スクリーン座標
            Rect ScreenS = new Rect(
                    (float)Screen.width * CameraMoveSideRateX,
                    (float)Screen.height * CameraMoveSideRateY,
                    (float)Screen.width * (1.0f - CameraMoveSideRateX),
                    (float)Screen.height * (1.0f - CameraMoveSideRateY)
                );

            // ワールド座標
            Vector3 mousePosW = _camera.ScreenToWorldPoint(mousePosS);
            Vector3 cannonPosW = Cannon.transform.position;
            Vector3 cameraPosW = transform.position;

            // スクリーンのワールド座標
            Rect screenW = new Rect(
                    _camera.ScreenToWorldPoint(new Vector3()),
                    _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height))
                );

            Vector3 moveVec = new Vector3();
            // カメラの位置を更新
            if (mousePosS.x < ScreenS.x) {
                moveVec.x = -CameraMoveSpeed;
                //cameraPosW.x -= CameraMoveSpeed;
            } else if (mousePosS.x > ScreenS.width) {
                moveVec.x = CameraMoveSpeed;
                //cameraPosW.x += CameraMoveSpeed;
            }

            if (mousePosS.y < ScreenS.y) {
                moveVec.y = -CameraMoveSpeed;
                //cameraPosW.y -= CameraMoveSpeed;
            } else if (mousePosS.y > ScreenS.height) {
                moveVec.y = CameraMoveSpeed;
                //cameraPosW.y += CameraMoveSpeed;
            }

            cameraPosW += moveVec;
            cameraPosW.x = Mathf.Clamp(cameraPosW.x, MinCameraPosX, MaxCameraPosX);
            cameraPosW.y = Mathf.Clamp(cameraPosW.y, MinCameraPosY, MaxCameraPosY);
            transform.position = cameraPosW;

            // カメラのズームを更新
            Vector3 targetsVector = new Vector3(
                    Mathf.Abs(cannonPosW.x - cameraPosW.x) + Margin / 2,
                    Mathf.Abs(cannonPosW.y - cameraPosW.y) + Margin / 2
                );

            // アスペクト比が縦長ならyの半分、横長ならxとアスペクト比でカメラのサイズを更新
            float targetsAspect = targetsVector.y / targetsVector.x;
            float targetOrthographicSize = 0;
            if (ScreenAspect < targetsAspect) {
                targetOrthographicSize = targetsVector.y;
            } else {
                targetOrthographicSize = targetsVector.x * (1 / _camera.aspect);
            }
            _camera.orthographicSize = Mathf.Clamp(targetOrthographicSize, MinCameraSize, MaxCameraSize);
        }

        /// <summary>
        /// カメラを初期値に戻す
        /// </summary>
        public void SetDefault() {
            Vector3 p = gameObject.transform.position;
            p.x = DefaultCameraPosX;
            p.y = DefaultCameraPosY;
            gameObject.transform.position = p;
            _camera.orthographicSize = DefaultCameraSize;
        }
    }
}
