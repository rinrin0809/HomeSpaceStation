using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MaterialSwitcher : MonoBehaviour
{
    public Material normalMaterial; // 通常マテリアル
    public Material uprMaterial;    // UPRシェーダマテリアル
    public string targetSceneName;  // UPRシェーダを適用したいシーン名

    private Material objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Material>();
        UpdateMaterial();
    }

    void UpdateMaterial()
    {
        if (SceneManager.GetActiveScene().name == targetSceneName)
        {
            objectRenderer = uprMaterial; // UPRシェーダを適用
        }
        else
        {
            objectRenderer= normalMaterial; // 通常マテリアルを適用
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateMaterial();
    }
}
