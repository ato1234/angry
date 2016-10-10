using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace camera {
    public class CameraAnimation : MonoBehaviour {

        private Image img;

        [Range(0, 1)]
        public float red, green, blue;

        // Use this for initialization
        void Start() {
            img = transform.FindChild("Canvas/Image").gameObject.GetComponent<Image>();
        }

        void OnUpdateColor(Color c) {
            img.color = c;
        }

        void OnUpdateOrthographicSize(float size) {
            Camera.main.orthographicSize = size;
        }

        public void OnComplete() {
        }

        public void FadeIn(float time, GameObject CallbackTarget = null, string CallbackMethodName = "OnComplete") {
            float nowalpha = img.color.a;
            Fade(new Color(red, green, blue, nowalpha),
                 new Color(red, green, blue, 0f),
                 time,
                 CallbackTarget,
                 CallbackMethodName
            );
        }


        public void FadeOut(float time, GameObject CallbackTarget = null, string CallbackMethodName = "OnComplete") {
            float nowalpha = img.color.a;
            Fade(new Color(red, green, blue, nowalpha),
                 new Color(red, green, blue, 1f),
                 time,
                 CallbackTarget,
                 CallbackMethodName
            );
        }

        public void Fade(Color from, Color to, float time, GameObject CallbackTarget = null, string CallbackMethodName = "OnComplete") {
            iTween.ValueTo(this.gameObject, iTween.Hash(
                    "from", from,
                    "to", to,
                    "time", time,
                    "easetype", "linear",
                    "onupdatetarget", this.gameObject,
                    "onupdate", "OnUpdateColor",
                    "oncompletetarget", CallbackTarget != null ? CallbackTarget : this.gameObject,
                    "oncomplete", CallbackMethodName
                )
            );
        }

        public void MoveTo(float x, float y, float time, GameObject CallbackTarget = null, string CallbackMethodName = "OnComplete") {
            iTween.MoveTo(this.gameObject, iTween.Hash(
                    "x", x,
                    "y", y,
                    "time", time,
                    "easetype", "linear",
                    "oncompletetarget", CallbackTarget != null ? CallbackTarget : this.gameObject,
                    "oncomplete", CallbackMethodName
                )
            );
        }

        public void OrthographicSizeTo(float size, float time, GameObject CallbackTarget = null, string CallbackMethodName = "OnComplete") {
            float from = Camera.main.orthographicSize;
            iTween.ValueTo(this.gameObject, iTween.Hash(
                    "from", from,
                    "to", size,
                    "time", time,
                    "easetype", "linear",
                    "onupdatetarget", this.gameObject,
                    "onupdate", "OnUpdateOrthographicSize",
                    "oncompletetarget", CallbackTarget != null ? CallbackTarget : this.gameObject,
                    "oncomplete", CallbackMethodName
                )
            );
        }
    }
}
