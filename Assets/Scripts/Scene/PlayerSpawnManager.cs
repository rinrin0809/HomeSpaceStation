using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{

    public  static string spawnPointTag = "PlayerSpawnPosition"; // �����ʒu��ݒ肷��I�u�W�F�N�g�̃^�O

    private void Awake()
    {
        // �v���C���[��T��
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            // "SpawnPoint" �^�O�̃I�u�W�F�N�g��T��
            GameObject spawnPoint = GameObject.FindWithTag(spawnPointTag);
            if (spawnPoint != null)
            {
                player.transform.position = spawnPoint.transform.position;
                Debug.Log($"�v���C���[�������|�W�V���� {spawnPoint.transform.position} �Ɉړ����܂���");
            }
            else
            {
                Debug.LogWarning($"�^�O '{spawnPointTag}' �̃I�u�W�F�N�g��������܂���ł���");
            }
        }
        else
        {
            Debug.LogWarning("�v���C���[��������܂���ł���");
        }
    }

}
