using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MoveEnemy : MonoBehaviour
{
    public Grid grid; // グリッドへの参照
    public float speed = 5f; // 敵の移動速度
    private int targetIndex = 0; // 現在のターゲットノードのインデックス
    private List<Node> path; // 現在の追跡経路
    public float spawnProbability = 0.5f; // 初期位置を変更する確率（0～1の範囲）

    [SerializeField]
    private SceneSpawnData sceneSpawnData; // シーンごとのスポーンデータ

    private void Start()
    {
        //SetInitialPosition();
        InitializePath();
    }

    private void Update()
    {
        FollowPath();
    }

    //private void SetInitialPosition()
    //{
    //    string currentSceneName = SceneManager.GetActiveScene().name;
    //    SceneSpawnData.SceneWarpData warpData = sceneSpawnData.sceneWarpDataList.Find(data => data.sceneName == currentSceneName);

    //    if (warpData != null && warpData.warpPositions.Count > 0 && Random.value < spawnProbability)
    //    {
    //        Vector2 randomPosition = warpData.warpPositions[Random.Range(0, warpData.warpPositions.Count)];
    //        transform.position = randomPosition;
    //        Debug.Log("Position changed to: " + randomPosition);
    //    }
    //    else
    //    {
    //        Debug.Log("Position not changed, using initial position.");
    //    }
    //}

    private void InitializePath()
    {
        if (grid.FinalPath != null && grid.FinalPath.Count > 0)
        {
            path = grid.FinalPath;
            targetIndex = 0;
        }
    }

    private void FollowPath()
    {
        if (path == null || path.Count == 0) return;

        Vector2 targetPosition = path[targetIndex].Position;
        Vector2 currentPosition = transform.position;

        transform.position = Vector2.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetIndex++;
            if (targetIndex >= path.Count) targetIndex = 0; // ループ
        }
    }

    //public void WarpToRandomPositionWithDelay(float delay)
    //{
    //    StartCoroutine(WarpAfterDelay(delay));
    //}

    //private IEnumerator WarpAfterDelay(float delay)
    //{
    //    yield return new WaitForSeconds(delay);

    //    string currentSceneName = SceneManager.GetActiveScene().name;
    //    SceneSpawnData.SceneWarpData warpData = sceneSpawnData.sceneWarpDataList.Find(data => data.sceneName == currentSceneName);

    //    if (warpData != null && warpData.warpPositions.Count > 0)
    //    {
    //        Vector2 randomPosition = warpData.warpPositions[Random.Range(0, warpData.warpPositions.Count)];
    //        transform.position = randomPosition;
    //        Debug.Log("Warped to position: " + randomPosition);
    //    }
    //    else
    //    {
    //        Debug.LogWarning("No spawn data found for scene: " + currentSceneName);
    //    }
    //}

    //private void ExecuteWarp()
    //{
    //    string currentSceneName = SceneManager.GetActiveScene().name;
    //    SceneSpawnData.SceneWarpData warpData = sceneSpawnData.sceneWarpDataList.Find(data => data.sceneName == currentSceneName);

    //    if (warpData != null && warpData.warpPositions.Count > 0)
    //    {
    //        Vector2 randomPosition = warpData.warpPositions[Random.Range(0, warpData.warpPositions.Count)];
    //        transform.position = randomPosition;
    //        Debug.Log("Warped to position: " + randomPosition);
    //    }
    //    else
    //    {
    //        Debug.LogWarning("No spawn data found for scene: " + currentSceneName);
    //    }
    //}

    public void UpdatePath(List<Node> newPath)
    {
        path = newPath;
        targetIndex = 0; // パスの最初から再スタート
    }
}
