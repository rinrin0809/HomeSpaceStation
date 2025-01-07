using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameOverFade : MonoBehaviour
{
    // フェード用のUIパネル（Image）
    public Image fadePanel;
    // フェードの完了にかかる時間
    public float fadeDuration = 1.0f;
    // フェードイン・アウトの繰り返し回数
    public int fadeRepeatCount = 3;
    // フェードアウト
    private FadeOutSceneLoader fadeOutSceneLoader;

    void Start()
    {
        fadeOutSceneLoader = FindObjectOfType<FadeOutSceneLoader>();
        // フェードイン・アウトのシーケンスを開始
        StartCoroutine(FadeInOutSequence(fadeRepeatCount));
    }

    // フェードインアニメーション処理
    private IEnumerator FadeIn()
    {
        // パネルを有効化
        fadePanel.enabled = true;
        // 経過時間を初期化
        float elapsedTime = 0.0f;
        // フェードパネルの開始色を取得
        Color startColor = fadePanel.color;
        // アルファ値を0に設定
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0.0f);

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadePanel.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        fadePanel.color = endColor;
    }

    // フェードアウトアニメーション処理
    private IEnumerator FadeOut()
    {
        //パネルを有効化
        fadePanel.enabled = true;
        //経過時間を初期化
        float elapsedTime = 0.0f;
        //フェードパネルの開始色を取得
        Color startColor = fadePanel.color;       
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0.8f);

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadePanel.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        fadePanel.color = endColor;
    }

    // フェードイン・アウトを交互に繰り返す
    private IEnumerator FadeInOutSequence(int repeatCount)
    {
        for (int i = 0; i < repeatCount; i++)
        {
            yield return StartCoroutine(FadeIn());
            yield return StartCoroutine(FadeOut());
        }

        // 繰り返しが完了した後に呼び出し
        if(fadeOutSceneLoader != null) fadeOutSceneLoader.NewGameCallCoroutine("Over");
    }
}
