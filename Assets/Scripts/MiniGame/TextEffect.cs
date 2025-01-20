using System.Collections;
using TMPro;
using UnityEngine;

public class TextEffect : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Text;
    [SerializeField] float FadeDuration = 2.0f; // フェードインの所要時間
    [SerializeField] bool FadeInFlg = true;
    void Start()
    {
        Text.color = new Color(Text.color.r, Text.color.g, Text.color.b, 0.0f); // 初期の透明度を設定
    }

    private void Update()
    {
        if(FadeInFlg)
        {
            StartCoroutine(FadeIn());
        }
        
        else
        {
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0.0f; // 経過時間を初期化
        Color startColor = Text.color; // 初期の色
        Color targetColor = new Color(Text.color.r, Text.color.g, Text.color.b, 1.0f); // 目標の色

        while (elapsedTime < FadeDuration)
        {
            elapsedTime += Time.deltaTime; // フレームごとの経過時間を加算
            Text.color = Color.Lerp(startColor, targetColor, elapsedTime / FadeDuration); // 徐々に透明度を変化
            yield return null; // 次のフレームまで待機
        }

        Text.color = targetColor; // 最終的に目標の色に設定
        FadeInFlg = false;
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0.0f; // 経過時間を初期化
        Color startColor = Text.color; // 初期の色
        Color targetColor = new Color(Text.color.r, Text.color.g, Text.color.b, 0.0f); // 目標の色

        while (elapsedTime < FadeDuration)
        {
            elapsedTime += Time.deltaTime; // フレームごとの経過時間を加算
            Text.color = Color.Lerp(startColor, targetColor, elapsedTime / FadeDuration); // 徐々に透明度を変化
            yield return null; // 次のフレームまで待機
        }

        Text.color = targetColor; // 最終的に目標の色に設定
        FadeInFlg = true;
    }
}
