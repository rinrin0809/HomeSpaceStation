using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WarpManager : MonoBehaviour
{
    [SerializeField]
    private SceneSpawnData sceneSpawnData; // シーンごとのスポーンデータ
    [SerializeField]
    private MoveEnemy moveEnemy; // MoveEnemyの参照（敵オブジェクト）

    private void Awake()
    {
        Debug.Log("Start method called in WarpManager.");
        StartCoroutine(StartWarpSequenceWithDelay(2f)); // 2秒遅延後にワープシーケンスを開始
    }

    private IEnumerator StartWarpSequenceWithDelay(float delay)
    {
        Debug.Log("Waiting for " + delay + " seconds before starting warp sequence.");
        yield return new WaitForSeconds(delay); // 指定秒数待機
        StartWarpSequence(); // 遅延後にワープシーケンスを開始
    }

    private void StartWarpSequence()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneSpawnData.SceneWarpData spawnData = sceneSpawnData.sceneWarpDataList.Find(data => data.sceneName == currentSceneName);

        if (spawnData != null && spawnData.preWarpWaitTimes.Count > 0)
        {
            Debug.Log("Starting warp sequence with multiple delays.");
            StartCoroutine(ExecuteWarpSequence(spawnData)); // ワープシーケンスコルーチン開始
        }
        else
        {
            Debug.LogWarning("No spawn data or wait times for scene: " + currentSceneName);
        }
    }

    private IEnumerator ExecuteWarpSequence(SceneSpawnData.SceneWarpData spawnData)
    {
        for (int i = 0; i < spawnData.preWarpWaitTimes.Count; i++)
        {
            float waitTime = spawnData.preWarpWaitTimes[i];
            Debug.Log("Waiting for " + waitTime + " seconds before warp #" + (i + 1));
            yield return new WaitForSeconds(waitTime);

            // 敵をワープさせる
            if (moveEnemy != null)
            {
                WarpEnemyToRandomPositionWithDelay(moveEnemy, waitTime); // 遅延を適用してワープ
            }
        }
    }

    public void WarpEnemyToRandomPosition(MoveEnemy enemy)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneSpawnData.SceneWarpData warpData = sceneSpawnData.sceneWarpDataList.Find(data => data.sceneName == currentSceneName);

        if (warpData != null && warpData.warpPositions.Count > 0)
        {
            Vector2 randomPosition = warpData.warpPositions[Random.Range(0, warpData.warpPositions.Count)];
            enemy.transform.position = randomPosition; // 敵の位置を更新
            Debug.Log("Warped to position: " + randomPosition);
        }
        else
        {
            Debug.LogWarning("No spawn data found for scene: " + currentSceneName);
        }
    }

    public void WarpEnemyToRandomPositionWithDelay(MoveEnemy enemy, float delay)
    {
        StartCoroutine(WarpAfterDelay(enemy, delay));
    }

    private IEnumerator WarpAfterDelay(MoveEnemy enemy, float delay)
    {
        yield return new WaitForSeconds(delay);
        WarpEnemyToRandomPosition(enemy);
    }

}