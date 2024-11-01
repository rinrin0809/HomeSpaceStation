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
    public Vector2 pos;
    public float k = 0;
    public float spawnProbability = 0.5f; // 初期位置を変更する確率（0～1の範囲）
    [SerializeField]
    private SceneSpawnData sceneSpawnData; // シーンごとのスポーンデータ
    // 移動可能な座標のリスト
    //private List<Vector2> spawnPositions = new List<Vector2>
    //{
    //    new Vector2(-4, -5),
    //    new Vector2(-4, 4),
    //    new Vector2(4, 4)
    //};


    private void Start()
    {
        SetInitialPosition();
        InitializePath();

        //// 50%の確率で初期位置を変更する
        //bool shouldChangePosition = Random.value > k;

        //if (shouldChangePosition && sceneSpawnData != null && sceneSpawnData.spawnPositions.Count > 0)
        //{
        //    // ScriptableObjectからランダムな位置を選んで配置
        //    Vector2 randomPosition = sceneSpawnData.spawnPositions[Random.Range(0, sceneSpawnData.spawnPositions.Count)];
        //    transform.position = randomPosition;
        //    Debug.Log("Position changed to: " + randomPosition);
        //}
        //else
        //{
        //    Debug.Log("Position not changed, using initial position.");
        //}

        //// 初期パスの設定
        //if (grid.FinalPath != null && grid.FinalPath.Count > 0)
        //{
        //    path = grid.FinalPath;
        //    targetIndex = 0;
        //}
    }

    private void Update()
    {
        FollowPath();
        //// パスが存在するかを確認
        //if (path != null && path.Count > 0)
        //{
        //    // 現在のターゲットノードに向かって移動
        //    Vector2 targetPosition = path[targetIndex].Position;
        //    Vector2 currentPosition = transform.position;

        //    // ターゲットノードへの移動
        //    Vector2 direction = (targetPosition - currentPosition).normalized;
        //    transform.position = Vector2.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);

        //    // ノードに到達したら次のノードへ移動
        //    if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        //    {
        //        targetIndex++; // 次のノードへ

        //        // 最後のノードに到達したら追跡を終了するかループを繰り返す
        //        if (targetIndex >= path.Count)
        //        {
        //            targetIndex = 0; // ループして再び最初のノードへ
        //        }
        //    }
        //}
        //else
        //{
        //    Debug.Log("No path to follow.");
        //}
    }

    // 初期位置を変更するメソッド
    private void SetInitialPosition()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneSpawnData.SceneWarpData warpData = sceneSpawnData.sceneWarpDataList.Find(data => data.sceneName == currentSceneName);

        if (warpData != null && warpData.warpPositions.Count > 0)
        {
            bool shouldChangePosition = Random.value < spawnProbability;
            if (shouldChangePosition)
            {
                Vector2 randomPosition = warpData.warpPositions[Random.Range(0, warpData.warpPositions.Count)];
                transform.position = randomPosition;
               // Debug.Log("Position changed to: " + randomPosition);
            }
            else
            {
               // Debug.Log("Position not changed, using initial position.");
            }
        }
        else
        {
            //Debug.LogWarning("No spawn data found for scene: " + currentSceneName);
        }
    }

    // 初期パスの設定
    private void InitializePath()
    {
        if (grid.FinalPath != null && grid.FinalPath.Count > 0)
        {
            path = grid.FinalPath;
            targetIndex = 0;
        }
        else
        {
           // Debug.LogWarning("No initial path found.");
        }
    }

    // パスに従って移動するメソッド
    private void FollowPath()
    {
        if (path == null || path.Count == 0)
        {
           // Debug.Log("No path to follow.");
            return;
        }

        Vector2 targetPosition = path[targetIndex].Position;
        Vector2 currentPosition = transform.position;

        // ターゲットノードへの移動
        transform.position = Vector2.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);

        // ノードに到達したら次のノードへ移動
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetIndex++;

            // 最後のノードに到達したら追跡をループする
            if (targetIndex >= path.Count)
            {
                targetIndex = 0;
            }
        }
    }

    // 外部からパスを更新するメソッド
    public void UpdatePath(List<Node> newPath)
    {
        path = newPath;
        targetIndex = 0; // パスの最初から再スタート
    }
}
