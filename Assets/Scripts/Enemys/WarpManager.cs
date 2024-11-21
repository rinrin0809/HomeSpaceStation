using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarpManager : MonoBehaviour
{

    [SerializeField] private GameObject enemyPrefab;

    [SerializeField]
    private SceneSpawnData sceneSpawnData; // �V�[�����Ƃ̃X�|�[���f�[�^
    [SerializeField]
    private MoveEnemy moveEnemy; // �G�̃X�N���v�g�iMoveEnemy�j

    private Transform playerTransform; // �v���C���[��Transform
    private Dictionary<Vector2, GameObject> spawnedEnemies = new Dictionary<Vector2, GameObject>(); // �ʒu���ƂɓG���Ǘ�
    private void Awake()
    {
        //// �v���C���[��Transform���擾
        //GameObject player = GameObject.FindGameObjectWithTag("Player");
        //if (player != null)
        //{
        //    playerTransform = player.transform;
        //}
        //else
        //{
        //    Debug.LogError("Player not found! Ensure the player has the 'Player' tag.");
        //}

        // �V�[���ǂݍ��݃C�x���g��o�^
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        // ���݂̃V�[���ɑΉ�����f�[�^���擾
        string currentScene = SceneManager.GetActiveScene().name;
        var warpData = sceneSpawnData.sceneWarpDataList.Find(data => data.sceneName == currentScene);

        if (warpData != null)
        {
            Debug.Log("Starting enemy spawn coroutine");
            StartCoroutine(HandleEnemySpawn(warpData));
        }
        else
        {
            Debug.LogWarning($"No warp data found for scene {currentScene}");
            return;
        }
        Debug.Log($"Found spawn data for scene: {warpData.sceneName}, warp positions count: {warpData.warpPositions.Count}");
    }

    private IEnumerator HandleEnemySpawn(SceneSpawnData.SceneWarpData warpData)
    {
        for (int i = 0; i < warpData.warpPositions.Count; i++)
        {
            // �x�����Ԃ��ݒ肳��Ă���Αҋ@
            if (i < warpData.preWarpWaitTimes.Count)
            {
                yield return new WaitForSeconds(warpData.preWarpWaitTimes[i]);
            }

            // �G�̃X�|�[���܂��̓��[�v
            Vector2 spawnPosition = warpData.warpPositions[i];
            if (!spawnedEnemies.ContainsKey(spawnPosition))
            {
                // �G��V�K�X�|�[��
                if (enemyPrefab != null)
                {
                    GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                    spawnedEnemies.Add(spawnPosition, enemy);
                    //GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                    //spawnedEnemies[spawnPosition] = enemy; // �V�����G��o�^
                    Debug.Log($"Enemy spawned at {spawnPosition} in {warpData.sceneName}");
                }
                else
                {
                    Debug.LogError("Enemy prefab is missing.");
                }
            }
            else
            {
                // �����̓G���Y���ʒu�Ɉړ�
                GameObject existingEnemy = spawnedEnemies[spawnPosition];
                if (existingEnemy != null)
                {
                    existingEnemy.transform.position = spawnPosition;
                    Debug.Log($"Enemy moved to {spawnPosition}");
                }
            }
        }
    }

    private void OnDestroy()
    {
        // �C�x���g�̉���
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �v���C���[��Transform���Ď擾
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player not found after scene load!");
        }

        // �V�[�����Ƃ̃X�|�[���f�[�^���擾
        string currentScene = scene.name;
        var warpData = sceneSpawnData.sceneWarpDataList.Find(data => data.sceneName == currentScene);

        if (warpData != null)
        {
            Debug.Log($"Found spawn data for scene: {warpData.sceneName}, warp positions count: {warpData.warpPositions.Count}");

            // �G�̃X�|�[�����������s
            StartCoroutine(HandleEnemySpawn(warpData));
        }
        else
        {
            Debug.LogWarning($"No spawn data found for scene {currentScene}");
        }

        // moveEnemy �����݂���ꍇ�̂݃��[�v���������s
        if (moveEnemy != null && playerTransform != null)
        {
            moveEnemy.transform.position = playerTransform.position;
            Debug.Log($"Enemy warped to player position: {playerTransform.position}");

            moveEnemy.StopChasing();     // �ǐՒ�~�i���Z�b�g�j
            moveEnemy.StartChasing();   // �ǐՍĊJ
        }
    }

    private void WarpEnemyToPlayerPosition()
    {
        // �G���v���C���[�̈ʒu�Ƀ��[�v
        if (playerTransform != null)
        {
            moveEnemy.transform.position = playerTransform.position;
            Debug.Log($"Enemy warped to player position: {playerTransform.position}");
        }
    }
}