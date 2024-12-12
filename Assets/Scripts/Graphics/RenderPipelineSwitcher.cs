using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RenderPipelineSwitcher : MonoBehaviour
{
    [SerializeField] private RenderPipelineAsset urpPipelineAsset; // URP用設定
    [SerializeField] private RenderPipelineAsset defaultPipelineAsset; // デフォルトの設定
    [SerializeField] private string targetSceneName; // URPを使うシーン名

    // Full Screen Pass Render Featureを管理
    [SerializeField] private ScriptableRendererFeature fullScreenPassFeature;

    void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        if (urpPipelineAsset == null || defaultPipelineAsset == null)
        {
            Debug.LogWarning("RenderPipelineAssets are not assigned in the inspector!");
            return;
        }

        if (scene.name == targetSceneName)
        {
            GraphicsSettings.renderPipelineAsset = urpPipelineAsset;
            Debug.Log($"Switched to URP for scene: {scene.name}");

            // Render Featureを有効化
            if (fullScreenPassFeature != null)
                fullScreenPassFeature.SetActive(true);
        }
        else
        {
            GraphicsSettings.renderPipelineAsset = defaultPipelineAsset;
            Debug.Log($"Switched to Default Pipeline for scene: {scene.name}");

            // Render Featureを無効化
            if (fullScreenPassFeature != null)
                fullScreenPassFeature.SetActive(false);
        }
    }
}
