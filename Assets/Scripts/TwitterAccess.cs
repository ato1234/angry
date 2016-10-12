using UnityEngine;
using System.Collections;

public class TwitterAccess : MonoBehaviour {
    
    /// <summary>
    /// スコアをつぶやく
    /// </summary>
	public void TweetScore() {
        string url = CreateURL();
        Application.OpenURL(url);
    }

    string CreateURL() {
        GameState st = GameObject.Find("GameManager").GetComponent<GameManager>().State;
        int score = st.Score;
        string stname = st.StageName;

        string baseurl = "http://twitter.com/intent/tweet";
        string url = "url=http%3a%2f%2fpoyopoyo%2ecom";
        string text = "text=" + stname + "で"+ score + "点とれたぽよ～(*'v'*) ";
        string hashtags = "hashtags=angrymary";

        return baseurl + "?" + url + "&" + url + "&" + text + "&" + hashtags;
    }
}