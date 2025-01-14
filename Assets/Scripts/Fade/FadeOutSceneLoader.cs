using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class FadeOutSceneLoader : MonoBehaviour
{
    public Image fadePanel;             // フェード用のUIパネル（Image）
    public float fadeDuration = 1.0f;   // フェードの完了にかかる時間
    [SerializeField] private InventryData Inventory;
    //イベント
    public EventData Event;

    //NewGameの設定
    public void NewGameCallCoroutine(string Name)
    {
        if (LoadManager.Instance != null)
        {
            LoadManager.Instance.NewGamePushFlg = true;
            LoadManager.Instance.LoadGameFlg = false;
            Inventory.ResetInventory();
            if(Event != null) Event.AllResetFlag();
            StartCoroutine(FadeOutAndNewGameOrTitle(Name));
        }
    }

    public void LoadGameCallCoroutine()
    {
        if (LoadManager.Instance != null)
        {
            LoadManager.Instance.NewGamePushFlg = false;
            LoadManager.Instance.LoadGameFlg = true;
            Debug.Log("NewFlg" + LoadManager.Instance.NewGamePushFlg);
            Debug.Log("LoadFlg" + LoadManager.Instance.LoadGameFlg);
            Inventory.ResetInventory();
            if (Event != null) Event.AllResetFlag();
            StartCoroutine(FadeOutAndLoadScene());
        }
    }

    // フェードアウトアニメーション処理
    private IEnumerator FadeOut()
    {
        fadePanel.enabled = true;                 // パネルを有効化
        float elapsedTime = 0.0f;                 // 経過時間を初期化
        Color startColor = fadePanel.color;       // フェードパネルの開始色を取得
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1.0f); // フェードパネルの最終色を設定

        // フェードアウトアニメーションを実行
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;                        // 経過時間を増やす
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);  // フェードの進行度を計算
            fadePanel.color = Color.Lerp(startColor, endColor, t); // パネルの色を変更してフェードアウト
            yield return null;                                     // 1フレーム待機
        }

        fadePanel.color = endColor;  // フェードが完了したら最終色に設定
    }

    // フェードアウトNewGameシーンまたはTitleシーン
    public IEnumerator FadeOutAndNewGameOrTitle(string Name)
    {
        yield return StartCoroutine(FadeOut());
        SceneManager.LoadScene(Name);
    }

    // プレイヤーデータのロードとシーン遷移処理
    public IEnumerator FadeOutAndLoadScene()
    {
        string filePath = "";

        if (LoadManager.Instance.GetSideFlg())
        {
            filePath = LoadManager.Instance.GetFilePathBySideNum(LoadManager.Instance.GetSideNum());
        }

        else
        {
            filePath = LoadManager.Instance.GetFilePathByLengthNum(LoadManager.Instance.GetLengthNum());
        }

        if (File.Exists(filePath))
        {
            yield return StartCoroutine(FadeOut());
            SceneManager.LoadScene(LoadManager.Instance.NextSceneName);
        }
    }
}
