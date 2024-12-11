using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string targetSceneName; // �J�ڐ�̃V�[����
    public float delay = 1.0f; // �J�ڑO�̑ҋ@����
    public string nextSpawnPointTag; // ���̏����|�W�V����

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // �J�ڌ�̏����ʒu�^�O��ݒ�
            PlayerSpawnManager.spawnPointTag = nextSpawnPointTag;

            // �V�[���J��
            SceneManager.LoadScene(targetSceneName);
            Debug.Log($"���̃V�[�� '{targetSceneName}' �ɑJ�ڂ��A�����ʒu�^�O�� '{nextSpawnPointTag}' �ɐݒ肵�܂���");
        }
    }


    private IEnumerator TransitionAfterDelay()
    {
        Debug.Log("�J�ڑO�̃G�t�F�N�g�Đ���...");
        yield return new WaitForSeconds(delay); // �w�肵���b���ҋ@
        SceneManager.LoadScene(targetSceneName);
        Debug.Log($"�V�[�� '{targetSceneName}' �ɑJ�ڂ��܂���");
    }
}
