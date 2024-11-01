using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WarpManager : MonoBehaviour
{
    [SerializeField]
    private SceneSpawnData sceneSpawnData; // シーンごとのスポーンデータ

    public bool shouldChangePosition = true;
    void Start()
    {
        Debug.Log("Waiting to start warp coroutine.");
        // ここで指定した時間が経過した後にコルーチンを呼び出す
        Invoke("StartWarpCoroutineWithDelay", 2f); // 2秒の遅延で開始
    }

    private void StartWarpCoroutineWithDelay()
    {
        Debug.Log("Starting DelayedChangePosition coroutine after initial delay.");
        StartCoroutine(DelayedChangePosition());
    }

    private IEnumerator DelayedChangePosition()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneSpawnData.SceneWarpData spawnData = sceneSpawnData.sceneWarpDataList.Find(data => data.sceneName == currentSceneName);

        if (spawnData != null)
        {
            Debug.Log("Scene delay: " + spawnData.preWarpWaitTime);
            yield return new WaitForSeconds(spawnData.preWarpWaitTime);

            Debug.Log("Delay completed, executing warp.");

            if (shouldChangePosition && spawnData.warpPositions.Count > 0)
            {
                Vector2 randomPosition = spawnData.warpPositions[Random.Range(0, spawnData.warpPositions.Count)];
                transform.position = randomPosition;
                Debug.Log("Warped to position: " + randomPosition);
            }
            else
            {
                Debug.LogWarning("No spawn positions found for scene: " + currentSceneName);
            }
        }
        else
        {
            Debug.LogWarning("No spawn data for scene: " + currentSceneName);
        }
    }

    //public void ChangePosition()
    //{
    //    // 現在のシーン名を取得
    //    string currentSceneName = SceneManager.GetActiveScene().name;
    //    // 現在のシーンに対応する SceneSpawnData を取得
    //    SceneSpawnData.SceneWarpData spawnData = sceneSpawnData.sceneWarpDataList.Find(data => data.sceneName == currentSceneName);

    //    // 現在のシーン名に対応する座標リストが存在する場合
    //    if (shouldChangePosition && spawnData != null && spawnData.warpPositions.Count > 0)
    //    {
    //        Vector2 randomPosition = spawnData.warpPositions[Random.Range(0, spawnData.warpPositions.Count)];
    //        transform.position = randomPosition;
    //        Debug.Log("Position changed to: " + randomPosition);
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Spawn positions not found for scene: " + currentSceneName);
    //    }
    //}
}

