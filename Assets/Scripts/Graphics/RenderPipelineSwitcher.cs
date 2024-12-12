using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RenderPipelineSwitcher : MonoBehaviour
{
    [SerializeField] private RenderPipelineAsset urpPipelineAsset; // URP�p�ݒ�
    [SerializeField] private RenderPipelineAsset defaultPipelineAsset; // �f�t�H���g�̐ݒ�
    [SerializeField] private string targetSceneName; // URP���g���V�[����

    // Full Screen Pass Render Feature���Ǘ�
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

            // Render Feature��L����
            if (fullScreenPassFeature != null)
                fullScreenPassFeature.SetActive(true);
        }
        else
        {
            GraphicsSettings.renderPipelineAsset = defaultPipelineAsset;
            Debug.Log($"Switched to Default Pipeline for scene: {scene.name}");

            // Render Feature�𖳌���
            if (fullScreenPassFeature != null)
                fullScreenPassFeature.SetActive(false);
        }
    }
}
