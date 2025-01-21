//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Rendering;
//using UnityEngine.Rendering.Universal;

//public class RenderPipelineSwitcher : MonoBehaviour
//{
//    [SerializeField] private RenderPipelineAsset urpPipelineAsset; // URP�p�ݒ�
//    [SerializeField] private RenderPipelineAsset defaultPipelineAsset; // �f�t�H���g�̐ݒ�
//    [SerializeField] private string targetSceneName; // URP���g���V�[����

//    // Full Screen Pass Render Feature���Ǘ�
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

//            // Render Feature��L����
//            if (fullScreenPassFeature != null)
//                fullScreenPassFeature.SetActive(true);
//        }
//        else
//        {
//            GraphicsSettings.renderPipelineAsset = defaultPipelineAsset;
//            Debug.Log($"Switched to Default Pipeline for scene: {scene.name}");

//            // Render Feature�𖳌���
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
    [SerializeField] private RenderPipelineAsset urpPipelineAsset;  // URP�p�ݒ�
    [SerializeField] private RenderPipelineAsset defaultPipelineAsset;  // �f�t�H���g�iBuilt-in RP�Ȃǁj
    [SerializeField] private string targetSceneName;  // URP�G�t�F�N�g��K�p����V�[����
    [SerializeField] private Volume volumeComponent;  // URP�|�X�g�v���Z�X Volume
    private void Awake()
    {

        DontDestroyOnLoad(gameObject); // �V�[�����܂����ł��̃I�u�W�F�N�g��ێ�����

    }
    private void OnEnable()
    {
        // �V�[�����ǂݍ��܂ꂽ���̃C�x���g��o�^
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // �C�x���g������
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        // �Q�[���J�n���Ɍ��݂̃V�[���ɉ������K�p�`�F�b�N
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"[URPEffectSwitcher] �ǂݍ��܂ꂽ�V�[����: {scene.name}, �ݒ�V�[����: {targetSceneName}");

        if (urpPipelineAsset == null || defaultPipelineAsset == null)
        {
            Debug.LogError("[URPEffectSwitcher] RenderPipelineAssets���ݒ肳��Ă��܂���I");
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
            Debug.Log("[URPEffectSwitcher] URP�ɐ؂�ւ��܂���");
        }
        else
        {
            Debug.Log("[URPEffectSwitcher] ���ł�URP���K�p����Ă��܂�");
        }

        if (volumeComponent != null)
        {
            volumeComponent.enabled = true;
            Debug.Log("[URPEffectSwitcher] �|�X�g�v���Z�X��L�������܂���");
        }
    }

    private void SwitchToDefault()
    {
        if (GraphicsSettings.renderPipelineAsset != defaultPipelineAsset)
        {
            GraphicsSettings.renderPipelineAsset = defaultPipelineAsset;
            QualitySettings.renderPipeline = defaultPipelineAsset;
            Debug.Log("[URPEffectSwitcher] �f�t�H���g�p�C�v���C���ɐ؂�ւ��܂���");
        }
        else
        {
            Debug.Log("[URPEffectSwitcher] ���łɃf�t�H���g�p�C�v���C���ł�");
        }

        if (volumeComponent != null)
        {
            volumeComponent.enabled = false;
            Debug.Log("[URPEffectSwitcher] �|�X�g�v���Z�X�𖳌������܂���");
        }
    }
}

