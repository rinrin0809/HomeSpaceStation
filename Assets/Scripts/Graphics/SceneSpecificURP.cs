using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement; // 必須

public class SceneSpecificURP : MonoBehaviour
{
    public string targetSceneName = "WinterVacation"; // 対象シーンの名前

    void Start()
    {
        // 現在のシーンを確認
        if (SceneManager.GetActiveScene().name == targetSceneName)
        {
            Debug.Log("特定のシーンが読み込まれています。");
            // シーン固有の処理をここに追加
        }
        else
        {
            Debug.Log("このシーンは対象ではありません。");
        }
    }
}
