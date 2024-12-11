using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string targetSceneName; // 遷移先のシーン名
    public float delay = 1.0f; // 遷移前の待機時間
    public string nextSpawnPointTag; // 次の初期ポジション

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 遷移後の初期位置タグを設定
            PlayerSpawnManager.spawnPointTag = nextSpawnPointTag;

            // シーン遷移
            SceneManager.LoadScene(targetSceneName);
            Debug.Log($"次のシーン '{targetSceneName}' に遷移し、初期位置タグを '{nextSpawnPointTag}' に設定しました");
        }
    }


    private IEnumerator TransitionAfterDelay()
    {
        Debug.Log("遷移前のエフェクト再生中...");
        yield return new WaitForSeconds(delay); // 指定した秒数待機
        SceneManager.LoadScene(targetSceneName);
        Debug.Log($"シーン '{targetSceneName}' に遷移しました");
    }
}
