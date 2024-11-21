using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarpManager : MonoBehaviour
{

    [SerializeField] private GameObject enemyPrefab;

    [SerializeField]
    private SceneSpawnData sceneSpawnData; // シーンごとのスポーンデータ
    [SerializeField]
    private MoveEnemy moveEnemy; // 敵のスクリプト（MoveEnemy）

    private Transform playerTransform; // プレイヤーのTransform
    private Dictionary<Vector2, GameObject> spawnedEnemies = new Dictionary<Vector2, GameObject>(); // 位置ごとに敵を管理
    private void Awake()
    {
        //// プレイヤーのTransformを取得
        //GameObject player = GameObject.FindGameObjectWithTag("Player");
        //if (player != null)
        //{
        //    playerTransform = player.transform;
        //}
        //else
        //{
        //    Debug.LogError("Player not found! Ensure the player has the 'Player' tag.");
        //}

        // シーン読み込みイベントを登録
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        // 現在のシーンに対応するデータを取得
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
            // 遅延時間が設定されていれば待機
            if (i < warpData.preWarpWaitTimes.Count)
            {
                yield return new WaitForSeconds(warpData.preWarpWaitTimes[i]);
            }

            // 敵のスポーンまたはワープ
            Vector2 spawnPosition = warpData.warpPositions[i];
            if (!spawnedEnemies.ContainsKey(spawnPosition))
            {
                // 敵を新規スポーン
                if (enemyPrefab != null)
                {
                    GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                    spawnedEnemies.Add(spawnPosition, enemy);
                    //GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                    //spawnedEnemies[spawnPosition] = enemy; // 新しい敵を登録
                    Debug.Log($"Enemy spawned at {spawnPosition} in {warpData.sceneName}");
                }
                else
                {
                    Debug.LogError("Enemy prefab is missing.");
                }
            }
            else
            {
                // 既存の敵を該当位置に移動
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
        // イベントの解除
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // プレイヤーのTransformを再取得
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player not found after scene load!");
        }

        // シーンごとのスポーンデータを取得
        string currentScene = scene.name;
        var warpData = sceneSpawnData.sceneWarpDataList.Find(data => data.sceneName == currentScene);

        if (warpData != null)
        {
            Debug.Log($"Found spawn data for scene: {warpData.sceneName}, warp positions count: {warpData.warpPositions.Count}");

            // 敵のスポーン処理を実行
            StartCoroutine(HandleEnemySpawn(warpData));
        }
        else
        {
            Debug.LogWarning($"No spawn data found for scene {currentScene}");
        }

        // moveEnemy が存在する場合のみワープ処理を実行
        if (moveEnemy != null && playerTransform != null)
        {
            moveEnemy.transform.position = playerTransform.position;
            Debug.Log($"Enemy warped to player position: {playerTransform.position}");

            moveEnemy.StopChasing();     // 追跡停止（リセット）
            moveEnemy.StartChasing();   // 追跡再開
        }
    }

    private void WarpEnemyToPlayerPosition()
    {
        // 敵をプレイヤーの位置にワープ
        if (playerTransform != null)
        {
            moveEnemy.transform.position = playerTransform.position;
            Debug.Log($"Enemy warped to player position: {playerTransform.position}");
        }
    }
}