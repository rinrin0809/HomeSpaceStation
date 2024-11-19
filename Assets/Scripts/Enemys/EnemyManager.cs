using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private SceneSpawnData sceneSpawnData; // �V�[�����Ƃ̃X�|�[���f�[�^

    [SerializeField]
    private MoveEnemy moveEnemy;

    [SerializeField]
    private float sceneStartDelay = 2f; // �V�[���J�n���̒x���b��
    private void Start()
    {
        StartCoroutine(SceneStartSequence());
    }

    private IEnumerator SceneStartSequence()
    {
        yield return new WaitForSeconds(sceneStartDelay);

        warpEnemyToScenePosition();

        moveEnemy.InitializePath();
    }

    private void warpEnemyToScenePosition()
    {
        // ���݂̃V�[�������擾
        string currentSceneName = SceneManager.GetActiveScene().name;

        // �Y������V�[���f�[�^���擾
        SceneSpawnData.SceneWarpData spawnData = sceneSpawnData.sceneWarpDataList.Find(data => data.sceneName == currentSceneName);

        if (spawnData != null && spawnData.warpPositions.Count > 0)
        {
            // �����_���ȃ��[�v�ʒu���擾
            Vector2 randomPosition = spawnData.warpPositions[Random.Range(0, spawnData.warpPositions.Count)];
            moveEnemy.WarpToPosition(randomPosition); // �G�����[�v������
            Debug.Log($"Enemy warped to {randomPosition} in scene {currentSceneName}");
        }
        else
        {
            Debug.LogWarning($"No spawn data or positions available for scene: {currentSceneName}");
        }
    }
}

