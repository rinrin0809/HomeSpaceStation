using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MaterialSwitcher : MonoBehaviour
{
    public Material normalMaterial; // �ʏ�}�e���A��
    public Material uprMaterial;    // UPR�V�F�[�_�}�e���A��
    public string targetSceneName;  // UPR�V�F�[�_��K�p�������V�[����

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
            objectRenderer = uprMaterial; // UPR�V�F�[�_��K�p
        }
        else
        {
            objectRenderer= normalMaterial; // �ʏ�}�e���A����K�p
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
