using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement; // �K�{

public class SceneSpecificURP : MonoBehaviour
{
    public string targetSceneName = "WinterVacation"; // �ΏۃV�[���̖��O

    void Start()
    {
        // ���݂̃V�[�����m�F
        if (SceneManager.GetActiveScene().name == targetSceneName)
        {
            Debug.Log("����̃V�[�����ǂݍ��܂�Ă��܂��B");
            // �V�[���ŗL�̏����������ɒǉ�
        }
        else
        {
            Debug.Log("���̃V�[���͑Ώۂł͂���܂���B");
        }
    }
}
