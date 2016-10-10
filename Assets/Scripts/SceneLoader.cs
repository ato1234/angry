using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

using camera;

public class SceneLoader : MonoBehaviour {

    void Start() {
    }

    void Update() {
    }

    /// <summary>
    /// ステージ選択画面のボタンをクリックされたときの処理
    /// </summary>
    /// <param name="SceneName"></param>
    public void ButtonClick(string SceneName) {
        Canvas c = GameObject.Find("Buttons").GetComponent<Canvas>();
        c.sortingOrder = -1;
        LoadSceneSingle(SceneName);
    }

    /// <summary>
    /// シーンを切り替える
    /// </summary>
    /// <param name="SceneName"></param>
    public void LoadSceneSingle(string SceneName) {
        StartCoroutine(LoadSceneSingleCoroutine(SceneName));
    }

    IEnumerator LoadSceneSingleCoroutine(string SceneName) {
        CameraAnimation ca = Camera.main.GetComponent<CameraAnimation>();
        CameraControl cc = Camera.main.GetComponent<CameraControl>();

        float fotime = 0.7f;
        ca.FadeOut(fotime);
        yield return new WaitForSeconds(fotime);
        SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
    }

}
