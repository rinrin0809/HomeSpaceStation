//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Rendering;
//using UnityEngine.Rendering.Universal;

//public class RenderPipelineSwitcher : MonoBehaviour
//{
//    [SerializeField] private RenderPipelineAsset urpPipelineAsset; // URP用設定
//    [SerializeField] private RenderPipelineAsset defaultPipelineAsset; // デフォルトの設定
//    [SerializeField] private string targetSceneName; // URPを使うシーン名

//    // Full Screen Pass Render Featureを管理
//    [SerializeField] private ScriptableRendererFeature fullScreenPassFeature;

//    void OnEnable()
//    {
//        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
//    }

//    void OnDisable()
//    {
//        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
//    }

//    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
//    {
//        if (urpPipelineAsset == null || defaultPipelineAsset == null)
//        {
//            Debug.LogWarning("RenderPipelineAssets are not assigned in the inspector!");
//            return;
//        }

//        if (scene.name == targetSceneName)
//        {
//            GraphicsSettings.renderPipelineAsset = urpPipelineAsset;
//            Debug.Log($"Switched to URP for scene: {scene.name}");

//            // Render Featureを有効化
//            if (fullScreenPassFeature != null)
//                fullScreenPassFeature.SetActive(true);
//        }
//        else
//        {
//            GraphicsSettings.renderPipelineAsset = defaultPipelineAsset;
//            Debug.Log($"Switched to Default Pipeline for scene: {scene.name}");

//            // Render Featureを無効化
//            if (fullScreenPassFeature != null)
//                fullScreenPassFeature.SetActive(false);
//        }
//    }
//}
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class RenderPipelineSwitcher : MonoBehaviour
{
    [SerializeField] private RenderPipelineAsset urpPipelineAsset;  // URP用設定
    [SerializeField] private RenderPipelineAsset defaultPipelineAsset;  // デフォルト（Built-in RPなど）
    [SerializeField] private string targetSceneName;  // URPエフェクトを適用するシーン名
    [SerializeField] private Volume volumeComponent;  // URPポストプロセス Volume
    private void Awake()
    {

        DontDestroyOnLoad(gameObject); // シーンをまたいでこのオブジェクトを保持する

    }
    private void OnEnable()
    {
        // シーンが読み込まれた時のイベントを登録
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // イベントを解除
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        // ゲーム開始時に現在のシーンに応じた適用チェック
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"[URPEffectSwitcher] 読み込まれたシーン名: {scene.name}, 設定シーン名: {targetSceneName}");

        if (urpPipelineAsset == null || defaultPipelineAsset == null)
        {
            Debug.LogError("[URPEffectSwitcher] RenderPipelineAssetsが設定されていません！");
            return;
        }

        if (string.Equals(scene.name, targetSceneName, System.StringComparison.OrdinalIgnoreCase))
        {
            SwitchToURP();
        }
        else
        {
            SwitchToDefault();
        }
    }

    private void SwitchToURP()
    {
        if (GraphicsSettings.renderPipelineAsset != urpPipelineAsset)
        {
            GraphicsSettings.renderPipelineAsset = urpPipelineAsset;
            QualitySettings.renderPipeline = urpPipelineAsset;
            Debug.Log("[URPEffectSwitcher] URPに切り替えました");
        }
        else
        {
            Debug.Log("[URPEffectSwitcher] すでにURPが適用されています");
        }

        if (volumeComponent != null)
        {
            volumeComponent.enabled = true;
            Debug.Log("[URPEffectSwitcher] ポストプロセスを有効化しました");
        }
    }

    private void SwitchToDefault()
    {
        if (GraphicsSettings.renderPipelineAsset != defaultPipelineAsset)
        {
            GraphicsSettings.renderPipelineAsset = defaultPipelineAsset;
            QualitySettings.renderPipeline = defaultPipelineAsset;
            Debug.Log("[URPEffectSwitcher] デフォルトパイプラインに切り替えました");
        }
        else
        {
            Debug.Log("[URPEffectSwitcher] すでにデフォルトパイプラインです");
        }

        if (volumeComponent != null)
        {
            volumeComponent.enabled = false;
            Debug.Log("[URPEffectSwitcher] ポストプロセスを無効化しました");
        }
    }
}

