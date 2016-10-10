using UnityEngine;
using System;
using System.Collections;

namespace utils {
    public static class Utils {

        /// <summary>
        /// 渡された処理を指定時間後に実行する
        /// </summary>
        /// <param name="waitTime">遅延時間[秒]</param>
        /// <param name="action">実行したい処理</param>
        /// <returns></returns>
        public static IEnumerator WaitForSeconds(this float waitTime, Action action) {
            yield return new WaitForSeconds(waitTime);
            action();
        }

        /// <summary>
        /// 渡された処理を指定フレーム後に実行する
        /// </summary>
        /// <param name="delayFrameCount">遅延時間[フレーム]</param>
        /// <param name="action">実行したい処理</param>
        /// <returns></returns>
        public static IEnumerator WaitForFrame(this int delayFrameCount, Action action) {
            for (var i = 0; i < delayFrameCount; i++) {
                yield return null;
            }
            action();
        }
    }
}