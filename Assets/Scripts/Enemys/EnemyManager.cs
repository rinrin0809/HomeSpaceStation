using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private SceneSpawnData sceneSpawnData; // シーンごとのスポーンデータ

    [SerializeField]
    private MoveEnemy moveEnemy;

    [SerializeField]
    private float sceneStartDelay = 2f; // シーン開始時の遅延秒数
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
        // 現在のシーン名を取得
        string currentSceneName = SceneManager.GetActiveScene().name;

        // 該当するシーンデータを取得
        SceneSpawnData.SceneWarpData spawnData = sceneSpawnData.sceneWarpDataList.Find(data => data.sceneName == currentSceneName);

        if (spawnData != null && spawnData.warpPositions.Count > 0)
        {
            // ランダムなワープ位置を取得
            Vector2 randomPosition = spawnData.warpPositions[Random.Range(0, spawnData.warpPositions.Count)];
            moveEnemy.WarpToPosition(randomPosition); // 敵をワープさせる
            Debug.Log($"Enemy warped to {randomPosition} in scene {currentSceneName}");
        }
        else
        {
            Debug.LogWarning($"No spawn data or positions available for scene: {currentSceneName}");
        }
    }
}

